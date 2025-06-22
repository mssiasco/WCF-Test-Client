using System.Windows.Forms;

namespace Main.Tools.TestClient.UI
{
    // Token: 0x0200004D RID: 77
    public partial class MainForm : Form
    {
        // Token: 0x060002A2 RID: 674 RVA: 0x00004040 File Offset: 0x00002240
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Token: 0x060002A3 RID: 675 RVA: 0x0000C878 File Offset: 0x0000AA78
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.serviceTreeView = new System.Windows.Forms.TreeView();
            this.rootContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addServiceContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addServiceWorker = new System.ComponentModel.BackgroundWorker();
            this.invokeServiceWorker = new System.ComponentModel.BackgroundWorker();
            this.serviceTabControl = new System.Windows.Forms.TabControl();
            this.tabPageContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalStatusStrip = new System.Windows.Forms.StatusStrip();
            this.GlobalStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxTreeViewFilter = new System.Windows.Forms.TextBox();
            this.serviceProjectContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyServiceProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyFullPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editWithSvcConfigEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.restoreToDefaultConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.endpointContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyEndpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operationContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyOperationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSystemWatcher = new System.IO.FileSystemWatcher();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addServiceMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.recentServicesMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxHideAsync = new System.Windows.Forms.CheckBox();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rootContextMenu.SuspendLayout();
            this.tabPageContextMenu.SuspendLayout();
            this.globalStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.serviceProjectContextMenu.SuspendLayout();
            this.fileContextMenu.SuspendLayout();
            this.endpointContextMenu.SuspendLayout();
            this.operationContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // serviceTreeView
            // 
            this.serviceTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serviceTreeView.Location = new System.Drawing.Point(3, 22);
            this.serviceTreeView.Name = "serviceTreeView";
            this.serviceTreeView.ShowNodeToolTips = true;
            this.serviceTreeView.Size = new System.Drawing.Size(219, 354);
            this.serviceTreeView.TabIndex = 0;
            this.serviceTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OpenNode);
            this.serviceTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.serviceTreeView_KeyDown);
            this.serviceTreeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.serviceTreeView_MouseDown);
            // 
            // rootContextMenu
            // 
            this.rootContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addServiceContextMenuItem});
            this.rootContextMenu.Name = "rootContextMenu";
            this.rootContextMenu.Size = new System.Drawing.Size(146, 26);
            // 
            // addServiceContextMenuItem
            // 
            this.addServiceContextMenuItem.Name = "addServiceContextMenuItem";
            this.addServiceContextMenuItem.Size = new System.Drawing.Size(145, 22);
            this.addServiceContextMenuItem.Text = "&Add Service...";
            this.addServiceContextMenuItem.Click += new System.EventHandler(this.addServiceMenuItem_Click);
            // 
            // addServiceWorker
            // 
            this.addServiceWorker.WorkerReportsProgress = true;
            this.addServiceWorker.WorkerSupportsCancellation = true;
            this.addServiceWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.addServiceWorker_DoWork);
            this.addServiceWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.addServiceWorker_RunWorkerCompleted);
            // 
            // invokeServiceWorker
            // 
            this.invokeServiceWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.invokeServiceWorker_DoWork);
            this.invokeServiceWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.invokeServiceWorker_RunWorkerCompleted);
            // 
            // serviceTabControl
            // 
            this.serviceTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serviceTabControl.Location = new System.Drawing.Point(0, 0);
            this.serviceTabControl.Name = "serviceTabControl";
            this.serviceTabControl.SelectedIndex = 0;
            this.serviceTabControl.Size = new System.Drawing.Size(371, 376);
            this.serviceTabControl.TabIndex = 0;
            this.serviceTabControl.SelectedIndexChanged += new System.EventHandler(this.serviceTabControl_SelectedIndexChanged);
            this.serviceTabControl.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.serviceTabControl_ControlAdded);
            this.serviceTabControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.serviceTabControl_KeyDown);
            this.serviceTabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.serviceTabControl_MouseDown);
            // 
            // tabPageContextMenu
            // 
            this.tabPageContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.closeAllToolStripMenuItem});
            this.tabPageContextMenu.Name = "tabPageContextMenu";
            this.tabPageContextMenu.Size = new System.Drawing.Size(121, 48);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.closeAllToolStripMenuItem.Text = "Close &All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click);
            // 
            // globalStatusStrip
            // 
            this.globalStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GlobalStatusLabel});
            this.globalStatusStrip.Location = new System.Drawing.Point(0, 400);
            this.globalStatusStrip.Name = "globalStatusStrip";
            this.globalStatusStrip.Size = new System.Drawing.Size(596, 22);
            this.globalStatusStrip.TabIndex = 7;
            // 
            // GlobalStatusLabel
            // 
            this.GlobalStatusLabel.Name = "GlobalStatusLabel";
            this.GlobalStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.serviceTreeView);
            this.splitContainer.Panel1.Controls.Add(this.panel1);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.serviceTabControl);
            this.splitContainer.Size = new System.Drawing.Size(596, 376);
            this.splitContainer.SplitterDistance = 222;
            this.splitContainer.SplitterWidth = 3;
            this.splitContainer.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxTreeViewFilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(219, 22);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // textBoxTreeViewFilter
            // 
            this.textBoxTreeViewFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxTreeViewFilter.Location = new System.Drawing.Point(0, 0);
            this.textBoxTreeViewFilter.Name = "textBoxTreeViewFilter";
            this.textBoxTreeViewFilter.Size = new System.Drawing.Size(219, 20);
            this.textBoxTreeViewFilter.TabIndex = 0;
            this.textBoxTreeViewFilter.TextChanged += new System.EventHandler(this.textBoxTreeViewFilter_TextChanged);
            // 
            // serviceProjectContextMenu
            // 
            this.serviceProjectContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshServiceToolStripMenuItem,
            this.removeServiceToolStripMenuItem,
            this.toolStripSeparator1,
            this.copyServiceProjectToolStripMenuItem});
            this.serviceProjectContextMenu.Name = "serviceProjectContextMenu";
            this.serviceProjectContextMenu.Size = new System.Drawing.Size(158, 76);
            // 
            // refreshServiceToolStripMenuItem
            // 
            this.refreshServiceToolStripMenuItem.Name = "refreshServiceToolStripMenuItem";
            this.refreshServiceToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.refreshServiceToolStripMenuItem.Text = "Re&fresh Service";
            this.refreshServiceToolStripMenuItem.Click += new System.EventHandler(this.refreshServiceToolStripMenuItem_Click);
            // 
            // removeServiceToolStripMenuItem
            // 
            this.removeServiceToolStripMenuItem.Name = "removeServiceToolStripMenuItem";
            this.removeServiceToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.removeServiceToolStripMenuItem.Text = "&Remove Service";
            this.removeServiceToolStripMenuItem.Click += new System.EventHandler(this.removeServiceToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
            // 
            // copyServiceProjectToolStripMenuItem
            // 
            this.copyServiceProjectToolStripMenuItem.Name = "copyServiceProjectToolStripMenuItem";
            this.copyServiceProjectToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.copyServiceProjectToolStripMenuItem.Text = "&Copy Address";
            this.copyServiceProjectToolStripMenuItem.Click += new System.EventHandler(this.copyServiceProjectToolStripMenuItem_Click);
            // 
            // fileContextMenu
            // 
            this.fileContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyFullPathToolStripMenuItem,
            this.editWithSvcConfigEditorToolStripMenuItem,
            this.toolStripSeparator2,
            this.restoreToDefaultConfigToolStripMenuItem});
            this.fileContextMenu.Name = "fileContextMenu";
            this.fileContextMenu.Size = new System.Drawing.Size(209, 76);
            // 
            // copyFullPathToolStripMenuItem
            // 
            this.copyFullPathToolStripMenuItem.Name = "copyFullPathToolStripMenuItem";
            this.copyFullPathToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.copyFullPathToolStripMenuItem.Text = "Copy &Full Path";
            this.copyFullPathToolStripMenuItem.Click += new System.EventHandler(this.copyFullPathToolStripMenuItem_Click);
            // 
            // editWithSvcConfigEditorToolStripMenuItem
            // 
            this.editWithSvcConfigEditorToolStripMenuItem.Name = "editWithSvcConfigEditorToolStripMenuItem";
            this.editWithSvcConfigEditorToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.editWithSvcConfigEditorToolStripMenuItem.Text = "Edit with SvcConfigEditor";
            this.editWithSvcConfigEditorToolStripMenuItem.Click += new System.EventHandler(this.editWithSvcConfigEditorToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(205, 6);
            // 
            // restoreToDefaultConfigToolStripMenuItem
            // 
            this.restoreToDefaultConfigToolStripMenuItem.Name = "restoreToDefaultConfigToolStripMenuItem";
            this.restoreToDefaultConfigToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.restoreToDefaultConfigToolStripMenuItem.Text = "Restore to Default Config";
            this.restoreToDefaultConfigToolStripMenuItem.Click += new System.EventHandler(this.restoreToDefaultConfigToolStripMenuItem_Click);
            // 
            // endpointContextMenu
            // 
            this.endpointContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyEndpointToolStripMenuItem});
            this.endpointContextMenu.Name = "endpointContextMenu";
            this.endpointContextMenu.Size = new System.Drawing.Size(103, 26);
            // 
            // copyEndpointToolStripMenuItem
            // 
            this.copyEndpointToolStripMenuItem.Name = "copyEndpointToolStripMenuItem";
            this.copyEndpointToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyEndpointToolStripMenuItem.Text = "&Copy";
            this.copyEndpointToolStripMenuItem.Click += new System.EventHandler(this.copyEndpointToolStripMenuItem_Click);
            // 
            // operationContextMenu
            // 
            this.operationContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyOperationToolStripMenuItem});
            this.operationContextMenu.Name = "operationContextMenu";
            this.operationContextMenu.Size = new System.Drawing.Size(103, 26);
            // 
            // copyOperationToolStripMenuItem
            // 
            this.copyOperationToolStripMenuItem.Name = "copyOperationToolStripMenuItem";
            this.copyOperationToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyOperationToolStripMenuItem.Text = "&Copy";
            this.copyOperationToolStripMenuItem.Click += new System.EventHandler(this.copyOperationToolStripMenuItem_Click);
            // 
            // fileSystemWatcher
            // 
            this.fileSystemWatcher.EnableRaisingEvents = true;
            this.fileSystemWatcher.Filter = "*.config";
            this.fileSystemWatcher.IncludeSubdirectories = true;
            this.fileSystemWatcher.NotifyFilter = System.IO.NotifyFilters.LastWrite;
            this.fileSystemWatcher.SynchronizingObject = this;
            this.fileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Changed);
            // 
            // menuStrip
            // 
            this.menuStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip.Size = new System.Drawing.Size(596, 24);
            this.menuStrip.TabIndex = 9;
            this.menuStrip.Text = " WCF Test Client";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addServiceMainMenuItem,
            this.toolStripSeparator3,
            this.recentServicesMainMenuItem,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // addServiceMainMenuItem
            // 
            this.addServiceMainMenuItem.Name = "addServiceMainMenuItem";
            this.addServiceMainMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
            this.addServiceMainMenuItem.Size = new System.Drawing.Size(225, 22);
            this.addServiceMainMenuItem.Text = "Add Service ... ";
            this.addServiceMainMenuItem.Click += new System.EventHandler(this.addServiceMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(222, 6);
            // 
            // recentServicesMainMenuItem
            // 
            this.recentServicesMainMenuItem.Name = "recentServicesMainMenuItem";
            this.recentServicesMainMenuItem.Size = new System.Drawing.Size(225, 22);
            this.recentServicesMainMenuItem.Text = "Recent Services";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(222, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // checkBoxHideAsync
            // 
            this.checkBoxHideAsync.AutoSize = true;
            this.checkBoxHideAsync.Checked = true;
            this.checkBoxHideAsync.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHideAsync.Location = new System.Drawing.Point(84, 4);
            this.checkBoxHideAsync.Name = "checkBoxHideAsync";
            this.checkBoxHideAsync.Size = new System.Drawing.Size(124, 17);
            this.checkBoxHideAsync.TabIndex = 10;
            this.checkBoxHideAsync.Text = "Hide Async Methods";
            this.checkBoxHideAsync.UseVisualStyleBackColor = true;
            this.checkBoxHideAsync.CheckedChanged += new System.EventHandler(this.checkBoxHideAsync_CheckedChanged);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(207, 22);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.aboutToolStripMenuItem.Text = "About WCF Test Client Ex";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 422);
            this.Controls.Add(this.checkBoxHideAsync);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.globalStatusStrip);
            this.Controls.Add(this.menuStrip);
            this.Name = "MainForm";
            this.Text = " WCF Test Client Ex";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.rootContextMenu.ResumeLayout(false);
            this.tabPageContextMenu.ResumeLayout(false);
            this.globalStatusStrip.ResumeLayout(false);
            this.globalStatusStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.serviceProjectContextMenu.ResumeLayout(false);
            this.fileContextMenu.ResumeLayout(false);
            this.endpointContextMenu.ResumeLayout(false);
            this.operationContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Token: 0x04000108 RID: 264
        private global::System.ComponentModel.IContainer components;

        // Token: 0x04000109 RID: 265
        private global::System.Windows.Forms.TreeView serviceTreeView;

        // Token: 0x0400010F RID: 271
        private global::System.ComponentModel.BackgroundWorker addServiceWorker;

        // Token: 0x04000110 RID: 272
        private global::System.ComponentModel.BackgroundWorker invokeServiceWorker;

        // Token: 0x04000111 RID: 273
        private global::System.Windows.Forms.TabControl serviceTabControl;

        // Token: 0x04000112 RID: 274
        private global::System.Windows.Forms.StatusStrip globalStatusStrip;

        // Token: 0x04000113 RID: 275
        private global::System.Windows.Forms.ToolStripStatusLabel GlobalStatusLabel;

        // Token: 0x04000114 RID: 276
        private global::System.Windows.Forms.SplitContainer splitContainer;

        // Token: 0x04000115 RID: 277
        private global::System.Windows.Forms.ContextMenuStrip rootContextMenu;

        // Token: 0x04000116 RID: 278
        private global::System.Windows.Forms.ToolStripMenuItem addServiceContextMenuItem;

        // Token: 0x04000117 RID: 279
        private global::System.Windows.Forms.ContextMenuStrip serviceProjectContextMenu;

        // Token: 0x04000118 RID: 280
        private global::System.Windows.Forms.ToolStripMenuItem removeServiceToolStripMenuItem;

        // Token: 0x04000119 RID: 281
        private global::System.Windows.Forms.ContextMenuStrip tabPageContextMenu;

        // Token: 0x0400011A RID: 282
        private global::System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;

        // Token: 0x0400011B RID: 283
        private global::System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;

        // Token: 0x0400011D RID: 285
        private global::System.Windows.Forms.ContextMenuStrip fileContextMenu;

        // Token: 0x0400011E RID: 286
        private global::System.Windows.Forms.ToolStripMenuItem copyFullPathToolStripMenuItem;

        // Token: 0x0400011F RID: 287
        private global::System.Windows.Forms.ContextMenuStrip endpointContextMenu;

        // Token: 0x04000120 RID: 288
        private global::System.Windows.Forms.ToolStripMenuItem copyEndpointToolStripMenuItem;

        // Token: 0x04000121 RID: 289
        private global::System.Windows.Forms.ContextMenuStrip operationContextMenu;

        // Token: 0x04000122 RID: 290
        private global::System.Windows.Forms.ToolStripMenuItem copyOperationToolStripMenuItem;

        // Token: 0x04000123 RID: 291
        private global::System.Windows.Forms.ToolStripMenuItem copyServiceProjectToolStripMenuItem;

        // Token: 0x04000124 RID: 292
        private global::System.Windows.Forms.ToolStripMenuItem refreshServiceToolStripMenuItem;

        // Token: 0x04000125 RID: 293
        private global::System.IO.FileSystemWatcher fileSystemWatcher;

        // Token: 0x04000128 RID: 296
        private global::System.Windows.Forms.ToolStripMenuItem editWithSvcConfigEditorToolStripMenuItem;

        // Token: 0x04000129 RID: 297
        private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

        // Token: 0x0400012A RID: 298
        private global::System.Windows.Forms.ToolStripMenuItem restoreToDefaultConfigToolStripMenuItem;

        // Token: 0x0400012F RID: 303
        private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem addServiceMainMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem recentServicesMainMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private Panel panel1;
        private TextBox textBoxTreeViewFilter;
        private CheckBox checkBoxHideAsync;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem1;
        private ToolStripMenuItem aboutToolStripMenuItem;
    }
}
