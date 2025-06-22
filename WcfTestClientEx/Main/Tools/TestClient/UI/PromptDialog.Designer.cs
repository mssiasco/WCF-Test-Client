namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000052 RID: 82
	internal partial class PromptDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x0000418E File Offset: 0x0000238E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000DA98 File Offset: 0x0000BC98
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Main.Tools.TestClient.UI.PromptDialog));
			this.promptLabel = new global::System.Windows.Forms.Label();
			this.cancelButton = new global::System.Windows.Forms.Button();
			this.okButton = new global::System.Windows.Forms.Button();
			this.inputBox = new global::System.Windows.Forms.ComboBox();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.promptLabel, "promptLabel");
			this.promptLabel.Name = "promptLabel";
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			componentResourceManager.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.okButton, "okButton");
			this.okButton.Name = "okButton";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new global::System.EventHandler(this.okButton_Click);
			this.inputBox.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.inputBox, "inputBox");
			this.inputBox.Name = "inputBox";
			base.AcceptButton = this.okButton;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.Controls.Add(this.inputBox);
			base.Controls.Add(this.okButton);
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.promptLabel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PromptDialog";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000145 RID: 325
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000146 RID: 326
		private global::System.Windows.Forms.Label promptLabel;

		// Token: 0x04000147 RID: 327
		private global::System.Windows.Forms.Button cancelButton;

		// Token: 0x04000148 RID: 328
		private global::System.Windows.Forms.Button okButton;

		// Token: 0x04000149 RID: 329
		private global::System.Windows.Forms.ComboBox inputBox;
	}
}
