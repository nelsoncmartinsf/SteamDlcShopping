namespace SteamDlcShopping
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
            this.lblGameCount = new System.Windows.Forms.Label();
            this.txtBlacklistSearch = new System.Windows.Forms.TextBox();
            this.lsbBlacklist = new System.Windows.Forms.ListBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblGameCount
            // 
            this.lblGameCount.AutoSize = true;
            this.lblGameCount.Location = new System.Drawing.Point(12, 243);
            this.lblGameCount.Name = "lblGameCount";
            this.lblGameCount.Size = new System.Drawing.Size(84, 15);
            this.lblGameCount.TabIndex = 0;
            this.lblGameCount.Text = "lblGameCount";
            // 
            // txtBlacklistSearch
            // 
            this.txtBlacklistSearch.Location = new System.Drawing.Point(12, 12);
            this.txtBlacklistSearch.Name = "txtBlacklistSearch";
            this.txtBlacklistSearch.Size = new System.Drawing.Size(200, 23);
            this.txtBlacklistSearch.TabIndex = 1;
            this.txtBlacklistSearch.TextChanged += new System.EventHandler(this.txtBlacklistSearch_TextChanged);
            // 
            // lsbBlacklist
            // 
            this.lsbBlacklist.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lsbBlacklist.FormattingEnabled = true;
            this.lsbBlacklist.ItemHeight = 15;
            this.lsbBlacklist.Location = new System.Drawing.Point(12, 41);
            this.lsbBlacklist.Name = "lsbBlacklist";
            this.lsbBlacklist.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lsbBlacklist.Size = new System.Drawing.Size(420, 199);
            this.lsbBlacklist.TabIndex = 2;
            this.lsbBlacklist.SelectedIndexChanged += new System.EventHandler(this.lsbBlacklist_SelectedIndexChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(357, 246);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // FrmBlacklist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 281);
            this.Controls.Add(this.lblGameCount);
            this.Controls.Add(this.txtBlacklistSearch);
            this.Controls.Add(this.lsbBlacklist);
            this.Controls.Add(this.btnRemove);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBlacklist";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blacklist";
            this.Load += new System.EventHandler(this.FrmBlacklist_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblGameCount;
        private TextBox txtBlacklistSearch;
        private ListBox lsbBlacklist;
        private Button btnRemove;
    }
}