using System;
using System.Globalization;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x02000031 RID: 49
	[Serializable]
	internal class ArrayVariable : ContainerVariable
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00003659 File Offset: 0x00001859
		internal ArrayVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008550 File Offset: 0x00006750
		internal override object CreateObject()
		{
			if (this.value.Equals("(null)"))
			{
				return null;
			}
			Type type = ClientSettings.GetType(this.currentMember.TypeName.Substring(0, this.currentMember.TypeName.Length - 2));
			Array array = Array.CreateInstance(type, int.Parse(this.value.Substring("length=".Length), CultureInfo.CurrentCulture));
			int num = 0;
			base.GetChildVariables();
			if (this.childVariables != null)
			{
				foreach (Variable variable in this.childVariables)
				{
					array.SetValue(variable.CreateObject(), num++);
				}
			}
			return array;
		}
	}
}
