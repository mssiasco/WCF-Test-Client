using System;
using System.Globalization;

namespace Main.Tools.TestClient.Variables
{
	// Token: 0x02000038 RID: 56
	[Serializable]
	internal class DateTimeOffsetVariable : Variable
	{
		// Token: 0x060001DF RID: 479 RVA: 0x00003662 File Offset: 0x00001862
		internal DateTimeOffsetVariable(TypeMemberInfo declaredMember)
			: base(declaredMember)
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000036EC File Offset: 0x000018EC
		internal override object CreateObject()
		{
			return DateTimeOffset.Parse(this.value, CultureInfo.CurrentCulture);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008A78 File Offset: 0x00006C78
		internal override void ValidateAndCanonicalize(string input)
		{
			base.ValidateAndCanonicalize(input);
			if (this.value == null)
			{
				return;
			}
			DateTimeOffset dateTimeOffset;
			if (DateTimeOffset.TryParse(input, out dateTimeOffset))
			{
				this.value = dateTimeOffset.ToString();
				return;
			}
			this.value = null;
		}
	}
}
