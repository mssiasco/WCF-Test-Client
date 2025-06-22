using System;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x02000032 RID: 50
	[Serializable]
	internal class BooleanVariable : Variable
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00003662 File Offset: 0x00001862
		internal BooleanVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000366B File Offset: 0x0000186B
		internal override object CreateObject()
		{
			return bool.Parse(this.value);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008604 File Offset: 0x00006804
		internal override void ValidateAndCanonicalize(string input)
		{
			bool flag;
			if (bool.TryParse(input, out flag))
			{
				this.value = input;
				return;
			}
			this.value = null;
		}
	}
}
