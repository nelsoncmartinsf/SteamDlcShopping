﻿using SteamDlcShopping.Core.Controllers;
using SteamDlcShopping.Core.ViewModels;
using SteamDlcShopping.Extensibility;
using SteamDlcShopping.Properties;
using System.Diagnostics;
using Timer = System.Threading.Timer;

namespace SteamDlcShopping
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        //////////////////////////////////////// FORM ////////////////////////////////////////

        private void FrmMain_Load(object sender, EventArgs e)
        {
            ucLoad ucLoad = new()
            {
                Name = "ucLoad",
                Location = new Point(0, 0)
            };

            Controls.Add(ucLoad);
            ucLoad.BringToFront();
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            SteamProfileController.Login(Settings.Default.SteamApiKey, Settings.Default.SessionId, Settings.Default.SteamLoginSecure);
            BlacklistController.Load();
            SetControlsState();

            Controls["ucLoad"].Visible = false;

            AutoBlacklistReminder();
        }

        private void tmrLibrary_Tick()
        {
            Invoke(new Action(() =>
            {
                smiSettings.Enabled = false;
                smiBlacklist.Enabled = false;
                smiFreeDlc.Enabled = false;
                btnLogout.Enabled = false;
                btnCalculate.Enabled = false;

                UnloadGames();
                UnloadDlc();

                ucCalculate ucCalculate = new()
                {
                    Name = "ucCalculate",
                    Location = grbLibrary.Location
                };

                Controls.Add(ucCalculate);
                ucCalculate.BringToFront();
            }));

            Stopwatch timer = Stopwatch.StartNew();//DEBUG

            LibraryController.Calculate(Settings.Default.SteamApiKey, Settings.Default.SessionId, Settings.Default.SteamLoginSecure, Settings.Default.AutoBlacklist);

            timer.Stop();//DEBUG
            lbldebug.Invoke(new Action(() => lbldebug.Text = $"{timer.Elapsed}"));//DEBUG

            Invoke(new Action(() =>
            {
                LoadGames();

                smiSettings.Enabled = true;
                smiBlacklist.Enabled = BlacklistController.HasGames;
                smiFreeDlc.Enabled = LibraryController.FreeDlcExist;
                btnLogout.Enabled = true;
                btnCalculate.Enabled = true;
                Controls["ucCalculate"].Dispose();
            }));
        }

        //////////////////////////////////////// MENU BAR ////////////////////////////////////////

        private void smiSettings_Click(object sender, EventArgs e)
        {
            FrmSettings form = new();
            form.ShowDialog();
            form.Dispose();
        }

        private void smiBlacklist_Click(object sender, EventArgs e)
        {
            FrmBlacklist form = new();
            form.ShowDialog();
            form.Dispose();

            smiBlacklist.Enabled = BlacklistController.HasGames;
        }

        private void smiFreeDlc_Click(object sender, EventArgs e)
        {
            FrmFreeDlc form = new();
            form.ShowDialog();
            form.Dispose();

            smiFreeDlc.Enabled = LibraryController.FreeDlcExist;
        }

        //////////////////////////////////////// BUTTONS ////////////////////////////////////////

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FrmLogin form = new();
            form.ShowDialog();
            form.Dispose();

            Controls["ucLoad"].Visible = true;

            SteamProfileController.Login(Settings.Default.SteamApiKey, Settings.Default.SessionId, Settings.Default.SteamLoginSecure);
            SetControlsState();

            Controls["ucLoad"].Visible = false;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            UnloadGames();
            UnloadDlc();

            Settings.Default.SessionId = null;
            Settings.Default.SteamLoginSecure = null;
            Settings.Default.Save();

            SteamProfileController.Logout();
            SetControlsState();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //Set a timer worker thread
            Timer tmrLibrary = new(_ => tmrLibrary_Tick(), null, 0, Timeout.Infinite);
        }

        private void btnBlacklist_Click(object sender, EventArgs e)
        {
            List<int> appIds = new();

            foreach (ListViewItem item in lsvGame.SelectedItems)
            {
                if (!int.TryParse(item.Tag.ToString(), out int appId))
                {
                    continue;
                }

                appIds.Add(appId);
            }

            LoadGames();
            UnloadDlc();

            BlacklistController.AddGames(appIds, false);
            smiBlacklist.Enabled = true;

            smiFreeDlc.Enabled = LibraryController.FreeDlcExist;
        }

        //////////////////////////////////////// GAME LIST ////////////////////////////////////////

        private int _selectedGame;

        private void LoadGames()
        {
            LibraryView library = LibraryController.GetGames(txtGameSearch.Text, chkHideGamesNotOnSale.Checked);

            if (library.Games is null)
            {
                return;
            }

            lsvGame_Load(library.Games);
            lsvGame.Sort();

            lsvGame.Enabled = true;
            txtGameSearch.Enabled = true;
            chkHideGamesNotOnSale.Enabled = true;

            lblGameCount.Text = $"Count: {lsvGame.Items.Count}";
            lblLibraryCost.Text = $"Cost: {library.TotalCost}€";
            btnBlacklist.Visible = false;
        }

        private void UnloadGames()
        {
            lsvGame.Unload();
            lsvGame.Enabled = false;

            _gamefilterEvents = false;

            txtGameSearch.Enabled = false;
            txtGameSearch.Text = null;

            _gamefilterEvents = true;

            chkHideGamesNotOnSale.Enabled = false;

            lblGameCount.Text = null;
            lblLibraryCost.Text = null;
            btnBlacklist.Visible = false;
        }

        private void lsvGame_Load(List<GameView> games)
        {
            lsvGame.Items.Clear();

            lsvGame.BeginUpdate();

            foreach (GameView game in games)
            {
                ListViewItem item;
                ListViewItem.ListViewSubItem subItem;

                //Game
                item = new() { Text = game.Name, Tag = game.AppId };
                item.SubItems[0].Tag = Types.String;

                //Cost
                subItem = new() { Text = game.DlcTotalPrice, Tag = Types.Decimal };
                item.SubItems.Add(subItem);

                //DLC Left
                subItem = new() { Text = game.DlcLeft.ToString(), Tag = Types.Decimal };
                item.SubItems.Add(subItem);

                //Max Discount
                subItem = new() { Text = game.DlcHighestPercentage, Tag = Types.Decimal };
                item.SubItems.Add(subItem);

                lsvGame.Items.Add(item);
            }

            lsvGame.EndUpdate();

            if (lsvGame.ListViewItemSorter is null)
            {
                lsvGame._columnSorter = new();
                lsvGame.ListViewItemSorter = lsvGame._columnSorter;
            }
        }

        private void lsvGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            //No selected game
            if (lsvGame.SelectedIndices.Count == 0)
            {
                _selectedGame = 0;
                btnBlacklist.Visible = false;
                UnloadDlc();
                return;
            }

            btnBlacklist.Visible = true;

            //Multiple games selected
            if (lsvGame.SelectedIndices.Count > 1)
            {
                _selectedGame = 0;
                UnloadDlc();
                return;
            }

            //Parse the item tag
            if (!int.TryParse(lsvGame.SelectedItems[0].Tag.ToString(), out int newGame))
            {
                _selectedGame = 0;
                return;
            }

            //Check if it's a different game from the one already selected
            if (newGame == _selectedGame)
            {
                return;
            }

            _selectedGame = newGame;
            LoadDlc();
        }

        private void lsvGame_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (lsvGame._columnSorter is null)
            {
                return;
            }

            lsvGame.SortList(e.Column);
        }

        private void lsvGame_DoubleClick(object sender, EventArgs e)
        {
            ClickLink($"https://store.steampowered.com/app/{lsvGame.SelectedItems[0].Tag}");
        }

        //////////////////////////////////////// DLC LIST ////////////////////////////////////////

        private void LoadDlc()
        {
            List<DlcView> dlcList = LibraryController.GetDlc(_selectedGame, txtDlcSearch.Text, chkHideDlcNotOnSale.Checked, chkHideDlcOwned.Checked);

            lsvDlc_Load(dlcList);
            lsvDlc.Sort();

            lsvDlc.Enabled = true;
            txtDlcSearch.Enabled = true;
            chkHideDlcOwned.Enabled = true;

            lblDlcCount.Text = $"Count: {lsvDlc.Items.Count}";

            if (LibraryController.GameHasTooManyDlc(_selectedGame))
            {
                lblTooManyDlc.Visible = true;
                lnkTooManyDlc.Visible = true;
            }
        }

        private void UnloadDlc()
        {
            lsvDlc.Unload();
            lsvDlc.Enabled = false;

            _dlcFilterEvents = false;

            txtDlcSearch.Enabled = false;
            txtDlcSearch.Text = null;

            _dlcFilterEvents = true;

            chkHideDlcOwned.Enabled = false;

            lblDlcCount.Text = null;

            lblTooManyDlc.Visible = false;
            lnkTooManyDlc.Visible = false;
        }

        private void lsvDlc_Load(List<DlcView> dlcs)
        {
            lsvDlc.Items.Clear();

            lsvDlc.BeginUpdate();

            foreach (DlcView dlc in dlcs)
            {
                ListViewItem item;
                ListViewItem.ListViewSubItem subItem;

                //DLC
                item = new() { Tag = dlc.AppId, Text = dlc.Name };
                item.SubItems[0].Tag = Types.String;

                //Price
                subItem = new() { Text = dlc.Price, Tag = Types.Decimal };
                item.SubItems.Add(subItem);

                //Discount
                subItem = new() { Text = dlc.Discount, Tag = Types.Decimal };
                item.SubItems.Add(subItem);

                item.BackColor = dlc.IsOwned ? Color.LightGreen : item.BackColor;
                lsvDlc.Items.Add(item);
            }

            lsvDlc.EndUpdate();

            if (lsvDlc.ListViewItemSorter is null)
            {
                lsvDlc._columnSorter = new();
                lsvDlc.ListViewItemSorter = lsvDlc._columnSorter;
            }
        }

        private void lsvDlc_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (lsvDlc._columnSorter is null)
            {
                return;
            }

            lsvDlc.SortList(e.Column);
        }

        private void lsvDlc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClickLink($"https://store.steampowered.com/app/{lsvDlc.SelectedItems[0].Tag}");
        }

        private void lnkTooManyDlc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClickLink($"https://store.steampowered.com/dlc/{_selectedGame}");
        }

        //////////////////////////////////////// FILTERS ////////////////////////////////////////

        private bool _gamefilterEvents;
        private bool _dlcFilterEvents;

        private void lsvGame_FilterChanged(object sender, EventArgs e)
        {
            if (!_gamefilterEvents)
            {
                return;
            }

            LoadGames();
            UnloadDlc();
        }

        private void lsvDlc_FilterChanged(object sender, EventArgs e)
        {
            if (!_dlcFilterEvents)
            {
                return;
            }

            LoadDlc();
        }

        //////////////////////////////////////// METHODS ////////////////////////////////////////

        private void SetControlsState()
        {
            bool session = SteamProfileController.IsSessionActive();

            if (session)
            {
                SteamProfileView steamProfile = SteamProfileController.GetSteamProfile();

                ptbAvatar.LoadAsync(steamProfile.AvatarUrl);
                lblUsername.Text = steamProfile.Username;
            }
            else
            {
                ptbAvatar.Image = ptbAvatar.InitialImage;
                lblUsername.Text = null;
            }

            smiBlacklist.Enabled = BlacklistController.HasGames;
            smiFreeDlc.Enabled = false;
            btnLogin.Visible = !session;
            btnLogout.Visible = session;
            btnCalculate.Enabled = session;

            UnloadGames();
            UnloadDlc();
        }

        private static void ClickLink(string url)
        {
            Process process = new()
            {
                StartInfo = new()
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    FileName = "cmd.exe",
                    Arguments = $"/c start {url}"
                }
            };

            process.Start();
        }

        private static void AutoBlacklistReminder()
        {
            if (!Settings.Default.AutoBlacklist)
            {
                return;
            }

            string timePeriod = string.Empty;
            DateTime reminderDate = new();

            switch (Settings.Default.AutoBlacklistReminder)
            {
                case 0:
                    timePeriod = "week";
                    reminderDate = Settings.Default.AutoBlacklistLastReminder.AddDays(7);
                    break;
                case 1:
                    timePeriod = "month";
                    reminderDate = Settings.Default.AutoBlacklistLastReminder.AddMonths(1);
                    break;
                case 2:
                    timePeriod = "year";
                    reminderDate = Settings.Default.AutoBlacklistLastReminder.AddYears(1);
                    break;
            }

            if (DateTime.Today.Date < reminderDate.Date)
            {
                return;
            }

            string title = "Auto Blacklist Reminder";
            string message = $"It has been a new {timePeriod} since the last time the auto blacklist was cleared. Is it okay to clear it now?";

            DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result != DialogResult.OK)
            {
                return;
            }

            BlacklistController.ClearAutoBlacklist();
        }
    }
}