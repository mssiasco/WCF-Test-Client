using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using Main.Tools.TestClient.UI;
using Main.Tools.TestClient.Variables;

namespace Main.Tools.TestClient
{
	// Token: 0x0200000D RID: 13
	internal class DataSetUITypeEditor : UITypeEditor
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00002B71 File Offset: 0x00000D71
		public DataSetUITypeEditor(bool isReadOnly, ParameterTreeAdapter paraTree)
		{
			this.isReadOnly = isReadOnly;
			this.paraTree = paraTree;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002B87 File Offset: 0x00000D87
		public void Close()
		{
			if (this.dataSetEditorForm != null)
			{
				this.dataSetEditorForm.Close();
				this.Clean();
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00002BA2 File Offset: 0x00000DA2
		private void Clean()
		{
			if (this.dataSetEditorForm != null)
			{
				this.dataSetEditorForm.Dispose();
				this.dataSetEditorForm = null;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005BC8 File Offset: 0x00003DC8
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			DataSetVariable dataSetVariable = (value as DataSetVariable).Clone() as DataSetVariable;
			this.dataSetEditorForm = new DataSetEditorForm(value, this.isReadOnly);
			if (this.dataSetEditorForm.ShowDialog() != DialogResult.OK)
			{
				if (!(value as DataSetVariable).CopyFrom(dataSetVariable))
				{
					value = null;
				}
			}
			else
			{
				this.paraTree.PropagateValueUpdateEvent();
			}
			this.Clean();
			return null;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002BBE File Offset: 0x00000DBE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x04000032 RID: 50
		private DataSetEditorForm dataSetEditorForm;

		// Token: 0x04000033 RID: 51
		private bool isReadOnly;

		// Token: 0x04000034 RID: 52
		private ParameterTreeAdapter paraTree;
	}
}
