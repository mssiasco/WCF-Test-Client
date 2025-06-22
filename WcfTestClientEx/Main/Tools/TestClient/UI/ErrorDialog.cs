using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WcfTestClientEx;

namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000049 RID: 73
	internal partial class ErrorDialog : Form
	{
		// Token: 0x06000238 RID: 568 RVA: 0x0000A158 File Offset: 0x00008358
		internal ErrorDialog()
		{
			this.InitializeComponent();
			this.Text = StringResources.ProductName;
			this.errorMsgWrapCb.Text = StringResources.ErrorMsgCbText;
			this.textViewTp.Text = StringResources.TextViewTpText;
			this.htmlViewTp.Text = StringResources.HtmlViewTpText;
			this.errorIconPictureBox.Image = SystemIcons.Error.ToBitmap();
			this.prevButton.Image = ResourceHelper.ArrowUpImage;
			this.nextButton.Image = ResourceHelper.ArrowDownImage;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00003A7D File Offset: 0x00001C7D
		internal ErrorDialog(ErrorItem[] errorItems)
			: this()
		{
			this.errorItems = errorItems;
			this.RefreshView();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000A1E4 File Offset: 0x000083E4
		internal static void DisplayError(Form owner, ErrorItem[] errorItems)
		{
			new ErrorDialog(errorItems)
			{
				StartPosition = FormStartPosition.CenterParent,
				ShowInTaskbar = false
			}.ShowDialog(owner);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00003A92 File Offset: 0x00001C92
		private void closeButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00003A9A File Offset: 0x00001C9A
		private void closeButton_KeyDown(object sender, KeyEventArgs e)
		{
			this.ProcessKeys(e);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00003AA3 File Offset: 0x00001CA3
		private void errorDetailTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.A)
			{
				this.errorDetailTextBox.SelectAll();
			}
			this.ProcessKeys(e);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00003AD9 File Offset: 0x00001CD9
		private void errorMessageTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.A)
			{
				this.errorMessageTextBox.SelectAll();
			}
			this.ProcessKeys(e);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00003B0F File Offset: 0x00001D0F
		private void nextButton_Click(object sender, EventArgs e)
		{
			this.currentIndex++;
			this.RefreshView();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00003A9A File Offset: 0x00001C9A
		private void nextButton_KeyDown(object sender, KeyEventArgs e)
		{
			this.ProcessKeys(e);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00003B25 File Offset: 0x00001D25
		private void prevButton_Click(object sender, EventArgs e)
		{
			this.currentIndex--;
			this.RefreshView();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00003A9A File Offset: 0x00001C9A
		private void prevButton_KeyDown(object sender, KeyEventArgs e)
		{
			this.ProcessKeys(e);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00003B3B File Offset: 0x00001D3B
		private void ProcessKeys(KeyEventArgs e)
		{
			if (!e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.Escape)
			{
				this.closeButton_Click(null, e);
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000A210 File Offset: 0x00008410
		private void RefreshView()
		{
			this.prevButton.Visible = (this.nextButton.Visible = (this.errorBrowseLocationLabel.Visible = this.errorItems.Length > 1));
			this.errorBrowseLocationLabel.Text = string.Format(CultureInfo.CurrentUICulture, StringResources.ErrorBrowseLocationFormat, this.currentIndex + 1, this.errorItems.Length);
			this.errorMessageTextBox.Text = this.errorItems[this.currentIndex].ErrorMessage;
			this.errorDetailTextBox.Text = this.TrimScript(this.errorItems[this.currentIndex].ErrorDetail);
			this.errorMsgWebBrowser.DocumentText = this.errorItems[this.currentIndex].ErrorDetail;
			this.prevButton.Enabled = this.currentIndex != 0;
			this.nextButton.Enabled = this.currentIndex != this.errorItems.Length - 1;
			this.errorMessageTextBox.Select(0, 0);
			this.errorDetailTextBox.Select(0, 0);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000A330 File Offset: 0x00008530
		private string TrimScript(string htmlDocText)
		{
			string text = "<script type=\"text/javascript\">(.*?)</script>";
			Regex regex = new Regex(text);
			return regex.Replace(htmlDocText, "");
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00003B67 File Offset: 0x00001D67
		private void errorMsgWrapCb_CheckedChanged(object sender, EventArgs e)
		{
			this.errorDetailTextBox.WordWrap = this.errorMsgWrapCb.Checked;
		}

		// Token: 0x040000D9 RID: 217
		private int currentIndex;

		// Token: 0x040000DA RID: 218
		private ErrorItem[] errorItems;
	}
}
