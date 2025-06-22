using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000050 RID: 80
	internal partial class OptionsForm : Form
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x00004093 File Offset: 0x00002293
		public OptionsForm()
		{
			this.InitializeComponent();
			this.regenerateConfigCheckBox.Checked = ApplicationSettings.GetInstance().RegenerateConfigEnabled;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000D60C File Offset: 0x0000B80C
		internal static bool Prompt(Form owner, out bool regenerateConfig)
		{
			OptionsForm optionsForm = new OptionsForm();
			optionsForm.ShowDialog(owner);
			regenerateConfig = optionsForm.regenerateConfigCheckBox.Checked;
			return optionsForm.DialogResult == DialogResult.OK;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000040B6 File Offset: 0x000022B6
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			this.regenerateConfigCheckBox.Focus();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00003A2D File Offset: 0x00001C2D
		private void okButton_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
		}
	}
}
