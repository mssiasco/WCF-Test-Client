using System;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	internal class UriVariable : Variable
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00003662 File Offset: 0x00001862
		internal UriVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000382D File Offset: 0x00001A2D
		internal override object CreateObject()
		{
			if (this.value.Equals("(null)"))
			{
				return null;
			}
			return new Uri(this.value);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00008ED8 File Offset: 0x000070D8
		internal override void ValidateAndCanonicalize(string input)
		{
			if (input.Equals("(null)"))
			{
				base.ValidateAndCanonicalize(input);
				return;
			}
			Uri uri;
			if (Uri.TryCreate(input, UriKind.Absolute, out uri))
			{
				this.value = uri.ToString();
				return;
			}
			this.value = null;
		}
	}
}
