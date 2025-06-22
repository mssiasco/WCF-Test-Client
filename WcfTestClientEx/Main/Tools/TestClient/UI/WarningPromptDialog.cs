using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000056 RID: 86
	internal partial class WarningPromptDialog : Form
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x000043D4 File Offset: 0x000025D4
		internal WarningPromptDialog()
			: this(string.Empty)
		{
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000043E1 File Offset: 0x000025E1
		internal WarningPromptDialog(string warningText)
		{
			this.InitializeComponent();
			this.txtWarningText.Text = warningText;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000E0FC File Offset: 0x0000C2FC
		internal static bool Prompt(Form owner, string text, string warningText, out bool doNotPromptAgain)
		{
			WarningPromptDialog warningPromptDialog = new WarningPromptDialog(warningText);
			warningPromptDialog.Text = text;
			warningPromptDialog.ShowDialog(owner);
			doNotPromptAgain = warningPromptDialog.checkDonotPromptAgain.Checked;
			return warningPromptDialog.DialogResult == DialogResult.OK;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00003A2D File Offset: 0x00001C2D
		private void okButton_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
		}
	}
}
