using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace Main.Internal.Performance
{
	// Token: 0x02000064 RID: 100
	internal sealed class CodeMarkers
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00004501 File Offset: 0x00002701
		public bool IsEnabled
		{
			get
			{
				return this.state == CodeMarkers.State.Enabled;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000E6D4 File Offset: 0x0000C8D4
		public bool ShouldUseTestDll
		{
			get
			{
				if (this.shouldUseTestDll == null)
				{
					try
					{
						if (this.regroot == null)
						{
							this.shouldUseTestDll = new bool?(CodeMarkers.NativeMethods.GetModuleHandle("Main.VisualStudio.CodeMarkers.dll") == IntPtr.Zero);
						}
						else
						{
							this.shouldUseTestDll = new bool?(CodeMarkers.UsePrivateCodeMarkers(this.regroot, this.registryView));
						}
					}
					catch (Exception)
					{
						this.shouldUseTestDll = new bool?(true);
					}
				}
				return this.shouldUseTestDll.Value;
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000450C File Offset: 0x0000270C
		private CodeMarkers()
		{
			this.state = ((CodeMarkers.NativeMethods.FindAtom("VSCodeMarkersEnabled") != 0) ? CodeMarkers.State.Enabled : CodeMarkers.State.Disabled);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000E760 File Offset: 0x0000C960
		public bool CodeMarker(int nTimerID)
		{
			if (!this.IsEnabled)
			{
				return false;
			}
			try
			{
				if (this.ShouldUseTestDll)
				{
					CodeMarkers.NativeMethods.TestDllPerfCodeMarker(new IntPtr(nTimerID), null, new IntPtr(0));
				}
				else
				{
					CodeMarkers.NativeMethods.ProductDllPerfCodeMarker(new IntPtr(nTimerID), null, new IntPtr(0));
				}
			}
			catch (DllNotFoundException)
			{
				this.state = CodeMarkers.State.DisabledDueToDllImportException;
				return false;
			}
			return true;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
		public bool CodeMarkerEx(int nTimerID, byte[] aBuff)
		{
			if (!this.IsEnabled)
			{
				return false;
			}
			if (aBuff == null)
			{
				throw new ArgumentNullException("aBuff");
			}
			try
			{
				if (this.ShouldUseTestDll)
				{
					CodeMarkers.NativeMethods.TestDllPerfCodeMarker(new IntPtr(nTimerID), aBuff, new IntPtr(aBuff.Length));
				}
				else
				{
					CodeMarkers.NativeMethods.ProductDllPerfCodeMarker(new IntPtr(nTimerID), aBuff, new IntPtr(aBuff.Length));
				}
			}
			catch (DllNotFoundException)
			{
				this.state = CodeMarkers.State.DisabledDueToDllImportException;
				return false;
			}
			return true;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000452A File Offset: 0x0000272A
		public void SetStateDLLException()
		{
			this.state = CodeMarkers.State.DisabledDueToDllImportException;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00004533 File Offset: 0x00002733
		public bool CodeMarkerEx(int nTimerID, Guid guidData)
		{
			return this.CodeMarkerEx(nTimerID, guidData.ToByteArray());
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000E844 File Offset: 0x0000CA44
		public bool CodeMarkerEx(int nTimerID, string stringData)
		{
			if (!this.IsEnabled)
			{
				return false;
			}
			if (stringData == null)
			{
				throw new ArgumentNullException("stringData");
			}
			try
			{
				int num = Encoding.Unicode.GetByteCount(stringData) + 2;
				if (this.ShouldUseTestDll)
				{
					CodeMarkers.NativeMethods.TestDllPerfCodeMarkerString(new IntPtr(nTimerID), stringData, new IntPtr(num));
				}
				else
				{
					CodeMarkers.NativeMethods.ProductDllPerfCodeMarkerString(new IntPtr(nTimerID), stringData, new IntPtr(num));
				}
			}
			catch (DllNotFoundException)
			{
				this.state = CodeMarkers.State.DisabledDueToDllImportException;
				return false;
			}
			return true;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000E8C8 File Offset: 0x0000CAC8
		internal static byte[] StringToBytesZeroTerminated(string stringData)
		{
			Encoding unicode = Encoding.Unicode;
			int byteCount = unicode.GetByteCount(stringData);
			byte[] array = new byte[byteCount + 2];
			unicode.GetBytes(stringData, 0, stringData.Length, array, 0);
			return array;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000E900 File Offset: 0x0000CB00
		public static byte[] AttachCorrelationId(byte[] buffer, Guid correlationId)
		{
			if (correlationId == Guid.Empty)
			{
				return buffer;
			}
			byte[] array = correlationId.ToByteArray();
			byte[] array2 = new byte[CodeMarkers.CorrelationMarkBytes.Length + array.Length + ((buffer != null) ? buffer.Length : 0)];
			CodeMarkers.CorrelationMarkBytes.CopyTo(array2, 0);
			array.CopyTo(array2, CodeMarkers.CorrelationMarkBytes.Length);
			if (buffer != null)
			{
				buffer.CopyTo(array2, CodeMarkers.CorrelationMarkBytes.Length + array.Length);
			}
			return array2;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00004543 File Offset: 0x00002743
		public bool CodeMarkerEx(int nTimerID, uint uintData)
		{
			return this.CodeMarkerEx(nTimerID, BitConverter.GetBytes(uintData));
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00004552 File Offset: 0x00002752
		public bool CodeMarkerEx(int nTimerID, ulong ulongData)
		{
			return this.CodeMarkerEx(nTimerID, BitConverter.GetBytes(ulongData));
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000E970 File Offset: 0x0000CB70
		private static bool UsePrivateCodeMarkers(string regRoot, RegistryView registryView)
		{
			if (regRoot == null)
			{
				throw new ArgumentNullException("regRoot");
			}
			using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
			{
				using (RegistryKey registryKey2 = registryKey.OpenSubKey(regRoot + "\\Performance"))
				{
					if (registryKey2 != null)
					{
						string text = registryKey2.GetValue(string.Empty).ToString();
						return !string.IsNullOrEmpty(text);
					}
				}
			}
			return false;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00004561 File Offset: 0x00002761
		public bool InitPerformanceDll(int iApp, string strRegRoot)
		{
			return this.InitPerformanceDll(iApp, strRegRoot, RegistryView.Default);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000EA00 File Offset: 0x0000CC00
		public bool InitPerformanceDll(int iApp, string strRegRoot, RegistryView registryView)
		{
			if (this.IsEnabled)
			{
				return true;
			}
			if (strRegRoot == null)
			{
				throw new ArgumentNullException("strRegRoot");
			}
			this.regroot = strRegRoot;
			this.registryView = registryView;
			try
			{
				if (this.ShouldUseTestDll)
				{
					CodeMarkers.NativeMethods.TestDllInitPerf(new IntPtr(iApp));
				}
				else
				{
					CodeMarkers.NativeMethods.ProductDllInitPerf(new IntPtr(iApp));
				}
				this.state = CodeMarkers.State.Enabled;
				CodeMarkers.NativeMethods.AddAtom("VSCodeMarkersEnabled");
			}
			catch (BadImageFormatException)
			{
				this.state = CodeMarkers.State.DisabledDueToDllImportException;
			}
			catch (DllNotFoundException)
			{
				this.state = CodeMarkers.State.DisabledDueToDllImportException;
				return false;
			}
			return true;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000EA9C File Offset: 0x0000CC9C
		public void UninitializePerformanceDLL(int iApp)
		{
			bool? flag = this.shouldUseTestDll;
			this.shouldUseTestDll = null;
			this.regroot = null;
			if (!this.IsEnabled)
			{
				return;
			}
			this.state = CodeMarkers.State.Disabled;
			ushort num = CodeMarkers.NativeMethods.FindAtom("VSCodeMarkersEnabled");
			if (num != 0)
			{
				CodeMarkers.NativeMethods.DeleteAtom(num);
			}
			try
			{
				if (flag != null)
				{
					if (flag.Value)
					{
						CodeMarkers.NativeMethods.TestDllUnInitPerf(new IntPtr(iApp));
					}
					else
					{
						CodeMarkers.NativeMethods.ProductDllUnInitPerf(new IntPtr(iApp));
					}
				}
			}
			catch (DllNotFoundException)
			{
			}
		}

		// Token: 0x040001A5 RID: 421
		public static readonly CodeMarkers Instance = new CodeMarkers();

		// Token: 0x040001A6 RID: 422
		private const string AtomName = "VSCodeMarkersEnabled";

		// Token: 0x040001A7 RID: 423
		private const string TestDllName = "Main.Internal.Performance.CodeMarkers.dll";

		// Token: 0x040001A8 RID: 424
		private const string ProductDllName = "Main.VisualStudio.CodeMarkers.dll";

		// Token: 0x040001A9 RID: 425
		private CodeMarkers.State state;

		// Token: 0x040001AA RID: 426
		private RegistryView registryView;

		// Token: 0x040001AB RID: 427
		private string regroot;

		// Token: 0x040001AC RID: 428
		private bool? shouldUseTestDll;

		// Token: 0x040001AD RID: 429
		private static readonly byte[] CorrelationMarkBytes = new Guid("AA10EEA0-F6AD-4E21-8865-C427DAE8EDB9").ToByteArray();

		// Token: 0x02000065 RID: 101
		private static class NativeMethods
		{
			// Token: 0x0600030E RID: 782
			[DllImport("Main.Internal.Performance.CodeMarkers.dll", EntryPoint = "InitPerf")]
			public static extern void TestDllInitPerf(IntPtr iApp);

			// Token: 0x0600030F RID: 783
			[DllImport("Main.Internal.Performance.CodeMarkers.dll", EntryPoint = "UnInitPerf")]
			public static extern void TestDllUnInitPerf(IntPtr iApp);

			// Token: 0x06000310 RID: 784
			[DllImport("Main.Internal.Performance.CodeMarkers.dll", EntryPoint = "PerfCodeMarker")]
			public static extern void TestDllPerfCodeMarker(IntPtr nTimerID, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] aUserParams, IntPtr cbParams);

			// Token: 0x06000311 RID: 785
			[DllImport("Main.Internal.Performance.CodeMarkers.dll", EntryPoint = "PerfCodeMarker")]
			public static extern void TestDllPerfCodeMarkerString(IntPtr nTimerID, [MarshalAs(UnmanagedType.LPWStr)] string aUserParams, IntPtr cbParams);

			// Token: 0x06000312 RID: 786
			[DllImport("Main.VisualStudio.CodeMarkers.dll", EntryPoint = "InitPerf")]
			public static extern void ProductDllInitPerf(IntPtr iApp);

			// Token: 0x06000313 RID: 787
			[DllImport("Main.VisualStudio.CodeMarkers.dll", EntryPoint = "UnInitPerf")]
			public static extern void ProductDllUnInitPerf(IntPtr iApp);

			// Token: 0x06000314 RID: 788
			[DllImport("Main.VisualStudio.CodeMarkers.dll", EntryPoint = "PerfCodeMarker")]
			public static extern void ProductDllPerfCodeMarker(IntPtr nTimerID, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] aUserParams, IntPtr cbParams);

			// Token: 0x06000315 RID: 789
			[DllImport("Main.VisualStudio.CodeMarkers.dll", EntryPoint = "PerfCodeMarker")]
			public static extern void ProductDllPerfCodeMarkerString(IntPtr nTimerID, [MarshalAs(UnmanagedType.LPWStr)] string aUserParams, IntPtr cbParams);

			// Token: 0x06000316 RID: 790
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
			public static extern ushort FindAtom([MarshalAs(UnmanagedType.LPWStr)] string lpString);

			// Token: 0x06000317 RID: 791
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
			public static extern ushort AddAtom([MarshalAs(UnmanagedType.LPWStr)] string lpString);

			// Token: 0x06000318 RID: 792
			[DllImport("kernel32.dll")]
			public static extern ushort DeleteAtom(ushort atom);

			// Token: 0x06000319 RID: 793
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
			public static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);
		}

		// Token: 0x02000066 RID: 102
		private enum State
		{
			// Token: 0x040001AF RID: 431
			Enabled,
			// Token: 0x040001B0 RID: 432
			Disabled,
			// Token: 0x040001B1 RID: 433
			DisabledDueToDllImportException
		}
	}
}
