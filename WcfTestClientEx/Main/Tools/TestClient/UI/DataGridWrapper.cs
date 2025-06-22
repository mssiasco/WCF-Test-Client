using System;
using System.Windows.Forms;
using WcfTestClientEx;

namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000048 RID: 72
	internal class DataGridWrapper : DataGrid
	{
		// Token: 0x06000234 RID: 564 RVA: 0x00003A55 File Offset: 0x00001C55
		public DataGridWrapper()
		{
			base.Navigate += this.DataGridWrapper_Navigate;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000A0B8 File Offset: 0x000082B8
		public override bool PreProcessMessage(ref Message msg)
		{
			bool flag;
			try
			{
				flag = base.PreProcessMessage(ref msg);
			}
			catch (ArgumentOutOfRangeException)
			{
				flag = false;
			}
			catch (IndexOutOfRangeException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00003A6F File Offset: 0x00001C6F
		protected void DataGridWrapper_Navigate(object sender, NavigateEventArgs ne)
		{
			this.isNavigatingForward = ne.Forward;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000A0F8 File Offset: 0x000082F8
		protected override void OnPaint(PaintEventArgs pe)
		{
			try
			{
				base.OnPaint(pe);
				this.isNavigatingForward = false;
			}
			catch (IndexOutOfRangeException)
			{
				if (!this.isNavigatingForward)
				{
					throw;
				}
				base.NavigateBack();
				this.isNavigatingForward = false;
				RtlAwareMessageBox.Show(StringResources.FailedToNavigateForward, StringResources.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
			}
		}

		// Token: 0x040000D8 RID: 216
		private bool isNavigatingForward;
	}
}
