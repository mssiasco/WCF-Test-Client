using System;
using System.Data;
using System.IO;
using System.Xml;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	internal class DataSetVariable : Variable
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x000036AA File Offset: 0x000018AA
		internal DataSetVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
			this.CreateEmptyDataSet(declaredMember.DataSetSchema);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008820 File Offset: 0x00006A20
		internal DataSetVariable(TypeMemberInfo declaredMember, object obj)
			: base(declaredMember, obj)
		{
			this.CreateEmptyDataSet(((DataSet)obj).GetXmlSchema());
			using (StringReader stringReader = new StringReader(((DataSet)obj).GetXml()))
			{
				using (XmlTextReader xmlTextReader = new XmlTextReader(stringReader)
				{
					DtdProcessing = DtdProcessing.Prohibit
				})
				{
					this.dataSetValue.ReadXml(xmlTextReader);
				}
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000036BF File Offset: 0x000018BF
		private bool IsGeneralDataSet
		{
			get
			{
				return string.Equals(this.currentMember.TypeName, "System.Data.DataSet", StringComparison.Ordinal);
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000088A4 File Offset: 0x00006AA4
		internal override Variable Clone()
		{
			DataSetVariable dataSetVariable = new DataSetVariable(this.currentMember);
			if (dataSetVariable.CopyFrom(this))
			{
				return dataSetVariable;
			}
			return null;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000088CC File Offset: 0x00006ACC
		internal override bool CopyFrom(Variable variable)
		{
			if (variable == null || this == variable)
			{
				return false;
			}
			this.dataSetValue.Dispose();
			DataSetVariable dataSetVariable = variable as DataSetVariable;
			this.dataSetValue = dataSetVariable.dataSetValue.Copy();
			return true;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008908 File Offset: 0x00006B08
		internal override object CreateObject()
		{
			if (this.value.Equals("(null)"))
			{
				return null;
			}
			if (this.IsGeneralDataSet)
			{
				return this.dataSetValue;
			}
			Type type = ClientSettings.GetType(this.currentMember.TypeName);
			object obj = Activator.CreateInstance(type);
			using (StringReader stringReader = new StringReader(this.dataSetValue.GetXml()))
			{
				using (XmlTextReader xmlTextReader = new XmlTextReader(stringReader)
				{
					DtdProcessing = DtdProcessing.Prohibit
				})
				{
					((DataSet)obj).ReadXml(xmlTextReader);
				}
			}
			return obj;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000036D7 File Offset: 0x000018D7
		internal object GetDataSetValue()
		{
			return this.dataSetValue;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000036DF File Offset: 0x000018DF
		internal string GetXmlSchema()
		{
			return this.dataSetValue.GetXmlSchema();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000089B0 File Offset: 0x00006BB0
		internal bool IsDefaultDataSet()
		{
			DataSet dataSet = new DataSet(this.dataSetValue.DataSetName);
			DataSetVariable dataSetVariable = new DataSetVariable(this.currentMember, dataSet);
			return this.SchemaEquals(dataSetVariable);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000089E4 File Offset: 0x00006BE4
		internal bool SchemaEquals(Variable variable)
		{
			DataSetVariable dataSetVariable = variable as DataSetVariable;
			return string.Equals(this.GetXmlSchema(), dataSetVariable.GetXmlSchema(), StringComparison.Ordinal);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00008A0C File Offset: 0x00006C0C
		private void CreateEmptyDataSet(string schema)
		{
			this.dataSetValue = new DataSet();
			using (StringReader stringReader = new StringReader(schema))
			{
				XmlTextReader xmlTextReader = new XmlTextReader(stringReader)
				{
					DtdProcessing = DtdProcessing.Prohibit
				};
				this.dataSetValue.ReadXmlSchema(xmlTextReader);
			}
			this.dataSetValue.Locale = this.dataSetValue.Locale;
		}

		// Token: 0x040000B8 RID: 184
		private DataSet dataSetValue;
	}
}
