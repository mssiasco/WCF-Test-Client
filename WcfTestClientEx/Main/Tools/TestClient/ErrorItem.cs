using System;

namespace Main.Tools.TestClient
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	internal class ErrorItem
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00002BDD File Offset: 0x00000DDD
		internal ErrorItem(string errorMessage, string errorDetail, TestCase testCase)
		{
			this.errorMessage = errorMessage;
			this.errorDetail = errorDetail;
			this.testCase = testCase;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002BFA File Offset: 0x00000DFA
		internal string ErrorDetail
		{
			get
			{
				return this.errorDetail;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002C02 File Offset: 0x00000E02
		internal string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00002C0A File Offset: 0x00000E0A
		internal TestCase TestCase
		{
			get
			{
				return this.testCase;
			}
		}

		// Token: 0x0400003A RID: 58
		private string errorDetail;

		// Token: 0x0400003B RID: 59
		private string errorMessage;

		// Token: 0x0400003C RID: 60
		private TestCase testCase;
	}
}
