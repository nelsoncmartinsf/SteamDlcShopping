namespace SteamDlcShopping
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.lblUsername = new System.Windows.Forms.Label();
            this.ptbAvatar = new System.Windows.Forms.PictureBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.lsvLibrary = new System.Windows.Forms.ListView();
            this.colGame = new System.Windows.Forms.ColumnHeader();
            this.ColCost = new System.Windows.Forms.ColumnHeader();
            this.colMaxDiscount = new System.Windows.Forms.ColumnHeader();
            this.lsvGame = new System.Windows.Forms.ListView();
            this.colDlc = new System.Windows.Forms.ColumnHeader();
            this.colPrice = new System.Windows.Forms.ColumnHeader();
            this.colDiscount = new System.Windows.Forms.ColumnHeader();
            this.lblLibraryCount = new System.Windows.Forms.Label();
            this.lblGameCount = new System.Windows.Forms.Label();
            this.lnkSteamPage = new System.Windows.Forms.LinkLabel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.txtLibrarySearch = new System.Windows.Forms.TextBox();
            this.ddlLibrarySort = new System.Windows.Forms.ComboBox();
            this.grbLibrary = new System.Windows.Forms.GroupBox();
            this.lblLibraryCost = new System.Windows.Forms.Label();
            this.chkHideGamesNotOnSale = new System.Windows.Forms.CheckBox();
            this.btnBlacklist = new System.Windows.Forms.Button();
            this.lbldebug = new System.Windows.Forms.Label();
            this.mnuMenu = new System.Windows.Forms.MenuStrip();
            this.smiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.smiBlacklist = new System.Windows.Forms.ToolStripMenuItem();
            this.smiFreeDlc = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.ptbAvatar)).BeginInit();
            this.grbLibrary.SuspendLayout();
            this.mnuMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(82, 24);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(73, 15);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "lblUsername";
            // 
            // ptbAvatar
            // 
            this.ptbAvatar.InitialImage = ((System.Drawing.Image)(resources.GetObject("ptbAvatar.InitialImage")));
            this.ptbAvatar.Location = new System.Drawing.Point(12, 27);
            this.ptbAvatar.Name = "ptbAvatar";
            this.ptbAvatar.Size = new System.Drawing.Size(64, 64);
            this.ptbAvatar.TabIndex = 1;
            this.ptbAvatar.TabStop = false;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(82, 42);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(163, 42);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 3;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // lsvLibrary
            // 
            this.lsvLibrary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colGame,
            this.ColCost,
            this.colMaxDiscount});
            this.lsvLibrary.FullRowSelect = true;
            this.lsvLibrary.GridLines = true;
            this.lsvLibrary.Location = new System.Drawing.Point(6, 51);
            this.lsvLibrary.Name = "lsvLibrary";
            this.lsvLibrary.Size = new System.Drawing.Size(581, 370);
            this.lsvLibrary.TabIndex = 4;
            this.lsvLibrary.UseCompatibleStateImageBehavior = false;
            this.lsvLibrary.View = System.Windows.Forms.View.Details;
            this.lsvLibrary.SelectedIndexChanged += new System.EventHandler(this.lsvLibrary_SelectedIndexChanged);
            this.lsvLibrary.EnabledChanged += new System.EventHandler(this.lsvLibrary_EnabledChanged);
            // 
            // colGame
            // 
            this.colGame.Text = "Game";
            this.colGame.Width = 420;
            // 
            // ColCost
            // 
            this.ColCost.Text = "Cost";
            this.ColCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ColCost.Width = 55;
            // 
            // colMaxDiscount
            // 
            this.colMaxDiscount.Text = "Max Discount";
            this.colMaxDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colMaxDiscount.Width = 85;
            // 
            // lsvGame
            // 
            this.lsvGame.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDlc,
            this.colPrice,
            this.colDiscount});
            this.lsvGame.FullRowSelect = true;
            this.lsvGame.GridLines = true;
            this.lsvGame.Location = new System.Drawing.Point(593, 51);
            this.lsvGame.MultiSelect = false;
            this.lsvGame.Name = "lsvGame";
            this.lsvGame.Size = new System.Drawing.Size(641, 370);
            this.lsvGame.TabIndex = 6;
            this.lsvGame.UseCompatibleStateImageBehavior = false;
            this.lsvGame.View = System.Windows.Forms.View.Details;
            this.lsvGame.EnabledChanged += new System.EventHandler(this.lsvGame_EnabledChanged);
            // 
            // colDlc
            // 
            this.colDlc.Text = "DLC";
            this.colDlc.Width = 500;
            // 
            // colPrice
            // 
            this.colPrice.Text = "Price";
            this.colPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colDiscount
            // 
            this.colDiscount.Text = "Discount";
            this.colDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblLibraryCount
            // 
            this.lblLibraryCount.AutoSize = true;
            this.lblLibraryCount.Location = new System.Drawing.Point(6, 431);
            this.lblLibraryCount.Name = "lblLibraryCount";
            this.lblLibraryCount.Size = new System.Drawing.Size(89, 15);
            this.lblLibraryCount.TabIndex = 0;
            this.lblLibraryCount.Text = "lblLibraryCount";
            // 
            // lblGameCount
            // 
            this.lblGameCount.AutoSize = true;
            this.lblGameCount.Location = new System.Drawing.Point(593, 431);
            this.lblGameCount.Name = "lblGameCount";
            this.lblGameCount.Size = new System.Drawing.Size(84, 15);
            this.lblGameCount.TabIndex = 0;
            this.lblGameCount.Text = "lblGameCount";
            // 
            // lnkSteamPage
            // 
            this.lnkSteamPage.AutoSize = true;
            this.lnkSteamPage.Location = new System.Drawing.Point(1162, 431);
            this.lnkSteamPage.Name = "lnkSteamPage";
            this.lnkSteamPage.Size = new System.Drawing.Size(72, 15);
            this.lnkSteamPage.TabIndex = 7;
            this.lnkSteamPage.TabStop = true;
            this.lnkSteamPage.Text = "Steam Page ";
            this.lnkSteamPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSteamPage_LinkClicked);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(82, 68);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // txtLibrarySearch
            // 
            this.txtLibrarySearch.Location = new System.Drawing.Point(6, 22);
            this.txtLibrarySearch.Name = "txtLibrarySearch";
            this.txtLibrarySearch.Size = new System.Drawing.Size(200, 23);
            this.txtLibrarySearch.TabIndex = 1;
            this.txtLibrarySearch.TextChanged += new System.EventHandler(this.txtLibrarySearch_TextChanged);
            // 
            // ddlLibrarySort
            // 
            this.ddlLibrarySort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlLibrarySort.FormattingEnabled = true;
            this.ddlLibrarySort.Items.AddRange(new object[] {
            "Cheapest Cost",
            "Highest Discount"});
            this.ddlLibrarySort.Location = new System.Drawing.Point(437, 22);
            this.ddlLibrarySort.Name = "ddlLibrarySort";
            this.ddlLibrarySort.Size = new System.Drawing.Size(150, 23);
            this.ddlLibrarySort.TabIndex = 3;
            this.ddlLibrarySort.SelectedIndexChanged += new System.EventHandler(this.ddlLibrarySort_SelectedIndexChanged);
            // 
            // grbLibrary
            // 
            this.grbLibrary.Controls.Add(this.lblLibraryCost);
            this.grbLibrary.Controls.Add(this.chkHideGamesNotOnSale);
            this.grbLibrary.Controls.Add(this.btnBlacklist);
            this.grbLibrary.Controls.Add(this.txtLibrarySearch);
            this.grbLibrary.Controls.Add(this.ddlLibrarySort);
            this.grbLibrary.Controls.Add(this.lblLibraryCount);
            this.grbLibrary.Controls.Add(this.lblGameCount);
            this.grbLibrary.Controls.Add(this.lnkSteamPage);
            this.grbLibrary.Controls.Add(this.lsvLibrary);
            this.grbLibrary.Controls.Add(this.lsvGame);
            this.grbLibrary.Location = new System.Drawing.Point(12, 97);
            this.grbLibrary.Name = "grbLibrary";
            this.grbLibrary.Size = new System.Drawing.Size(1240, 456);
            this.grbLibrary.TabIndex = 4;
            this.grbLibrary.TabStop = false;
            this.grbLibrary.Text = "Library";
            this.grbLibrary.EnabledChanged += new System.EventHandler(this.grbLibrary_EnabledChanged);
            // 
            // lblLibraryCost
            // 
            this.lblLibraryCost.AutoSize = true;
            this.lblLibraryCost.Location = new System.Drawing.Point(212, 431);
            this.lblLibraryCost.Name = "lblLibraryCost";
            this.lblLibraryCost.Size = new System.Drawing.Size(80, 15);
            this.lblLibraryCost.TabIndex = 0;
            this.lblLibraryCost.Text = "lblLibraryCost";
            // 
            // chkHideGamesNotOnSale
            // 
            this.chkHideGamesNotOnSale.AutoSize = true;
            this.chkHideGamesNotOnSale.Location = new System.Drawing.Point(212, 24);
            this.chkHideGamesNotOnSale.Name = "chkHideGamesNotOnSale";
            this.chkHideGamesNotOnSale.Size = new System.Drawing.Size(150, 19);
            this.chkHideGamesNotOnSale.TabIndex = 2;
            this.chkHideGamesNotOnSale.Text = "Hide games not on sale";
            this.chkHideGamesNotOnSale.UseVisualStyleBackColor = true;
            this.chkHideGamesNotOnSale.CheckedChanged += new System.EventHandler(this.chkHideGamesNotOnSale_CheckedChanged);
            // 
            // btnBlacklist
            // 
            this.btnBlacklist.Location = new System.Drawing.Point(487, 427);
            this.btnBlacklist.Name = "btnBlacklist";
            this.btnBlacklist.Size = new System.Drawing.Size(100, 23);
            this.btnBlacklist.TabIndex = 5;
            this.btnBlacklist.Text = "Add to Blacklist";
            this.btnBlacklist.UseVisualStyleBackColor = true;
            this.btnBlacklist.Click += new System.EventHandler(this.btnBlacklist_Click);
            // 
            // lbldebug
            // 
            this.lbldebug.AutoSize = true;
            this.lbldebug.Location = new System.Drawing.Point(605, 24);
            this.lbldebug.Name = "lbldebug";
            this.lbldebug.Size = new System.Drawing.Size(38, 15);
            this.lbldebug.TabIndex = 0;
            this.lbldebug.Text = "label1";
            // 
            // mnuMenu
            // 
            this.mnuMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiSettings,
            this.smiBlacklist,
            this.smiFreeDlc});
            this.mnuMenu.Location = new System.Drawing.Point(0, 0);
            this.mnuMenu.Name = "mnuMenu";
            this.mnuMenu.Size = new System.Drawing.Size(1264, 24);
            this.mnuMenu.TabIndex = 0;
            this.mnuMenu.Text = "menuStrip1";
            // 
            // smiSettings
            // 
            this.smiSettings.Name = "smiSettings";
            this.smiSettings.Size = new System.Drawing.Size(61, 20);
            this.smiSettings.Text = "Settings";
            this.smiSettings.Click += new System.EventHandler(this.smiSettings_Click);
            // 
            // smiBlacklist
            // 
            this.smiBlacklist.Name = "smiBlacklist";
            this.smiBlacklist.Size = new System.Drawing.Size(62, 20);
            this.smiBlacklist.Text = "Blacklist";
            this.smiBlacklist.Click += new System.EventHandler(this.smiBlacklist_Click);
            // 
            // smiFreeDlc
            // 
            this.smiFreeDlc.Name = "smiFreeDlc";
            this.smiFreeDlc.Size = new System.Drawing.Size(66, 20);
            this.smiFreeDlc.Text = "Free DLC";
            this.smiFreeDlc.Click += new System.EventHandler(this.smiFreeDlc_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 565);
            this.Controls.Add(this.lbldebug);
            this.Controls.Add(this.grbLibrary);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.ptbAvatar);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.mnuMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mnuMenu;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Steam DLC Shopping";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ptbAvatar)).EndInit();
            this.grbLibrary.ResumeLayout(false);
            this.grbLibrary.PerformLayout();
            this.mnuMenu.ResumeLayout(false);
            this.mnuMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblUsername;
        private PictureBox ptbAvatar;
        private Button btnLogin;
        private Button btnCalculate;
        private ListView lsvLibrary;
        private ColumnHeader colGame;
        private ListView lsvGame;
        private ColumnHeader colDlc;
        private ColumnHeader colPrice;
        private ColumnHeader colDiscount;
        private Label lblLibraryCount;
        private Label lblGameCount;
        private LinkLabel lnkSteamPage;
        private Button btnLogout;
        private TextBox txtLibrarySearch;
        private ComboBox ddlLibrarySort;
        private ColumnHeader ColCost;
        private ColumnHeader colMaxDiscount;
        private GroupBox grbLibrary;
        private Label lbldebug;
        private MenuStrip mnuMenu;
        private ToolStripMenuItem smiSettings;
        private CheckBox chkHideGamesNotOnSale;
        private Label lblLibraryCost;
        private Button btnBlacklist;
        private ToolStripMenuItem smiBlacklist;
        private ToolStripMenuItem smiFreeDlc;
    }
}