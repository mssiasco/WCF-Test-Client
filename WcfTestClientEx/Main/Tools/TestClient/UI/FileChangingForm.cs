using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using WcfTestClientEx;

namespace Main.Tools.TestClient.UI
{
	// Token: 0x0200004B RID: 75
	internal partial class FileChangingForm : Form
	{
		// Token: 0x06000250 RID: 592 RVA: 0x00003C25 File Offset: 0x00001E25
		internal FileChangingForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000AA04 File Offset: 0x00008C04
		internal static bool Prompt(Form owner, string nodeName)
		{
			FileChangingForm fileChangingForm = new FileChangingForm();
			fileChangingForm.fileNameTextBox.Text = string.Format(CultureInfo.CurrentUICulture, StringResources.ConfigChanging, nodeName);
			FileChangingForm.promptedFileList.Add(nodeName);
			fileChangingForm.ShowDialog(owner);
			FileChangingForm.promptedFileList.Remove(nodeName);
			return fileChangingForm.DialogResult == DialogResult.Yes;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00003C33 File Offset: 0x00001E33
		internal static bool ShouldPrompt(string fileName)
		{
			return !FileChangingForm.promptedFileList.Contains(fileName);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00003C45 File Offset: 0x00001E45
		private void yesButton_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Yes;
		}

		// Token: 0x040000EB RID: 235
		private static IList<string> promptedFileList = new List<string>();
	}
}
