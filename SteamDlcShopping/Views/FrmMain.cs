using SteamDlcShopping.Core.Controllers;
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
            SteamProfileController.Login(Settings.Default.SteamApiKey, Settings.Default.SessionId, Settings.Default.SteamLoginSecure);
            VerifySession();
        }

        private void FrmMain_Shown(object sender, EventArgs e)
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

        private void tmrLibrary_Tick()
        {
            Invoke(new Action(() =>
            {
                smiSettings.Enabled = false;
                smiBlacklist.Enabled = false;
                btnLogout.Visible = false;
                btnCalculate.Enabled = false;
                lsvDlc.Enabled = false;
                grbLibrary.Enabled = false;

                ucLoading ucLoading = new()
                {
                    Name = "ucLoading",
                    Location = grbLibrary.Location
                };

                Controls.Add(ucLoading);
                ucLoading.BringToFront();
            }));

            Stopwatch timer = Stopwatch.StartNew();//DEBUG

            LibraryController.Calculate(Settings.Default.SteamApiKey, Settings.Default.SessionId, Settings.Default.SteamLoginSecure, Settings.Default.AutoBlacklist);

            timer.Stop();//DEBUG
            lbldebug.Invoke(new Action(() => lbldebug.Text = $"{timer.Elapsed}"));//DEBUG

            Invoke(new Action(() =>
            {
                smiSettings.Enabled = true;
                smiBlacklist.Enabled = true;
                btnLogout.Visible = true;
                btnCalculate.Enabled = true;
                grbLibrary.Enabled = true;
                Controls["ucLoading"].Dispose();
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
        }

        private void smiFreeDlc_Click(object sender, EventArgs e)
        {
            FrmFreeDlc form = new();
            form.ShowDialog();
            form.Dispose();
        }

        //////////////////////////////////////// BUTTONS ////////////////////////////////////////

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FrmLogin form = new();
            form.ShowDialog();
            form.Dispose();

            SteamProfileController.Login(Settings.Default.SteamApiKey, Settings.Default.SessionId, Settings.Default.SteamLoginSecure);
            VerifySession();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Settings.Default.SessionId = null;
            Settings.Default.SteamLoginSecure = null;
            Settings.Default.Save();

            SteamProfileController.Logout();
            VerifySession();
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

                lsvGame.Items.Remove(item);
            }

            BlacklistController.AddGames(appIds, false);

            lsvGame_EnabledChanged(new(), new());
        }

        //////////////////////////////////////// GAME LIST ////////////////////////////////////////

        private ColumnSorter? _gameColumnSorter;
        private int _selectedGame;

        private void LoadGameToListview(List<GameView> games)
        {
            lsvGame.Items.Clear();
            lsvGame.ListViewItemSorter = null;

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

                //Min Discount
                subItem = new() { Text = game.DlcLowestPercentage, Tag = Types.Decimal };
                item.SubItems.Add(subItem);

                //Max Discount
                subItem = new() { Text = game.DlcHighestPercentage, Tag = Types.Decimal };
                item.SubItems.Add(subItem);

                lsvGame.Items.Add(item);
            }

            lsvGame.EndUpdate();

            _gameColumnSorter = new();
            lsvGame.ListViewItemSorter = _gameColumnSorter;
        }

        private void lsvGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            //No selected game
            if (lsvGame.SelectedIndices.Count == 0)
            {
                _selectedGame = 0;
                btnBlacklist.Visible = false;
                lsvDlc.Enabled = false;
                return;
            }

            btnBlacklist.Visible = true;

            //Multiple games selected
            if (lsvGame.SelectedIndices.Count > 1)
            {
                _selectedGame = 0;
                lsvDlc.Enabled = false;
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
            lsvDlc.Enabled = false;
            lsvDlc.Enabled = true;
        }

        private void lsvGame_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (_gameColumnSorter is null)
            {
                return;
            }

            SortList(lsvGame, _gameColumnSorter, e.Column);
        }

        private void lsvGame_DoubleClick(object sender, EventArgs e)
        {
            ClickLink($"https://store.steampowered.com/app/{lsvGame.SelectedItems[0].Tag}");
        }

        //////////////////////////////////////// DLC LIST ////////////////////////////////////////

        private ColumnSorter? _dlcColumnSorter;

        private void LoadDlcToListview(List<DlcView> dlcs)
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

            _dlcColumnSorter = new();
            lsvDlc.ListViewItemSorter = _dlcColumnSorter;
        }

        private void lsvDlc_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (_dlcColumnSorter is null)
            {
                return;
            }

            SortList(lsvDlc, _dlcColumnSorter, e.Column);
        }

        private void lsvDlc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClickLink($"https://store.steampowered.com/app/{lsvDlc.SelectedItems[0].Tag}");
        }

        private void lnkTooManyDlc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClickLink($"https://store.steampowered.com/dlc/{_selectedGame}");
        }

        //////////////////////////////////////// GAME FILTERS ////////////////////////////////////////

        private string? _filterGame;
        private bool _filterOnSale;

        private void txtLibrarySearch_TextChanged(object sender, EventArgs e)
        {
            _filterGame = txtGameSearch.Text;
            lsvGame_EnabledChanged(new(), new());
            lsvDlc.Enabled = false;
        }

        private void chkHideGamesNotOnSale_CheckedChanged(object sender, EventArgs e)
        {
            _filterOnSale = chkHideGamesNotOnSale.Checked;
            lsvGame_EnabledChanged(new(), new());
            lsvDlc.Enabled = false;
        }

        //////////////////////////////////////// DLC FILTERS ////////////////////////////////////////

        private string? _filterDlc;
        private bool _filterOwned;

        private void txtDlcSearch_TextChanged(object sender, EventArgs e)
        {
            _filterDlc = txtDlcSearch.Text;
            lsvDlc_EnabledChanged(new(), new());
        }

        private void chkHideOwnedDlc_CheckedChanged(object sender, EventArgs e)
        {
            _filterOwned = chkHideOwnedDlc.Checked;
            lsvDlc_EnabledChanged(new(), new());
        }

        //////////////////////////////////////// CONTROL STATES ////////////////////////////////////////

        private void VerifySession()
        {
            if (SteamProfileController.IsSessionActive())
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

            smiFreeDlc.Enabled = default;
            btnLogin.Visible = !SteamProfileController.IsSessionActive();
            btnLogout.Visible = SteamProfileController.IsSessionActive();
            btnCalculate.Enabled = SteamProfileController.IsSessionActive();
            grbLibrary.Enabled = default;
        }

        private void grbLibrary_EnabledChanged(object sender, EventArgs e)
        {
            smiFreeDlc.Enabled = grbLibrary.Enabled;
            txtGameSearch.Text = default;
            chkHideGamesNotOnSale.Checked = default;
            txtDlcSearch.Text = default;
            chkHideOwnedDlc.Checked = default;
        }

        private void lsvGame_EnabledChanged(object sender, EventArgs e)
        {
            LibraryView library = new();

            if (lsvGame.Enabled)
            {
                library = LibraryController.GetGames(_filterGame, _filterOnSale);

                if (library.Games is null)
                {
                    return;
                }

                LoadGameToListview(library.Games);
            }
            else
            {
                lsvGame.Items.Clear();

                foreach (ColumnHeader column in lsvGame.Columns)
                {
                    if (!int.TryParse(column.Tag?.ToString(), out int length))
                    {
                        continue;
                    }

                    column.Text = column.Text[..length];
                }
            }

            lblGameCount.Text = lsvGame.Enabled ? $"Count: {lsvGame.Items.Count}" : default;
            lblLibraryCost.Text = lsvGame.Enabled ? $"Cost: {library.TotalCost}€" : default;
            btnBlacklist.Visible = default;
        }

        private void lsvDlc_EnabledChanged(object sender, EventArgs e)
        {
            if (lsvDlc.Enabled)
            {
                List<DlcView> dlcList = LibraryController.GetDlc(_selectedGame, _filterDlc, _filterOwned);
                LoadDlcToListview(dlcList);

                if (LibraryController.GameHasTooManyDlc(_selectedGame))
                {
                    lblTooManyDlc.Visible = true;
                    lnkTooManyDlc.Visible = true;
                }
            }
            else
            {
                lsvDlc.Items.Clear();
                lblTooManyDlc.Visible = false;
                lnkTooManyDlc.Visible = false;

                foreach (ColumnHeader column in lsvDlc.Columns)
                {
                    if (!int.TryParse(column.Tag?.ToString(), out int length))
                    {
                        continue;
                    }

                    column.Text = column.Text[..length];
                }
            }

            txtDlcSearch.Enabled = lsvDlc.Enabled;
            chkHideOwnedDlc.Enabled = lsvDlc.Enabled;

            txtDlcSearch.Text = lsvDlc.Enabled ? txtDlcSearch.Text : default;
            chkHideOwnedDlc.Checked = lsvDlc.Enabled && chkHideOwnedDlc.Checked;
            lblDlcCount.Text = lsvDlc.Enabled ? $"Count: {lsvDlc.Items.Count}" : default;
        }

        //////////////////////////////////////// METHODS ////////////////////////////////////////

        private static void SortList(ListView listView, ColumnSorter columnSorter, int newColumn)
        {
            //Remove sorting characters from the previous column
            if (columnSorter.Column >= 0 && int.TryParse(listView.Columns[columnSorter.Column].Tag.ToString(), out int length))
            {
                listView.Columns[columnSorter.Column].Text = listView.Columns[columnSorter.Column].Text[..length];
            }

            //Store column title length in order to add and remove sorting characters
            if (listView.Columns[newColumn].Tag is null)
            {
                listView.Columns[newColumn].Tag = listView.Columns[newColumn].Text.Length;
            }

            //Revert sorting on the same column
            if (newColumn == columnSorter.Column)
            {
                columnSorter.Order = columnSorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                columnSorter.Column = newColumn;
                columnSorter.Order = SortOrder.Ascending;
            }

            //Apply the sorting character
            switch (columnSorter.Order)
            {
                case SortOrder.Ascending:
                    listView.Columns[columnSorter.Column].Text += " ▲";
                    break;
                case SortOrder.Descending:
                    listView.Columns[columnSorter.Column].Text += " ▼";
                    break;
                case SortOrder.None:
                    break;
            }

            listView.Sort();
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
    }
}