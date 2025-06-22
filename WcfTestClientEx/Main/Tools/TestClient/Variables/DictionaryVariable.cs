using System;
using System.Collections.Generic;
using System.Reflection;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x0200003A RID: 58
	[Serializable]
	internal class DictionaryVariable : ContainerVariable
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x00003659 File Offset: 0x00001859
		internal DictionaryVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00008AFC File Offset: 0x00006CFC
		internal static object CreateAndValidateDictionary(string typeName, Variable[] variables, out List<int> invalidList)
		{
			Type type = DataContractAnalyzer.TypesCache[typeName];
			object obj = Activator.CreateInstance(type);
			invalidList = new List<int>();
			if (variables != null)
			{
				MethodInfo method = type.GetMethod("Add");
				if (method == null)
				{
					return null;
				}
				int num = 0;
				foreach (KeyValuePairVariable keyValuePairVariable in variables)
				{
					if (keyValuePairVariable != null && keyValuePairVariable.IsValid)
					{
						object[] array = new object[2];
						Variable[] childVariables = keyValuePairVariable.GetChildVariables();
						array[0] = childVariables[0].CreateObject();
						array[1] = childVariables[1].CreateObject();
						try
						{
							method.Invoke(obj, array);
						}
						catch (TargetInvocationException)
						{
							invalidList.Add(num);
						}
						num++;
					}
				}
			}
			return obj;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00008BC8 File Offset: 0x00006DC8
		internal override object CreateObject()
		{
			if (this.value.Equals("(null)"))
			{
				return null;
			}
			base.GetChildVariables();
			List<int> list = null;
			return DictionaryVariable.CreateAndValidateDictionary(this.currentMember.TypeName, this.childVariables, out list);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00003715 File Offset: 0x00001915
		internal override void ValidateAndCanonicalize(string input)
		{
			base.ValidateAndCanonicalize(input);
			if (this.value != null)
			{
				base.GetChildVariables();
				this.Validate();
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008C0C File Offset: 0x00006E0C
		internal IList<int> ValidateDictionary()
		{
			List<int> list = null;
			DictionaryVariable.CreateAndValidateDictionary(base.TypeName, this.childVariables, out list);
			return list;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008C30 File Offset: 0x00006E30
		private void Validate()
		{
			if (this.childVariables != null)
			{
				foreach (KeyValuePairVariable keyValuePairVariable in this.childVariables)
				{
					keyValuePairVariable.IsValid = true;
				}
				IList<int> list = ServiceExecutor.ValidateDictionary(this, this.serviceMethodInfo.Endpoint.ServiceProject.ClientDomain);
				foreach (int num in list)
				{
					((KeyValuePairVariable)this.childVariables[num]).IsValid = false;
				}
			}
		}
	}
}
