using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.CSharp;

namespace Main.Tools.TestClient
{
	// Token: 0x0200002A RID: 42
	internal class StringFormatter
	{
		// Token: 0x06000193 RID: 403 RVA: 0x000081C4 File Offset: 0x000063C4
		internal static string FromEscapeCode(string input)
		{
			char[] array = input.ToCharArray();
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			int num = 0;
			while (i < array.Length)
			{
				char c = array[i];
				if (num == 0)
				{
					if (array[i] == '\\')
					{
						num = 1;
					}
					else
					{
						num = 0;
						stringBuilder.Append(c);
					}
				}
				else
				{
					if (num != 1)
					{
						return null;
					}
					if (c == 'r')
					{
						num = 0;
						stringBuilder.Append('\r');
					}
					else if (c == 'n')
					{
						num = 0;
						stringBuilder.Append('\n');
					}
					else if (c == 't')
					{
						num = 0;
						stringBuilder.Append('\t');
					}
					else
					{
						if (c != '\\')
						{
							return null;
						}
						num = 0;
						stringBuilder.Append('\\');
					}
				}
				i++;
			}
			if (num == 0)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00008270 File Offset: 0x00006470
		internal static string ToEscapeCode(string input)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture);
			CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider();
			csharpCodeProvider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), stringWriter, new CodeGeneratorOptions());
			return stringWriter.ToString();
		}
	}
}
