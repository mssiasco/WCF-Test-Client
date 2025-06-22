using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.ServiceModel.Configuration;
using System.Windows.Forms;
using WcfTestClientEx;

namespace Main.Tools.TestClient
{
	// Token: 0x02000029 RID: 41
	internal class ServiceProject
	{
		// Token: 0x0600017C RID: 380 RVA: 0x00007C94 File Offset: 0x00005E94
		internal ServiceProject(string address, string projectDirectory, string configPath, string assemblyPath, string proxyPath, ICollection<ClientEndpointInfo> endpoints, AppDomain clientDomain)
		{
			this.address = address;
			this.projectDirectory = projectDirectory;
			this.configFile = new FileItem(configPath);
			this.proxyFile = new FileItem(proxyPath);
			this.endpoints = endpoints;
			this.clientDomain = clientDomain;
			this.assemblyPath = assemblyPath;
			this.fileStream = File.Open(assemblyPath, FileMode.Open, FileAccess.Read, FileShare.Read);
			this.foldName = Path.GetFileName(this.projectDirectory);
			this.UpdateAndValidateEndpointsInfo();
			if (!ApplicationSettings.GetInstance().RegenerateConfigEnabled)
			{
				this.CreateProxiesForEndpoints();
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000342B File Offset: 0x0000162B
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00003433 File Offset: 0x00001633
		public bool IsConfigChanged
		{
			get
			{
				return this.isConfigChanged;
			}
			set
			{
				this.isConfigChanged = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000343C File Offset: 0x0000163C
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00003444 File Offset: 0x00001644
		public bool IsWorking
		{
			get
			{
				return this.isWorking;
			}
			set
			{
				this.isWorking = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000344D File Offset: 0x0000164D
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00003455 File Offset: 0x00001655
		public TreeNode ServiceProjectNode
		{
			get
			{
				return this.serviceProjectNode;
			}
			set
			{
				this.serviceProjectNode = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000345E File Offset: 0x0000165E
		internal string Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00003466 File Offset: 0x00001666
		internal AppDomain ClientDomain
		{
			get
			{
				return this.clientDomain;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000346E File Offset: 0x0000166E
		internal FileItem ConfigFile
		{
			get
			{
				return this.configFile;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00003476 File Offset: 0x00001676
		internal ICollection<ClientEndpointInfo> Endpoints
		{
			get
			{
				return this.endpoints;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000347E File Offset: 0x0000167E
		internal FileItem ProxyFile
		{
			get
			{
				return this.proxyFile;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00007D20 File Offset: 0x00005F20
		internal List<FileItem> ReferencedFiles
		{
			get
			{
				return new List<FileItem> { this.ConfigFile, this.ProxyFile };
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00007D4C File Offset: 0x00005F4C
		internal List<TestCase> ReferencedTestCases
		{
			get
			{
				List<TestCase> list = new List<TestCase>();
				foreach (ClientEndpointInfo clientEndpointInfo in this.endpoints)
				{
					foreach (ServiceMethodInfo serviceMethodInfo in clientEndpointInfo.Methods)
					{
						list.AddRange(serviceMethodInfo.TestCases);
					}
				}
				return list;
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007DDC File Offset: 0x00005FDC
		internal bool RefreshConfig(out List<ErrorItem> errorList)
		{
			IDictionary<ChannelEndpointElement, ClientEndpointInfo> dictionary = new Dictionary<ChannelEndpointElement, ClientEndpointInfo>();
			string empty = string.Empty;
			ServiceModelSectionGroup serviceModelSectionGroup = ServiceAnalyzer.AnalyzeConfig(dictionary, this.configFile.FileName, ref empty);
			if (serviceModelSectionGroup == null)
			{
				errorList = new List<ErrorItem>();
				errorList.Add(new ErrorItem(StringResources.InvalidConfig, empty, null));
				return false;
			}
			this.UnloadAppDomain();
			this.clientDomain = ServiceAnalyzer.AnalyzeProxy(dictionary, serviceModelSectionGroup, this.configFile.FileName, this.assemblyPath);
			this.endpoints = dictionary.Values;
			this.UpdateAndValidateEndpointsInfo();
			this.ConfigFile.FilePage.RefreshFile(this.ConfigFile.FileName);
			errorList = this.CreateProxiesForEndpoints();
			ConfigFileMappingManager instance = ConfigFileMappingManager.GetInstance();
			instance.AddConfigFileMapping(this.address);
			ServiceAnalyzer.CopyConfigFile(this.configFile.FileName, instance.GetSavedConfigPath(this.address));
			return true;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00003486 File Offset: 0x00001686
		internal void Remove()
		{
			this.UnloadAppDomain();
			this.DeleteProjectFolder();
			if (ApplicationSettings.GetInstance().RegenerateConfigEnabled)
			{
				ConfigFileMappingManager.GetInstance().DeleteConfigFileMapping(this.address);
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007EB0 File Offset: 0x000060B0
		internal void RestoreDefaultConfig(out string errorMessage)
		{
			errorMessage = null;
			string text = Path.Combine(this.projectDirectory, "default.config");
			if (!File.Exists(text))
			{
				errorMessage = StringResources.DefaultConfigNotFoundDetail;
			}
			ServiceAnalyzer.CopyConfigFile(text, this.configFile.FileName);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007EF4 File Offset: 0x000060F4
		internal bool StartSvcConfigEditor(out string errorMessage)
		{
			errorMessage = null;
			if (ToolingEnvironment.SvcConfigEditorPath == null)
			{
				errorMessage = StringResources.SvcConfigEditorNotFound;
				return false;
			}
			ProcessStartInfo processStartInfo = new ProcessStartInfo(ToolingEnvironment.SvcConfigEditorPath, string.Format(CultureInfo.CurrentUICulture, "\"{0}\"", this.configFile.FileName));
			processStartInfo.UseShellExecute = false;
			try
			{
				Process.Start(processStartInfo);
			}
			catch (Win32Exception ex)
			{
				errorMessage = ex.Message;
				return false;
			}
			return true;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007F6C File Offset: 0x0000616C
		private List<ErrorItem> CreateProxiesForEndpoints()
		{
			ServiceExecutor serviceExecutor = (ServiceExecutor)this.clientDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(ServiceExecutor).FullName);
			List<ErrorItem> list = new List<ErrorItem>();
			foreach (ClientEndpointInfo clientEndpointInfo in this.endpoints)
			{
				if (clientEndpointInfo.Valid)
				{
					ServiceInvocationOutputs serviceInvocationOutputs = serviceExecutor.ConstructClientToCache(clientEndpointInfo.ProxyIdentifier, clientEndpointInfo.ClientTypeName, clientEndpointInfo.EndpointConfigurationName);
					if (serviceInvocationOutputs != null)
					{
						clientEndpointInfo.Valid = false;
						clientEndpointInfo.InvalidReason = StringResources.EndpointError;
						list.Add(new ErrorItem(StringResources.EndpointError, serviceInvocationOutputs.ExceptionMessages[0], null));
					}
				}
			}
			return list;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00008038 File Offset: 0x00006238
		private void DeleteProjectFolder()
		{
			try
			{
				this.fileStream.Close();
				Directory.Delete(this.projectDirectory, true);
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008080 File Offset: 0x00006280
		private void DeleteProxiesForEndpoints()
		{
			ServiceExecutor serviceExecutor = (ServiceExecutor)this.clientDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(ServiceExecutor).FullName);
			foreach (ClientEndpointInfo clientEndpointInfo in this.endpoints)
			{
				if (clientEndpointInfo.Valid)
				{
					serviceExecutor.DeleteClient(clientEndpointInfo.ProxyIdentifier);
				}
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000034B0 File Offset: 0x000016B0
		private void UnloadAppDomain()
		{
			this.DeleteProxiesForEndpoints();
			ServiceAnalyzer.UnloadAppDomain(this.clientDomain);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008104 File Offset: 0x00006304
		private void UpdateAndValidateEndpointsInfo()
		{
			ClientEndpointInfo clientEndpointInfo = null;
			foreach (ClientEndpointInfo clientEndpointInfo2 in this.endpoints)
			{
				clientEndpointInfo2.ProxyIdentifier = this.foldName + clientEndpointInfo2.EndpointConfigurationName;
				if (clientEndpointInfo2.Methods.Count < 1)
				{
					clientEndpointInfo2.InvalidReason = string.Format(CultureInfo.CurrentUICulture, StringResources.InvalidContract, clientEndpointInfo2.OperationContractTypeName);
					clientEndpointInfo = clientEndpointInfo2;
				}
				else
				{
					clientEndpointInfo2.ServiceProject = this;
				}
			}
			if (clientEndpointInfo != null)
			{
				RtlAwareMessageBox.Show(string.Format(CultureInfo.CurrentUICulture, "{0}\n{1}", clientEndpointInfo.InvalidReason, StringResources.InvalidContractNameAction), StringResources.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
			}
		}

		// Token: 0x04000094 RID: 148
		private const string CommandLineFormatString = "\"{0}\"";

		// Token: 0x04000095 RID: 149
		private string address;

		// Token: 0x04000096 RID: 150
		private string assemblyPath;

		// Token: 0x04000097 RID: 151
		private AppDomain clientDomain;

		// Token: 0x04000098 RID: 152
		private FileItem configFile;

		// Token: 0x04000099 RID: 153
		private ICollection<ClientEndpointInfo> endpoints;

		// Token: 0x0400009A RID: 154
		private FileStream fileStream;

		// Token: 0x0400009B RID: 155
		private string foldName;

		// Token: 0x0400009C RID: 156
		private bool isConfigChanged;

		// Token: 0x0400009D RID: 157
		private bool isWorking;

		// Token: 0x0400009E RID: 158
		private string projectDirectory;

		// Token: 0x0400009F RID: 159
		private FileItem proxyFile;

		// Token: 0x040000A0 RID: 160
		private TreeNode serviceProjectNode;
	}
}
