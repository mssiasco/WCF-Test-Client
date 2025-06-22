using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Main.Tools.Common
{
	// Token: 0x02000061 RID: 97
	internal static class SafeClipboard
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x0000E634 File Offset: 0x0000C834
		public static void Clear()
		{
			try
			{
				Clipboard.Clear();
			}
			catch (ExternalException)
			{
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000E65C File Offset: 0x0000C85C
		public static void SetText(string text)
		{
			try
			{
				Clipboard.SetText(text);
			}
			catch (ExternalException)
			{
			}
		}
	}
}
