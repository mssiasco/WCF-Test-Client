using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Main.Tools.TestClient
{
	// Token: 0x02000012 RID: 18
	internal class ExceptionUtility
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00005C2C File Offset: 0x00003E2C
		internal static bool IsFatal(Exception exception)
		{
			while (exception != null)
			{
				if ((exception is OutOfMemoryException && !(exception is InsufficientMemoryException)) || exception is ThreadAbortException || exception is AccessViolationException || exception is SEHException)
				{
					return true;
				}
				if (!(exception is TypeInitializationException) && !(exception is TargetInvocationException))
				{
					break;
				}
				exception = exception.InnerException;
			}
			return false;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00002C12 File Offset: 0x00000E12
		internal ArgumentException ThrowHelperArgument(string message)
		{
			return (ArgumentException)this.ThrowHelperError(new ArgumentException(message));
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00002C25 File Offset: 0x00000E25
		internal ArgumentException ThrowHelperArgument(string paramName, string message)
		{
			return (ArgumentException)this.ThrowHelperError(new ArgumentException(message, paramName));
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00002C39 File Offset: 0x00000E39
		internal ArgumentNullException ThrowHelperArgumentNull(string paramName)
		{
			return (ArgumentNullException)this.ThrowHelperError(new ArgumentNullException(paramName));
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00002C4C File Offset: 0x00000E4C
		internal ArgumentNullException ThrowHelperArgumentNull(string paramName, string message)
		{
			return (ArgumentNullException)this.ThrowHelperError(new ArgumentNullException(paramName, message));
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00002C60 File Offset: 0x00000E60
		internal Exception ThrowHelperCritical(Exception exception)
		{
			return exception;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00002C60 File Offset: 0x00000E60
		internal Exception ThrowHelperError(Exception exception)
		{
			return exception;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00002C60 File Offset: 0x00000E60
		internal Exception ThrowHelperWarning(Exception exception)
		{
			return exception;
		}
	}
}
