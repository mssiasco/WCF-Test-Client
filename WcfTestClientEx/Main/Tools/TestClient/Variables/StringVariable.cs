using System;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	internal class StringVariable : Variable
	{
		// Token: 0x060001FB RID: 507 RVA: 0x00003662 File Offset: 0x00001862
		internal StringVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000037F1 File Offset: 0x000019F1
		internal override object CreateObject()
		{
			if (this.value.Equals("(null)"))
			{
				return null;
			}
			return StringFormatter.FromEscapeCode(this.value);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00003812 File Offset: 0x00001A12
		internal override void ValidateAndCanonicalize(string input)
		{
			base.ValidateAndCanonicalize(input);
		}
	}
}
