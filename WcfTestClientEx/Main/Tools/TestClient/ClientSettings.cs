using System;
using System.Reflection;

namespace Main.Tools.TestClient
{
	// Token: 0x02000007 RID: 7
	internal static class ClientSettings
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00004D38 File Offset: 0x00002F38
		internal static Assembly ClientAssembly
		{
			get
			{
				if (ClientSettings.clientAssembly == null)
				{
					string text = (string)AppDomain.CurrentDomain.GetData("clientAssemblyPath");
					ClientSettings.clientAssembly = Assembly.Load(new AssemblyName
					{
						CodeBase = text
					});
				}
				return ClientSettings.clientAssembly;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004D84 File Offset: 0x00002F84
		internal static Type GetType(string typeName)
		{
			Type type = ClientSettings.ClientAssembly.GetType(typeName);
			if (type == null)
			{
				type = Type.GetType(typeName);
				if (type == null)
				{
					AssemblyName[] referencedAssemblies = ClientSettings.ClientAssembly.GetReferencedAssemblies();
					foreach (AssemblyName assemblyName in referencedAssemblies)
					{
						Assembly assembly = Assembly.Load(assemblyName);
						if (assembly != null)
						{
							type = assembly.GetType(typeName);
							if (type != null)
							{
								break;
							}
						}
					}
				}
			}
			return type;
		}

		// Token: 0x0400001C RID: 28
		private static Assembly clientAssembly;
	}
}
