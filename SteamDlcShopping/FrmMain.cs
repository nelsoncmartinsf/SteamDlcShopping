using SteamDlcShopping.Dtos;
using SteamDlcShopping.Enums;
using System.Diagnostics;
using SortOrder = SteamDlcShopping.Enums.SortOrder;
using Timer = System.Threading.Timer;

namespace SteamDlcShopping
{
    public partial class FrmMain : Form
    {
        private int _selectedGame;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            VerifySession();
        }

        private void smiSettings_Click(object sender, EventArgs e)
        {
            new FrmSettings().ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            new FrmLogin().ShowDialog();

            Middleware.Login();
            VerifySession();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Middleware.Logout();
            VerifySession();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //Set a timer worker thread
            Timer tmrLibrary = new(_ => tmrLibrary_Tick(), null, 0, Timeout.Infinite);
        }

        private void tmrLibrary_Tick()
        {
            btnLogout.Invoke(new Action(() => btnLogout.Enabled = false));
            btnCalculate.Invoke(new Action(() => btnCalculate.Enabled = false));
            grbLibrary.Invoke(new Action(() => grbLibrary.Enabled = false));
            lsvGame.Invoke(new Action(() => lsvGame.Enabled = false));

            Stopwatch timer = Stopwatch.StartNew();//DEBUG

            Middleware.LoadGamesDlc();

            timer.Stop();//DEBUG
            lbldebug.Invoke(new Action(() => lbldebug.Text = $"{timer.Elapsed}"));//DEBUG

            btnLogout.Invoke(new Action(() => btnLogout.Enabled = true));
            btnCalculate.Invoke(new Action(() => btnCalculate.Enabled = true));
            grbLibrary.Invoke(new Action(() => grbLibrary.Enabled = true));
            lsvGame.Invoke(new Action(() => lsvGame.Enabled = true));
        }

        //////////////////////////////////////// FILTERS ////////////////////////////////////////

        private string _filterName;
        private bool _filterOnSale;
        private SortField _sortField;
        private SortOrder _sortOrder;

        private void txtLibrarySearch_TextChanged(object sender, EventArgs e)
        {
            _filterName = txtLibrarySearch.Text;
            lsvLibrary_EnabledChanged(new(), new());
            lsvGame.Enabled = false;
        }

        private void chkHideGamesNotOnSale_CheckedChanged(object sender, EventArgs e)
        {
            _filterOnSale = chkHideGamesNotOnSale.Checked;
            lsvLibrary_EnabledChanged(new(), new());
            lsvGame.Enabled = false;
        }

