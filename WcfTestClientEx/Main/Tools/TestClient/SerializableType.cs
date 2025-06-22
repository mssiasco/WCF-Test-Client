using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using WcfTestClientEx;

namespace Main.Tools.TestClient
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	internal class SerializableType
	{
		// Token: 0x0600016A RID: 362 RVA: 0x0000748C File Offset: 0x0000568C
		internal SerializableType(Type type)
		{
			this.typeName = type.FullName;
			if (type.IsEnum)
			{
				this.enumChoices = Enum.GetNames(type);
				this.typeProperty.IsEnum = true;
			}
			else if (SerializableType.numericTypes.Contains(this.typeName))
			{
				this.typeProperty.IsNumeric = true;
			}
			else if (DataContractAnalyzer.IsDataSet(type))
			{
				this.typeProperty.IsDataSet = true;
				DataSet dataSet = Activator.CreateInstance(type) as DataSet;
				this.dataSetSchema = dataSet.GetXmlSchema();
			}
			else if (type.IsArray)
			{
				this.typeProperty.IsArray = true;
			}
			else if (DataContractAnalyzer.IsNullableType(type))
			{
				this.typeProperty.IsNullable = true;
			}
			else if (DataContractAnalyzer.IsCollectionType(type))
			{
				this.typeProperty.IsCollection = true;
			}
			else if (DataContractAnalyzer.IsDictionaryType(type))
			{
				this.typeProperty.IsDictionary = true;
			}
			else if (DataContractAnalyzer.IsKeyValuePairType(type))
			{
				this.typeProperty.IsKeyValuePair = true;
			}
			else if (DataContractAnalyzer.IsSupportedType(type))
			{
				this.typeProperty.IsComposite = true;
				if (type.IsValueType)
				{
					this.typeProperty.IsStruct = true;
				}
			}
			if (SerializableType.numericTypes.Contains(this.typeName) || this.typeName.Equals("System.Char", StringComparison.Ordinal) || this.typeName.Equals("System.Guid", StringComparison.Ordinal) || this.typeName.Equals("System.DateTime", StringComparison.Ordinal) || this.typeName.Equals("System.DateTimeOffset", StringComparison.Ordinal) || this.typeName.Equals("System.TimeSpan", StringComparison.Ordinal))
			{
				this.editorType = EditorType.TextBox;
				return;
			}
			if (this.typeName.Equals("System.String", StringComparison.Ordinal) || this.typeName.Equals("System.Uri", StringComparison.Ordinal) || this.typeName.Equals("System.Xml.XmlQualifiedName", StringComparison.Ordinal) || this.IsContainer())
			{
				this.editorType = EditorType.EditableDropDownBox;
				return;
			}
			if (this.typeName.Equals("System.Boolean", StringComparison.Ordinal) || this.HasMembers() || this.enumChoices != null || this.typeProperty.IsDataSet)
			{
				this.editorType = EditorType.DropDownBox;
				return;
			}
			this.isInvalid = !this.typeName.Equals(typeof(NullObject).FullName, StringComparison.Ordinal);
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00003374 File Offset: 0x00001574
		internal string DataSetSchema
		{
			get
			{
				return this.dataSetSchema;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000337C File Offset: 0x0000157C
		internal EditorType EditorType
		{
			get
			{
				return this.editorType;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00003384 File Offset: 0x00001584
		internal string FriendlyName
		{
			get
			{
				if (this.friendlyName == null)
				{
					this.ComposeFriendlyName();
				}
				return this.friendlyName;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000339A File Offset: 0x0000159A
		internal bool IsInvalid
		{
			get
			{
				return this.isInvalid;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000033A2 File Offset: 0x000015A2
		internal ICollection<TypeMemberInfo> Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000033AA File Offset: 0x000015AA
		internal ICollection<SerializableType> SubTypes
		{
			get
			{
				return this.subTypes;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000033B2 File Offset: 0x000015B2
		internal string TypeName
		{
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000033BA File Offset: 0x000015BA
		internal TypeProperty TypeProperty
		{
			get
			{
				return this.typeProperty;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000033C2 File Offset: 0x000015C2
		internal static bool IsNullRepresentation(string value)
		{
			return string.Equals(value, "(null)", StringComparison.Ordinal);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000076FC File Offset: 0x000058FC
		internal string GetDefaultValue()
		{
			if (this.enumChoices != null)
			{
				return this.enumChoices[0];
			}
			if (SerializableType.numericTypes.Contains(this.typeName))
			{
				return "0";
			}
			if (this.typeName.Equals("System.Boolean", StringComparison.Ordinal))
			{
				return bool.FalseString;
			}
			if (this.typeName.Equals("System.Char", StringComparison.Ordinal))
			{
				return "A";
			}
			if (this.typeName.Equals("System.Guid", StringComparison.Ordinal))
			{
				return Guid.NewGuid().ToString();
			}
			if (this.typeName.Equals("System.DateTime", StringComparison.Ordinal))
			{
				return DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
			}
			if (this.typeName.Equals("System.DateTimeOffset", StringComparison.Ordinal))
			{
				return DateTimeOffset.Now.ToString();
			}
			if (this.typeName.Equals("System.TimeSpan", StringComparison.Ordinal))
			{
				return TimeSpan.Zero.ToString();
			}
			if (this.typeName.Equals("System.Uri", StringComparison.Ordinal))
			{
				return "http://localhost";
			}
			if (this.typeName.Equals("System.Xml.XmlQualifiedName", StringComparison.Ordinal))
			{
				return "namespace:name";
			}
			if (this.IsContainer())
			{
				return "length=0";
			}
			if (this.typeProperty.IsKeyValuePair || this.typeProperty.IsStruct)
			{
				return this.typeName;
			}
			return "(null)";
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007878 File Offset: 0x00005A78
		internal string[] GetSelectionList()
		{
			string[] array;
			if (this.editorType == EditorType.EditableDropDownBox)
			{
				array = new string[] { "(null)" };
			}
			else
			{
				if (this.editorType != EditorType.DropDownBox)
				{
					return null;
				}
				if (this.typeName.Equals("System.Boolean"))
				{
					array = new string[] { "True", "False" };
				}
				else
				{
					if (this.enumChoices != null)
					{
						return this.enumChoices;
					}
					if (this.typeProperty.IsKeyValuePair || this.typeProperty.IsStruct)
					{
						array = new string[] { this.typeName };
					}
					else if (this.typeProperty.IsDataSet)
					{
						array = new string[]
						{
							"(null)",
							StringResources.EditDataSet
						};
					}
					else
					{
						array = new string[0];
					}
				}
			}
			if (array != null && array.Length == 0)
			{
				List<string> list = new List<string>();
				list.Add("(null)");
				list.Add(this.typeName);
				foreach (SerializableType serializableType in this.subTypes)
				{
					if (!serializableType.IsInvalid)
					{
						list.Add(serializableType.TypeName);
					}
				}
				array = new string[list.Count];
				list.CopyTo(array);
			}
			return array;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000079CC File Offset: 0x00005BCC
		internal string GetStringRepresentation(object obj)
		{
			if (obj == null || this.typeName.Equals("Main.Tools.TestClient.NullObject"))
			{
				return "(null)";
			}
			if (this.typeProperty.IsDataSet)
			{
				return StringResources.ViewDataSet;
			}
			if (this.editorType == EditorType.DropDownBox)
			{
				if (obj.GetType().Equals(typeof(bool)) || this.enumChoices != null)
				{
					return obj.ToString();
				}
				return string.Empty;
			}
			else
			{
				if (obj.GetType().IsArray)
				{
					return "length=" + ((Array)obj).Length.ToString();
				}
				if (DataContractAnalyzer.IsDictionaryType(obj.GetType()) || DataContractAnalyzer.IsCollectionType(obj.GetType()))
				{
					return "length=" + ((ICollection)obj).Count.ToString();
				}
				if (obj is string)
				{
					return StringFormatter.ToEscapeCode(obj.ToString());
				}
				return obj.ToString();
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000033D0 File Offset: 0x000015D0
		internal bool HasMembers()
		{
			return this.typeProperty.IsComposite || this.typeProperty.IsNullable || this.typeProperty.IsKeyValuePair;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000033F9 File Offset: 0x000015F9
		internal bool IsContainer()
		{
			return this.typeProperty.IsArray || this.typeProperty.IsDictionary || this.typeProperty.IsCollection;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00003422 File Offset: 0x00001622
		internal void MarkAsInvalid()
		{
			this.isInvalid = true;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007ABC File Offset: 0x00005CBC
		private void ComposeFriendlyName()
		{
			int num = this.TypeName.IndexOf('`');
			if (num > -1)
			{
				StringBuilder stringBuilder = new StringBuilder(this.TypeName.Substring(0, num));
				stringBuilder.Append("<");
				ICollection<TypeMemberInfo> collection = this.members;
				if (this.typeProperty.IsDictionary)
				{
					collection = ((List<TypeMemberInfo>)this.members)[0].Members;
				}
				int num2 = 0;
				foreach (TypeMemberInfo typeMemberInfo in collection)
				{
					if (num2++ > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(typeMemberInfo.FriendlyTypeName);
				}
				stringBuilder.Append(">");
				this.friendlyName = stringBuilder.ToString();
				return;
			}
			this.friendlyName = this.TypeName;
		}

		// Token: 0x04000088 RID: 136
		internal const string LengthRepresentation = "length=";

		// Token: 0x04000089 RID: 137
		internal const string NullRepresentation = "(null)";

		// Token: 0x0400008A RID: 138
		private static List<string> numericTypes = new List<string>(new string[]
		{
			typeof(short).FullName,
			typeof(int).FullName,
			typeof(long).FullName,
			typeof(ushort).FullName,
			typeof(uint).FullName,
			typeof(ulong).FullName,
			typeof(byte).FullName,
			typeof(sbyte).FullName,
			typeof(float).FullName,
			typeof(double).FullName,
			typeof(decimal).FullName
		});

		// Token: 0x0400008B RID: 139
		private string dataSetSchema;

		// Token: 0x0400008C RID: 140
		private EditorType editorType;

		// Token: 0x0400008D RID: 141
		private string[] enumChoices;

		// Token: 0x0400008E RID: 142
		private string friendlyName;

		// Token: 0x0400008F RID: 143
		private bool isInvalid;

		// Token: 0x04000090 RID: 144
		private ICollection<TypeMemberInfo> members = new List<TypeMemberInfo>();

		// Token: 0x04000091 RID: 145
		private ICollection<SerializableType> subTypes = new List<SerializableType>();

		// Token: 0x04000092 RID: 146
		private string typeName;

		// Token: 0x04000093 RID: 147
		private TypeProperty typeProperty = new TypeProperty();
	}
}
