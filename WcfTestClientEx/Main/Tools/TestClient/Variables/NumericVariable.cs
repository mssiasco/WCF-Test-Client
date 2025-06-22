using System;
using System.Globalization;
using System.Reflection;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	internal class NumericVariable : Variable
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00008DAC File Offset: 0x00006FAC
		internal NumericVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
			this.parseMethod = Type.GetType(this.currentMember.TypeName).GetMethod("Parse", new Type[]
			{
				typeof(string),
				typeof(IFormatProvider)
			});
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000037CC File Offset: 0x000019CC
		internal override object CreateObject()
		{
			return this.parseMethod.Invoke(null, new object[]
			{
				this.value,
				CultureInfo.CurrentUICulture
			});
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008E00 File Offset: 0x00007000
		internal override void ValidateAndCanonicalize(string input)
		{
			Type type = Type.GetType(this.currentMember.TypeName);
			object[] array = new object[2];
			array[0] = input;
			object[] array2 = array;
			MethodInfo method = type.GetMethod("TryParse", new Type[]
			{
				typeof(string),
				Type.GetType(this.currentMember.TypeName + "&")
			});
			bool flag = (bool)method.Invoke(null, array2);
			if (flag)
			{
				this.value = array2[1].ToString();
				return;
			}
			this.value = null;
		}

		// Token: 0x040000BB RID: 187
		private MethodInfo parseMethod;
	}
}
