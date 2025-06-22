using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Main.Tools.TestClient
{
	// Token: 0x0200000E RID: 14
	internal static class DiagnosticUtility
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002BC1 File Offset: 0x00000DC1
		internal static ExceptionUtility ExceptionUtility
		{
			get
			{
				if (DiagnosticUtility.exceptionUtility == null)
				{
					DiagnosticUtility.exceptionUtility = new ExceptionUtility();
				}
				return DiagnosticUtility.exceptionUtility;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002BD9 File Offset: 0x00000DD9
		[Conditional("DEBUG")]
		internal static void DebugAssert(bool condition, string message)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002B6F File Offset: 0x00000D6F
		[Conditional("DEBUG")]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void DebugAssert(string message)
		{
		}

		// Token: 0x04000035 RID: 53
		private static ExceptionUtility exceptionUtility;
	}
}
