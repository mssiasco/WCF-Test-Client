using System.ComponentModel;
using System.Windows.Forms;

namespace Main.Tools.TestClient.UI
{
	// Token: 0x02000052 RID: 82
	internal partial class StartPage : TabPage
	{
        // Token: 0x060002B6 RID: 694 RVA: 0x0000418E File Offset: 0x0000238E

        // Token: 0x04000153 RID: 339
        private IContainer components;

        // Token: 0x04000154 RID: 340
        private PictureBox hintImagePictureBox;

        // Token: 0x04000155 RID: 341
        private TextBox hintTextBox;


        // Token: 0x060002CB RID: 715 RVA: 0x00004360 File Offset: 0x00002560
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Token: 0x060002CC RID: 716 RVA: 0x0000DED8 File Offset: 0x0000C0D8

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main.Tools.TestClient.UI.StartPage));
            this.hintImagePictureBox = new System.Windows.Forms.PictureBox();
            this.hintTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)this.hintImagePictureBox).BeginInit();
            base.SuspendLayout();
            resources.ApplyResources(this.hintImagePictureBox, "hintImagePictureBox");
            this.hintImagePictureBox.Name = "hintImagePictureBox";
            this.hintImagePictureBox.TabStop = false;
            this.hintTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.hintTextBox, "hintTextBox");
            this.hintTextBox.Name = "hintTextBox";
            this.hintTextBox.ReadOnly = true;
            this.hintTextBox.GotFocus += new System.EventHandler(hintTextBox_GotFocus);
            resources.ApplyResources(this, "$this");
            base.Controls.Add(this.hintTextBox);
            base.Controls.Add(this.hintImagePictureBox);
            base.Name = "StartPage";
            ((System.ComponentModel.ISupportInitialize)this.hintImagePictureBox).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

    }
}
