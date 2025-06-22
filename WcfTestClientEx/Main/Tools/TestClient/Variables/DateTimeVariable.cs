using System;
using System.ComponentModel;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x02000039 RID: 57
	[Serializable]
	internal class DateTimeVariable : Variable
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x00003662 File Offset: 0x00001862
		internal DateTimeVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00003703 File Offset: 0x00001903
		internal override object CreateObject()
		{
			return new DateTimeConverter().ConvertFrom(this.value);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008ABC File Offset: 0x00006CBC
		internal override void ValidateAndCanonicalize(string input)
		{
			try
			{
				this.value = new DateTimeConverter().ConvertFrom(input).ToString();
			}
			catch (FormatException)
			{
				this.value = null;
			}
		}
	}
}
