namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000050 RID: 80
	internal partial class OptionsForm : global::System.Windows.Forms.Form
	{
		// Token: 0x060002AC RID: 684 RVA: 0x000040CB File Offset: 0x000022CB
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000D640 File Offset: 0x0000B840
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Main.Tools.TestClient.UI.OptionsForm));
			this.optionsTabControl = new global::System.Windows.Forms.TabControl();
			this.configPolicyTabPage = new global::System.Windows.Forms.TabPage();
			this.regenerateConfigCheckBox = new global::System.Windows.Forms.CheckBox();
			this.okButton = new global::System.Windows.Forms.Button();
			this.cancelButton = new global::System.Windows.Forms.Button();
			this.optionsTabControl.SuspendLayout();
			this.configPolicyTabPage.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.optionsTabControl, "OptionsTabControl");
			this.optionsTabControl.Controls.Add(this.configPolicyTabPage);
			this.optionsTabControl.Name = "OptionsTabControl";
			this.optionsTabControl.SelectedIndex = 0;
			this.configPolicyTabPage.Controls.Add(this.regenerateConfigCheckBox);
			componentResourceManager.ApplyResources(this.configPolicyTabPage, "ConfigPolicyTabPage");
			this.configPolicyTabPage.Name = "ConfigPolicyTabPage";
			this.configPolicyTabPage.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.regenerateConfigCheckBox, "regenerateConfigCheckBox");
			this.regenerateConfigCheckBox.Name = "regenerateConfigCheckBox";
			this.regenerateConfigCheckBox.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.okButton, "okButton");
			this.okButton.Name = "okButton";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new global::System.EventHandler(this.okButton_Click);
			componentResourceManager.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			base.AcceptButton = this.okButton;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.okButton);
			base.Controls.Add(this.optionsTabControl);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "OptionsPromptDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.optionsTabControl.ResumeLayout(false);
			this.configPolicyTabPage.ResumeLayout(false);
			this.configPolicyTabPage.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x0400013A RID: 314
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400013B RID: 315
		private global::System.Windows.Forms.TabControl optionsTabControl;

		// Token: 0x0400013C RID: 316
		private global::System.Windows.Forms.TabPage configPolicyTabPage;

		// Token: 0x0400013D RID: 317
		private global::System.Windows.Forms.CheckBox regenerateConfigCheckBox;

		// Token: 0x0400013E RID: 318
		private global::System.Windows.Forms.Button okButton;

		// Token: 0x0400013F RID: 319
		private global::System.Windows.Forms.Button cancelButton;
	}
}
