namespace SteamDlcShopping
{
    partial class FrmSettings
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
            this.lblSteamApiKey = new System.Windows.Forms.Label();
            this.txtSteamApiKey = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lnkSteamPath = new System.Windows.Forms.LinkLabel();
            this.lblSteamPath = new System.Windows.Forms.Label();
            this.pbtSteamApiKey = new System.Windows.Forms.PictureBox();
            this.ptbSteamIsInstalled = new System.Windows.Forms.PictureBox();
            this.lblGetSteamApiKey = new System.Windows.Forms.Label();
            this.lnkGetSteamApiKey = new System.Windows.Forms.LinkLabel();
            this.chkAutoBlacklist = new System.Windows.Forms.CheckBox();
            this.ptbSmartLoading = new System.Windows.Forms.PictureBox();
            this.chkSteamIsInstalled = new System.Windows.Forms.CheckBox();
            this.lsbBlacklist = new System.Windows.Forms.ListBox();
            this.txtBlacklistSearch = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbtSteamApiKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbSteamIsInstalled)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbSmartLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSteamApiKey
            // 
            this.lblSteamApiKey.AutoSize = true;
            this.lblSteamApiKey.Location = new System.Drawing.Point(28, 15);
            this.lblSteamApiKey.Name = "lblSteamApiKey";
            this.lblSteamApiKey.Size = new System.Drawing.Size(83, 15);
            this.lblSteamApiKey.TabIndex = 0;
            this.lblSteamApiKey.Text = "Steam API Key";
            // 
            // txtSteamApiKey
            // 
            this.txtSteamApiKey.Location = new System.Drawing.Point(117, 12);
            this.txtSteamApiKey.Name = "txtSteamApiKey";
            this.txtSteamApiKey.Size = new System.Drawing.Size(250, 23);
            this.txtSteamApiKey.TabIndex = 1;
            this.txtSteamApiKey.TextChanged += new System.EventHandler(this.txtSteamApiKey_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(276, 401);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(357, 401);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lnkSteamPath
            // 
            this.lnkSteamPath.AutoSize = true;
            this.lnkSteamPath.Enabled = false;
            this.lnkSteamPath.Location = new System.Drawing.Point(117, 87);
            this.lnkSteamPath.Name = "lnkSteamPath";
            this.lnkSteamPath.Size = new System.Drawing.Size(162, 15);
            this.lnkSteamPath.TabIndex = 4;
            this.lnkSteamPath.TabStop = true;
            this.lnkSteamPath.Text = "C:\\Program Files (x86)\\Steam";
            this.lnkSteamPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSteamPath_LinkClicked);
            // 
            // lblSteamPath
            // 
            this.lblSteamPath.AutoSize = true;
            this.lblSteamPath.Enabled = false;
            this.lblSteamPath.Location = new System.Drawing.Point(44, 87);
            this.lblSteamPath.Name = "lblSteamPath";
            this.lblSteamPath.Size = new System.Drawing.Size(67, 15);
            this.lblSteamPath.TabIndex = 0;
            this.lblSteamPath.Text = "Steam Path";
            // 
            // pbtSteamApiKey
            // 
            this.pbtSteamApiKey.Location = new System.Drawing.Point(12, 14);
            this.pbtSteamApiKey.Name = "pbtSteamApiKey";
            this.pbtSteamApiKey.Size = new System.Drawing.Size(16, 16);
            this.pbtSteamApiKey.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbtSteamApiKey.TabIndex = 5;
            this.pbtSteamApiKey.TabStop = false;
            // 
            // ptbSteamIsInstalled
            // 
            this.ptbSteamIsInstalled.Location = new System.Drawing.Point(12, 66);
            this.ptbSteamIsInstalled.Name = "ptbSteamIsInstalled";
            this.ptbSteamIsInstalled.Size = new System.Drawing.Size(16, 16);
            this.ptbSteamIsInstalled.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbSteamIsInstalled.TabIndex = 6;
            this.ptbSteamIsInstalled.TabStop = false;
            // 
            // lblGetSteamApiKey
            // 
            this.lblGetSteamApiKey.AutoSize = true;
            this.lblGetSteamApiKey.Location = new System.Drawing.Point(210, 38);
            this.lblGetSteamApiKey.Name = "lblGetSteamApiKey";
            this.lblGetSteamApiKey.Size = new System.Drawing.Size(131, 15);
            this.lblGetSteamApiKey.TabIndex = 0;
            this.lblGetSteamApiKey.Text = "Get your Steam API Key";
            // 
            // lnkGetSteamApiKey
            // 
            this.lnkGetSteamApiKey.AutoSize = true;
            this.lnkGetSteamApiKey.Location = new System.Drawing.Point(337, 38);
            this.lnkGetSteamApiKey.Name = "lnkGetSteamApiKey";
            this.lnkGetSteamApiKey.Size = new System.Drawing.Size(30, 15);
            this.lnkGetSteamApiKey.TabIndex = 2;
            this.lnkGetSteamApiKey.TabStop = true;
            this.lnkGetSteamApiKey.Text = "here";
            // 
            // chkAutoBlacklist
            // 
            this.chkAutoBlacklist.AutoSize = true;
            this.chkAutoBlacklist.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAutoBlacklist.Location = new System.Drawing.Point(28, 114);
            this.chkAutoBlacklist.Name = "chkAutoBlacklist";
            this.chkAutoBlacklist.Size = new System.Drawing.Size(98, 19);
            this.chkAutoBlacklist.TabIndex = 3;
            this.chkAutoBlacklist.Text = "Auto Blacklist";
            this.chkAutoBlacklist.UseVisualStyleBackColor = true;
            // 
            // ptbSmartLoading
            // 
            this.ptbSmartLoading.Location = new System.Drawing.Point(12, 115);
            this.ptbSmartLoading.Name = "ptbSmartLoading";
            this.ptbSmartLoading.Size = new System.Drawing.Size(16, 16);
            this.ptbSmartLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbSmartLoading.TabIndex = 6;
            this.ptbSmartLoading.TabStop = false;
            // 
            // chkSteamIsInstalled
            // 
            this.chkSteamIsInstalled.AutoSize = true;
            this.chkSteamIsInstalled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSteamIsInstalled.Location = new System.Drawing.Point(28, 65);
            this.chkSteamIsInstalled.Name = "chkSteamIsInstalled";
            this.chkSteamIsInstalled.Size = new System.Drawing.Size(205, 19);
            this.chkSteamIsInstalled.TabIndex = 3;
            this.chkSteamIsInstalled.Text = "Steam is installed on this machine";
            this.chkSteamIsInstalled.UseVisualStyleBackColor = true;
            this.chkSteamIsInstalled.CheckedChanged += new System.EventHandler(this.chkSteamIsInstalled_CheckedChanged);
            // 
            // lsbBlacklist
            // 
            this.lsbBlacklist.FormattingEnabled = true;
            this.lsbBlacklist.ItemHeight = 15;
            this.lsbBlacklist.Location = new System.Drawing.Point(12, 177);
            this.lsbBlacklist.Name = "lsbBlacklist";
            this.lsbBlacklist.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lsbBlacklist.Size = new System.Drawing.Size(420, 199);
            this.lsbBlacklist.TabIndex = 7;
            // 
            // txtBlacklistSearch
            // 
            this.txtBlacklistSearch.Location = new System.Drawing.Point(12, 148);
            this.txtBlacklistSearch.Name = "txtBlacklistSearch";
            this.txtBlacklistSearch.Size = new System.Drawing.Size(200, 23);
            this.txtBlacklistSearch.TabIndex = 8;
            this.txtBlacklistSearch.TextChanged += new System.EventHandler(this.txtBlacklistSearch_TextChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(357, 148);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 436);
            this.Controls.Add(this.txtBlacklistSearch);
            this.Controls.Add(this.lsbBlacklist);
            this.Controls.Add(this.lnkGetSteamApiKey);
            this.Controls.Add(this.lblGetSteamApiKey);
            this.Controls.Add(this.ptbSmartLoading);
            this.Controls.Add(this.ptbSteamIsInstalled);
            this.Controls.Add(this.pbtSteamApiKey);
            this.Controls.Add(this.chkAutoBlacklist);
            this.Controls.Add(this.chkSteamIsInstalled);
            this.Controls.Add(this.lnkSteamPath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtSteamApiKey);
            this.Controls.Add(this.lblSteamPath);
            this.Controls.Add(this.lblSteamApiKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSettings_FormClosing);
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbtSteamApiKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbSteamIsInstalled)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbSmartLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblSteamApiKey;
        private TextBox txtSteamApiKey;
        private Button btnSave;
        private Button btnCancel;
        private LinkLabel lnkSteamPath;
        private Label lblSteamPath;
        private PictureBox pbtSteamApiKey;
        private PictureBox ptbSteamIsInstalled;
        private Label lblGetSteamApiKey;
        private LinkLabel lnkGetSteamApiKey;
        private CheckBox chkAutoBlacklist;
        private PictureBox ptbSmartLoading;
        private CheckBox chkSteamIsInstalled;
        private ListBox lsbBlacklist;
        private TextBox txtBlacklistSearch;
        private Button btnRemove;
    }
}