        private void ddlLibrarySort_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlLibrarySort.SelectedIndex)
            {
                case 0:
                    _sortField = SortField.TotalCost;
                    _sortOrder = SortOrder.Ascending;
                    break;
                case 1:
                    _sortField = SortField.MaxDiscount;
                    _sortOrder = SortOrder.Descending;
                    break;
                default:
                    _sortField = SortField.AppId;
                    _sortOrder = SortOrder.Ascending;
                    break;
            }

            lsvLibrary_EnabledChanged(new(), new());
            lsvGame.Enabled = false;
        }

        //////////////////////////////////////// LIBRARY ////////////////////////////////////////

        private void LoadLibraryToListview(List<GameDto> games)
        {
            lsvLibrary.Items.Clear();

            lsvLibrary.BeginUpdate();

            foreach (GameDto game in games)
            {
                ListViewItem item;
                ListViewItem.ListViewSubItem subItem;

                //Game
                item = new() { Tag = game.AppId, Text = game.Name };

                //Cost
                subItem = new() { Text = game.DlcTotalPrice };
                item.SubItems.Add(subItem);

                //Max Discount
                subItem = new() { Text = game.DlcHighestPercentage };
                item.SubItems.Add(subItem);

                lsvLibrary.Items.Add(item);
            }

            lsvLibrary.EndUpdate();
        }

        private void lsvLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            //No selected game
            if (lsvLibrary.SelectedIndices.Count < 1)
            {
                btnBlacklist.Enabled = false;
                lsvGame.Enabled = false;
                return;
            }

            btnBlacklist.Enabled = true;

            //Multiple games selected
            if (lsvLibrary.SelectedIndices.Count > 1)
            {
                lsvGame.Enabled = false;
                return;
            }

            int.TryParse(lsvLibrary.SelectedItems[0].Tag.ToString(), out _selectedGame);
            lsvGame.Enabled = false;
            lsvGame.Enabled = true;
        }

        private void btnBlacklist_Click(object sender, EventArgs e)
        {
            List<int> appIds = new();

            foreach (ListViewItem item in lsvLibrary.SelectedItems)
            {
                int.TryParse(item.Tag.ToString(), out int appId);
                appIds.Add(appId);

                lsvLibrary.Items.Remove(item);
            }

            Middleware.BlacklistGames(appIds);

            lsvLibrary_EnabledChanged(new(), new());
        }

        //////////////////////////////////////// GAME ////////////////////////////////////////

        private void LoadDlcToListview(List<DlcDto> dlcs)
        {
            lsvGame.Items.Clear();

            lsvGame.BeginUpdate();

            foreach (DlcDto dlc in dlcs)
            {
                ListViewItem item;
                ListViewItem.ListViewSubItem subItem;

                //DLC
                item = new() { Text = dlc.Name };

                //Price
                subItem = new() { Text = dlc.Price };
                item.SubItems.Add(subItem);

                //Discount
                subItem = new() { Text = dlc.Discount };
                item.SubItems.Add(subItem);

                item.BackColor = dlc.IsOwned ? Color.LightGreen : item.BackColor;
                lsvGame.Items.Add(item);
            }

            lsvGame.EndUpdate();
        }

        private void lnkSteamPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process process = new()
            {
                StartInfo = new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    FileName = "cmd.exe",
                    Arguments = $"/c start https://store.steampowered.com/app/{_selectedGame}"
                }
            };

            process.Start();
        }

        //////////////////////////////////////// EVENTS ////////////////////////////////////////

        private void VerifySession()
        {
            if (Middleware.IsSessionActive())
            {
                SteamProfileDto steamProfile = Middleware.GetSteamProfile();

                ptbAvatar.LoadAsync(steamProfile.AvatarUrl);
                lblUsername.Text = steamProfile.Username;
            }
            else
            {
                ptbAvatar.Image = ptbAvatar.InitialImage;
                lblUsername.Text = null;
            }

            btnLogin.Enabled = !Middleware.IsSessionActive();
            btnLogout.Enabled = Middleware.IsSessionActive();
            btnCalculate.Enabled = Middleware.IsSessionActive();
            grbLibrary.Enabled = false;
            lsvGame.Enabled = false;
        }

        private void grbLibrary_EnabledChanged(object sender, EventArgs e)
        {
            txtLibrarySearch.Text = null;
            chkHideGamesNotOnSale.Checked = false;
            ddlLibrarySort.SelectedIndex = -1;
        }

        private void lsvLibrary_EnabledChanged(object sender, EventArgs e)
        {
            LibraryDto library = new();

            if (lsvLibrary.Enabled)
            {
                library = Middleware.GetLibrary(_filterName, _filterOnSale, _sortField, _sortOrder);
                LoadLibraryToListview(library.Games);
            }
            else
            {
                lsvLibrary.Items.Clear();
            }

            lblLibraryCount.Text = lsvLibrary.Enabled ? $"Count: {library.Size}" : null;
            lblLibraryCost.Text = lsvLibrary.Enabled ? $"Cost: {library.TotalCost}€" : null;
            btnBlacklist.Enabled = false;
        }

        private void lsvGame_EnabledChanged(object sender, EventArgs e)
        {
            if (lsvGame.Enabled)
            {
                List<DlcDto> dlcList = Middleware.GetGame(_selectedGame);
                LoadDlcToListview(dlcList);
            }
            else
            {
                lsvGame.Items.Clear();
            }

            lblGameCount.Text = lsvGame.Enabled ? $"Count: {lsvGame.Items.Count}" : null;
            lnkSteamPage.Enabled = lsvGame.Enabled;
        }
    }
}