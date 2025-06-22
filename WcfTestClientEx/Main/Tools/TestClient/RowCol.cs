using System;

namespace Main.Tools.TestClient
{
	// Token: 0x0200001D RID: 29
	internal class RowCol
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00002F5A File Offset: 0x0000115A
		public RowCol(int row, int col)
		{
			this.row = row;
			this.col = col;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00002F70 File Offset: 0x00001170
		internal int Col
		{
			get
			{
				return this.col;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00002F78 File Offset: 0x00001178
		internal int Row
		{
			get
			{
				return this.row;
			}
		}

		// Token: 0x04000061 RID: 97
		private int col;

		// Token: 0x04000062 RID: 98
		private int row;
	}
}
