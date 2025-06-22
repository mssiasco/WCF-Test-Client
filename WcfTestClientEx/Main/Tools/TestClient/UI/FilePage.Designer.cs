using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using WcfTestClientEx;

namespace Main.Tools.TestClient.UI
{
	// Token: 0x0200004A RID: 74
	internal class FilePage : TabPage
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00003B9E File Offset: 0x00001D9E
		internal FilePage(string filePath)
		{
			this.InitializeComponent();
			this.Text = Path.GetFileName(filePath);
			this.LoadTextFromFile(filePath);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00003BBF File Offset: 0x00001DBF
		internal void RefreshFile(string filePath)
		{
			this.LoadTextFromFile(filePath);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00003BC8 File Offset: 0x00001DC8
		private void fileText_GotFocus(object sender, EventArgs e)
		{
			this.fileText.Select(0, 0);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00003BD7 File Offset: 0x00001DD7
		private void fileText_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.A)
			{
				this.fileText.SelectAll();
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000A8D4 File Offset: 0x00008AD4
		private void LoadTextFromFile(string filePath)
		{
			if (!File.Exists(filePath))
			{
				this.fileText.Text = StringResources.ConfigNotFound;
				return;
			}
			try
			{
				this.fileText.Text = File.ReadAllText(filePath);
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00003C06 File Offset: 0x00001E06
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000A930 File Offset: 0x00008B30
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FilePage));
			this.fileText = new TextBox();
			base.SuspendLayout();
			this.fileText.CausesValidation = false;
			componentResourceManager.ApplyResources(this.fileText, "fileText");
			this.fileText.Name = "fileText";
			this.fileText.ReadOnly = true;
			this.fileText.GotFocus += this.fileText_GotFocus;
			this.fileText.KeyDown += this.fileText_KeyDown;
			base.CausesValidation = false;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.fileText);
			base.Name = "FilePage";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000E9 RID: 233
		private IContainer components;

		// Token: 0x040000EA RID: 234
		private TextBox fileText;
	}
}
