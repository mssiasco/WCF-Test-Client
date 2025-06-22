using System;
using System.Collections.Generic;
using System.Globalization;

namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000055 RID: 85
	internal class TabPageCloseOrderManager
	{
		// Token: 0x060002CD RID: 717 RVA: 0x0000DFEC File Offset: 0x0000C1EC
		public override string ToString()
		{
			string text = string.Empty;
			int num = 0;
			foreach (int num2 in this.leastRecentlyAccessedTabIndexes)
			{
				string text2 = text + "{0}";
				if (++num != this.leastRecentlyAccessedTabIndexes.Count)
				{
					text2 += ", ";
				}
				text = string.Format(CultureInfo.CurrentUICulture, text2, num2);
			}
			return text;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000437F File Offset: 0x0000257F
		internal void HandleTabAdded(int addedTabIndex)
		{
			this.leastRecentlyAccessedTabIndexes.Add(addedTabIndex);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000438D File Offset: 0x0000258D
		internal void HandleTabChanged(int selectedTabIndex)
		{
			this.leastRecentlyAccessedTabIndexes.Remove(selectedTabIndex);
			this.leastRecentlyAccessedTabIndexes.Add(selectedTabIndex);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000E084 File Offset: 0x0000C284
		internal void HandleTabClosed(int removingIndex)
		{
			this.leastRecentlyAccessedTabIndexes.Remove(removingIndex);
			List<int> list = new List<int>();
			foreach (int num in this.leastRecentlyAccessedTabIndexes)
			{
				int num2 = num;
				if (num2 > removingIndex)
				{
					num2--;
				}
				list.Add(num2);
			}
			this.leastRecentlyAccessedTabIndexes = list;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000043A8 File Offset: 0x000025A8
		internal int LastTab()
		{
			return this.leastRecentlyAccessedTabIndexes.FindLast(new Predicate<int>(TabPageCloseOrderManager.AlwaysTruePredicate));
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00002D77 File Offset: 0x00000F77
		private static bool AlwaysTruePredicate(int anyNumber)
		{
			return true;
		}

		// Token: 0x04000156 RID: 342
		private List<int> leastRecentlyAccessedTabIndexes = new List<int>();
	}
}
