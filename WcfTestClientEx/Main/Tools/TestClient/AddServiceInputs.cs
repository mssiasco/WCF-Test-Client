using System;
using System.Collections.Generic;

namespace Main.Tools.TestClient
{
	// Token: 0x02000004 RID: 4
	internal class AddServiceInputs
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000027AB File Offset: 0x000009AB
		internal AddServiceInputs(params string[] endpoints)
		{
			if (endpoints != null)
			{
				this.endpointsCount = endpoints.Length;
			}
			this.endpoints = endpoints;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000027C6 File Offset: 0x000009C6
		public int EndpointsCount
		{
			get
			{
				return this.endpointsCount;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000027CE File Offset: 0x000009CE
		internal IEnumerable<string> Endpoints
		{
			get
			{
				return this.endpoints;
			}
		}

		// Token: 0x04000003 RID: 3
		private IEnumerable<string> endpoints;

		// Token: 0x04000004 RID: 4
		private int endpointsCount;
	}
}
