using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Main.Tools.TestClient.Variables;
using WcfTestClientEx;

namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000047 RID: 71
	internal partial class DataSetEditorForm : Form
	{
		// Token: 0x0600022A RID: 554 RVA: 0x00009C2C File Offset: 0x00007E2C
		public DataSetEditorForm(object value, bool isReadOnly)
		{
			this.InitializeComponent();
			this.dataSetVariable = value as DataSetVariable;
			this.SetDataSource(this.dataSetVariable.GetDataSetValue());
			this.dataGrid.ReadOnly = isReadOnly;
			if (this.dataGrid.ReadOnly)
			{
				this.pasteButton.Visible = false;
				this.copyButton.Location = this.pasteButton.Location;
			}
			if (Variable.VariablesPool.Count < 1)
			{
				this.pasteButton.Enabled = false;
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000039E2 File Offset: 0x00001BE2
		private static bool IsAtDataSetRoot(DataGrid dataGrid)
		{
			return dataGrid.DataMember == null || dataGrid.DataMember == string.Empty;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000039FE File Offset: 0x00001BFE
		private void cancelButton_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00003A07 File Offset: 0x00001C07
		private void copyButton_Click(object sender, EventArgs e)
		{
			Variable.SaveToPool(this.dataSetVariable);
			if (Variable.VariablesPool.Count > 0)
			{
				this.pasteButton.Enabled = true;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009CB8 File Offset: 0x00007EB8
		private void dataGrid_Navigate(object sender, NavigateEventArgs ne)
		{
			string dataSetName = (this.dataSetVariable.GetDataSetValue() as DataSet).DataSetName;
			if (DataSetEditorForm.IsAtDataSetRoot(this.dataGrid))
			{
				this.dataGrid.CaptionText = dataSetName;
				return;
			}
			this.dataGrid.CaptionText = string.Format(CultureInfo.CurrentUICulture, "{0}: {1}", dataSetName, this.dataGrid.DataMember);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00003A2D File Offset: 0x00001C2D
		private void okButton_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009D1C File Offset: 0x00007F1C
		private void pasteButton_Click(object sender, EventArgs e)
		{
			if (!this.dataSetVariable.IsDefaultDataSet() && !this.dataSetVariable.SchemaEquals(Variable.VariablesPool[0]) && RtlAwareMessageBox.Show(StringResources.DifferentSchemaWarning, StringResources.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0) != DialogResult.OK)
			{
				return;
			}
			bool flag = this.dataSetVariable.CopyFrom(Variable.VariablesPool[0]);
			if (flag)
			{
				this.dataGrid.DataSource = null;
				this.SetDataSource(this.dataSetVariable.GetDataSetValue());
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00009DA0 File Offset: 0x00007FA0
		private void SetDataSource(object o)
		{
			DataSet dataSet = o as DataSet;
			if (dataSet != null)
			{
				this.dataGrid.DataSource = dataSet;
				this.dataGrid.Expand(-1);
				this.dataGrid.CaptionText = dataSet.DataSetName;
				if (dataSet.Tables.Count == 1)
				{
					this.dataGrid.NavigateTo(0, dataSet.Tables[0].TableName);
				}
			}
		}

		// Token: 0x040000D1 RID: 209
		private DataSetVariable dataSetVariable;
	}
}
