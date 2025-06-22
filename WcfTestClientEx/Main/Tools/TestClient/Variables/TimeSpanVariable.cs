using System;
using System.ComponentModel;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	internal class TimeSpanVariable : Variable
	{
		// Token: 0x060001FE RID: 510 RVA: 0x00003662 File Offset: 0x00001862
		internal TimeSpanVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000381B File Offset: 0x00001A1B
		internal override object CreateObject()
		{
			return new TimeSpanConverter().ConvertFrom(this.value);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008E8C File Offset: 0x0000708C
		internal override void ValidateAndCanonicalize(string input)
		{
			try
			{
				this.value = new TimeSpanConverter().ConvertFrom(input).ToString();
			}
			catch (FormatException)
			{
				this.value = null;
				return;
			}
			base.ValidateAndCanonicalize(this.value);
		}
	}
}
