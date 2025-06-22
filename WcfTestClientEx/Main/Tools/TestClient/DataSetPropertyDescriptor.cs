using System;
using System.ComponentModel;
using System.Data;

namespace Main.Tools.TestClient
{
	// Token: 0x0200000C RID: 12
	internal class DataSetPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00002AE8 File Offset: 0x00000CE8
		internal DataSetPropertyDescriptor(object value, bool isReadOnly, ParameterTreeAdapter paraTree)
			: base("DataSetProperty", null)
		{
			this.value = value;
			this.isReadOnly = isReadOnly;
			this.paraTree = paraTree;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002B0B File Offset: 0x00000D0B
		public override Type ComponentType
		{
			get
			{
				throw new ExceptionUtility().ThrowHelperError(new InvalidOperationException());
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00002B1C File Offset: 0x00000D1C
		public override bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002B24 File Offset: 0x00000D24
		public override Type PropertyType
		{
			get
			{
				return typeof(DataSet);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002B30 File Offset: 0x00000D30
		public void Close()
		{
			if (this.dataSetUITypeEditor != null)
			{
				this.dataSetUITypeEditor.Close();
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002B45 File Offset: 0x00000D45
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002B48 File Offset: 0x00000D48
		public override object GetEditor(Type editorBaseType)
		{
			this.dataSetUITypeEditor = new DataSetUITypeEditor(this.isReadOnly, this.paraTree);
			return this.dataSetUITypeEditor;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002B67 File Offset: 0x00000D67
		public override object GetValue(object component)
		{
			return this.value;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00002B0B File Offset: 0x00000D0B
		public override void ResetValue(object component)
		{
			throw new ExceptionUtility().ThrowHelperError(new InvalidOperationException());
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002B6F File Offset: 0x00000D6F
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002B45 File Offset: 0x00000D45
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x0400002E RID: 46
		private bool isReadOnly;

		// Token: 0x0400002F RID: 47
		private ParameterTreeAdapter paraTree;

		// Token: 0x04000030 RID: 48
		private object value;

		// Token: 0x04000031 RID: 49
		private DataSetUITypeEditor dataSetUITypeEditor;
	}
}
