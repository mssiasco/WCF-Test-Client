using System;
using System.Globalization;
using System.Windows.Forms;

namespace Main.Tools.TestClient
{
	// Token: 0x0200001E RID: 30
	internal static class RtlAwareMessageBox
	{
		// Token: 0x06000107 RID: 263 RVA: 0x00002F80 File Offset: 0x00001180
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			return RtlAwareMessageBox.Show(null, text, caption, buttons, icon, defaultButton, options);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00002F90 File Offset: 0x00001190
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			if (RtlAwareMessageBox.IsRightToLeft(owner))
			{
				options |= MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading;
			}
			return MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000638C File Offset: 0x0000458C
		private static bool IsRightToLeft(IWin32Window owner)
		{
			Control control = owner as Control;
			if (control != null)
			{
				return control.RightToLeft == RightToLeft.Yes;
			}
			return CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;
		}
	}
}
