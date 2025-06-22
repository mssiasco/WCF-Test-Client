using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Threading;
using System.Xml;
using Microsoft.CSharp;
using WcfTestClientEx;

namespace Main.Tools.TestClient
{
	// Token: 0x0200001F RID: 31
	internal class ServiceAnalyzer
	{
		// Token: 0x0600010A RID: 266 RVA: 0x000063BC File Offset: 0x000045BC
		internal static ServiceModelSectionGroup AnalyzeConfig(IDictionary<ChannelEndpointElement, ClientEndpointInfo> services, string configPath, ref string errorMessage)
		{
			ServiceModelSectionGroup serviceModelSectionGroup;
			try
			{
				ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap();
				Configuration configuration = ConfigurationManager.OpenMachineConfiguration();
				exeConfigurationFileMap.MachineConfigFilename = configuration.FilePath;
				exeConfigurationFileMap.ExeConfigFilename = configPath;
				Configuration configuration2 = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);
				ServiceModelSectionGroup sectionGroup = ServiceModelSectionGroup.GetSectionGroup(configuration2);
				foreach (object obj in sectionGroup.Client.Endpoints)
				{
					ChannelEndpointElement channelEndpointElement = (ChannelEndpointElement)obj;
					services.Add(channelEndpointElement, new ClientEndpointInfo(channelEndpointElement.Contract));
				}
				serviceModelSectionGroup = sectionGroup;
			}
			catch (ConfigurationErrorsException ex)
			{
				errorMessage += ex.Message;
				serviceModelSectionGroup = null;
			}
			return serviceModelSectionGroup;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006488 File Offset: 0x00004688
		internal static AppDomain AnalyzeProxy(IDictionary<ChannelEndpointElement, ClientEndpointInfo> services, ServiceModelSectionGroup configObject, string configPath, string assemblyPath)
		{
			AppDomain appDomain = ServiceAnalyzer.CreateAppDomain(configPath, assemblyPath);
			DataContractAnalyzer dataContractAnalyzer = (DataContractAnalyzer)appDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(DataContractAnalyzer).FullName);
			foreach (object obj in configObject.Client.Endpoints)
			{
				ChannelEndpointElement channelEndpointElement = (ChannelEndpointElement)obj;
				ClientEndpointInfo clientEndpointInfo = services[channelEndpointElement];
				clientEndpointInfo = dataContractAnalyzer.AnalyzeDataContract(clientEndpointInfo);
				if (clientEndpointInfo == null)
				{
					services.Remove(channelEndpointElement);
				}
				else
				{
					services[channelEndpointElement] = clientEndpointInfo;
				}
			}
			foreach (KeyValuePair<ChannelEndpointElement, ClientEndpointInfo> keyValuePair in services)
			{
				keyValuePair.Value.EndpointConfigurationName = keyValuePair.Key.Name;
			}
			return appDomain;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006588 File Offset: 0x00004788
		internal static void CopyConfigFile(string oldPath, string newPath)
		{
			try
			{
				File.Copy(oldPath, newPath, true);
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000065C4 File Offset: 0x000047C4
		internal static void DeleteProjectFolder(string projectDirectory)
		{
			try
			{
				Directory.Delete(projectDirectory, true);
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000065FC File Offset: 0x000047FC
		internal static void UnloadAppDomain(AppDomain clientDomain)
		{
			try
			{
				AppDomain.Unload(clientDomain);
			}
			catch (CannotUnloadAppDomainException)
			{
			}
			clientDomain = null;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006628 File Offset: 0x00004828
		internal ServiceProject AnalyzeService(string address, BackgroundWorker addServiceWorker, float startProgress, float progressRange, out string errorMessage)
		{
			errorMessage = string.Empty;
			IDictionary<ChannelEndpointElement, ClientEndpointInfo> dictionary = new Dictionary<ChannelEndpointElement, ClientEndpointInfo>();
			string projectBase = ApplicationSettings.GetInstance().ProjectBase;
			string text = Path.Combine(projectBase, Guid.NewGuid().ToString());
			string text2 = Path.Combine(text, "Client.dll.config");
			string text3 = Path.Combine(text, "Client.cs");
			if (!ServiceAnalyzer.GenerateProxyAndConfig(text, address, text2, text3, this.GetIntValueOfProgress(startProgress, progressRange, 0.1f), this.GetIntValueOfProgress(startProgress, progressRange, 0.5f), addServiceWorker, out errorMessage))
			{
				ServiceAnalyzer.DeleteProjectFolder(text);
				return null;
			}
			ServiceModelSectionGroup serviceModelSectionGroup = ServiceAnalyzer.AnalyzeConfig(dictionary, text2, ref errorMessage);
			if (serviceModelSectionGroup == null || this.CancelOrReportProgress(addServiceWorker, null, this.GetIntValueOfProgress(startProgress, progressRange, 0.7f), text))
			{
				return null;
			}
			string text4;
			AppDomain appDomain = this.AnalyzeProxy(dictionary, text, text3, serviceModelSectionGroup, text2, ref errorMessage, out text4);
			if (appDomain == null || this.CancelOrReportProgress(addServiceWorker, appDomain, this.GetIntValueOfProgress(startProgress, progressRange, 0.9f), text))
			{
				return null;
			}
			return new ServiceProject(address, text, text2, text4, text3, dictionary.Values, appDomain);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006728 File Offset: 0x00004928
		private static AppDomain CreateAppDomain(string configPath, string clientAssemblyPath)
		{
			AppDomainSetup appDomainSetup = new AppDomainSetup();
			appDomainSetup.ConfigurationFile = configPath;
			appDomainSetup.ApplicationBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			AppDomain appDomain = AppDomain.CreateDomain(configPath, AppDomain.CurrentDomain.Evidence, appDomainSetup);
			appDomain.SetData("clientAssemblyPath", clientAssemblyPath);
			return appDomain;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006778 File Offset: 0x00004978
		private static bool GenerateProxyAndConfig(string projectPath, string address, string configPath, string proxyPath, int startProgressPosition, int endProgressPostition, BackgroundWorker addServiceWorker, out string errorMessage)
		{
			string metadataTool = ToolingEnvironment.MetadataTool;
			string text = Path.Combine(projectPath, "default.config");
			if (!File.Exists(metadataTool))
			{
				errorMessage = string.Format(CultureInfo.CurrentUICulture, StringResources.ErrorMetadataToolNotFound, metadataTool);
				return false;
			}
			int num = 0;
			Process process;
			bool flag;
			for (;;)
			{
				process = ServiceAnalyzer.StartSvcutil(metadataTool, address, proxyPath, text);
				while (!process.HasExited)
				{
					if (addServiceWorker.CancellationPending)
					{
						goto Block_2;
					}
					if (startProgressPosition < endProgressPostition)
					{
						addServiceWorker.ReportProgress(startProgressPosition++);
					}
					Thread.Sleep(50);
				}
				process.Dispose();
				if (!File.Exists(proxyPath) || !File.Exists(text))
				{
					flag = false;
					while (!ServiceAnalyzer.isErrorStreamClosed)
					{
						Thread.Sleep(50);
					}
				}
				else
				{
					flag = true;
				}
				errorMessage = ServiceAnalyzer.svcutilError;
				if (flag || num++ >= 1)
				{
					goto IL_00D3;
				}
			}
			Block_2:
			if (!process.HasExited)
			{
				process.Kill();
				process.Dispose();
			}
			errorMessage = string.Empty;
			return false;
			IL_00D3:
			if (flag)
			{
				if (ApplicationSettings.GetInstance().RegenerateConfigEnabled || !ConfigFileMappingManager.GetInstance().DoesConfigMappingExist(address))
				{
					ServiceAnalyzer.CopyConfigFile(text, configPath);
				}
				else
				{
					ServiceAnalyzer.CopyConfigFile(ConfigFileMappingManager.GetInstance().GetSavedConfigPath(address), configPath);
				}
				ServiceAnalyzer.SetServiceInvocationTimeout(configPath);
			}
			return flag;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006898 File Offset: 0x00004A98
		private static void SetServiceInvocationTimeout(string configPath)
		{
			try
			{
				ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap();
				Configuration configuration = ConfigurationManager.OpenMachineConfiguration();
				exeConfigurationFileMap.MachineConfigFilename = configuration.FilePath;
				exeConfigurationFileMap.ExeConfigFilename = configPath;
				Configuration configuration2 = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);
				ServiceModelSectionGroup sectionGroup = ServiceModelSectionGroup.GetSectionGroup(configuration2);
				foreach (BindingCollectionElement bindingCollectionElement in sectionGroup.Bindings.BindingCollections)
				{
					foreach (IBindingConfigurationElement bindingConfigurationElement in bindingCollectionElement.ConfiguredBindings)
					{
						if (bindingConfigurationElement.SendTimeout.CompareTo(ServiceAnalyzer.defaultSvcInvocationTimeout) < 0)
						{
							Type type = bindingConfigurationElement.GetType();
							if (ServiceAnalyzer.customBindingType.IsAssignableFrom(type))
							{
								(bindingConfigurationElement as CustomBindingElement).SendTimeout = ServiceAnalyzer.defaultSvcInvocationTimeout;
							}
							else if (ServiceAnalyzer.standardBindingType.IsAssignableFrom(type))
							{
								(bindingConfigurationElement as StandardBindingElement).SendTimeout = ServiceAnalyzer.defaultSvcInvocationTimeout;
							}
						}
					}
				}
				configuration2.Save();
			}
			catch (ConfigurationErrorsException)
			{
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000069F8 File Offset: 0x00004BF8
		private static Process StartSvcutil(string svcutilPath, string address, string proxyPath, string defaultConfigPath)
		{
			Process process = new Process();
			ProcessStartInfo processStartInfo = new ProcessStartInfo(svcutilPath);
			processStartInfo.UseShellExecute = false;
			processStartInfo.RedirectStandardError = true;
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.CreateNoWindow = true;
			processStartInfo.Arguments = string.Concat(new string[]
			{
				"/targetClientVersion:Version35 /r:\"",
				typeof(DataSet).Assembly.Location,
				"\" \"",
				address,
				"\" \"/out:",
				proxyPath,
				"\" \"/config:",
				defaultConfigPath,
				"\""
			});
			if (svcutilPath.EndsWith("v8.0A\\bin\\NETFX 4.0 Tools\\SvcUtil.exe", StringComparison.OrdinalIgnoreCase))
			{
				processStartInfo.Arguments = "/syncOnly " + processStartInfo.Arguments;
			}
			process.StartInfo = processStartInfo;
			process.ErrorDataReceived += ServiceAnalyzer.Svcutil_ErrorDataReceived;
			ServiceAnalyzer.svcutilError = string.Empty;
			ServiceAnalyzer.isErrorStreamClosed = false;
			process.Start();
			process.BeginErrorReadLine();
			return process;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00002FB3 File Offset: 0x000011B3
		private static void Svcutil_ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (e.Data == null)
			{
				ServiceAnalyzer.isErrorStreamClosed = true;
				return;
			}
			ServiceAnalyzer.svcutilError += e.Data;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006AE8 File Offset: 0x00004CE8
		private AppDomain AnalyzeProxy(IDictionary<ChannelEndpointElement, ClientEndpointInfo> services, string projectPath, string proxyPath, ServiceModelSectionGroup configObject, string configPath, ref string errorMessage, out string assemblyPath)
		{
			assemblyPath = null;
			CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider();
			CompilerResults compilerResults = csharpCodeProvider.CompileAssemblyFromFile(new CompilerParameters
			{
				OutputAssembly = Path.Combine(projectPath, "Client.dll"),
				ReferencedAssemblies = 
				{
					"System.dll",
					typeof(DataSet).Assembly.Location,
					typeof(TypedTableBaseExtensions).Assembly.Location,
					typeof(XmlReader).Assembly.Location,
					typeof(OperationDescription).Assembly.Location,
					typeof(DataContractAttribute).Assembly.Location
				},
				GenerateExecutable = false,
				CompilerOptions = "/platform:x86"
			}, new string[] { proxyPath });
			if (compilerResults.Errors.Count == 0)
			{
				assemblyPath = compilerResults.PathToAssembly;
				return ServiceAnalyzer.AnalyzeProxy(services, configObject, configPath, assemblyPath);
			}
			if (errorMessage == null)
			{
				errorMessage = string.Empty;
			}
			foreach (object obj in compilerResults.Errors)
			{
				CompilerError compilerError = (CompilerError)obj;
				errorMessage = errorMessage + compilerError.ToString() + Environment.NewLine;
			}
			return null;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00002FD9 File Offset: 0x000011D9
		private bool CancelOrReportProgress(BackgroundWorker addServiceWorker, AppDomain clientDomain, int percentProgress, string projectPath)
		{
			if (addServiceWorker.CancellationPending)
			{
				if (clientDomain != null)
				{
					ServiceAnalyzer.UnloadAppDomain(clientDomain);
				}
				ServiceAnalyzer.DeleteProjectFolder(projectPath);
				return true;
			}
			addServiceWorker.ReportProgress(percentProgress);
			return false;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00002FFD File Offset: 0x000011FD
		private int GetIntValueOfProgress(float startProgress, float progressRange, float percent)
		{
			return (int)(startProgress + progressRange * percent);
		}

		// Token: 0x04000063 RID: 99
		internal const string DefaultConfigName = "default.config";

		// Token: 0x04000064 RID: 100
		private static bool isErrorStreamClosed;

		// Token: 0x04000065 RID: 101
		private static string svcutilError;

		// Token: 0x04000066 RID: 102
		private static Type customBindingType = typeof(CustomBindingElement);

		// Token: 0x04000067 RID: 103
		private static Type standardBindingType = typeof(StandardBindingElement);

		// Token: 0x04000068 RID: 104
		private static TimeSpan defaultSvcInvocationTimeout = new TimeSpan(0, 5, 0);
	}
}
