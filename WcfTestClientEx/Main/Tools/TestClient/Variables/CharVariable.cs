using System;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	internal class CharVariable : Variable
	{
		// Token: 0x060001CB RID: 459 RVA: 0x00003662 File Offset: 0x00001862
		internal CharVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000367D File Offset: 0x0000187D
		internal override object CreateObject()
		{
			return this.value[0];
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00003690 File Offset: 0x00001890
		internal override void ValidateAndCanonicalize(string input)
		{
			if (input.Length == 1)
			{
				this.value = input;
				return;
			}
			this.value = null;
		}
	}
}
