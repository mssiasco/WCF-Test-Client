using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Main.Tools.TestClient
{
	// Token: 0x02000009 RID: 9
	internal class ConfigFileMappingManager
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00002997 File Offset: 0x00000B97
		private ConfigFileMappingManager()
		{
			this.ReadFromMappingFile();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000029B0 File Offset: 0x00000BB0
		internal static ConfigFileMappingManager GetInstance()
		{
			if (ConfigFileMappingManager.configFileManager == null)
			{
				ConfigFileMappingManager.configFileManager = new ConfigFileMappingManager();
			}
			return ConfigFileMappingManager.configFileManager;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004DFC File Offset: 0x00002FFC
		internal void AddConfigFileMapping(string address)
		{
			if (!this.addressToFileEntries.ContainsKey(address))
			{
				string text = Guid.NewGuid().ToString() + ".config";
				string text2 = Path.Combine(ConfigFileMappingManager.savedConfigFolder, text);
				this.addressToFileEntries.Add(address, text2);
				try
				{
					this.WriteToMappingFile();
				}
				catch (IOException)
				{
				}
				catch (UnauthorizedAccessException)
				{
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004E78 File Offset: 0x00003078
		internal void Clear()
		{
			this.addressToFileEntries.Clear();
			try
			{
				Directory.Delete(ConfigFileMappingManager.savedConfigFolder, true);
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004EC0 File Offset: 0x000030C0
		internal void DeleteConfigFileMapping(string address)
		{
			if (this.addressToFileEntries.ContainsKey(address))
			{
				try
				{
					File.Delete(this.addressToFileEntries[address]);
					this.WriteToMappingFile();
				}
				catch (IOException)
				{
				}
				catch (UnauthorizedAccessException)
				{
				}
				this.addressToFileEntries.Remove(address);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000029C8 File Offset: 0x00000BC8
		internal bool DoesConfigMappingExist(string address)
		{
			return this.addressToFileEntries.ContainsKey(address) && File.Exists(this.addressToFileEntries[address]);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000029EB File Offset: 0x00000BEB
		internal string GetSavedConfigPath(string address)
		{
			if (this.addressToFileEntries.ContainsKey(address))
			{
				return this.addressToFileEntries[address];
			}
			return null;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004F24 File Offset: 0x00003124
		private void ReadFromMappingFile()
		{
			this.addressToFileEntries.Clear();
			if (!File.Exists(ConfigFileMappingManager.mappingFilePath))
			{
				return;
			}
			XmlDocument xmlDocument = new XmlDocument
			{
				XmlResolver = null
			};
			try
			{
				XmlReader xmlReader = XmlReader.Create(ConfigFileMappingManager.mappingFilePath, new XmlReaderSettings
				{
					XmlResolver = null
				});
				xmlDocument.Load(xmlReader);
			}
			catch (XmlException)
			{
				return;
			}
			catch (IOException)
			{
				return;
			}
			catch (UnauthorizedAccessException)
			{
				return;
			}
			foreach (object obj in xmlDocument.DocumentElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.ChildNodes.Count >= 2)
				{
					this.addressToFileEntries.Add(xmlNode.ChildNodes[0].InnerText, xmlNode.ChildNodes[1].InnerText);
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000502C File Offset: 0x0000322C
		private void WriteToMappingFile()
		{
			XmlDocument xmlDocument = new XmlDocument
			{
				XmlResolver = null
			};
			xmlDocument.AppendChild(xmlDocument.CreateElement("Mapping"));
			foreach (KeyValuePair<string, string> keyValuePair in this.addressToFileEntries)
			{
				XmlElement xmlElement = xmlDocument.CreateElement("Entry");
				XmlElement xmlElement2 = xmlDocument.CreateElement("Address");
				xmlElement2.InnerText = keyValuePair.Key;
				xmlElement.AppendChild(xmlElement2);
				XmlElement xmlElement3 = xmlDocument.CreateElement("ConfigPath");
				xmlElement3.InnerText = keyValuePair.Value;
				xmlElement.AppendChild(xmlElement3);
				xmlDocument.DocumentElement.AppendChild(xmlElement);
			}
			if (!Directory.Exists(ConfigFileMappingManager.savedConfigFolder))
			{
				Directory.CreateDirectory(ConfigFileMappingManager.savedConfigFolder);
			}
			xmlDocument.Save(ConfigFileMappingManager.mappingFilePath);
		}

		// Token: 0x04000025 RID: 37
		private static string savedConfigFolder = Path.Combine(ToolingEnvironment.SavedDataBase, "CachedConfig");

		// Token: 0x04000026 RID: 38
		private static string mappingFilePath = Path.Combine(ConfigFileMappingManager.savedConfigFolder, "AddressToConfigMapping.xml");

		// Token: 0x04000027 RID: 39
		private static ConfigFileMappingManager configFileManager;

		// Token: 0x04000028 RID: 40
		private IDictionary<string, string> addressToFileEntries = new Dictionary<string, string>();
	}
}
