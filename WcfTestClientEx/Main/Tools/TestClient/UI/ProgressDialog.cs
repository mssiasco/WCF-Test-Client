using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000051 RID: 81
	internal partial class ProgressDialog : Form
	{
		// Token: 0x060002AE RID: 686 RVA: 0x000040EA File Offset: 0x000022EA
		public ProgressDialog(string title, string labelText, BackgroundWorker backgroundWorker)
		{
			this.InitializeComponent();
			this.Text = title;
			this.actionLabel.Text = labelText;
			this.backgroundWorker = backgroundWorker;
			this.backgroundWorker.ProgressChanged += this.backgroundWorker_ProgressChanged;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000D898 File Offset: 0x0000BA98
		internal static bool Prompt(Form owner, string title, string labelText, BackgroundWorker backgroundWorker)
		{
			ProgressDialog progressDialog = new ProgressDialog(title, labelText, backgroundWorker);
			progressDialog.ShowDialog(owner);
			return progressDialog.DialogResult == DialogResult.OK;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00004129 File Offset: 0x00002329
		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.progressBar.Value = e.ProgressPercentage;
			if (this.progressBar.Value == this.progressBar.Maximum)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x04000140 RID: 320
		private BackgroundWorker backgroundWorker;
	}
}
