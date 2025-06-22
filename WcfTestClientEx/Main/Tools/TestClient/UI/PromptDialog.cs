using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000052 RID: 82
	internal partial class PromptDialog : Form
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x00004180 File Offset: 0x00002380
		internal PromptDialog()
		{
			this.InitializeComponent();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000DA0C File Offset: 0x0000BC0C
		internal static string Prompt(Form owner, string title, string prompt, params string[] defaultValues)
		{
			PromptDialog promptDialog = new PromptDialog();
			promptDialog.Text = title;
			promptDialog.promptLabel.Text = prompt;
			promptDialog.StartPosition = FormStartPosition.CenterParent;
			promptDialog.ShowInTaskbar = false;
			foreach (string text in defaultValues)
			{
				promptDialog.inputBox.Items.Add(text);
			}
			if (defaultValues.Length != 0)
			{
				promptDialog.inputBox.Text = defaultValues[0];
			}
			promptDialog.ShowDialog(owner);
			if (promptDialog.DialogResult != DialogResult.OK)
			{
				return null;
			}
			return promptDialog.inputBox.Text;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00003A2D File Offset: 0x00001C2D
		private void okButton_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
		}
	}
}
