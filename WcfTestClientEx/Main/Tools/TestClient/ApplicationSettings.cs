using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WcfTestClientEx;

namespace Main.Tools.TestClient
{
	// Token: 0x02000006 RID: 6
	internal class ApplicationSettings
	{
		// Token: 0x06000061 RID: 97 RVA: 0x0000484C File Offset: 0x00002A4C
		private ApplicationSettings()
		{
			string location = Assembly.GetExecutingAssembly().Location;
			this.settingPath = Path.GetFileName(location) + ".option";
			this.settingPath = Path.Combine(ToolingEnvironment.SavedDataBase, this.settingPath);
			if (File.Exists(this.settingPath))
			{
				this.ReadSettingFile();
				return;
			}
			this.CreateSettingFile();
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00004924 File Offset: 0x00002B24
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000284C File Offset: 0x00000A4C
		internal string ProjectBase
		{
			get
			{
				if (!Directory.Exists(this.projectBase))
				{
					try
					{
						Directory.CreateDirectory(this.projectBase);
					}
					catch (IOException)
					{
						ApplicationSettings.ReportCreateFolderError(this.projectBase);
					}
					catch (UnauthorizedAccessException)
					{
						ApplicationSettings.ReportCreateFolderError(this.projectBase);
					}
				}
				return this.projectBase;
			}
			set
			{
				this.projectBase = value;
				this.WriteSettingFile(true);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000285C File Offset: 0x00000A5C
		internal ICollection<string> RecentUrls
		{
			get
			{
				return this.recentUrls;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002864 File Offset: 0x00000A64
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000286C File Offset: 0x00000A6C
		internal bool RefreshPromptEnabled
		{
			get
			{
				return this.refreshPromptEnabled;
			}
			set
			{
				this.refreshPromptEnabled = value;
				this.WriteSettingFile(true);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000287C File Offset: 0x00000A7C
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002884 File Offset: 0x00000A84
		internal bool RegenerateConfigEnabled
		{
			get
			{
				return this.regenerateConfigEnabled;
			}
			set
			{
				this.regenerateConfigEnabled = value;
				this.WriteSettingFile(true);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002894 File Offset: 0x00000A94
		// (set) Token: 0x0600006A RID: 106 RVA: 0x0000289C File Offset: 0x00000A9C
		internal bool SecurityPromptEnabled
		{
			get
			{
				return this.securityPromptEnabled;
			}
			set
			{
				this.securityPromptEnabled = value;
				this.WriteSettingFile(true);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000028AC File Offset: 0x00000AAC
		internal static ApplicationSettings GetInstance()
		{
			if (ApplicationSettings.instance == null)
			{
				ApplicationSettings.instance = new ApplicationSettings();
			}
			return ApplicationSettings.instance;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000498C File Offset: 0x00002B8C
		internal static void ReportCreateFolderError(string folderName)
		{
			string text = string.Format(CultureInfo.CurrentUICulture, StringResources.CannotCreateFolder, folderName);
			RtlAwareMessageBox.Show(text, StringResources.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000049BC File Offset: 0x00002BBC
		internal void RecordUrl(string pathToRecord)
		{
			foreach (string text in this.recentUrls)
			{
				if (string.Compare(text, pathToRecord, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.recentUrls.Remove(pathToRecord);
					break;
				}
			}
			this.recentUrls.AddFirst(pathToRecord);
			if (this.recentUrls.Count > 10)
			{
				this.recentUrls.RemoveLast();
			}
			this.WriteSettingFile(false);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004A50 File Offset: 0x00002C50
		private static string ReadLine(StreamReader reader)
		{
			string text = null;
			while (string.IsNullOrEmpty(text) && !reader.EndOfStream)
			{
				text = reader.ReadLine().Trim();
			}
			return text;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000028C4 File Offset: 0x00000AC4
		private static void ReportAccessError()
		{
			RtlAwareMessageBox.Show(StringResources.ErrorSettingsNotAccessible, StringResources.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004A80 File Offset: 0x00002C80
		private void CreateSettingFile()
		{
			try
			{
				File.CreateText(this.settingPath).Close();
			}
			catch (IOException)
			{
				ApplicationSettings.ReportAccessError();
			}
			catch (UnauthorizedAccessException)
			{
				ApplicationSettings.ReportAccessError();
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004ACC File Offset: 0x00002CCC
		private void ReadProjectBaseOrUrl(StreamReader reader)
		{
			string text = ApplicationSettings.ReadLine(reader);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (text.StartsWith("ProjectBase:", StringComparison.OrdinalIgnoreCase))
			{
				text = text.Substring("ProjectBase:".Length).Trim();
				if (!string.IsNullOrEmpty(text) && Directory.Exists(text))
				{
					this.projectBase = text;
					return;
				}
			}
			else if (Uri.IsWellFormedUriString(text, UriKind.Absolute))
			{
				this.recentUrls.AddLast(text);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004B3C File Offset: 0x00002D3C
		private void ReadSettingFile()
		{
			try
			{
				using (StreamReader streamReader = File.OpenText(this.settingPath))
				{
					if (!streamReader.EndOfStream)
					{
						this.ReadValueFromFile(streamReader, this.securityRegex, out this.securityPromptEnabled);
						this.ReadValueFromFile(streamReader, this.refreshRegex, out this.refreshPromptEnabled);
						this.ReadValueFromFile(streamReader, this.regenerateRegex, out this.regenerateConfigEnabled);
					}
					this.ReadProjectBaseOrUrl(streamReader);
					while (!streamReader.EndOfStream)
					{
						string text = streamReader.ReadLine();
						if (Uri.IsWellFormedUriString(text, UriKind.Absolute))
						{
							this.recentUrls.AddLast(text);
						}
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004BF4 File Offset: 0x00002DF4
		private void ReadValueFromFile(StreamReader reader, Regex regex, out bool enabled)
		{
			enabled = true;
			string text = ApplicationSettings.ReadLine(reader);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			Match match = regex.Match(text);
			if (match.Success)
			{
				enabled = match.Groups[1].ToString().StartsWith("E", StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004C44 File Offset: 0x00002E44
		private void WriteSettingFile(bool reportError)
		{
			try
			{
				using (StreamWriter streamWriter = File.CreateText(this.settingPath))
				{
					streamWriter.WriteLine(this.securityPromptEnabled ? "SecurityPrompt : Enabled" : "SecurityPrompt : Disabled");
					streamWriter.WriteLine(this.refreshPromptEnabled ? "RefreshPrompt : Enabled" : "RefreshPrompt : Disabled");
					streamWriter.WriteLine(this.regenerateConfigEnabled ? "RegenerateConfig : Enabled" : "RegenerateConfig : Disabled");
					streamWriter.WriteLine("ProjectBase:" + this.projectBase);
					foreach (string text in this.recentUrls)
					{
						streamWriter.WriteLine(text);
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				if (reportError)
				{
					ApplicationSettings.ReportAccessError();
				}
			}
		}

		// Token: 0x0400000A RID: 10
		private const string DisableRefreshPrompt = "RefreshPrompt : Disabled";

		// Token: 0x0400000B RID: 11
		private const string DisableRegenerateConfig = "RegenerateConfig : Disabled";

		// Token: 0x0400000C RID: 12
		private const string DisableSecurityPrompt = "SecurityPrompt : Disabled";

		// Token: 0x0400000D RID: 13
		private const string EnableRefreshPrompt = "RefreshPrompt : Enabled";

		// Token: 0x0400000E RID: 14
		private const string EnableRegenerateConfig = "RegenerateConfig : Enabled";

		// Token: 0x0400000F RID: 15
		private const string EnableSecurityPrompt = "SecurityPrompt : Enabled";

		// Token: 0x04000010 RID: 16
		private const int maxRecentUrlsCount = 10;

		// Token: 0x04000011 RID: 17
		private const string projectBaseTitle = "ProjectBase:";

		// Token: 0x04000012 RID: 18
		private static ApplicationSettings instance;

		// Token: 0x04000013 RID: 19
		private string projectBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp\\" + ToolingEnvironment.TestClientProjectFolderName);

		// Token: 0x04000014 RID: 20
		private LinkedList<string> recentUrls = new LinkedList<string>();

		// Token: 0x04000015 RID: 21
		private bool refreshPromptEnabled = true;

		// Token: 0x04000016 RID: 22
		private Regex refreshRegex = new Regex("^\\s*RefreshPrompt\\s*:\\s*(En|Dis)abled\\s*$", RegexOptions.IgnoreCase);

		// Token: 0x04000017 RID: 23
		private bool regenerateConfigEnabled = true;

		// Token: 0x04000018 RID: 24
		private Regex regenerateRegex = new Regex("^\\s*RegenerateConfig\\s*:\\s*(En|Dis)abled\\s*$", RegexOptions.IgnoreCase);

		// Token: 0x04000019 RID: 25
		private bool securityPromptEnabled = true;

		// Token: 0x0400001A RID: 26
		private Regex securityRegex = new Regex("^\\s*SecurityPrompt\\s*:\\s*(En|Dis)abled\\s*$", RegexOptions.IgnoreCase);

		// Token: 0x0400001B RID: 27
		private string settingPath;
	}
}
