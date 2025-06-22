using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Main.Tools.Common
{
	// Token: 0x02000058 RID: 88
	internal static class NativeMethods
	{
		// Token: 0x060002E1 RID: 737
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool AssignProcessToJobObject(SafeFileHandle hJob, IntPtr hProcess);

		// Token: 0x060002E2 RID: 738
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll")]
		public static extern bool CloseHandle(IntPtr handle);

		// Token: 0x060002E3 RID: 739
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern SafeFileHandle CreateJobObject(IntPtr lpJobAttributes, string lpName);

		// Token: 0x060002E4 RID: 740
		[DllImport("user32")]
		public static extern int EnumWindows(NativeMethods.EnumWindowsCallback func, IntPtr lParam);

		// Token: 0x060002E5 RID: 741 RVA: 0x000044C1 File Offset: 0x000026C1
		public static bool Failed(int hr)
		{
			return !NativeMethods.Succeeded(hr);
		}

		// Token: 0x060002E6 RID: 742
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

		// Token: 0x060002E7 RID: 743
		[DllImport("user32.dll")]
		public static extern IntPtr GetDesktopWindow();

		// Token: 0x060002E8 RID: 744
		[DllImport("user32.dll")]
		public static extern bool GetWindowRect(IntPtr hWnd, out NativeMethods.RECT lpRect);

		// Token: 0x060002E9 RID: 745
		[DllImport("user32")]
		public static extern uint GetWindowThreadProcessId(SafeHandle hwnd, out int lpdwProcessId);

		// Token: 0x060002EA RID: 746
		[DllImport("ieframe.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int IELaunchURL(string pszUrl, out NativeMethods.PROCESS_INFORMATION pProcInfo, IntPtr lpInfo);

		// Token: 0x060002EB RID: 747
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsProcessInJob(IntPtr ProcessHandle, IntPtr JobHandle, out bool Result);

		// Token: 0x060002EC RID: 748
		[DllImport("user32")]
		public static extern int IsWindow(SafeHandle hWnd);

		// Token: 0x060002ED RID: 749
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(SafeHandle hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x060002EE RID: 750
		[DllImport("user32")]
		public static extern IntPtr SendMessageTimeout(SafeHandle hWnd, int msg, IntPtr wParam, IntPtr lParam, int flags, int timeout, out IntPtr pdwResult);

		// Token: 0x060002EF RID: 751
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetInformationJobObject(SafeFileHandle hJob, NativeMethods.JOBOBJECTINFOCLASS JobObjectInfoClass, ref NativeMethods.JOBOBJECT_EXTENDED_LIMIT_INFORMATION lpJobObjectInfo, int cbJobObjectInfoLength);

		// Token: 0x060002F0 RID: 752
		[DllImport("kernel32.dll")]
		public static extern void SetLastError(uint dwErrCode);

		// Token: 0x060002F1 RID: 753
		[DllImport("User32", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr SetParent(IntPtr hWnd, IntPtr hWndParent);

		// Token: 0x060002F2 RID: 754
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool WaitNamedPipe(string name, int timeout);

		// Token: 0x060002F3 RID: 755 RVA: 0x000044CC File Offset: 0x000026CC
		public static bool Succeeded(int hr)
		{
			return hr >= 0;
		}

		// Token: 0x04000164 RID: 356
		public const int ENDSESSION_CLOSEAPP = 1;

		// Token: 0x04000165 RID: 357
		public const int SMTO_ABORTIFHUNG = 2;

		// Token: 0x04000166 RID: 358
		public const int WM_CLOSE = 16;

		// Token: 0x04000167 RID: 359
		public const int WM_ENDSESSION = 22;

		// Token: 0x04000168 RID: 360
		public const int WM_MOUSEMOVE = 512;

		// Token: 0x02000059 RID: 89
		// (Invoke) Token: 0x060002F5 RID: 757
		public delegate bool EnumWindowsCallback(IntPtr hwnd, IntPtr lParam);

		// Token: 0x0200005A RID: 90
		public enum JOBOBJECTINFOCLASS
		{
			// Token: 0x0400016A RID: 362
			JobObjectBasicAccountingInformation = 1,
			// Token: 0x0400016B RID: 363
			JobObjectBasicLimitInformation,
			// Token: 0x0400016C RID: 364
			JobObjectBasicProcessIdList,
			// Token: 0x0400016D RID: 365
			JobObjectBasicUIRestrictions,
			// Token: 0x0400016E RID: 366
			JobObjectSecurityLimitInformation,
			// Token: 0x0400016F RID: 367
			JobObjectEndOfJobTimeInformation,
			// Token: 0x04000170 RID: 368
			JobObjectAssociateCompletionPortInformation,
			// Token: 0x04000171 RID: 369
			JobObjectBasicAndIoAccountingInformation,
			// Token: 0x04000172 RID: 370
			JobObjectExtendedLimitInformation,
			// Token: 0x04000173 RID: 371
			JobObjectJobSetInformation,
			// Token: 0x04000174 RID: 372
			MaxJobObjectInfoClass
		}

		// Token: 0x0200005B RID: 91
		public enum JOB_OBJECT_LIMIT : uint
		{
			// Token: 0x04000176 RID: 374
			WORKINGSET = 1U,
			// Token: 0x04000177 RID: 375
			PROCESS_TIME,
			// Token: 0x04000178 RID: 376
			JOB_TIME = 4U,
			// Token: 0x04000179 RID: 377
			ACTIVE_PROCESS = 8U,
			// Token: 0x0400017A RID: 378
			AFFINITY = 16U,
			// Token: 0x0400017B RID: 379
			PRIORITY_CLASS = 32U,
			// Token: 0x0400017C RID: 380
			PRESERVE_JOB_TIME = 64U,
			// Token: 0x0400017D RID: 381
			SCHEDULING_CLASS = 128U,
			// Token: 0x0400017E RID: 382
			PROCESS_MEMORY = 256U,
			// Token: 0x0400017F RID: 383
			JOB_MEMORY = 512U,
			// Token: 0x04000180 RID: 384
			DIE_ON_UNHANDLED_EXCEPTION = 1024U,
			// Token: 0x04000181 RID: 385
			BREAKAWAY_OK = 2048U,
			// Token: 0x04000182 RID: 386
			SILENT_BREAKAWAY_OK = 4096U,
			// Token: 0x04000183 RID: 387
			KILL_ON_JOB_CLOSE = 8192U,
			// Token: 0x04000184 RID: 388
			SUBSET_AFFINITY = 16384U
		}

		// Token: 0x0200005C RID: 92
		public struct IO_COUNTERS
		{
			// Token: 0x04000185 RID: 389
			public ulong ReadOperationCount;

			// Token: 0x04000186 RID: 390
			public ulong WriteOperationCount;

			// Token: 0x04000187 RID: 391
			public ulong OtherOperationCount;

			// Token: 0x04000188 RID: 392
			public ulong ReadTransferCount;

			// Token: 0x04000189 RID: 393
			public ulong WriteTransferCount;

			// Token: 0x0400018A RID: 394
			public ulong OtherTransferCount;
		}

		// Token: 0x0200005D RID: 93
		public struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
		{
			// Token: 0x0400018B RID: 395
			public NativeMethods.JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;

			// Token: 0x0400018C RID: 396
			public NativeMethods.IO_COUNTERS IoInfo;

			// Token: 0x0400018D RID: 397
			public UIntPtr ProcessMemoryLimit;

			// Token: 0x0400018E RID: 398
			public UIntPtr JobMemoryLimit;

			// Token: 0x0400018F RID: 399
			public UIntPtr PeakProcessMemoryUsed;

			// Token: 0x04000190 RID: 400
			public UIntPtr PeakJobMemoryUsed;
		}

		// Token: 0x0200005E RID: 94
		public struct JOBOBJECT_BASIC_LIMIT_INFORMATION
		{
			// Token: 0x04000191 RID: 401
			public long PerProcessUserTimeLimit;

			// Token: 0x04000192 RID: 402
			public long PerJobUserTimeLimit;

			// Token: 0x04000193 RID: 403
			public NativeMethods.JOB_OBJECT_LIMIT LimitFlags;

			// Token: 0x04000194 RID: 404
			public UIntPtr MinimumWorkingSetSize;

			// Token: 0x04000195 RID: 405
			public UIntPtr MaximumWorkingSetSize;

			// Token: 0x04000196 RID: 406
			public uint ActiveProcessLimit;

			// Token: 0x04000197 RID: 407
			public UIntPtr Affinity;

			// Token: 0x04000198 RID: 408
			public uint PriorityClass;

			// Token: 0x04000199 RID: 409
			public uint SchedulingClass;
		}

		// Token: 0x0200005F RID: 95
		public struct PROCESS_INFORMATION
		{
			// Token: 0x0400019A RID: 410
			internal IntPtr hProcess;

			// Token: 0x0400019B RID: 411
			internal IntPtr hThread;

			// Token: 0x0400019C RID: 412
			internal int dwProcessId;

			// Token: 0x0400019D RID: 413
			internal int dwThreadId;
		}

		// Token: 0x02000060 RID: 96
		public struct RECT
		{
			// Token: 0x0400019E RID: 414
			public int left;

			// Token: 0x0400019F RID: 415
			public int top;

			// Token: 0x040001A0 RID: 416
			public int right;

			// Token: 0x040001A1 RID: 417
			public int bottom;
		}
	}
}
