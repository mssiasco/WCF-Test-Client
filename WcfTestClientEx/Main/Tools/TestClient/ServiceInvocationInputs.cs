using System;
using Main.Tools.TestClient.UI;
using Main.Tools.TestClient.Variables;

namespace Main.Tools.TestClient
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	internal class ServiceInvocationInputs
	{
		// Token: 0x06000139 RID: 313 RVA: 0x0000313B File Offset: 0x0000133B
		internal ServiceInvocationInputs(Variable[] inputs, ServicePage servicePage, bool startNewClient)
			: this(inputs, servicePage.TestCase, startNewClient)
		{
			this.servicePage = servicePage;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000072BC File Offset: 0x000054BC
		internal ServiceInvocationInputs(Variable[] inputs, TestCase testCase, bool startNewClient)
		{
			ServiceMethodInfo method = testCase.Method;
			ClientEndpointInfo endpoint = method.Endpoint;
			ServiceProject serviceProject = endpoint.ServiceProject;
			this.clientTypeName = endpoint.ClientTypeName;
			this.contractTypeName = endpoint.OperationContractTypeName;
			this.endpointConfigurationName = endpoint.EndpointConfigurationName;
			this.proxyIdentifier = endpoint.ProxyIdentifier;
			this.methodName = method.MethodName;
			this.inputs = inputs;
			this.startNewClient = startNewClient;
			if (serviceProject != null)
			{
				this.domain = serviceProject.ClientDomain;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00003152 File Offset: 0x00001352
		internal string ClientTypeName
		{
			get
			{
				return this.clientTypeName;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000315A File Offset: 0x0000135A
		internal string ContractTypeName
		{
			get
			{
				return this.contractTypeName;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00003162 File Offset: 0x00001362
		internal AppDomain Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000316A File Offset: 0x0000136A
		internal string EndpointConfigurationName
		{
			get
			{
				return this.endpointConfigurationName;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00003172 File Offset: 0x00001372
		internal string MethodName
		{
			get
			{
				return this.methodName;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000317A File Offset: 0x0000137A
		internal string ProxyIdentifier
		{
			get
			{
				return this.proxyIdentifier;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00003182 File Offset: 0x00001382
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000318A File Offset: 0x0000138A
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

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00003193 File Offset: 0x00001393
		internal bool StartNewClient
		{
			get
			{
				return this.startNewClient;
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000319B File Offset: 0x0000139B
		internal Variable[] GetInputs()
		{
			return this.inputs;
		}

		// Token: 0x04000070 RID: 112
		private string clientTypeName;

		// Token: 0x04000071 RID: 113
		private string contractTypeName;

		// Token: 0x04000072 RID: 114
		[NonSerialized]
		private AppDomain domain;

		// Token: 0x04000073 RID: 115
		private string endpointConfigurationName;

		// Token: 0x04000074 RID: 116
		private Variable[] inputs;

		// Token: 0x04000075 RID: 117
		private string methodName;

		// Token: 0x04000076 RID: 118
		private string proxyIdentifier;

		// Token: 0x04000077 RID: 119
		[NonSerialized]
		private ServicePage servicePage;

		// Token: 0x04000078 RID: 120
		private bool startNewClient;
	}
}
