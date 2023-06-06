namespace SteamDlcShopping.Views
{
    partial class FrmFreeDlc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lsbDlc = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lsbDlc
            // 
            this.lsbDlc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lsbDlc.FormattingEnabled = true;
            this.lsbDlc.ItemHeight = 15;
            this.lsbDlc.Location = new System.Drawing.Point(12, 12);
            this.lsbDlc.Name = "lsbDlc";
            this.lsbDlc.Size = new System.Drawing.Size(660, 199);
            this.lsbDlc.TabIndex = 1;
            this.lsbDlc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LsbDlc_MouseDoubleClick);
            // 
            // FrmFreeDlc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 223);
            this.Controls.Add(this.lsbDlc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFreeDlc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Free DLC";
            this.Load += new System.EventHandler(this.FrmFreeDlc_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox lsbDlc;
    }
}