using System;
using System.Collections.Generic;
using System.Globalization;
using WcfTestClientEx;

namespace Main.Tools.TestClient
{
	// Token: 0x02000005 RID: 5
	internal class AddServiceOutputs
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000027D6 File Offset: 0x000009D6
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000027DE File Offset: 0x000009DE
		public bool Cancelled
		{
			get
			{
				return this.cancelled;
			}
			set
			{
				this.cancelled = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000027E7 File Offset: 0x000009E7
		internal List<ErrorItem> Errors
		{
			get
			{
				return this.addServiceErrors;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000027EF File Offset: 0x000009EF
		internal List<ServiceProject> ServiceProjects
		{
			get
			{
				return this.serviceProjects;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000027F7 File Offset: 0x000009F7
		internal void AddError(string errorMessage)
		{
			this.addServiceErrors.Add(new ErrorItem(StringResources.ErrorFailedAddingService, errorMessage, null));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002810 File Offset: 0x00000A10
		internal void AddServiceProject(ServiceProject serviceProject)
		{
			this.serviceProjects.Add(serviceProject);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000047AC File Offset: 0x000029AC
		internal string GetStatusMessage()
		{
			if (this.cancelled)
			{
				if (!AddServiceOutputs.IsRefreshing)
				{
					return StringResources.StatusAddServiceCancelled;
				}
				return StringResources.StatusRefreshServiceCancelled;
			}
			else
			{
				int count = this.addServiceErrors.Count;
				int num = this.addServiceErrors.Count + this.succeedCount;
				if (this.addServiceErrors.Count == 0)
				{
					if (!AddServiceOutputs.IsRefreshing)
					{
						return StringResources.StatusAddingServiceCompleted;
					}
					return StringResources.StatusRefreshingServiceCompleted;
				}
				else
				{
					if (count != 1 || num != 1)
					{
						return string.Format(CultureInfo.CurrentCulture, StringResources.StatusAddingMultipleServicesFailed, count, num);
					}
					if (!AddServiceOutputs.IsRefreshing)
					{
						return StringResources.ErrorFailedAddingService;
					}
					return StringResources.ErrorFailedRefreshingService;
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000281E File Offset: 0x00000A1E
		internal void IncrementSucceedCount()
		{
			this.succeedCount++;
		}

		// Token: 0x04000005 RID: 5
		internal static bool IsRefreshing;

		// Token: 0x04000006 RID: 6
		private List<ErrorItem> addServiceErrors = new List<ErrorItem>();

		// Token: 0x04000007 RID: 7
		private bool cancelled;

		// Token: 0x04000008 RID: 8
		private List<ServiceProject> serviceProjects = new List<ServiceProject>();

		// Token: 0x04000009 RID: 9
		private int succeedCount;
	}
}
