using System;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x02000044 RID: 68
	internal class VariableFactory
	{
		// Token: 0x0600021D RID: 541 RVA: 0x00009590 File Offset: 0x00007790
		internal static Variable CreateAssociateVariable(TypeMemberInfo memberInfo)
		{
			if (memberInfo.TypeProperty.IsEnum)
			{
				return new EnumVariable(memberInfo);
			}
			if (memberInfo.TypeProperty.IsNumeric)
			{
				return new NumericVariable(memberInfo);
			}
			if (memberInfo.TypeName.Equals("System.Boolean", StringComparison.Ordinal))
			{
				return new BooleanVariable(memberInfo);
			}
			if (memberInfo.TypeName.Equals("System.Char", StringComparison.Ordinal))
			{
				return new CharVariable(memberInfo);
			}
			if (memberInfo.TypeName.Equals("System.Guid", StringComparison.Ordinal))
			{
				return new GuidVariable(memberInfo);
			}
			if (memberInfo.TypeName.Equals("System.String", StringComparison.Ordinal))
			{
				return new StringVariable(memberInfo);
			}
			if (memberInfo.TypeName.Equals("System.DateTime", StringComparison.Ordinal))
			{
				return new DateTimeVariable(memberInfo);
			}
			if (memberInfo.TypeName.Equals("System.DateTimeOffset", StringComparison.Ordinal))
			{
				return new DateTimeOffsetVariable(memberInfo);
			}
			if (memberInfo.TypeName.Equals("System.TimeSpan", StringComparison.Ordinal))
			{
				return new TimeSpanVariable(memberInfo);
			}
			if (memberInfo.TypeName.Equals("System.Uri", StringComparison.Ordinal))
			{
				return new UriVariable(memberInfo);
			}
			if (memberInfo.TypeName.Equals("System.Xml.XmlQualifiedName", StringComparison.Ordinal))
			{
				return new XmlQualifiedNameVariable(memberInfo);
			}
			if (memberInfo.TypeProperty.IsArray)
			{
				return new ArrayVariable(memberInfo);
			}
			if (memberInfo.TypeProperty.IsCollection)
			{
				return new CollectionVariable(memberInfo);
			}
			if (memberInfo.TypeProperty.IsDictionary)
			{
				return new DictionaryVariable(memberInfo);
			}
			if (memberInfo.TypeProperty.IsNullable)
			{
				return new NullableVariable(memberInfo);
			}
			if (memberInfo.TypeProperty.IsKeyValuePair)
			{
				return new KeyValuePairVariable(memberInfo);
			}
			if (memberInfo.TypeProperty.IsDataSet)
			{
				return new DataSetVariable(memberInfo);
			}
			return new CompositeVariable(memberInfo);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000398F File Offset: 0x00001B8F
		internal static Variable CreateAssociateVariable(TypeMemberInfo memberInfo, object obj)
		{
			if (memberInfo.TypeProperty.IsDataSet)
			{
				return new DataSetVariable(memberInfo, obj);
			}
			return new Variable(memberInfo, obj);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009730 File Offset: 0x00007930
		internal static Variable CreateAssociateVariable(string name, TypeMemberInfo memberInfo)
		{
			Variable variable = VariableFactory.CreateAssociateVariable(memberInfo);
			variable.Name = name;
			return variable;
		}
	}
}
