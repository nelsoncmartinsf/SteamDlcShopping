namespace SteamDlcShopping
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
            this.lsbFreeDlc = new System.Windows.Forms.ListBox();
            this.lnkStorePage = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lsbFreeDlc
            // 
            this.lsbFreeDlc.FormattingEnabled = true;
            this.lsbFreeDlc.ItemHeight = 15;
            this.lsbFreeDlc.Location = new System.Drawing.Point(12, 12);
            this.lsbFreeDlc.Name = "lsbFreeDlc";
            this.lsbFreeDlc.Size = new System.Drawing.Size(660, 199);
            this.lsbFreeDlc.TabIndex = 1;
            this.lsbFreeDlc.SelectedIndexChanged += new System.EventHandler(this.lsbFreeDlc_SelectedIndexChanged);
            // 
            // lnkStorePage
            // 
            this.lnkStorePage.AutoSize = true;
            this.lnkStorePage.Location = new System.Drawing.Point(606, 214);
            this.lnkStorePage.Name = "lnkStorePage";
            this.lnkStorePage.Size = new System.Drawing.Size(66, 15);
            this.lnkStorePage.TabIndex = 2;
            this.lnkStorePage.TabStop = true;
            this.lnkStorePage.Text = "Store Page ";
            this.lnkStorePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkStorePage_LinkClicked);
            // 
            // FrmFreeDlc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 238);
            this.Controls.Add(this.lnkStorePage);
            this.Controls.Add(this.lsbFreeDlc);
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
            this.PerformLayout();

        }

        #endregion

        private ListBox lsbFreeDlc;
        private LinkLabel lnkStorePage;
    }
}