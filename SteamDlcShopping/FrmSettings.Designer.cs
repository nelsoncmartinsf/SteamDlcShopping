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
            this.chkSteamIsInstalled = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblSteamApiKey
            // 
            this.lblSteamApiKey.AutoSize = true;
            this.lblSteamApiKey.Location = new System.Drawing.Point(12, 15);
            this.lblSteamApiKey.Name = "lblSteamApiKey";
            this.lblSteamApiKey.Size = new System.Drawing.Size(83, 15);
            this.lblSteamApiKey.TabIndex = 0;
            this.lblSteamApiKey.Text = "Steam API Key";
            // 
            // txtSteamApiKey
            // 
            this.txtSteamApiKey.Location = new System.Drawing.Point(107, 12);
            this.txtSteamApiKey.Name = "txtSteamApiKey";
            this.txtSteamApiKey.Size = new System.Drawing.Size(250, 23);
            this.txtSteamApiKey.TabIndex = 1;
            this.txtSteamApiKey.TextChanged += new System.EventHandler(this.txtSteamApiKey_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(233, 126);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(314, 126);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lnkSteamPath
            // 
            this.lnkSteamPath.AutoSize = true;
            this.lnkSteamPath.Enabled = false;
            this.lnkSteamPath.Location = new System.Drawing.Point(107, 72);
            this.lnkSteamPath.Name = "lnkSteamPath";
            this.lnkSteamPath.Size = new System.Drawing.Size(162, 15);
            this.lnkSteamPath.TabIndex = 3;
            this.lnkSteamPath.TabStop = true;
            this.lnkSteamPath.Text = "C:\\Program Files (x86)\\Steam";
            this.lnkSteamPath.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSteamPath_LinkClicked);
            // 
            // lblSteamPath
            // 
            this.lblSteamPath.AutoSize = true;
            this.lblSteamPath.Enabled = false;
            this.lblSteamPath.Location = new System.Drawing.Point(12, 72);
            this.lblSteamPath.Name = "lblSteamPath";
            this.lblSteamPath.Size = new System.Drawing.Size(67, 15);
            this.lblSteamPath.TabIndex = 0;
            this.lblSteamPath.Text = "Steam Path";
            // 
            // chkSteamIsInstalled
            // 
            this.chkSteamIsInstalled.AutoSize = true;
            this.chkSteamIsInstalled.Location = new System.Drawing.Point(12, 50);
            this.chkSteamIsInstalled.Name = "chkSteamIsInstalled";
            this.chkSteamIsInstalled.Size = new System.Drawing.Size(205, 19);
            this.chkSteamIsInstalled.TabIndex = 4;
            this.chkSteamIsInstalled.Text = "Steam is installed on this machine";
            this.chkSteamIsInstalled.UseVisualStyleBackColor = true;
            this.chkSteamIsInstalled.CheckedChanged += new System.EventHandler(this.chkSteamIsInstalled_CheckedChanged);
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 161);
            this.Controls.Add(this.chkSteamIsInstalled);
            this.Controls.Add(this.lnkSteamPath);
            this.Controls.Add(this.btnCancel);
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
        private CheckBox chkSteamIsInstalled;
    }
}