using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using WcfTestClientEx;

namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000046 RID: 70
	internal partial class AboutForm : Form
	{
		// Token: 0x06000225 RID: 549 RVA: 0x00009820 File Offset: 0x00007A20
		internal AboutForm()
		{
			this.InitializeComponent();
			this.Text = string.Format(CultureInfo.InvariantCulture, StringResources.About, StringResources.ProductName);
			this.labelProductName.Text = StringResources.ProductName;
			this.labelVersion.Text = string.Format(CultureInfo.CurrentUICulture, StringResources.AssemblyVersion, AboutForm.GetExecutingAssemblyFileVersion());
			this.labelCopyright.Text = StringResources.Copyright;
			this.labelCompanyName.Text = StringResources.Company;
			this.textBoxDescription.Text = StringResources.ProductDescription;
			this.wcfPicture.Image = ResourceHelper.AboutBoxImage;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000039AD File Offset: 0x00001BAD
		private static string GetExecutingAssemblyFileVersion()
		{
			return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000098C4 File Offset: 0x00007AC4
		private void buttonSystemInformation_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("Msinfo32.exe");
			}
			catch (Win32Exception)
			{
				RtlAwareMessageBox.Show(this, StringResources.FailToRunSystemInfo, StringResources.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
			}
		}
	}
}
