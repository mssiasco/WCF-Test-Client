using System;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	internal class GuidVariable : Variable
	{
		// Token: 0x060001ED RID: 493 RVA: 0x00003662 File Offset: 0x00001862
		internal GuidVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00003750 File Offset: 0x00001950
		internal override object CreateObject()
		{
			return new Guid(this.value);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008CD4 File Offset: 0x00006ED4
		internal override void ValidateAndCanonicalize(string input)
		{
			try
			{
				this.value = new Guid(input).ToString();
			}
			catch (FormatException)
			{
				this.value = null;
				return;
			}
			catch (OverflowException)
			{
				this.value = null;
				return;
			}
			catch (ArgumentException)
			{
				this.value = null;
				return;
			}
			base.ValidateAndCanonicalize(this.value);
		}
	}
}
