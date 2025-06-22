using System;
using System.Collections.Generic;

namespace Main.Tools.TestClient
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	internal class TypeMemberInfo : IComparable
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00003217 File Offset: 0x00001417
		internal TypeMemberInfo(string variableName, SerializableType type)
		{
			this.variableName = variableName;
			this.type = type;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000322D File Offset: 0x0000142D
		internal string DataSetSchema
		{
			get
			{
				return this.type.DataSetSchema;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000323A File Offset: 0x0000143A
		internal EditorType EditorType
		{
			get
			{
				return this.type.EditorType;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00003247 File Offset: 0x00001447
		internal string FriendlyTypeName
		{
			get
			{
				return this.type.FriendlyName;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00003254 File Offset: 0x00001454
		internal bool IsInvalid
		{
			get
			{
				return this.type.IsInvalid;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00003261 File Offset: 0x00001461
		internal ICollection<TypeMemberInfo> Members
		{
			get
			{
				return this.type.Members;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000326E File Offset: 0x0000146E
		internal ICollection<SerializableType> SubTypes
		{
			get
			{
				return this.type.SubTypes;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000327B File Offset: 0x0000147B
		internal string TypeName
		{
			get
			{
				return this.type.TypeName;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00003288 File Offset: 0x00001488
		internal TypeProperty TypeProperty
		{
			get
			{
				return this.type.TypeProperty;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00003295 File Offset: 0x00001495
		internal string VariableName
		{
			get
			{
				return this.variableName;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007340 File Offset: 0x00005540
		public int CompareTo(object obj)
		{
			TypeMemberInfo typeMemberInfo = (TypeMemberInfo)obj;
			return string.Compare(this.variableName, typeMemberInfo.variableName, StringComparison.Ordinal);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000329D File Offset: 0x0000149D
		internal string GetDefaultValue()
		{
			return this.type.GetDefaultValue();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000032AA File Offset: 0x000014AA
		internal string[] GetSelectionList()
		{
			return this.type.GetSelectionList();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000032B7 File Offset: 0x000014B7
		internal string GetStringRepresentation(object obj)
		{
			return this.type.GetStringRepresentation(obj);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000032C5 File Offset: 0x000014C5
		internal bool HasMembers()
		{
			return this.type.HasMembers();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000032D2 File Offset: 0x000014D2
		internal bool IsContainer()
		{
			return this.type.IsContainer();
		}

		// Token: 0x0400007F RID: 127
		private SerializableType type;

		// Token: 0x04000080 RID: 128
		private string variableName;
	}
}
