using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Main.Tools.TestClient
{
	// Token: 0x02000030 RID: 48
	internal class Workspace
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00003629 File Offset: 0x00001829
		internal ICollection<ServiceProject> ServiceProjects
		{
			get
			{
				return this.serviceProjects;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008434 File Offset: 0x00006634
		internal ServiceProject AddServiceProject(string endpoint, BackgroundWorker addServiceWorker, float startProgress, float progressRange, out string error)
		{
			ServiceAnalyzer serviceAnalyzer = new ServiceAnalyzer();
			ServiceProject serviceProject = serviceAnalyzer.AnalyzeService(endpoint, addServiceWorker, startProgress, progressRange, out error);
			if (serviceProject == null)
			{
				return null;
			}
			if (addServiceWorker.CancellationPending)
			{
				serviceProject.Remove();
				return null;
			}
			this.serviceProjects.Add(serviceProject);
			error = null;
			return serviceProject;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000847C File Offset: 0x0000667C
		internal void Close()
		{
			foreach (ServiceProject serviceProject in this.serviceProjects)
			{
				serviceProject.Remove();
			}
			this.serviceProjects.Clear();
			if (ApplicationSettings.GetInstance().RegenerateConfigEnabled)
			{
				ConfigFileMappingManager.GetInstance().Clear();
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000084EC File Offset: 0x000066EC
		internal ServiceProject FindServiceProject(string configPath)
		{
			foreach (ServiceProject serviceProject in this.serviceProjects)
			{
				if (string.Equals(configPath, serviceProject.ConfigFile.FileName, StringComparison.OrdinalIgnoreCase))
				{
					return serviceProject;
				}
			}
			return null;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00003631 File Offset: 0x00001831
		internal void Remove(ServiceProject serviceProject)
		{
			this.serviceProjects.Remove(serviceProject);
			serviceProject.Remove();
		}

		// Token: 0x040000B7 RID: 183
		private ICollection<ServiceProject> serviceProjects = new List<ServiceProject>();
	}
}
