namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000051 RID: 81
	internal partial class ProgressDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x00004161 File Offset: 0x00002361
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000D8C0 File Offset: 0x0000BAC0
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Main.Tools.TestClient.UI.ProgressDialog));
			this.progressBar = new global::System.Windows.Forms.ProgressBar();
			this.cancelButton = new global::System.Windows.Forms.Button();
			this.actionLabel = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.progressBar, "progressBar");
			this.progressBar.Name = "progressBar";
			componentResourceManager.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.actionLabel, "actionLabel");
			this.actionLabel.Name = "actionLabel";
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.Controls.Add(this.actionLabel);
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.progressBar);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ProgressDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000141 RID: 321
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000142 RID: 322
		private global::System.Windows.Forms.ProgressBar progressBar;

		// Token: 0x04000143 RID: 323
		private global::System.Windows.Forms.Button cancelButton;

		// Token: 0x04000144 RID: 324
		private global::System.Windows.Forms.Label actionLabel;
	}
}
