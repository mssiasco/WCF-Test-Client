using System;
using System.Xml;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x02000045 RID: 69
	[Serializable]
	internal class XmlQualifiedNameVariable : Variable
	{
		// Token: 0x06000221 RID: 545 RVA: 0x00003662 File Offset: 0x00001862
		internal XmlQualifiedNameVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000974C File Offset: 0x0000794C
		internal override object CreateObject()
		{
			if (this.value.Equals("(null)"))
			{
				return null;
			}
			XmlQualifiedName xmlQualifiedName;
			if (!XmlQualifiedNameVariable.TryParseXmlQualifiedName(this.value, out xmlQualifiedName))
			{
				xmlQualifiedName = null;
			}
			return xmlQualifiedName;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009780 File Offset: 0x00007980
		internal override void ValidateAndCanonicalize(string input)
		{
			if (input.Equals("(null)"))
			{
				base.ValidateAndCanonicalize(input);
				return;
			}
			if (this.value == null)
			{
				return;
			}
			XmlQualifiedName xmlQualifiedName;
			if (XmlQualifiedNameVariable.TryParseXmlQualifiedName(input, out xmlQualifiedName))
			{
				this.value = xmlQualifiedName.ToString();
				return;
			}
			this.value = null;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000097CC File Offset: 0x000079CC
		private static bool TryParseXmlQualifiedName(string stringRepresentation, out XmlQualifiedName result)
		{
			int num = stringRepresentation.LastIndexOf(":", StringComparison.Ordinal);
			if (num == -1)
			{
				result = new XmlQualifiedName(stringRepresentation);
				return true;
			}
			string text = stringRepresentation.Substring(0, num);
			string text2 = stringRepresentation.Substring(num + 1);
			if (string.IsNullOrEmpty(text2))
			{
				result = null;
				return false;
			}
			result = new XmlQualifiedName(text2, text);
			return true;
		}
	}
}
