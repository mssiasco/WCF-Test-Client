using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Main.Tools.TestClient.UI;
using WcfTestClientEx;

namespace Main.Tools.TestClient
{
	// Token: 0x0200001B RID: 27
	internal static class Program
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x000062B8 File Offset: 0x000044B8
		[LoaderOptimization(LoaderOptimization.MultiDomainHost)]
		[STAThread]
		internal static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if (args != null && args.Length != 0)
			{
				if (string.Equals(args[0], "/ProjectPath", StringComparison.OrdinalIgnoreCase))
				{
					if (args.Length == 2)
					{
						Program.SetProjectBase(args[1]);
						return;
					}
					Program.ShowUsage();
					return;
				}
				else if (string.Equals(args[0], "/RestoreProjectPath", StringComparison.OrdinalIgnoreCase))
				{
					if (args.Length == 1)
					{
						ApplicationSettings.GetInstance().ProjectBase = string.Empty;
						return;
					}
					Program.ShowUsage();
					return;
				}
				else
				{
					foreach (string text in args)
					{
						if (string.Equals("/?", text, StringComparison.Ordinal) || string.Equals("-?", text, StringComparison.Ordinal))
						{
							Program.ShowUsage();
							return;
						}
					}
				}
			}
			Application.Run(new MainForm(args));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00002D85 File Offset: 0x00000F85
		private static void ReportError(string error)
		{
			RtlAwareMessageBox.Show(error, StringResources.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002D98 File Offset: 0x00000F98
		private static void SetProjectBase(string value)
		{
			value = Path.GetFullPath(value);
			if (!Directory.Exists(value))
			{
				Program.ReportError(string.Format(CultureInfo.CurrentUICulture, StringResources.DirectoryNotExist, value));
				return;
			}
			ApplicationSettings.GetInstance().ProjectBase = value;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00002DCB File Offset: 0x00000FCB
		private static void ShowUsage()
		{
			RtlAwareMessageBox.Show(StringResources.Usage, StringResources.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
		}
	}
}
