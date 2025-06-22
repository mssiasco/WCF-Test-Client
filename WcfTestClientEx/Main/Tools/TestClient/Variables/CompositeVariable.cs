using System;
using System.Reflection;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x02000035 RID: 53
	[Serializable]
	internal class CompositeVariable : Variable
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00003662 File Offset: 0x00001862
		internal CompositeVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000086C0 File Offset: 0x000068C0
		internal override object CreateObject()
		{
			if (this.value.Equals("(null)"))
			{
				return null;
			}
			base.GetChildVariables();
			Type type = ClientSettings.GetType(this.currentMember.TypeName);
			object obj = Activator.CreateInstance(type);
			foreach (Variable variable in this.childVariables)
			{
				PropertyInfo property = type.GetProperty(variable.Name);
				if (property != null)
				{
					property.SetValue(obj, variable.CreateObject(), null);
				}
				else
				{
					FieldInfo field = type.GetField(variable.Name);
					field.SetValue(obj, variable.CreateObject());
				}
			}
			return obj;
		}
	}
}
