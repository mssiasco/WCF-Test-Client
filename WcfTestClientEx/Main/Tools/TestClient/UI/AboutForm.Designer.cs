namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000046 RID: 70
	internal partial class AboutForm : global::System.Windows.Forms.Form
	{
		// Token: 0x06000228 RID: 552 RVA: 0x000039C3 File Offset: 0x00001BC3
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009908 File Offset: 0x00007B08
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Main.Tools.TestClient.UI.AboutForm));
			this.buttonSystemInformation = new global::System.Windows.Forms.Button();
			this.labelCompanyName = new global::System.Windows.Forms.Label();
			this.textBoxDescription = new global::System.Windows.Forms.TextBox();
			this.buttonOk = new global::System.Windows.Forms.Button();
			this.labelProductName = new global::System.Windows.Forms.Label();
			this.labelVersion = new global::System.Windows.Forms.Label();
			this.labelCopyright = new global::System.Windows.Forms.Label();
			this.wcfPicture = new global::System.Windows.Forms.PictureBox();
			((global::System.ComponentModel.ISupportInitialize)this.wcfPicture).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.buttonSystemInformation, "buttonSystemInformation");
			this.buttonSystemInformation.Name = "buttonSystemInformation";
			this.buttonSystemInformation.UseVisualStyleBackColor = true;
			this.buttonSystemInformation.Click += new global::System.EventHandler(this.buttonSystemInformation_Click);
			componentResourceManager.ApplyResources(this.labelCompanyName, "labelCompanyName");
			this.labelCompanyName.MaximumSize = new global::System.Drawing.Size(0, 17);
			this.labelCompanyName.Name = "labelCompanyName";
			componentResourceManager.ApplyResources(this.textBoxDescription, "textBoxDescription");
			this.textBoxDescription.Name = "textBoxDescription";
			this.textBoxDescription.ReadOnly = true;
			componentResourceManager.ApplyResources(this.buttonOk, "buttonOk");
			this.buttonOk.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.labelProductName, "labelProductName");
			this.labelProductName.MaximumSize = new global::System.Drawing.Size(0, 17);
			this.labelProductName.Name = "labelProductName";
			componentResourceManager.ApplyResources(this.labelVersion, "labelVersion");
			this.labelVersion.MaximumSize = new global::System.Drawing.Size(0, 17);
			this.labelVersion.Name = "labelVersion";
			componentResourceManager.ApplyResources(this.labelCopyright, "labelCopyright");
			this.labelCopyright.MaximumSize = new global::System.Drawing.Size(0, 17);
			this.labelCopyright.Name = "labelCopyright";
			componentResourceManager.ApplyResources(this.wcfPicture, "wcfPicture");
			this.wcfPicture.Name = "wcfPicture";
			this.wcfPicture.TabStop = false;
			base.AcceptButton = this.buttonOk;
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.None;
			base.CancelButton = this.buttonOk;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.labelProductName);
			base.Controls.Add(this.buttonOk);
			base.Controls.Add(this.labelVersion);
			base.Controls.Add(this.buttonSystemInformation);
			base.Controls.Add(this.labelCopyright);
			base.Controls.Add(this.wcfPicture);
			base.Controls.Add(this.labelCompanyName);
			base.Controls.Add(this.textBoxDescription);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "About****Form";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			((global::System.ComponentModel.ISupportInitialize)this.wcfPicture).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000C8 RID: 200
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000C9 RID: 201
		private global::System.Windows.Forms.Button buttonSystemInformation;

		// Token: 0x040000CA RID: 202
		private global::System.Windows.Forms.Label labelCompanyName;

		// Token: 0x040000CB RID: 203
		private global::System.Windows.Forms.TextBox textBoxDescription;

		// Token: 0x040000CC RID: 204
		private global::System.Windows.Forms.Button buttonOk;

		// Token: 0x040000CD RID: 205
		private global::System.Windows.Forms.Label labelProductName;

		// Token: 0x040000CE RID: 206
		private global::System.Windows.Forms.Label labelVersion;

		// Token: 0x040000CF RID: 207
		private global::System.Windows.Forms.Label labelCopyright;

		// Token: 0x040000D0 RID: 208
		private global::System.Windows.Forms.PictureBox wcfPicture;
	}
}
