using System;
using Main.Tools.TestClient.UI;
using Main.Tools.TestClient.Variables;

namespace Main.Tools.TestClient
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	internal class ServiceInvocationOutputs
	{
		// Token: 0x06000145 RID: 325 RVA: 0x000031A3 File Offset: 0x000013A3
		internal ServiceInvocationOutputs(Variable[] serviceInvocationResult, string responseXml)
		{
			this.serviceInvocationResult = serviceInvocationResult;
			this.responseXml = responseXml;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000031B9 File Offset: 0x000013B9
		internal ServiceInvocationOutputs(ExceptionType exceptionType, string[] exceptionMessages, string[] exceptionStacks, string responseXml)
		{
			this.exceptionType = exceptionType;
			this.exceptionMessages = exceptionMessages;
			this.exceptionStacks = exceptionStacks;
			this.responseXml = responseXml;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000031DE File Offset: 0x000013DE
		internal string[] ExceptionMessages
		{
			get
			{
				return this.exceptionMessages;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000031E6 File Offset: 0x000013E6
		internal string[] ExceptionStacks
		{
			get
			{
				return this.exceptionStacks;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000031EE File Offset: 0x000013EE
		internal ExceptionType ExceptionType
		{
			get
			{
				return this.exceptionType;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000031F6 File Offset: 0x000013F6
		internal string ResponseXml
		{
			get
			{
				return this.responseXml;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000031FE File Offset: 0x000013FE
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00003206 File Offset: 0x00001406
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

		// Token: 0x0600014D RID: 333 RVA: 0x0000320F File Offset: 0x0000140F
		internal Variable[] GetServiceInvocationResult()
		{
			return this.serviceInvocationResult;
		}

		// Token: 0x04000079 RID: 121
		private string[] exceptionMessages;

		// Token: 0x0400007A RID: 122
		private string[] exceptionStacks;

		// Token: 0x0400007B RID: 123
		private ExceptionType exceptionType;

		// Token: 0x0400007C RID: 124
		private string responseXml;

		// Token: 0x0400007D RID: 125
		private Variable[] serviceInvocationResult;

		// Token: 0x0400007E RID: 126
		[NonSerialized]
		private ServicePage servicePage;
	}
}
