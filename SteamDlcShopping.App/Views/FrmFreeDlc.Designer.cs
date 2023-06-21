namespace SteamDlcShopping.App.Views
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
            lsbDlc = new ListBox();
            SuspendLayout();
            // 
            // lsbDlc
            // 
            lsbDlc.Cursor = Cursors.Hand;
            lsbDlc.FormattingEnabled = true;
            lsbDlc.ItemHeight = 15;
            lsbDlc.Location = new Point(12, 12);
            lsbDlc.Name = "lsbDlc";
            lsbDlc.Size = new Size(660, 199);
            lsbDlc.TabIndex = 1;
            lsbDlc.MouseDoubleClick += LsbDlc_MouseDoubleClick;
            // 
            // FrmFreeDlc
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 223);
            Controls.Add(lsbDlc);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmFreeDlc";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Free DLC";
            Load += FrmFreeDlc_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListBox lsbDlc;
    }
}