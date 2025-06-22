using System;
using Main.Tools.TestClient.UI;

namespace Main.Tools.TestClient
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	internal class TestCase
	{
		// Token: 0x06000196 RID: 406 RVA: 0x000034C3 File Offset: 0x000016C3
		internal TestCase(ServiceMethodInfo method)
		{
			this.method = method;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000197 RID: 407 RVA: 0x000034D2 File Offset: 0x000016D2
		// (remove) Token: 0x06000198 RID: 408 RVA: 0x000034DB File Offset: 0x000016DB
		internal event TestCase.ErrorHandler OnErrorReported
		{
			add
			{
				this.onErrorReported += value;
			}
			remove
			{
				this.onErrorReported -= value;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000199 RID: 409 RVA: 0x000082A8 File Offset: 0x000064A8
		// (remove) Token: 0x0600019A RID: 410 RVA: 0x000082E0 File Offset: 0x000064E0
		private event TestCase.ErrorHandler onErrorReported;

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000034E4 File Offset: 0x000016E4
		internal ServiceMethodInfo Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000034EC File Offset: 0x000016EC
		// (set) Token: 0x0600019D RID: 413 RVA: 0x000034F4 File Offset: 0x000016F4
		internal ServicePage ServicePage
		{
			get
			{
				return this.servicePage;
			}
			set
			{
				this.servicePage = value;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000034FD File Offset: 0x000016FD
		internal void Remove()
		{
			this.method.TestCases.Remove(this);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00003511 File Offset: 0x00001711
		internal void SetError(ErrorItem errorItem)
		{
			if (this.onErrorReported != null)
			{
				this.onErrorReported(errorItem);
			}
			this.error = errorItem;
		}

		// Token: 0x040000A1 RID: 161
		private ErrorItem error;

		// Token: 0x040000A2 RID: 162
		private ServiceMethodInfo method;

		// Token: 0x040000A3 RID: 163
		[NonSerialized]
		private ServicePage servicePage;

		// Token: 0x0200002C RID: 44
		// (Invoke) Token: 0x060001A1 RID: 417
		internal delegate void ErrorHandler(ErrorItem errorItem);
	}
}
