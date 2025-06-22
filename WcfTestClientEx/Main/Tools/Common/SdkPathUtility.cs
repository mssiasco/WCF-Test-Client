//using Main.Build.Utilities;
using System;
using System.IO;
using System.Reflection;

namespace Main.Tools.Common
{
	// Token: 0x02000062 RID: 98
	internal static class SdkPathUtility
	{
		// Token: 0x060002FA RID: 762 RVA: 0x000044D5 File Offset: 0x000026D5
		internal static string GetSdkPath(string filename)
		{
            string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            return $"{assemblyDirectory}\\sdk-tools\\{filename}";
            //return SdkPathUtility._GetSdkPath(VersionNumbers.NETFxCurrentVersion, filename);
            //return ToolLocationHelper.GetPathToDotNetFrameworkSdkFile(filename, TargetDotNetFrameworkVersion.VersionLatest);
        }

		// Token: 0x060002FB RID: 763 RVA: 0x000044E2 File Offset: 0x000026E2
		/*internal static string _GetSdkPath(Version targetFrameworkVersion, string filename)
		{
			//if (targetFrameworkVersion <= new Version(3, 5))
			//{
			//	return ToolLocationHelper.GetPathToDotNetFrameworkSdkFile(filename, 3);
			//}
			return ToolLocationHelper.GetPathToDotNetFrameworkSdkFile(filename,TargetDotNetFrameworkVersion.VersionLatest);
		}*/
	}
}
