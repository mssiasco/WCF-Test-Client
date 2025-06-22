using System;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x0200003E RID: 62
	[Serializable]
	internal class NullableVariable : Variable
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x00003662 File Offset: 0x00001862
		internal NullableVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000037A2 File Offset: 0x000019A2
		internal override object CreateObject()
		{
			if (this.value.Equals("(null)"))
			{
				return null;
			}
			base.GetChildVariables();
			return this.childVariables[0].CreateObject();
		}
	}
}
