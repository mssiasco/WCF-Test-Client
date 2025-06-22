using Main.Internal.Performance;
using Main.Tools.Common;
using Main.Tools.TestClient.Variables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WcfTestClientEx;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Main.Tools.TestClient.UI
{
    // Token: 0x0200004D RID: 77
    public partial class MainForm : Form
    {
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000265 RID: 613 RVA: 0x0000B304 File Offset: 0x00009504
		// (remove) Token: 0x06000266 RID: 614 RVA: 0x0000B33C File Offset: 0x0000953C
		public event EventHandler ServicesRefreshed;
        private List<TreeNode> originalNodes=new List<TreeNode>();

        private void FilterTreeView(string filterText="")
        {
            serviceTreeView.BeginUpdate();
            serviceTreeView.Nodes.Clear();

            foreach (TreeNode node in originalNodes)
            {
                TreeNode filtered = FilterLeaves(node, filterText);
                if (filtered != null)
                    serviceTreeView.Nodes.Add(filtered);
            }
            serviceTreeView.ExpandAll();
            serviceTreeView.EndUpdate();
        }

        private TreeNode FilterLeaves(TreeNode node, string filterText)
        {
			if (node.Nodes.Count == 0)
			{
				// This is a leaf node
				if (

					(
					((!checkBoxHideAsync.Checked && node.ImageIndex == 5) || node.ImageIndex == 4)					
					&&
                    node.Text.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0
					)
					||
				
					node.ImageIndex==3 

					) {
				 return (TreeNode)node.Clone();
				}
                   
                else
                    return null;
            }

            TreeNode result = (TreeNode)node.Clone();
            result.Nodes.Clear();

            foreach (TreeNode child in node.Nodes)
            {
                TreeNode match = FilterLeaves(child, filterText);
                if (match != null)
                    result.Nodes.Add(match);
            }

            return result.Nodes.Count > 0 ? result : null;
        }


        // Token: 0x06000267 RID: 615 RVA: 0x0000B374 File Offset: 0x00009574
        internal MainForm()
		{
			this.disabledColor = SystemColors.GrayText;
			this.addServiceExecutor = new AddServiceExecutor();
			this.tabPageCloseOrderManager = new TabPageCloseOrderManager();
			this.workspace = new Workspace();
			MainForm.codeMarkers.InitPerformanceDll(73, MainForm.CodeMarkersRegRoot);
			MainForm.codeMarkers.CodeMarker(16720);
			this.InitializeComponent();
			base.Icon = ResourceHelper.ApplicationIcon;
			this.serviceTreeView.ImageList = new ImageList();
			this.serviceTreeView.ImageList.Images.Add(ResourceHelper.ApplicationIcon);
			this.serviceTreeView.ImageList.Images.Add(ResourceHelper.ContractImage);
			this.serviceTreeView.ImageList.Images.Add(ResourceHelper.EndpointImage);
			this.serviceTreeView.ImageList.Images.Add(ResourceHelper.FileImage);
			this.serviceTreeView.ImageList.Images.Add(ResourceHelper.OperationImage);
			this.serviceTreeView.ImageList.Images.Add(ResourceHelper.ErrorImage);
			this.serviceTabControl.TabPages.Add(this.startPage = new StartPage());
			this.ConstructRecentServiceMenuItems();
			this.SetFileWatchPath();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000B4C8 File Offset: 0x000096C8
		internal MainForm(string[] endpoints)
			: this()
		{
			if (endpoints != null && endpoints.Length != 0)
			{
				this.endpoints = endpoints;
			}
			else
			{
				this.UpdateStatusText(StringResources.StatusReady);
			}
			this.serviceTreeView.Nodes.Add(StringResources.RootNodeName);
			this.serviceTreeView.Nodes[0].ContextMenuStrip = this.rootContextMenu;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000B528 File Offset: 0x00009728
		internal void InvokeTestCase(ServicePage servicePage, Variable[] inputs, bool newClient)
		{
			if (ApplicationSettings.GetInstance().SecurityPromptEnabled)
			{
				bool flag;
				if (!WarningPromptDialog.Prompt(this, StringResources.SecurityWarningTitle, StringResources.SecurityWarning, out flag))
				{
					return;
				}
				if (flag)
				{
					ApplicationSettings.GetInstance().SecurityPromptEnabled = false;
				}
			}
			this.UpdateStatusText(StringResources.StatusInvokingService);
			this.UpdateButtonStatus(false, true);
			this.invokeServiceWorker.RunWorkerAsync(new ServiceInvocationInputs(inputs, servicePage, newClient));
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00003D19 File Offset: 0x00001F19
		internal void OnErrorReported(params ErrorItem[] errorItem)
		{
			ErrorDialog.DisplayError(this, errorItem);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00003D22 File Offset: 0x00001F22
		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new AboutForm().ShowDialog();
		}


		// Token: 0x0600026C RID: 620 RVA: 0x0000B58C File Offset: 0x0000978C
		private void AddNodesForServiceProject(ServiceProject serviceProject, TreeNode serviceProjectNode)
		{
			this.serviceTreeView.BeginUpdate();
			serviceProjectNode.Tag = serviceProject;
			serviceProject.ServiceProjectNode = serviceProjectNode;
			foreach (ClientEndpointInfo clientEndpointInfo in serviceProject.Endpoints)
			{
				TreeNode treeNode = new TreeNode(clientEndpointInfo.ToString());
				treeNode.ContextMenuStrip = this.endpointContextMenu;
				treeNode.Tag = clientEndpointInfo;


                serviceProjectNode.Nodes.Add(treeNode);
                //


                if (clientEndpointInfo.Valid)
				{
					treeNode.SelectedImageIndex = (treeNode.ImageIndex = 1);
				}
				else
				{
					treeNode.SelectedImageIndex = (treeNode.ImageIndex = 5);
					treeNode.ForeColor = this.disabledColor;
					if (!string.IsNullOrEmpty(clientEndpointInfo.InvalidReason))
					{
						treeNode.ToolTipText = clientEndpointInfo.InvalidReason;
					}
					else
					{
						treeNode.ToolTipText = StringResources.ErrorUnsupportedContract;
					}
				}

				var sotedMethods = clientEndpointInfo.Methods.OrderBy((ServiceMethodInfo m) => m.MethodName).ToList<ServiceMethodInfo>();

				foreach (ServiceMethodInfo serviceMethodInfo in sotedMethods)
				{
					TreeNode treeNode2 = new TreeNode(serviceMethodInfo.MethodName + "()");
					treeNode2.ContextMenuStrip = this.operationContextMenu;
					treeNode2.Tag = serviceMethodInfo;
					treeNode.Nodes.Add(treeNode2);
					if (clientEndpointInfo.Valid && serviceMethodInfo.Valid)
					{
						treeNode2.SelectedImageIndex = (treeNode2.ImageIndex = 4);
					}
					else
					{
						treeNode2.SelectedImageIndex = (treeNode2.ImageIndex = 5);
						treeNode2.ForeColor = this.disabledColor;
						if (!string.IsNullOrEmpty(clientEndpointInfo.InvalidReason))
						{
							treeNode2.ToolTipText = clientEndpointInfo.InvalidReason;
						}
						else if (!serviceMethodInfo.Valid)
						{
							TypeMemberInfo typeMemberInfo = serviceMethodInfo.InvalidMembers[0];
							treeNode2.ToolTipText = string.Format(CultureInfo.CurrentUICulture, StringResources.ErrorUnsupportedOperationBecauseOfUnsupportedType, typeMemberInfo.TypeName);
						}
						else
						{
							treeNode2.ToolTipText = StringResources.ErrorUnsupportedOperation;
						}
					}
				}
			}
			TreeNode treeNode3 = new TreeNode(StringResources.ConfigFileNodeName);
			treeNode3.SelectedImageIndex = (treeNode3.ImageIndex = 3);
			treeNode3.Tag = serviceProject.ConfigFile;
			serviceProjectNode.Nodes.Add(treeNode3);
			treeNode3.ContextMenuStrip = this.fileContextMenu;
			serviceProjectNode.ExpandAll();
			this.serviceTreeView.EndUpdate();
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000B83C File Offset: 0x00009A3C
		private void AddService(string address)
		{
			if (!string.IsNullOrEmpty(address))
			{
				while (address.EndsWith("/", StringComparison.Ordinal))
				{
					address = address.Substring(0, address.Length - 1);
				}
				AddServiceOutputs.IsRefreshing = false;
				this.StartAddServiceWorker(new AddServiceInputs(new string[] { address }), StringResources.AddingAction, StringResources.StatusAddingService);
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000B898 File Offset: 0x00009A98
		private void addServiceMenuItem_Click(object sender, EventArgs e)
		{
			ICollection<string> recentUrls = ApplicationSettings.GetInstance().RecentUrls;
			string[] array = new string[recentUrls.Count];
			recentUrls.CopyTo(array, 0);
			string text = PromptDialog.Prompt(this, StringResources.AddServiceTitle, StringResources.AddServicePrompt, array);
			this.AddService(text);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000B8DC File Offset: 0x00009ADC
		private void addServiceWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
			AddServiceOutputs addServiceOutputs = this.addServiceExecutor.Execute((AddServiceInputs)e.Argument, this.workspace, backgroundWorker);
			if (!backgroundWorker.CancellationPending)
			{
				backgroundWorker.ReportProgress(100);
			}
			else
			{
				addServiceOutputs.Cancelled = true;
			}
			e.Result = addServiceOutputs;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000B930 File Offset: 0x00009B30
		private void addServiceWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			AddServiceOutputs addServiceOutputs = (AddServiceOutputs)e.Result;
			this.UpdateStatusText(addServiceOutputs.GetStatusMessage());
			if (AddServiceOutputs.IsRefreshing)
			{
				if (addServiceOutputs.Cancelled)
				{
					this.UpdateButtonStatus();
					this.TriggerDelayedConfigRefresh(this.serviceTreeView.SelectedNode.Tag as ServiceProject);
					return;
				}
				ServiceProject serviceProject = this.RemoveServiceProject();
				this.serviceTreeView.SelectedNode.Nodes.Clear();
				if (addServiceOutputs.ServiceProjects.Count < 1)
				{
					this.OnServiceProjectRemoved(serviceProject);
				}
			}
			if (addServiceOutputs.Errors.Count > 0)
			{
				ErrorItem[] array = new ErrorItem[addServiceOutputs.Errors.Count];
				addServiceOutputs.Errors.CopyTo(array);
				this.OnErrorReported(array);
			}
			this.ConstructRecentServiceMenuItems();
			foreach (ServiceProject serviceProject2 in addServiceOutputs.ServiceProjects)
			{
				if (AddServiceOutputs.IsRefreshing)
				{
					this.OnServiceProjectRefreshed(serviceProject2);
				}
				else
				{
					this.OnServiceProjectAdded(serviceProject2);
				}
			}
			this.UpdateButtonStatus();
			if (this.ServicesRefreshed != null)
			{
				this.ServicesRefreshed(this, e);
			}
			MainForm.codeMarkers.CodeMarker(16721);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00003D2F File Offset: 0x00001F2F
		private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			while (this.serviceTabControl.TabCount > 0)
			{
				this.closeToolStripMenuItem_Click(sender, e);
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000BA74 File Offset: 0x00009C74
		private void CloseProjectServicePages(List<TestCase> referencedTestCases)
		{
			foreach (TestCase testCase in referencedTestCases)
			{
				if (this.serviceTabControl.Contains(testCase.ServicePage))
				{
					this.serviceTabControl.SelectedTab = testCase.ServicePage;
					this.RemoveSelectedTabPage();
				}
				testCase.ServicePage.Dispose();
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00003D49 File Offset: 0x00001F49
		private void CloseStartPageIfExists()
		{
			if (this.startPage != null)
			{
				this.serviceTabControl.TabPages.Remove(this.startPage);
				this.startPage.Dispose();
				this.startPage = null;
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TestCase testCase = this.serviceTabControl.SelectedTab.Tag as TestCase;
			if (testCase != null)
			{
				testCase.Remove();
			}
			this.RemoveSelectedTabPage();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000BB24 File Offset: 0x00009D24
		private void ConstructRecentServiceMenuItems()
		{
			ICollection<string> recentUrls = ApplicationSettings.GetInstance().RecentUrls;
			this.recentServicesMainMenuItem.DropDownItems.Clear();
			if (recentUrls.Count == 0)
			{
				this.recentServicesMainMenuItem.Enabled = false;
				return;
			}
			this.recentServicesMainMenuItem.Enabled = true;
			int num = 1;
			foreach (string text in recentUrls)
			{
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(string.Format(CultureInfo.CurrentUICulture, "{0} {1}", num, text));
				toolStripMenuItem.Click += this.recentServiceMenuItem_Click;
				this.recentServicesMainMenuItem.DropDownItems.Add(toolStripMenuItem);
				num++;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00003D7B File Offset: 0x00001F7B
		private void copyEndpointToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SafeClipboard.SetText(((ClientEndpointInfo)this.serviceTreeView.SelectedNode.Tag).ToString());
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00003D9C File Offset: 0x00001F9C
		private void copyFullPathToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SafeClipboard.SetText(((FileItem)this.serviceTreeView.SelectedNode.Tag).FileName);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000BBEC File Offset: 0x00009DEC
		private void CopyNode(TreeNode selectedNode)
		{
			if (selectedNode == null)
			{
				return;
			}
			object tag = selectedNode.Tag;
			ServiceProject serviceProject = tag as ServiceProject;
			ClientEndpointInfo clientEndpointInfo = tag as ClientEndpointInfo;
			ServiceMethodInfo serviceMethodInfo = tag as ServiceMethodInfo;
			bool flag = tag is FileItem;
			if (serviceProject != null)
			{
				this.copyServiceProjectToolStripMenuItem_Click(null, null);
			}
			if (clientEndpointInfo != null)
			{
				this.copyEndpointToolStripMenuItem_Click(null, null);
			}
			if (serviceMethodInfo != null)
			{
				this.copyOperationToolStripMenuItem_Click(null, null);
			}
			if (flag)
			{
				this.copyFullPathToolStripMenuItem_Click(null, null);
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000BC4C File Offset: 0x00009E4C
		private void copyOperationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ServiceMethodInfo serviceMethodInfo = (ServiceMethodInfo)this.serviceTreeView.SelectedNode.Tag;
			SafeClipboard.SetText(serviceMethodInfo.Endpoint.OperationContractTypeName + "." + serviceMethodInfo.MethodName);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00003DBD File Offset: 0x00001FBD
		private void copyServiceProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SafeClipboard.SetText(((ServiceProject)this.serviceTreeView.SelectedNode.Tag).Address);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000BC90 File Offset: 0x00009E90
		private void editWithSvcConfigEditorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string text;
			if (!((ServiceProject)this.serviceTreeView.SelectedNode.Parent.Tag).StartSvcConfigEditor(out text))
			{
				ErrorItem[] array = new ErrorItem[]
				{
					new ErrorItem(StringResources.StartSvcConfigEditorFail, text, null)
				};
				this.OnErrorReported(array);
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00003A92 File Offset: 0x00001C92
		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000BCE0 File Offset: 0x00009EE0
		private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			string fullPath = e.FullPath;
			if (e.ChangeType == WatcherChangeTypes.Changed)
			{
				ServiceProject serviceProject = this.workspace.FindServiceProject(fullPath);
				if (serviceProject != null)
				{
					this.TryRefreshConfig(serviceProject);
				}
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000BD14 File Offset: 0x00009F14
		private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			string directoryName = Path.GetDirectoryName(base.GetType().Assembly.Location);
			string twoLetterISOLanguageName = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
			string text = Path.Combine(directoryName, twoLetterISOLanguageName, "WcfTestClientEx.chm");
			if (File.Exists(text))
			{
				Help.ShowHelp(this, text);
				return;
			}
			Help.ShowHelp(this, "WcfTestClientEx.chm");
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000BD68 File Offset: 0x00009F68
		private void invokeServiceWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			ServiceInvocationInputs serviceInvocationInputs = (ServiceInvocationInputs)e.Argument;
			serviceInvocationInputs.ServicePage.TestCase.Method.Endpoint.ServiceProject.IsWorking = true;
			ServiceInvocationOutputs serviceInvocationOutputs = ServiceExecutor.ExecuteInClientDomain(serviceInvocationInputs);
			serviceInvocationOutputs.ServicePage = serviceInvocationInputs.ServicePage;
			e.Result = serviceInvocationOutputs;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00003DDE File Offset: 0x00001FDE
		private void CancelInvokeServiceWorker()
		{
			if (this.invokeServiceWorker.IsBusy)
			{
				this.isCancellingInvokeServiceWorker = true;
				this.invokeServiceWorker.WorkerSupportsCancellation = true;
				this.invokeServiceWorker.CancelAsync();
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000BDBC File Offset: 0x00009FBC
		private void invokeServiceWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (!this.isCancellingInvokeServiceWorker)
			{
				ServiceInvocationOutputs serviceInvocationOutputs = (ServiceInvocationOutputs)e.Result;
				Variable[] serviceInvocationResult = serviceInvocationOutputs.GetServiceInvocationResult();
				if (serviceInvocationResult != null)
				{
					serviceInvocationOutputs.ServicePage.PopulateOutput(serviceInvocationResult, serviceInvocationOutputs.ResponseXml);
					this.UpdateStatusText(StringResources.StatusInvokingServiceCompleted);
					if (serviceInvocationOutputs.ServicePage.TestCase.Method.IsOneWay)
					{
						RtlAwareMessageBox.Show(this, StringResources.OneWayMessageDisplay, StringResources.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
					}
				}
				else if (serviceInvocationOutputs.ExceptionType == ExceptionType.InvalidInput)
				{
					RtlAwareMessageBox.Show(serviceInvocationOutputs.ExceptionMessages[0], StringResources.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
				}
				else
				{
					serviceInvocationOutputs.ServicePage.PopulateOutput(serviceInvocationResult, serviceInvocationOutputs.ResponseXml);
					this.UpdateStatusText(StringResources.StatusInvokingServiceFailed);
					StringBuilder stringBuilder = new StringBuilder(serviceInvocationOutputs.ExceptionMessages[0]);
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(serviceInvocationOutputs.ExceptionStacks[0]);
					for (int i = 1; i < serviceInvocationOutputs.ExceptionMessages.Length; i++)
					{
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append("Inner Exception:");
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append(serviceInvocationOutputs.ExceptionMessages[i]);
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append(serviceInvocationOutputs.ExceptionStacks[i]);
					}
					serviceInvocationOutputs.ServicePage.TestCase.SetError(new ErrorItem(StringResources.StatusInvokingServiceFailed, stringBuilder.ToString(), serviceInvocationOutputs.ServicePage.TestCase));
				}
				ServiceProject serviceProject = serviceInvocationOutputs.ServicePage.TestCase.Method.Endpoint.ServiceProject;
				this.TriggerDelayedConfigRefresh(serviceProject);
			}
			this.isCancellingInvokeServiceWorker = false;
			this.UpdateButtonStatus();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00003E0D File Offset: 0x0000200D
		private bool IsBusy(bool startingAddServiceWorker, bool startingInvokeServiceWorker)
		{
			return startingInvokeServiceWorker || this.invokeServiceWorker.IsBusy || startingAddServiceWorker || this.addServiceWorker.IsBusy;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00003E31 File Offset: 0x00002031
		private void MainForm_Closing(object sender, CancelEventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			this.UpdateStatusText(StringResources.ClosingClientStatus);
			this.fileSystemWatcher.Dispose();
			this.workspace.Close();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00003E5F File Offset: 0x0000205F
		private void MainForm_Shown(object sender, EventArgs e)
		{
			if (this.endpoints == null)
			{
				return;
			}
			this.StartAddServiceWorker(new AddServiceInputs(this.endpoints), StringResources.AddingAction, StringResources.StatusAddingService);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000BF78 File Offset: 0x0000A178
		private void OnServiceProjectAdded(ServiceProject serviceProject)
		{
			this.serviceTreeView.BeginUpdate();
			TreeNode treeNode = this.serviceTreeView.Nodes[0];

				TreeNode treeNode2 = new TreeNode(serviceProject.Address);
				treeNode2.SelectedImageIndex = (treeNode2.ImageIndex = 2);
				treeNode2.ContextMenuStrip = this.serviceProjectContextMenu;
				this.AddNodesForServiceProject(serviceProject, treeNode2);



            //treeNode.Nodes.Add(treeNode2);
            treeNode2.Expand();
            originalNodes.Add(treeNode2);

			FilterTreeView();

            //treeNode.Expand();
            this.serviceTreeView.EndUpdate();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00003E85 File Offset: 0x00002085
		private void OnServiceProjectRefreshed(ServiceProject serviceProject)
		{
			this.AddNodesForServiceProject(serviceProject, this.serviceTreeView.SelectedNode);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00003E99 File Offset: 0x00002099
		private void OnServiceProjectRemoved(ServiceProject serviceProject)
		{
			serviceProject.ServiceProjectNode.Remove();
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000BFF0 File Offset: 0x0000A1F0
		private void OpenMethod(ServiceMethodInfo selectedMethod)
		{
			ServicePage servicePage = new ServicePage(this, selectedMethod);
			this.serviceTabControl.TabPages.Add(servicePage);
			servicePage.ResetInput();
			this.serviceTabControl.SelectedTab = servicePage;
			this.UpdateButtonStatus();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00003EA6 File Offset: 0x000020A6
		private void OpenNode(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.OpenNode(this.serviceTreeView.SelectedNode);
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000C030 File Offset: 0x0000A230
		private void OpenNode(TreeNode selectedNode)
		{
			if (selectedNode == null)
			{
				return;
			}
			ServiceMethodInfo serviceMethodInfo = selectedNode.Tag as ServiceMethodInfo;
			FileItem fileItem = selectedNode.Tag as FileItem;
			if (serviceMethodInfo != null && serviceMethodInfo.Endpoint.Valid && serviceMethodInfo.Valid)
			{
				this.CloseStartPageIfExists();
				this.OpenMethod(serviceMethodInfo);
				return;
			}
			if (fileItem != null)
			{
				this.CloseStartPageIfExists();
				this.SwitchTab(fileItem.FilePage);
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000C094 File Offset: 0x0000A294
		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool flag;
			if (OptionsForm.Prompt(this, out flag))
			{
				ApplicationSettings.GetInstance().RegenerateConfigEnabled = flag;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
		private void recentServiceMenuItem_Click(object sender, EventArgs e)
		{
			string text = ((ToolStripMenuItem)sender).Text;
			int num = text.IndexOf(' ');
			string text2 = text.Substring(num + 1);
			this.AddService(text2);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		private void RefreshConfig(ServiceProject serviceProject)
		{
			List<TestCase> referencedTestCases = serviceProject.ReferencedTestCases;
			this.UpdateStatusText(StringResources.StatusLoadingConfig);
			List<ErrorItem> list;
			if (!serviceProject.RefreshConfig(out list))
			{
				this.UpdateStatusText(StringResources.StatusLoadingConfigFailed);
			}
			else
			{
				this.CloseProjectServicePages(referencedTestCases);
				this.serviceTreeView.BeginUpdate();
				foreach (object obj in this.serviceTreeView.Nodes[0].Nodes)
				{
					TreeNode treeNode = (TreeNode)obj;
					if (treeNode.Tag as ServiceProject == serviceProject)
					{
						treeNode.Nodes.Clear();
						this.AddNodesForServiceProject(serviceProject, treeNode);
						break;
					}
				}
				this.serviceTreeView.EndUpdate();
				this.UpdateStatusText(StringResources.StatusLoadingConfigCompleted);
			}
			if (list != null && list.Count > 0)
			{
				ErrorItem[] array = new ErrorItem[list.Count];
				list.CopyTo(array);
				this.OnErrorReported(array);
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000C1F4 File Offset: 0x0000A3F4
		private void refreshServiceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.serviceTreeView.SelectedNode.Tag is ServiceProject)
			{
				if (ApplicationSettings.GetInstance().RefreshPromptEnabled)
				{
					bool flag;
					if (!WarningPromptDialog.Prompt(this, StringResources.RefreshServiceWarningTitle, StringResources.RefreshServiceWarning, out flag))
					{
						return;
					}
					if (flag)
					{
						ApplicationSettings.GetInstance().RefreshPromptEnabled = false;
					}
				}
				ServiceProject serviceProject = this.serviceTreeView.SelectedNode.Tag as ServiceProject;
				serviceProject.IsWorking = true;
				AddServiceOutputs.IsRefreshing = true;
				this.StartAddServiceWorker(new AddServiceInputs(new string[] { serviceProject.Address }), StringResources.RefreshingAction, StringResources.StatusRefreshingService);
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000C290 File Offset: 0x0000A490
		private void RemoveSelectedTabPage()
		{
			this.serviceTabControl.SelectedTab.Focus();
			this.tabPageCloseOrderManager.HandleTabClosed(this.serviceTabControl.SelectedIndex);
			this.removingTabPage = true;
			if (this.serviceTabControl.SelectedTab is ServicePage)
			{
				((ServicePage)this.serviceTabControl.SelectedTab).Close();
			}
			this.serviceTabControl.TabPages.RemoveAt(this.serviceTabControl.SelectedIndex);
			this.removingTabPage = false;
			this.serviceTabControl.SelectedIndex = this.tabPageCloseOrderManager.LastTab();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000C32C File Offset: 0x0000A52C
		private ServiceProject RemoveServiceProject()
		{
			ServiceProject serviceProject = (ServiceProject)this.serviceTreeView.SelectedNode.Tag;
			ICollection<TabPage> collection = new List<TabPage>();
			foreach (FileItem fileItem in serviceProject.ReferencedFiles)
			{
				collection.Add(fileItem.FilePage);
			}
			foreach (TestCase testCase in serviceProject.ReferencedTestCases)
			{
				ServicePage servicePage = testCase.ServicePage;
				if (!collection.Contains(servicePage))
				{
					collection.Add(servicePage);
				}
			}
			foreach (TabPage tabPage in collection)
			{
				if (this.serviceTabControl.Contains(tabPage))
				{
					this.serviceTabControl.SelectedTab = tabPage;
					this.RemoveSelectedTabPage();
				}
				tabPage.Dispose();
			}
			this.workspace.Remove(serviceProject);
			return serviceProject;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000C460 File Offset: 0x0000A660
		private void removeServiceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.serviceTreeView.SelectedNode.Tag is ServiceProject && RtlAwareMessageBox.Show(this, StringResources.RemoveServiceWarning, StringResources.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0) == DialogResult.OK)
			{
				this.UpdateStatusText(StringResources.StatusRemovingService);
				this.UpdateButtonStatus(true, false);
				ServiceProject serviceProject = this.RemoveServiceProject();
				this.OnServiceProjectRemoved(serviceProject);
				this.UpdateStatusText(StringResources.StatusRemovingServiceCompleted);
				this.UpdateButtonStatus();
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
		private void restoreToDefaultConfigToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ServiceProject serviceProject = (ServiceProject)this.serviceTreeView.SelectedNode.Parent.Tag;
			this.fileSystemWatcher.EnableRaisingEvents = false;
			string text;
			serviceProject.RestoreDefaultConfig(out text);
			if (text != null)
			{
				ErrorItem[] array = new ErrorItem[]
				{
					new ErrorItem(StringResources.DefaultConfigNotFound, text, null)
				};
				this.OnErrorReported(array);
			}
			this.fileSystemWatcher.EnableRaisingEvents = true;
			this.RefreshConfig(serviceProject);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00003EC6 File Offset: 0x000020C6
		private void serviceTabControl_ControlAdded(object sender, ControlEventArgs e)
		{
			if (this.serviceTabControl.TabCount == 1)
			{
				this.tabPageCloseOrderManager.HandleTabAdded(this.serviceTabControl.SelectedIndex);
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000C540 File Offset: 0x0000A740
		private void serviceTabControl_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.F4)
			{
				if (this.serviceTabControl.TabCount > 0)
				{
					this.RemoveSelectedTabPage();
					return;
				}
			}
			else if (e.KeyCode == Keys.Apps && this.serviceTabControl.TabCount > 0 && this.serviceTabControl.Focused)
			{
				Rectangle tabRect = this.serviceTabControl.GetTabRect(this.serviceTabControl.SelectedIndex);
				this.tabPageContextMenu.Show(this.serviceTabControl, (tabRect.Left + tabRect.Right) / 2, (tabRect.Top + tabRect.Bottom) / 2);
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
		private void serviceTabControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right && e.Button != MouseButtons.Middle)
			{
				return;
			}
			for (int i = 0; i < this.serviceTabControl.TabCount; i++)
			{
				if (this.serviceTabControl.GetTabRect(i).Contains(e.X, e.Y))
				{
					this.serviceTabControl.SelectedTab = this.serviceTabControl.TabPages[i];
					break;
				}
			}
            if (e.Button == MouseButtons.Middle)
            {
                RemoveSelectedTabPage();
				return;

            }

            this.tabPageContextMenu.Show(this.serviceTabControl, e.X, e.Y);

           
        }

		// Token: 0x06000296 RID: 662 RVA: 0x00003EEC File Offset: 0x000020EC
		private void serviceTabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.serviceTabControl.SelectedIndex != -1 && !this.removingTabPage)
			{
				this.tabPageCloseOrderManager.HandleTabChanged(this.serviceTabControl.SelectedIndex);
			}
			this.UpdateButtonStatus();
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000C684 File Offset: 0x0000A884
		private void serviceTreeView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				this.removeServiceToolStripMenuItem_Click(null, null);
			}
			if (!e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.Return)
			{
				this.OpenNode(this.serviceTreeView.SelectedNode);
			}
			if (e.Control && !e.Alt && !e.Shift && e.KeyCode == Keys.C)
			{
				this.CopyNode(this.serviceTreeView.SelectedNode);
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00003F20 File Offset: 0x00002120
		private void serviceTreeView_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				this.serviceTreeView.SelectedNode = this.serviceTreeView.GetNodeAt(e.X, e.Y);
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000C70C File Offset: 0x0000A90C
		private void SetFileWatchPath()
		{
			try
			{
				this.fileSystemWatcher.Path = ApplicationSettings.GetInstance().ProjectBase;
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00003F51 File Offset: 0x00002151
		private void StartAddServiceWorker(AddServiceInputs inputs, string action, string status)
		{
			this.UpdateButtonStatus(true, false);
			this.UpdateStatusText(status);
			this.addServiceWorker.RunWorkerAsync(inputs);
			if (!ProgressDialog.Prompt(this, action, status, this.addServiceWorker))
			{
				this.addServiceWorker.CancelAsync();
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00003F89 File Offset: 0x00002189
		private void SwitchTab(TabPage tabPage)
		{
			if (!this.serviceTabControl.TabPages.Contains(tabPage))
			{
				this.serviceTabControl.TabPages.Add(tabPage);
			}
			this.serviceTabControl.SelectedTab = tabPage;
			this.UpdateButtonStatus();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00003FC1 File Offset: 0x000021C1
		private void TriggerDelayedConfigRefresh(ServiceProject project)
		{
			project.IsWorking = false;
			if (project.IsConfigChanged)
			{
				project.IsConfigChanged = false;
				this.TryRefreshConfig(project);
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00003FE0 File Offset: 0x000021E0
		private void TryRefreshConfig(ServiceProject serviceProject)
		{
			if (serviceProject.IsWorking)
			{
				serviceProject.IsConfigChanged = true;
				return;
			}
			if (FileChangingForm.ShouldPrompt(serviceProject.Address) && FileChangingForm.Prompt(this, serviceProject.Address))
			{
				this.RefreshConfig(serviceProject);
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00004014 File Offset: 0x00002214
		private void UpdateButtonStatus()
		{
			this.UpdateButtonStatus(false, false);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000C744 File Offset: 0x0000A944
		private void UpdateButtonStatus(bool startingAddServiceWorker, bool startingInvokeServiceWorker)
		{
			bool flag = this.IsBusy(startingAddServiceWorker, startingInvokeServiceWorker);
			bool flag2 = !flag && this.serviceTabControl.SelectedTab is ServicePage;
			foreach (object obj in this.serviceTabControl.TabPages)
			{
				TabPage tabPage = (TabPage)obj;
				if (tabPage is ServicePage)
				{
					((ServicePage)tabPage).ChangeInvokeStatus(flag2);
				}
			}
			this.removeServiceToolStripMenuItem.Enabled = !flag;
			this.addServiceMainMenuItem.Enabled = !flag;
			this.addServiceContextMenuItem.Enabled = this.addServiceMainMenuItem.Enabled;
			this.recentServicesMainMenuItem.Enabled = !flag && ApplicationSettings.GetInstance().RecentUrls.Count > 0;
			this.refreshServiceToolStripMenuItem.Enabled = !flag;
			this.editWithSvcConfigEditorToolStripMenuItem.Enabled = !flag;
			this.restoreToDefaultConfigToolStripMenuItem.Enabled = !flag;
			this.closeToolStripMenuItem.Enabled = this.serviceTabControl.TabPages.Count != 0;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000401E File Offset: 0x0000221E
		private void UpdateStatusText(string status)
		{
			this.GlobalStatusLabel.Text = status;
			this.globalStatusStrip.AccessibleName = status;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00004038 File Offset: 0x00002238
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.CancelInvokeServiceWorker();
		}

		// Token: 0x040000FC RID: 252
		private const string chmName = "WcfTestClientEx.chm";

		// Token: 0x040000FD RID: 253
		private static readonly string CodeMarkersRegRoot = "Software\\Main\\VisualStudio\\" + VersionNumbers.VSCurrentVersionString;

		// Token: 0x040000FE RID: 254
		private static CodeMarkers codeMarkers = CodeMarkers.Instance;

		// Token: 0x040000FF RID: 255
		private readonly Color disabledColor;

		// Token: 0x04000100 RID: 256
		private AddServiceExecutor addServiceExecutor;

		// Token: 0x04000101 RID: 257
		private string[] endpoints;

		// Token: 0x04000102 RID: 258
		private bool removingTabPage;

		// Token: 0x04000103 RID: 259
		private StartPage startPage;

		// Token: 0x04000104 RID: 260
		private TabPageCloseOrderManager tabPageCloseOrderManager;

		// Token: 0x04000105 RID: 261
		private Workspace workspace;

		// Token: 0x04000106 RID: 262
		private volatile bool isCancellingInvokeServiceWorker;

		// Token: 0x04000130 RID: 304
		private List<TreeNode> originalRootNodes = new List<TreeNode>();

		// Token: 0x0200004E RID: 78
		private enum IconIndex
		{
			// Token: 0x04000132 RID: 306
			ApplicationIcon,
			// Token: 0x04000133 RID: 307
			EndpointImage,
			// Token: 0x04000134 RID: 308
			ContractImage,
			// Token: 0x04000135 RID: 309
			FileImage,
			// Token: 0x04000136 RID: 310
			OperationImage,
			// Token: 0x04000137 RID: 311
			ErrorImage
		}

       

        private void MainForm_Load(object sender, EventArgs e)
        {
            //AddService("http://ias22:8733/Design_Time_Addresses/AccountServices/ACNService/mex");
        }


        private void recentServicesMainMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxShowAsync_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxTreeViewFilter_TextChanged(object sender, EventArgs e)
        {
			FilterTreeView(textBoxTreeViewFilter.Text);
        }

        private void checkBoxHideAsync_CheckedChanged(object sender, EventArgs e)
        {
			textBoxTreeViewFilter_TextChanged(sender, e);
        }
    }
}
