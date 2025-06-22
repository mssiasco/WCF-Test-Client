using System;
using System.Reflection;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	internal class CollectionVariable : ContainerVariable
	{
		// Token: 0x060001CE RID: 462 RVA: 0x00003659 File Offset: 0x00001859
		internal CollectionVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000862C File Offset: 0x0000682C
		internal override object CreateObject()
		{
			if (this.value.Equals("(null)"))
			{
				return null;
			}
			base.GetChildVariables();
			Type type = ClientSettings.GetType(this.currentMember.TypeName);
			object obj = Activator.CreateInstance(type);
			if (this.childVariables != null)
			{
				MethodInfo method = type.GetMethod("Add");
				foreach (Variable variable in this.childVariables)
				{
					method.Invoke(obj, new object[] { variable.CreateObject() });
				}
			}
			return obj;
		}
	}
}
