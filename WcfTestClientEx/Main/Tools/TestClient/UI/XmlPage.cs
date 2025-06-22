using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000057 RID: 87
	internal class XmlPage : TabPage
	{
		// Token: 0x060002DA RID: 730 RVA: 0x0000441A File Offset: 0x0000261A
		internal XmlPage()
		{
			this.InitializeComponent();
		}

		// Token: 0x170000CC RID: 204
		// (set) Token: 0x060002DB RID: 731 RVA: 0x00004428 File Offset: 0x00002628
		internal string RequestXml
		{
			set
			{
				this.requestXmlTextBox.Text = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00004436 File Offset: 0x00002636
		internal string ResponseXml
		{
			set
			{
				this.responseXmlTextBox.Text = value;
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00004444 File Offset: 0x00002644
		private void requestXmlTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.A)
			{
				this.requestXmlTextBox.SelectAll();
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00004473 File Offset: 0x00002673
		private void responseXmlTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.A)
			{
				this.responseXmlTextBox.SelectAll();
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000044A2 File Offset: 0x000026A2
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000E310 File Offset: 0x0000C510
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(XmlPage));
			this.xmlSplitContainer = new SplitContainer();
			this.requestXmlTextBox = new TextBox();
			this.requestPanel = new Panel();
			this.requestLabel = new Label();
			this.responseXmlTextBox = new TextBox();
			this.responsePanel = new Panel();
			this.responseLabel = new Label();
			this.xmlSplitContainer.Panel1.SuspendLayout();
			this.xmlSplitContainer.Panel2.SuspendLayout();
			this.xmlSplitContainer.SuspendLayout();
			this.requestPanel.SuspendLayout();
			this.responsePanel.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.xmlSplitContainer, "xmlSplitContainer");
			this.xmlSplitContainer.Name = "xmlSplitContainer";
			this.xmlSplitContainer.Panel1.Controls.Add(this.requestXmlTextBox);
			this.xmlSplitContainer.Panel1.Controls.Add(this.requestPanel);
			this.xmlSplitContainer.Panel2.Controls.Add(this.responseXmlTextBox);
			this.xmlSplitContainer.Panel2.Controls.Add(this.responsePanel);
			componentResourceManager.ApplyResources(this.requestXmlTextBox, "requestXmlTextBox");
			this.requestXmlTextBox.Name = "requestXmlTextBox";
			this.requestXmlTextBox.ReadOnly = true;
			this.requestXmlTextBox.KeyDown += this.requestXmlTextBox_KeyDown;
			this.requestPanel.Controls.Add(this.requestLabel);
			componentResourceManager.ApplyResources(this.requestPanel, "requestPanel");
			this.requestPanel.Name = "requestPanel";
			componentResourceManager.ApplyResources(this.requestLabel, "requestLabel");
			this.requestLabel.Name = "requestLabel";
			componentResourceManager.ApplyResources(this.responseXmlTextBox, "responseXmlTextBox");
			this.responseXmlTextBox.Name = "responseXmlTextBox";
			this.responseXmlTextBox.ReadOnly = true;
			this.responseXmlTextBox.KeyDown += this.responseXmlTextBox_KeyDown;
			this.responsePanel.Controls.Add(this.responseLabel);
			componentResourceManager.ApplyResources(this.responsePanel, "responsePanel");
			this.responsePanel.Name = "responsePanel";
			componentResourceManager.ApplyResources(this.responseLabel, "responseLabel");
			this.responseLabel.Name = "responseLabel";
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.xmlSplitContainer);
			base.Name = "XmlPage";
			this.xmlSplitContainer.Panel1.ResumeLayout(false);
			this.xmlSplitContainer.Panel1.PerformLayout();
			this.xmlSplitContainer.Panel2.ResumeLayout(false);
			this.xmlSplitContainer.Panel2.PerformLayout();
			this.xmlSplitContainer.ResumeLayout(false);
			this.requestPanel.ResumeLayout(false);
			this.requestPanel.PerformLayout();
			this.responsePanel.ResumeLayout(false);
			this.responsePanel.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x0400015C RID: 348
		private IContainer components;

		// Token: 0x0400015D RID: 349
		private SplitContainer xmlSplitContainer;

		// Token: 0x0400015E RID: 350
		private TextBox requestXmlTextBox;

		// Token: 0x0400015F RID: 351
		private TextBox responseXmlTextBox;

		// Token: 0x04000160 RID: 352
		private Panel requestPanel;

		// Token: 0x04000161 RID: 353
		private Label requestLabel;

		// Token: 0x04000162 RID: 354
		private Panel responsePanel;

		// Token: 0x04000163 RID: 355
		private Label responseLabel;
	}
}
