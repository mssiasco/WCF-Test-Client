using System;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x0200003B RID: 59
	[Serializable]
	internal class EnumVariable : Variable
	{
		// Token: 0x060001EB RID: 491 RVA: 0x00003662 File Offset: 0x00001862
		internal EnumVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00003733 File Offset: 0x00001933
		internal override object CreateObject()
		{
			return Enum.Parse(ClientSettings.GetType(this.currentMember.TypeName), this.value);
		}
	}
}
