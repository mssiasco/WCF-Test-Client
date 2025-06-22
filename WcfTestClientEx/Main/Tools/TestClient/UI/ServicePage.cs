using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Main.Tools.TestClient.Variables;
using WcfTestClientEx;


namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000053 RID: 83
	internal class ServicePage : TabPage
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x000041AD File Offset: 0x000023AD
		internal ServicePage()
		{
			this.InitializeComponent();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000DC50 File Offset: 0x0000BE50
		internal ServicePage(MainForm mainForm, ServiceMethodInfo method) : this()
		{
			this.mainForm = mainForm;
			this.method = method;
			this.testCase = this.TestCase;
			this.formatTabControl.TabPages.Add(this.formattedPage = new FormattedPage(this));
			this.formatTabControl.TabPages.Add(this.xmlPage = new XmlPage());
			string text = method.Endpoint.OperationContractTypeName + "." + method.MethodName;
			if (ServicePage.tabNumbers.ContainsKey(text))
			{
				string str = " (";
				IDictionary<string, int> dictionary = ServicePage.tabNumbers;
				string key = text;
				int value = dictionary[key] + 1;
				dictionary[key] = value;
				string str2 = str + value.ToString() + ")";
				this.Text = this.Wrap(method.MethodName + str2);
				base.ToolTipText = text + str2;
			}
			else
			{
				this.Text = this.Wrap(method.MethodName);
				base.ToolTipText = text;
				ServicePage.tabNumbers.Add(text, 0);
			}
			base.ToolTipText = base.ToolTipText + " [" + method.Endpoint.ServiceProject.Address + "]";
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000DD90 File Offset: 0x0000BF90
		internal TestCase TestCase
		{
			get
			{
				if (this.testCase == null)
				{
					this.testCase = this.method.CreateTestCase();
					this.testCase.OnErrorReported += this.testCase_OnErrorReported;
					this.testCase.ServicePage = this;
				}
				return this.testCase;
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000041BB File Offset: 0x000023BB
		internal void Close()
		{
			this.formattedPage.Close();
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000041C8 File Offset: 0x000023C8
		internal void ChangeInvokeStatus(bool invokeEnabled)
		{
			this.formattedPage.ChangeInvokeStatus(invokeEnabled);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000041D6 File Offset: 0x000023D6
		internal void InvokeTestCase(Variable[] variables, bool newProxy)
		{
			this.mainForm.InvokeTestCase(this, variables, newProxy);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000041E6 File Offset: 0x000023E6
		internal void OnValueUpdated()
		{
			this.xmlPage.ResponseXml = "";
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000041F8 File Offset: 0x000023F8
		internal void PopulateOutput(Variable[] variables, string responseXml)
		{
			if (variables != null)
			{
				this.formattedPage.PopulateOutput(variables);
			}
			if (responseXml != null)
			{
				this.xmlPage.ResponseXml = responseXml;
			}
			this.testCase.Remove();
			this.testCase = null;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000422A File Offset: 0x0000242A
		internal void ResetInput()
		{
			this.formattedPage.ResetInputTree();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00004237 File Offset: 0x00002437
		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			e.Result = ServiceExecutor.TranslateToXmlInClientDomain(this.testCase, this.formattedPage.GetVariables());
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00004255 File Offset: 0x00002455
		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.xmlPage.RequestXml = (e.Result as string);
			this.formatTabControl.Enabled = true;
			this.formatTabControl.Focus();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00004285 File Offset: 0x00002485
		private void formatTabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.formatTabControl.SelectedTab is XmlPage)
			{
				this.xmlPage.RequestXml = StringResources.Loading;
				this.formatTabControl.Enabled = false;
				this.backgroundWorker.RunWorkerAsync();
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000042C0 File Offset: 0x000024C0
		private void testCase_OnErrorReported(ErrorItem errorItem)
		{
			this.mainForm.OnErrorReported(new ErrorItem[]
			{
				errorItem
			});
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000042D7 File Offset: 0x000024D7
		private string Wrap(string p)
		{
			if (p.Length > 33)
			{
				return p.Substring(0, 15) + "..." + p.Substring(p.Length - 15);
			}
			return p;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00004307 File Offset: 0x00002507
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ServicePage));
			this.backgroundWorker = new BackgroundWorker();
			this.formatTabControl = new TabControl();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.backgroundWorker, "backgroundWorker");
			this.backgroundWorker.DoWork += this.backgroundWorker_DoWork;
			this.backgroundWorker.RunWorkerCompleted += this.backgroundWorker_RunWorkerCompleted;
			componentResourceManager.ApplyResources(this.formatTabControl, "formatTabControl");
			this.formatTabControl.Multiline = true;
			this.formatTabControl.Name = "formatTabControl";
			this.formatTabControl.SelectedIndex = 0;
			this.formatTabControl.SelectedIndexChanged += this.formatTabControl_SelectedIndexChanged;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.formatTabControl);
			base.Name = "ServicePage";
			base.ResumeLayout(false);
		}

		// Token: 0x0400014A RID: 330
		private static IDictionary<string, int> tabNumbers = new Dictionary<string, int>();

		// Token: 0x0400014B RID: 331
		private FormattedPage formattedPage;

		// Token: 0x0400014C RID: 332
		private MainForm mainForm;

		// Token: 0x0400014D RID: 333
		private ServiceMethodInfo method;

		// Token: 0x0400014E RID: 334
		private TestCase testCase;

		// Token: 0x0400014F RID: 335
		private XmlPage xmlPage;

		// Token: 0x04000150 RID: 336
		private IContainer components;

		// Token: 0x04000151 RID: 337
		private BackgroundWorker backgroundWorker;

		// Token: 0x04000152 RID: 338
		private TabControl formatTabControl;
	}
}
