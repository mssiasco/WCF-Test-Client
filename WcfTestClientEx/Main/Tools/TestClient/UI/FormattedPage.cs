using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Main.Tools.Common;
using Main.Tools.TestClient.Variables;
using Microsoft.VisualStudio.VirtualTreeGrid;
using WcfTestClientEx;

namespace Main.Tools.TestClient.UI
{
	// Token: 0x0200004C RID: 76
	internal class FormattedPage : TabPage
	{
		// Token: 0x06000257 RID: 599 RVA: 0x0000AC38 File Offset: 0x00008E38
		internal FormattedPage()
		{
			this.InitializeComponent();
			VirtualTreeColumnHeader[] array = new VirtualTreeColumnHeader[]
			{
				new VirtualTreeColumnHeader(StringResources.NameHeader),
				new VirtualTreeColumnHeader(StringResources.ValueHeader),
				new VirtualTreeColumnHeader(StringResources.TypeHeader)
			};
			this.inputControl.SetColumnHeaders(array, true);
			VirtualTreeColumnHeader[] array2 = new VirtualTreeColumnHeader[]
			{
				new VirtualTreeColumnHeader(StringResources.NameHeader),
				new VirtualTreeColumnHeader(StringResources.ValueHeader),
				new VirtualTreeColumnHeader(StringResources.TypeHeader)
			};
			this.outputControl.SetColumnHeaders(array2, true);
			this.outputControl.KeyDown += this.outputControl_KeyDown;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00003C79 File Offset: 0x00001E79
		internal FormattedPage(ServicePage servicePage) : this()
		{
			this.servicePage = servicePage;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000ACF8 File Offset: 0x00008EF8
		internal void ChangeInvokeStatus(bool invokeButtonStatus)
		{
			Control control = this.newProxyCheckBox;
			Control control2 = this.inputControl;
			this.invokeButton.Enabled = invokeButtonStatus;
			control2.Enabled = invokeButtonStatus;
			control.Enabled = invokeButtonStatus;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00003C88 File Offset: 0x00001E88
		internal Variable[] GetVariables()
		{
			return ((ParameterTreeAdapter)((ITree)this.inputControl.MultiColumnTree).Root).GetVariables();
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000AD30 File Offset: 0x00008F30
		private void CloseVirtualTreeControl(VirtualTreeControl virtualTreeControl)
		{
			if (virtualTreeControl.MultiColumnTree != null)
			{
				ParameterTreeAdapter parameterTreeAdapter = ((ITree)virtualTreeControl.MultiColumnTree).Root as ParameterTreeAdapter;
				if (parameterTreeAdapter != null)
				{
					parameterTreeAdapter.Close();
				}
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00003CA9 File Offset: 0x00001EA9
		internal void Close()
		{
			this.CloseVirtualTreeControl(this.inputControl);
			this.CloseVirtualTreeControl(this.outputControl);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000AD64 File Offset: 0x00008F64
		internal void PopulateOutput(Variable[] variables)
		{
			this.PopulateTree(variables, this.outputControl, true);
			for (int i = 0; i < variables.Length; i++)
			{
				int num = variables.Length - i - 1;
				if (this.outputControl.Tree.IsExpandable(num, 0))
				{
					this.outputControl.Tree.ToggleExpansion(num, 0);
				}
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000ADBC File Offset: 0x00008FBC
		internal void ResetInputTree()
		{
			Variable[] variables = this.servicePage.TestCase.Method.GetVariables();
			this.PopulateTree(variables, this.inputControl, false);
			for (int i = 0; i < variables.Length; i++)
			{
				int num = variables.Length - i - 1;
				if (this.inputControl.Tree.IsExpandable(num, 0))
				{
					this.inputControl.Tree.ToggleExpansion(num, 0);
				}
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00003CC3 File Offset: 0x00001EC3
		private void FormattedPage_OnValueUpdated()
		{
			this.outputControl.MultiColumnTree = null;
			this.servicePage.OnValueUpdated();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00003CDC File Offset: 0x00001EDC
		private void invokeButton_Click(object sender, EventArgs e)
		{
			this.servicePage.InvokeTestCase(this.GetVariables(), this.newProxyCheckBox.Checked);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000AE2C File Offset: 0x0000902C
		private void outputControl_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.C)
			{
				ColumnItemEnumerator columnItemEnumerator = this.outputControl.CreateSelectedItemEnumerator();
				if (columnItemEnumerator != null)
				{
					int num = columnItemEnumerator.RowInTree + 1;
					int columnInTree = columnItemEnumerator.ColumnInTree;
					VirtualTreeItemInfo itemInfo = this.outputControl.Tree.GetItemInfo(num, columnInTree, false);
					string text = itemInfo.Branch.GetText(itemInfo.Row, itemInfo.Column);
					if (string.IsNullOrEmpty(text))
					{
						SafeClipboard.Clear();
						return;
					}
					SafeClipboard.SetText(text);
				}
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000AEC0 File Offset: 0x000090C0
		private void PopulateTree(Variable[] variables, VirtualTreeControl parameterTreeView, bool readOnly)
		{
			parameterTreeView.MultiColumnTree = new MultiColumnTree(3);
			ITree tree = (ITree)parameterTreeView.MultiColumnTree;
			tree.Root = new ParameterTreeAdapter(tree, parameterTreeView, variables, readOnly, null);
			((ParameterTreeAdapter)((ITree)this.inputControl.MultiColumnTree).Root).OnValueUpdated += this.FormattedPage_OnValueUpdated;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00003CFA File Offset: 0x00001EFA
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000AF20 File Offset: 0x00009120
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormattedPage));
			this.splitContainer1 = new SplitContainer();
			this.inputControl = new VirtualTreeControl();
			this.requestHeaderPanel = new Panel();
			this.requestLabel = new Label();
			this.outputControl = new VirtualTreeControl();
			this.requestControlPanel = new Panel();
			this.responseLabel = new Label();
			this.invokeButton = new Button();
			this.newProxyCheckBox = new CheckBox();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.requestHeaderPanel.SuspendLayout();
			this.requestControlPanel.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.splitContainer1, "splitContainer1");
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.inputControl);
			this.splitContainer1.Panel1.Controls.Add(this.requestHeaderPanel);
			this.splitContainer1.Panel2.Controls.Add(this.outputControl);
			this.splitContainer1.Panel2.Controls.Add(this.requestControlPanel);
			componentResourceManager.ApplyResources(this.inputControl, "inputControl");
			this.inputControl.HasGridLines = true;
			this.inputControl.HasHorizontalGridLines = true;
			this.inputControl.HasVerticalGridLines = true;
			this.inputControl.LabelEditSupport = VirtualTreeLabelEditActivationStyles.ImmediateSelection;
			this.inputControl.Name = "inputControl";
			this.requestHeaderPanel.BackColor = SystemColors.Control;
			this.requestHeaderPanel.Controls.Add(this.requestLabel);
			componentResourceManager.ApplyResources(this.requestHeaderPanel, "requestHeaderPanel");
			this.requestHeaderPanel.Name = "requestHeaderPanel";
			componentResourceManager.ApplyResources(this.requestLabel, "requestLabel");
			this.requestLabel.Name = "requestLabel";
			componentResourceManager.ApplyResources(this.outputControl, "outputControl");
			this.outputControl.HasGridLines = true;
			this.outputControl.HasHorizontalGridLines = true;
			this.outputControl.HasVerticalGridLines = true;
			this.outputControl.LabelEditSupport = VirtualTreeLabelEditActivationStyles.ImmediateSelection;
			this.outputControl.Name = "outputControl";
			this.requestControlPanel.BackColor = SystemColors.Control;
			this.requestControlPanel.Controls.Add(this.responseLabel);
			this.requestControlPanel.Controls.Add(this.invokeButton);
			this.requestControlPanel.Controls.Add(this.newProxyCheckBox);
			componentResourceManager.ApplyResources(this.requestControlPanel, "requestControlPanel");
			this.requestControlPanel.Name = "requestControlPanel";
			componentResourceManager.ApplyResources(this.responseLabel, "responseLabel");
			this.responseLabel.Name = "responseLabel";
			componentResourceManager.ApplyResources(this.invokeButton, "invokeButton");
			this.invokeButton.Name = "invokeButton";
			this.invokeButton.UseVisualStyleBackColor = true;
			this.invokeButton.Click += this.invokeButton_Click;
			componentResourceManager.ApplyResources(this.newProxyCheckBox, "newProxyCheckBox");
			this.newProxyCheckBox.Name = "newProxyCheckBox";
			this.newProxyCheckBox.UseVisualStyleBackColor = true;
			base.Controls.Add(this.splitContainer1);
			componentResourceManager.ApplyResources(this, "$this");
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.requestHeaderPanel.ResumeLayout(false);
			this.requestHeaderPanel.PerformLayout();
			this.requestControlPanel.ResumeLayout(false);
			this.requestControlPanel.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x040000F1 RID: 241
		private ServicePage servicePage;

		// Token: 0x040000F2 RID: 242
		private IContainer components;

		// Token: 0x040000F3 RID: 243
		private SplitContainer splitContainer1;

		// Token: 0x040000F4 RID: 244
		private VirtualTreeControl inputControl;

		// Token: 0x040000F5 RID: 245
		private VirtualTreeControl outputControl;

		// Token: 0x040000F6 RID: 246
		private Panel requestHeaderPanel;

		// Token: 0x040000F7 RID: 247
		private Panel requestControlPanel;

		// Token: 0x040000F8 RID: 248
		private Button invokeButton;

		// Token: 0x040000F9 RID: 249
		private Label requestLabel;

		// Token: 0x040000FA RID: 250
		private Label responseLabel;

		// Token: 0x040000FB RID: 251
		private CheckBox newProxyCheckBox;
	}
}
