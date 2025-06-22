using System;
using System.IO;
using System.Reflection;
using Main.Tools.Common;

namespace Main.Tools.TestClient
{
	// Token: 0x0200002D RID: 45
	internal static class ToolingEnvironment
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00008318 File Offset: 0x00006518
		internal static string MetadataTool
		{
			get
			{
				if (ToolingEnvironment.svcutilPath == null)
				{
					string sdkPath = SdkPathUtility.GetSdkPath("svcutil.exe");
					if (sdkPath != null && File.Exists(sdkPath))
					{
						return ToolingEnvironment.svcutilPath = sdkPath;
					}
					string location = Assembly.GetExecutingAssembly().Location;
					string directoryName = Path.GetDirectoryName(location);
					string text = Path.Combine(directoryName, "svcutil.exe");
					if (File.Exists(text))
					{
						return ToolingEnvironment.svcutilPath = text;
					}
					string text2 = "svcutil.exe";
					if (File.Exists(text2))
					{
						return ToolingEnvironment.svcutilPath = text2;
					}
				}
				return ToolingEnvironment.svcutilPath;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00008398 File Offset: 0x00006598
		internal static string SavedDataBase
		{
			get
			{
				if (!Directory.Exists(ToolingEnvironment.expectedDirectory))
				{
					try
					{
						Directory.CreateDirectory(ToolingEnvironment.expectedDirectory);
					}
					catch (IOException)
					{
						ApplicationSettings.ReportCreateFolderError(ToolingEnvironment.expectedDirectory);
					}
					catch (UnauthorizedAccessException)
					{
						ApplicationSettings.ReportCreateFolderError(ToolingEnvironment.expectedDirectory);
					}
				}
				return ToolingEnvironment.expectedDirectory;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000083FC File Offset: 0x000065FC
		internal static string SvcConfigEditorPath
		{
			get
			{
				if (ToolingEnvironment.svcConfigEditorPath == null)
				{
					string sdkPath = SdkPathUtility.GetSdkPath("SvcConfigEditor.exe");
					if (sdkPath != null && File.Exists(sdkPath))
					{
						return ToolingEnvironment.svcConfigEditorPath = sdkPath;
					}
				}
				return ToolingEnvironment.svcConfigEditorPath;
			}
		}

		// Token: 0x040000A5 RID: 165
		internal static readonly string TestClientProjectFolderName = "Test Client Projects\\" + VersionNumbers.VSCurrentVersionString;

		// Token: 0x040000A6 RID: 166
		private static readonly string expectedDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ToolingEnvironment.TestClientProjectFolderName);

		// Token: 0x040000A7 RID: 167
		private const string svcConfigEditorName = "SvcConfigEditor.exe";

		// Token: 0x040000A8 RID: 168
		private const string svcutilBinaryName = "svcutil.exe";

		// Token: 0x040000A9 RID: 169
		private static string svcConfigEditorPath;

		// Token: 0x040000AA RID: 170
		private static string svcutilPath;
	}
}
