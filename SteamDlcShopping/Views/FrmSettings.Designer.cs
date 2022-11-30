namespace SteamDlcShopping.Views
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
            this.pbtSteamApiKey = new System.Windows.Forms.PictureBox();
            this.lblGetSteamApiKey = new System.Windows.Forms.Label();
            this.lnkGetSteamApiKey = new System.Windows.Forms.LinkLabel();
            this.chkAutoBlacklist = new System.Windows.Forms.CheckBox();
            this.ptbSmartLoading = new System.Windows.Forms.PictureBox();
            this.lblReminder = new System.Windows.Forms.Label();
            this.ddlReminder = new System.Windows.Forms.ComboBox();
            this.chkUseMemeLoading = new System.Windows.Forms.CheckBox();
            this.ddlGameDefaultSort = new System.Windows.Forms.ComboBox();
            this.lblGameDefaultSort = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbtSteamApiKey)).BeginInit();
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
            this.btnSave.Location = new System.Drawing.Point(232, 226);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(313, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.lnkGetSteamApiKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGetSteamApiKey_LinkClicked);
            // 
            // chkAutoBlacklist
            // 
            this.chkAutoBlacklist.AutoSize = true;
            this.chkAutoBlacklist.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAutoBlacklist.Location = new System.Drawing.Point(28, 65);
            this.chkAutoBlacklist.Name = "chkAutoBlacklist";
            this.chkAutoBlacklist.Size = new System.Drawing.Size(190, 19);
            this.chkAutoBlacklist.TabIndex = 3;
            this.chkAutoBlacklist.Text = "Auto Blacklist (Recommended)";
            this.chkAutoBlacklist.UseVisualStyleBackColor = true;
            this.chkAutoBlacklist.CheckedChanged += new System.EventHandler(this.chkAutoBlacklist_CheckedChanged);
            // 
            // ptbSmartLoading
            // 
            this.ptbSmartLoading.Location = new System.Drawing.Point(12, 66);
            this.ptbSmartLoading.Name = "ptbSmartLoading";
            this.ptbSmartLoading.Size = new System.Drawing.Size(16, 16);
            this.ptbSmartLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbSmartLoading.TabIndex = 6;
            this.ptbSmartLoading.TabStop = false;
            // 
            // lblReminder
            // 
            this.lblReminder.AutoSize = true;
            this.lblReminder.Location = new System.Drawing.Point(130, 93);
            this.lblReminder.Name = "lblReminder";
            this.lblReminder.Size = new System.Drawing.Size(156, 15);
            this.lblReminder.TabIndex = 0;
            this.lblReminder.Text = "Reminder to clear every new";
            // 
            // ddlReminder
            // 
            this.ddlReminder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlReminder.FormattingEnabled = true;
            this.ddlReminder.Items.AddRange(new object[] {
            "Week",
            "Month",
            "Year",
            "Big Bang"});
            this.ddlReminder.Location = new System.Drawing.Point(292, 90);
            this.ddlReminder.Name = "ddlReminder";
            this.ddlReminder.Size = new System.Drawing.Size(75, 23);
            this.ddlReminder.TabIndex = 4;
            // 
            // chkUseMemeLoading
            // 
            this.chkUseMemeLoading.AutoSize = true;
            this.chkUseMemeLoading.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseMemeLoading.Location = new System.Drawing.Point(28, 166);
            this.chkUseMemeLoading.Name = "chkUseMemeLoading";
            this.chkUseMemeLoading.Size = new System.Drawing.Size(182, 19);
            this.chkUseMemeLoading.TabIndex = 3;
            this.chkUseMemeLoading.Text = "Use meme loading animation";
            this.chkUseMemeLoading.UseVisualStyleBackColor = true;
            this.chkUseMemeLoading.CheckedChanged += new System.EventHandler(this.chkAutoBlacklist_CheckedChanged);
            // 
            // ddlGameDefaultSort
            // 
            this.ddlGameDefaultSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlGameDefaultSort.Items.AddRange(new object[] {
            "None",
            "Game ▲",
            "Game ▼",
            "Total Cost ▲",
            "Total Cost ▼",
            "DLC Left ▲",
            "DLC Left ▼",
            "Highest % ▲",
            "Highest % ▼"});
            this.ddlGameDefaultSort.Location = new System.Drawing.Point(156, 128);
            this.ddlGameDefaultSort.Name = "ddlGameDefaultSort";
            this.ddlGameDefaultSort.Size = new System.Drawing.Size(100, 23);
            this.ddlGameDefaultSort.TabIndex = 8;
            // 
            // lblGameDefaultSort
            // 
            this.lblGameDefaultSort.AutoSize = true;
            this.lblGameDefaultSort.Location = new System.Drawing.Point(28, 131);
            this.lblGameDefaultSort.Name = "lblGameDefaultSort";
            this.lblGameDefaultSort.Size = new System.Drawing.Size(122, 15);
            this.lblGameDefaultSort.TabIndex = 7;
            this.lblGameDefaultSort.Text = "Games default sort by";
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 261);
            this.Controls.Add(this.ddlGameDefaultSort);
            this.Controls.Add(this.lblGameDefaultSort);
            this.Controls.Add(this.ddlReminder);
            this.Controls.Add(this.lblReminder);
            this.Controls.Add(this.lnkGetSteamApiKey);
            this.Controls.Add(this.lblGetSteamApiKey);
            this.Controls.Add(this.ptbSmartLoading);
            this.Controls.Add(this.pbtSteamApiKey);
            this.Controls.Add(this.chkUseMemeLoading);
            this.Controls.Add(this.chkAutoBlacklist);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtSteamApiKey);
            this.Controls.Add(this.lblSteamApiKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbtSteamApiKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbSmartLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblSteamApiKey;
        private TextBox txtSteamApiKey;
        private Button btnSave;
        private Button btnCancel;
        private PictureBox pbtSteamApiKey;
        private Label lblGetSteamApiKey;
        private LinkLabel lnkGetSteamApiKey;
        private CheckBox chkAutoBlacklist;
        private PictureBox ptbSmartLoading;
        private Label lblReminder;
        private ComboBox ddlReminder;
        private CheckBox chkUseMemeLoading;
        private ComboBox ddlGameDefaultSort;
        private Label lblGameDefaultSort;
    }
}