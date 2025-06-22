using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000054 RID: 84
	internal partial class StartPage : TabPage
    {
		// Token: 0x060002C9 RID: 713 RVA: 0x00004332 File Offset: 0x00002532
		public StartPage()
		{
			this.InitializeComponent();
			this.hintTextBox.BackColor = this.BackColor;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00004351 File Offset: 0x00002551
		private void hintTextBox_GotFocus(object sender, EventArgs e)
		{
			this.hintTextBox.Select(0, 0);
		}

		

	}
}
