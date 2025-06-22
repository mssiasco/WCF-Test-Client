namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000049 RID: 73
	internal partial class ErrorDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x06000247 RID: 583 RVA: 0x00003B7F File Offset: 0x00001D7F
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000A358 File Offset: 0x00008558
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Main.Tools.TestClient.UI.ErrorDialog));
			this.errorIconPictureBox = new global::System.Windows.Forms.PictureBox();
			this.errorMessageTextBox = new global::System.Windows.Forms.TextBox();
			this.prevButton = new global::System.Windows.Forms.Button();
			this.nextButton = new global::System.Windows.Forms.Button();
			this.errorDetailLabel = new global::System.Windows.Forms.Label();
			this.errorBrowseLocationLabel = new global::System.Windows.Forms.Label();
			this.closeButton = new global::System.Windows.Forms.Button();
			this.errorMsgTabControl = new global::System.Windows.Forms.TabControl();
			this.textViewTp = new global::System.Windows.Forms.TabPage();
			this.errorMsgWrapCb = new global::System.Windows.Forms.CheckBox();
			this.errorDetailTextBox = new global::System.Windows.Forms.TextBox();
			this.htmlViewTp = new global::System.Windows.Forms.TabPage();
			this.errorMsgWebBrowser = new global::System.Windows.Forms.WebBrowser();
			((global::System.ComponentModel.ISupportInitialize)this.errorIconPictureBox).BeginInit();
			this.errorMsgTabControl.SuspendLayout();
			this.textViewTp.SuspendLayout();
			this.htmlViewTp.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.errorIconPictureBox, "errorIconPictureBox");
			this.errorIconPictureBox.Name = "errorIconPictureBox";
			this.errorIconPictureBox.TabStop = false;
			componentResourceManager.ApplyResources(this.errorMessageTextBox, "errorMessageTextBox");
			this.errorMessageTextBox.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
			this.errorMessageTextBox.Name = "errorMessageTextBox";
			this.errorMessageTextBox.ReadOnly = true;
			this.errorMessageTextBox.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.errorMessageTextBox_KeyDown);
			componentResourceManager.ApplyResources(this.prevButton, "prevButton");
			this.prevButton.Name = "prevButton";
			this.prevButton.UseVisualStyleBackColor = true;
			this.prevButton.Click += new global::System.EventHandler(this.prevButton_Click);
			this.prevButton.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.prevButton_KeyDown);
			componentResourceManager.ApplyResources(this.nextButton, "nextButton");
			this.nextButton.Name = "nextButton";
			this.nextButton.UseVisualStyleBackColor = true;
			this.nextButton.Click += new global::System.EventHandler(this.nextButton_Click);
			this.nextButton.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.nextButton_KeyDown);
			componentResourceManager.ApplyResources(this.errorDetailLabel, "errorDetailLabel");
			this.errorDetailLabel.Name = "errorDetailLabel";
			componentResourceManager.ApplyResources(this.errorBrowseLocationLabel, "errorBrowseLocationLabel");
			this.errorBrowseLocationLabel.Name = "errorBrowseLocationLabel";
			componentResourceManager.ApplyResources(this.closeButton, "closeButton");
			this.closeButton.Name = "closeButton";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new global::System.EventHandler(this.closeButton_Click);
			this.closeButton.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.closeButton_KeyDown);
			componentResourceManager.ApplyResources(this.errorMsgTabControl, "errorMsgTabControl");
			this.errorMsgTabControl.Controls.Add(this.textViewTp);
			this.errorMsgTabControl.Controls.Add(this.htmlViewTp);
			this.errorMsgTabControl.Name = "errorMsgTabControl";
			this.errorMsgTabControl.SelectedIndex = 0;
			this.errorMsgTabControl.SizeMode = global::System.Windows.Forms.TabSizeMode.FillToRight;
			this.textViewTp.Controls.Add(this.errorMsgWrapCb);
			this.textViewTp.Controls.Add(this.errorDetailTextBox);
			componentResourceManager.ApplyResources(this.textViewTp, "textViewTp");
			this.textViewTp.Name = "textViewTp";
			this.textViewTp.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.errorMsgWrapCb, "errorMsgWrapCb");
			this.errorMsgWrapCb.Name = "errorMsgWrapCb";
			this.errorMsgWrapCb.UseVisualStyleBackColor = true;
			this.errorMsgWrapCb.CheckedChanged += new global::System.EventHandler(this.errorMsgWrapCb_CheckedChanged);
			componentResourceManager.ApplyResources(this.errorDetailTextBox, "errorDetailTextBox");
			this.errorDetailTextBox.Name = "errorDetailTextBox";
			this.errorDetailTextBox.ReadOnly = true;
			this.errorDetailTextBox.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.errorDetailTextBox_KeyDown);
			this.htmlViewTp.Controls.Add(this.errorMsgWebBrowser);
			componentResourceManager.ApplyResources(this.htmlViewTp, "htmlViewTp");
			this.htmlViewTp.Name = "htmlViewTp";
			this.htmlViewTp.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.errorMsgWebBrowser, "errorMsgWebBrowser");
			this.errorMsgWebBrowser.MinimumSize = new global::System.Drawing.Size(20, 20);
			this.errorMsgWebBrowser.Name = "errorMsgWebBrowser";
			this.errorMsgWebBrowser.ScriptErrorsSuppressed = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.errorMsgTabControl);
			base.Controls.Add(this.closeButton);
			base.Controls.Add(this.errorBrowseLocationLabel);
			base.Controls.Add(this.errorDetailLabel);
			base.Controls.Add(this.nextButton);
			base.Controls.Add(this.prevButton);
			base.Controls.Add(this.errorMessageTextBox);
			base.Controls.Add(this.errorIconPictureBox);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ErrorDialog";
			base.ShowIcon = false;
			((global::System.ComponentModel.ISupportInitialize)this.errorIconPictureBox).EndInit();
			this.errorMsgTabControl.ResumeLayout(false);
			this.textViewTp.ResumeLayout(false);
			this.textViewTp.PerformLayout();
			this.htmlViewTp.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000DB RID: 219
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040000DC RID: 220
		private global::System.Windows.Forms.PictureBox errorIconPictureBox;

		// Token: 0x040000DD RID: 221
		private global::System.Windows.Forms.TextBox errorMessageTextBox;

		// Token: 0x040000DE RID: 222
		private global::System.Windows.Forms.Button prevButton;

		// Token: 0x040000DF RID: 223
		private global::System.Windows.Forms.Button nextButton;

		// Token: 0x040000E0 RID: 224
		private global::System.Windows.Forms.Label errorDetailLabel;

		// Token: 0x040000E1 RID: 225
		private global::System.Windows.Forms.Label errorBrowseLocationLabel;

		// Token: 0x040000E2 RID: 226
		private global::System.Windows.Forms.Button closeButton;

		// Token: 0x040000E3 RID: 227
		private global::System.Windows.Forms.TabControl errorMsgTabControl;

		// Token: 0x040000E4 RID: 228
		private global::System.Windows.Forms.TabPage textViewTp;

		// Token: 0x040000E5 RID: 229
		private global::System.Windows.Forms.CheckBox errorMsgWrapCb;

		// Token: 0x040000E6 RID: 230
		private global::System.Windows.Forms.TextBox errorDetailTextBox;

		// Token: 0x040000E7 RID: 231
		private global::System.Windows.Forms.TabPage htmlViewTp;

		// Token: 0x040000E8 RID: 232
		private global::System.Windows.Forms.WebBrowser errorMsgWebBrowser;
	}
}
