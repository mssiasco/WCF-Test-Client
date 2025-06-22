using System;
using Main.Tools.TestClient.UI;

namespace Main.Tools.TestClient
{
	// Token: 0x02000013 RID: 19
	internal class FileItem
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00002C63 File Offset: 0x00000E63
		internal FileItem(string fileName)
		{
			this.fileName = fileName;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00002C72 File Offset: 0x00000E72
		internal string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00002C7A File Offset: 0x00000E7A
		internal FilePage FilePage
		{
			get
			{
				if (this.filePage == null)
				{
					this.filePage = new FilePage(this.fileName);
				}
				return this.filePage;
			}
		}

		// Token: 0x04000041 RID: 65
		private string fileName;

		// Token: 0x04000042 RID: 66
		private FilePage filePage;
	}
}
