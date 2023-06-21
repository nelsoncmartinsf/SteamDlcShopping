namespace SteamDlcShopping.App.Views
{
    partial class FrmBlacklist
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
            lblGameCount = new Label();
            txtBlacklistSearch = new TextBox();
            lsbBlacklist = new ListBox();
            btnRemove = new Button();
            btnClearAutoBlacklisted = new Button();
            chkHideAutoBlacklistedGames = new CheckBox();
            SuspendLayout();
            // 
            // lblGameCount
            // 
            lblGameCount.AutoSize = true;
            lblGameCount.Location = new Point(12, 243);
            lblGameCount.Name = "lblGameCount";
            lblGameCount.Size = new Size(84, 15);
            lblGameCount.TabIndex = 0;
            lblGameCount.Text = "lblGameCount";
            // 
            // txtBlacklistSearch
            // 
            txtBlacklistSearch.Location = new Point(12, 12);
            txtBlacklistSearch.Name = "txtBlacklistSearch";
            txtBlacklistSearch.Size = new Size(200, 23);
            txtBlacklistSearch.TabIndex = 1;
            txtBlacklistSearch.TextChanged += TxtBlacklistSearch_TextChanged;
            // 
            // lsbBlacklist
            // 
            lsbBlacklist.Cursor = Cursors.Hand;
            lsbBlacklist.FormattingEnabled = true;
            lsbBlacklist.ItemHeight = 15;
            lsbBlacklist.Location = new Point(12, 41);
            lsbBlacklist.Name = "lsbBlacklist";
            lsbBlacklist.SelectionMode = SelectionMode.MultiExtended;
            lsbBlacklist.Size = new Size(610, 199);
            lsbBlacklist.TabIndex = 3;
            lsbBlacklist.SelectedIndexChanged += LsbBlacklist_SelectedIndexChanged;
            // 
            // btnRemove
            // 
            btnRemove.Location = new Point(411, 246);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(75, 23);
            btnRemove.TabIndex = 4;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += BtnRemove_Click;
            // 
            // btnClearAutoBlacklisted
            // 
            btnClearAutoBlacklisted.Location = new Point(492, 246);
            btnClearAutoBlacklisted.Name = "btnClearAutoBlacklisted";
            btnClearAutoBlacklisted.Size = new Size(130, 23);
            btnClearAutoBlacklisted.TabIndex = 5;
            btnClearAutoBlacklisted.Text = "Clear Auto Blacklisted";
            btnClearAutoBlacklisted.UseVisualStyleBackColor = true;
            btnClearAutoBlacklisted.Click += BtnClearAutoBlacklisted_Click;
            // 
            // chkHideAutoBlacklistedGames
            // 
            chkHideAutoBlacklistedGames.AutoSize = true;
            chkHideAutoBlacklistedGames.Location = new Point(218, 16);
            chkHideAutoBlacklistedGames.Name = "chkHideAutoBlacklistedGames";
            chkHideAutoBlacklistedGames.Size = new Size(178, 19);
            chkHideAutoBlacklistedGames.TabIndex = 2;
            chkHideAutoBlacklistedGames.Text = "Hide Auto Blacklisted Games";
            chkHideAutoBlacklistedGames.UseVisualStyleBackColor = true;
            chkHideAutoBlacklistedGames.CheckedChanged += ChkHideAutoBlacklistedGames_CheckedChanged;
            // 
            // FrmBlacklist
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(634, 281);
            Controls.Add(chkHideAutoBlacklistedGames);
            Controls.Add(lblGameCount);
            Controls.Add(txtBlacklistSearch);
            Controls.Add(lsbBlacklist);
            Controls.Add(btnClearAutoBlacklisted);
            Controls.Add(btnRemove);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmBlacklist";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Blacklist";
            Load += FrmBlacklist_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblGameCount;
        private TextBox txtBlacklistSearch;
        private ListBox lsbBlacklist;
        private Button btnRemove;
        private Button btnClearAutoBlacklisted;
        private CheckBox chkHideAutoBlacklistedGames;
    }
}