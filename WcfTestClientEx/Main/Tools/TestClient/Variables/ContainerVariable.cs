using System;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x02000036 RID: 54
	[Serializable]
	internal class ContainerVariable : Variable
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00003662 File Offset: 0x00001862
		internal ContainerVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00008764 File Offset: 0x00006964
		internal override void ValidateAndCanonicalize(string input)
		{
			base.ValidateAndCanonicalize(input);
			int num = -1;
			if (this.value == null || input.Equals("(null)"))
			{
				return;
			}
			if (!input.TrimStart(new char[] { ' ' }).StartsWith("length", StringComparison.OrdinalIgnoreCase))
			{
				this.value = null;
				return;
			}
			this.value = input.Replace(" ", "");
			if (this.value.StartsWith("length=", StringComparison.OrdinalIgnoreCase))
			{
				input = this.value.Substring("length=".Length);
				if (int.TryParse(input, out num) && num >= 0)
				{
					this.value = "length=" + input;
					return;
				}
			}
			this.value = null;
		}
	}
}
