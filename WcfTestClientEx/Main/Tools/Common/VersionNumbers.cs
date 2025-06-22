using System;
using System.Reflection;

namespace Main.Tools.Common
{
	// Token: 0x02000063 RID: 99
	internal static class VersionNumbers
	{
		// Token: 0x040001A2 RID: 418
		public static readonly Version VSCurrentVersion = Assembly.GetExecutingAssembly().GetName().Version;

		// Token: 0x040001A3 RID: 419
		public static readonly string VSCurrentVersionString = VersionNumbers.VSCurrentVersion.ToString(2);

		// Token: 0x040001A4 RID: 420
		public static readonly Version NETFxCurrentVersion = typeof(object).Assembly.GetName().Version;
	}
}
