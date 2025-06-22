namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000047 RID: 71
	internal partial class DataSetEditorForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000232 RID: 562 RVA: 0x00003A36 File Offset: 0x00001C36
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00009E0C File Offset: 0x0000800C
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Main.Tools.TestClient.UI.DataSetEditorForm));
			this.dataGrid = new global::Main.Tools.TestClient.UI.DataGridWrapper();
			this.cancelButton = new global::System.Windows.Forms.Button();
			this.okButton = new global::System.Windows.Forms.Button();
			this.pasteButton = new global::System.Windows.Forms.Button();
			this.copyButton = new global::System.Windows.Forms.Button();
			((global::System.ComponentModel.ISupportInitialize)this.dataGrid).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.dataGrid, "dataGrid");
			this.dataGrid.DataMember = "";
			this.dataGrid.HeaderForeColor = global::System.Drawing.SystemColors.ControlText;
			this.dataGrid.Name = "dataGrid";
			this.dataGrid.Navigate += new global::System.Windows.Forms.NavigateEventHandler(this.dataGrid_Navigate);
			componentResourceManager.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new global::System.EventHandler(this.cancelButton_Click);
			componentResourceManager.ApplyResources(this.okButton, "okButton");
			this.okButton.Name = "okButton";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new global::System.EventHandler(this.okButton_Click);
			componentResourceManager.ApplyResources(this.pasteButton, "pasteButton");
			this.pasteButton.Name = "pasteButton";
			this.pasteButton.UseVisualStyleBackColor = true;
			this.pasteButton.Click += new global::System.EventHandler(this.pasteButton_Click);
			componentResourceManager.ApplyResources(this.copyButton, "copyButton");
			this.copyButton.BackColor = global::System.Drawing.SystemColors.Control;
			this.copyButton.Name = "copyButton";
			this.copyButton.UseVisualStyleBackColor = true;
			this.copyButton.Click += new global::System.EventHandler(this.copyButton_Click);
			base.AcceptButton = this.okButton;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.ControlBox = false;
			base.Controls.Add(this.copyButton);
			base.Controls.Add(this.pasteButton);
			base.Controls.Add(this.okButton);
			base.Controls.Add(this.cancelButton);
			base.Controls.Add(this.dataGrid);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DataSetEditorForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			((global::System.ComponentModel.ISupportInitialize)this.dataGrid).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x040000D2 RID: 210
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000D3 RID: 211
		private global::Main.Tools.TestClient.UI.DataGridWrapper dataGrid;

		// Token: 0x040000D4 RID: 212
		private global::System.Windows.Forms.Button cancelButton;

		// Token: 0x040000D5 RID: 213
		private global::System.Windows.Forms.Button okButton;

		// Token: 0x040000D6 RID: 214
		private global::System.Windows.Forms.Button pasteButton;

		// Token: 0x040000D7 RID: 215
		private global::System.Windows.Forms.Button copyButton;
	}
}
