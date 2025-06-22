using System;
using System.Collections.Generic;
using System.Globalization;

namespace Main.Tools.TestClient
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	internal class ClientEndpointInfo
	{
		// Token: 0x06000077 RID: 119 RVA: 0x000028DB File Offset: 0x00000ADB
		internal ClientEndpointInfo(string operationContractTypeName)
		{
			this.operationContractTypeName = operationContractTypeName;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000028F5 File Offset: 0x00000AF5
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000028FD File Offset: 0x00000AFD
		public string InvalidReason
		{
			get
			{
				return this.invalidReason;
			}
			set
			{
				this.invalidReason = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002906 File Offset: 0x00000B06
		// (set) Token: 0x0600007B RID: 123 RVA: 0x0000290E File Offset: 0x00000B0E
		public bool Valid
		{
			get
			{
				return this.valid;
			}
			set
			{
				this.valid = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002917 File Offset: 0x00000B17
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000291F File Offset: 0x00000B1F
		internal string ClientTypeName
		{
			get
			{
				return this.clientTypeName;
			}
			set
			{
				this.clientTypeName = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002928 File Offset: 0x00000B28
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002930 File Offset: 0x00000B30
		internal string EndpointConfigurationName
		{
			get
			{
				return this.endpointConfigurationName;
			}
			set
			{
				this.endpointConfigurationName = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002939 File Offset: 0x00000B39
		internal IList<ServiceMethodInfo> Methods
		{
			get
			{
				return this.methods;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002941 File Offset: 0x00000B41
		internal string OperationContractTypeName
		{
			get
			{
				return this.operationContractTypeName;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002949 File Offset: 0x00000B49
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002951 File Offset: 0x00000B51
		internal string ProxyIdentifier
		{
			get
			{
				return this.proxyIdentifier;
			}
			set
			{
				this.proxyIdentifier = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000295A File Offset: 0x00000B5A
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002962 File Offset: 0x00000B62
		internal ServiceProject ServiceProject
		{
			get
			{
				return this.serviceProject;
			}
			set
			{
				this.serviceProject = value;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000296B File Offset: 0x00000B6B
		public override string ToString()
		{
			if (this.endpointConfigurationName == null)
			{
				return this.operationContractTypeName;
			}
			return string.Format(CultureInfo.CurrentUICulture, "{0} ({1})", this.OperationContractTypeName, this.endpointConfigurationName);
		}

		// Token: 0x0400001D RID: 29
		private string clientTypeName;

		// Token: 0x0400001E RID: 30
		private string endpointConfigurationName;

		// Token: 0x0400001F RID: 31
		[NonSerialized]
		private string invalidReason;

		// Token: 0x04000020 RID: 32
		private IList<ServiceMethodInfo> methods = new List<ServiceMethodInfo>();

		// Token: 0x04000021 RID: 33
		private string operationContractTypeName;

		// Token: 0x04000022 RID: 34
		private string proxyIdentifier;

		// Token: 0x04000023 RID: 35
		[NonSerialized]
		private ServiceProject serviceProject;

		// Token: 0x04000024 RID: 36
		private bool valid;
	}
}
