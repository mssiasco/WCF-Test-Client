namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000056 RID: 86
	internal partial class WarningPromptDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x000043FB File Offset: 0x000025FB
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000E138 File Offset: 0x0000C338
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Main.Tools.TestClient.UI.WarningPromptDialog));
			this.cancelButton = new global::System.Windows.Forms.Button();
			this.okButton = new global::System.Windows.Forms.Button();
			this.checkDonotPromptAgain = new global::System.Windows.Forms.CheckBox();
			this.txtWarningText = new global::System.Windows.Forms.TextBox();
			base.SuspendLayout();
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			componentResourceManager.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.okButton, "okButton");
			this.okButton.Name = "okButton";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new global::System.EventHandler(this.okButton_Click);
			componentResourceManager.ApplyResources(this.checkDonotPromptAgain, "checkBox1");
			this.checkDonotPromptAgain.Name = "checkBox1";
			this.checkDonotPromptAgain.UseVisualStyleBackColor = true;
			this.txtWarningText.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			componentResourceManager.ApplyResources(this.txtWarningText, "txtWarningText");
			this.txtWarningText.Name = "txtWarningText";
			this.txtWarningText.ReadOnly = true;
			base.AcceptButton = this.okButton;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.Controls.Add(this.txtWarningText);
			base.Controls.Add(this.checkDonotPromptAgain);
			base.Controls.Add(this.okButton);
			base.Controls.Add(this.cancelButton);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "WarningPromptDialog";
			base.ShowInTaskbar = false;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000157 RID: 343
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000158 RID: 344
		private global::System.Windows.Forms.Button cancelButton;

		// Token: 0x04000159 RID: 345
		private global::System.Windows.Forms.Button okButton;

		// Token: 0x0400015A RID: 346
		private global::System.Windows.Forms.CheckBox checkDonotPromptAgain;

		// Token: 0x0400015B RID: 347
		private global::System.Windows.Forms.TextBox txtWarningText;
	}
}
