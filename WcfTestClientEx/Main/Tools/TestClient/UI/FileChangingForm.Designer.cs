namespace Main.Tools.TestClient.UI
{
	// Token: 0x0200004B RID: 75
	internal partial class FileChangingForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000254 RID: 596 RVA: 0x00003C4E File Offset: 0x00001E4E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000AA5C File Offset: 0x00008C5C
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Main.Tools.TestClient.UI.FileChangingForm));
			this.yesButton = new global::System.Windows.Forms.Button();
			this.noButton = new global::System.Windows.Forms.Button();
			this.warningTextLabel = new global::System.Windows.Forms.Label();
			this.fileNameTextBox = new global::System.Windows.Forms.TextBox();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.yesButton, "yesButton");
			this.yesButton.Name = "yesButton";
			this.yesButton.UseVisualStyleBackColor = true;
			this.yesButton.Click += new global::System.EventHandler(this.yesButton_Click);
			this.noButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			componentResourceManager.ApplyResources(this.noButton, "noButton");
			this.noButton.Name = "noButton";
			this.noButton.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.warningTextLabel, "warningTextLabel");
			this.warningTextLabel.Name = "warningTextLabel";
			this.fileNameTextBox.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			componentResourceManager.ApplyResources(this.fileNameTextBox, "fileNameTextBox");
			this.fileNameTextBox.Name = "fileNameTextBox";
			this.fileNameTextBox.ReadOnly = true;
			this.fileNameTextBox.TabStop = false;
			base.AcceptButton = this.yesButton;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.noButton;
			base.Controls.Add(this.fileNameTextBox);
			base.Controls.Add(this.warningTextLabel);
			base.Controls.Add(this.noButton);
			base.Controls.Add(this.yesButton);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FileChangingForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000EC RID: 236
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000ED RID: 237
		private global::System.Windows.Forms.Button yesButton;

		// Token: 0x040000EE RID: 238
		private global::System.Windows.Forms.Button noButton;

		// Token: 0x040000EF RID: 239
		private global::System.Windows.Forms.Label warningTextLabel;

		// Token: 0x040000F0 RID: 240
		private global::System.Windows.Forms.TextBox fileNameTextBox;
	}
}
