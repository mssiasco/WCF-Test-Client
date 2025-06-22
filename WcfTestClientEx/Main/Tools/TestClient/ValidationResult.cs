using System;

namespace Main.Tools.TestClient
{
	// Token: 0x0200002F RID: 47
	internal class ValidationResult
	{
		// Token: 0x060001BD RID: 445 RVA: 0x00003603 File Offset: 0x00001803
		internal ValidationResult(bool valid, bool refreshRequired)
		{
			this.valid = valid;
			this.refreshRequired = refreshRequired;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00003619 File Offset: 0x00001819
		internal bool RefreshRequired
		{
			get
			{
				return this.refreshRequired;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00003621 File Offset: 0x00001821
		internal bool Valid
		{
			get
			{
				return this.valid;
			}
		}

		// Token: 0x040000B5 RID: 181
		private bool refreshRequired;

		// Token: 0x040000B6 RID: 182
		private bool valid;
	}
}
