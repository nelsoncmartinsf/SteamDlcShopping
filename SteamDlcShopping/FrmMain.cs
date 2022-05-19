using SteamDlcShopping.Dtos;
using System.Diagnostics;
using Timer = System.Threading.Timer;

namespace SteamDlcShopping
{
    public partial class FrmMain : Form
    {
        private readonly ListViewColumnSorter _columnSorter;
        private int _selectedGame;

        public FrmMain()
        {
            InitializeComponent();

            _columnSorter = new();
            lsvGame.ListViewItemSorter = _columnSorter;
            lsvDlc.ListViewItemSorter = _columnSorter;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            VerifySession();
        }

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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FrmLogin form = new();
            form.ShowDialog();
            form.Dispose();

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

            Middleware.BlacklistGames(appIds);

            lsvGame_EnabledChanged(new(), new());
        }

        private void tmrLibrary_Tick()
        {
            Invoke(new Action(() =>
            {
                smiSettings.Enabled = false;
                smiBlacklist.Enabled = false;
                btnLogout.Enabled = false;
                btnCalculate.Enabled = false;
                grbLibrary.Enabled = false;
                lsvDlc.Enabled = false;
            }));

            Stopwatch timer = Stopwatch.StartNew();//DEBUG

            Middleware.LoadGamesDlc();

            timer.Stop();//DEBUG
            lbldebug.Invoke(new Action(() => lbldebug.Text = $"{timer.Elapsed}"));//DEBUG

            Invoke(new Action(() =>
            {
                smiSettings.Enabled = true;
                smiBlacklist.Enabled = true;
                btnLogout.Enabled = true;
                btnCalculate.Enabled = true;
                grbLibrary.Enabled = true;
                lsvDlc.Enabled = true;
            }));
        }

        //////////////////////////////////////// GAME FILTERS ////////////////////////////////////////

        private string? _filterName;
        private bool _filterOnSale;

        private void txtLibrarySearch_TextChanged(object sender, EventArgs e)
        {
            _filterName = txtLibrarySearch.Text;
            lsvGame_EnabledChanged(new(), new());
            lsvDlc.Enabled = false;
        }

        private void chkHideGamesNotOnSale_CheckedChanged(object sender, EventArgs e)
        {
            _filterOnSale = chkHideGamesNotOnSale.Checked;
            lsvGame_EnabledChanged(new(), new());
            lsvDlc.Enabled = false;
        }

        //////////////////////////////////////// GAME ////////////////////////////////////////

        private void LoadGameToListview(List<GameDto> games)
        {
            lsvGame.Items.Clear();

            lsvGame.BeginUpdate();

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

                lsvGame.Items.Add(item);
            }

            lsvGame.EndUpdate();
        }

        private void lsvGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            //No selected game
            if (lsvGame.SelectedIndices.Count == 0)
            {
                btnBlacklist.Enabled = false;
                lsvDlc.Enabled = false;
                return;
            }

            btnBlacklist.Enabled = true;

            //Multiple games selected
            if (lsvGame.SelectedIndices.Count > 1)
            {
                lsvDlc.Enabled = false;
                return;
            }

            if (!int.TryParse(lsvGame.SelectedItems[0].Tag.ToString(), out _selectedGame))
            {
                _selectedGame = 0;
            }

            lsvDlc.Enabled = false;
            lsvDlc.Enabled = true;
        }

        private void lsvGame_DoubleClick(object sender, EventArgs e)
        {
            Process process = new()
            {
                StartInfo = new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    FileName = "cmd.exe",
                    Arguments = $"/c start https://store.steampowered.com/app/{lsvGame.SelectedItems[0].Tag}"
                }
            };

            process.Start();
        }

        private void lsvGame_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == _columnSorter.Column)
            {
                _columnSorter.Order = _columnSorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                _columnSorter.Column = e.Column;
                _columnSorter.Order = SortOrder.Ascending;
            }

            lsvGame.Sort();
        }

        //////////////////////////////////////// DLC FILTERS ////////////////////////////////////////
        


        //////////////////////////////////////// DLC ////////////////////////////////////////

        private void LoadDlcToListview(List<DlcDto> dlcs)
        {
            lsvDlc.Items.Clear();

            lsvDlc.BeginUpdate();

            foreach (DlcDto dlc in dlcs)
            {
                ListViewItem item;
                ListViewItem.ListViewSubItem subItem;

                //DLC
                item = new() { Tag = dlc.AppId, Text = dlc.Name };

                //Price
                subItem = new() { Text = dlc.Price };
                item.SubItems.Add(subItem);

                //Discount
                subItem = new() { Text = dlc.Discount };
                item.SubItems.Add(subItem);

                item.BackColor = dlc.IsOwned ? Color.LightGreen : item.BackColor;
                lsvDlc.Items.Add(item);
            }

            lsvDlc.EndUpdate();
        }

        private void lsvDlc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Process process = new()
            {
                StartInfo = new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    FileName = "cmd.exe",
                    Arguments = $"/c start https://store.steampowered.com/app/{lsvDlc.SelectedItems[0].Tag}"
                }
            };

            process.Start();
        }

        private void lsvDlc_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == _columnSorter.Column)
            {
                _columnSorter.Order = _columnSorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                _columnSorter.Column = e.Column;
                _columnSorter.Order = SortOrder.Ascending;
            }

            lsvDlc.Sort();
        }

        //////////////////////////////////////// METHODS ////////////////////////////////////////

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

            smiFreeDlc.Enabled = false;
            btnLogin.Enabled = !Middleware.IsSessionActive();
            btnLogout.Enabled = Middleware.IsSessionActive();
            btnCalculate.Enabled = Middleware.IsSessionActive();
            grbLibrary.Enabled = false;
        }

        private void grbLibrary_EnabledChanged(object sender, EventArgs e)
        {
            smiFreeDlc.Enabled = grbLibrary.Enabled;
            txtLibrarySearch.Text = null;
            chkHideGamesNotOnSale.Checked = false;
        }

        private void lsvGame_EnabledChanged(object sender, EventArgs e)
        {
            LibraryDto library = new();

            if (lsvGame.Enabled)
            {
                library = Middleware.GetGames(_filterName, _filterOnSale);

                if (library.Games is null)
                {
                    return;
                }

                LoadGameToListview(library.Games);
            }
            else
            {
                lsvGame.Items.Clear();
            }

            lblGameCount.Text = lsvGame.Enabled ? $"Count: {library.Size}" : null;
            lblLibraryCost.Text = lsvGame.Enabled ? $"Cost: {library.TotalCost}€" : null;
            btnBlacklist.Enabled = false;
        }

        private void lsvDlc_EnabledChanged(object sender, EventArgs e)
        {
            if (lsvDlc.Enabled)
            {
                List<DlcDto> dlcList = Middleware.GetDlc(_selectedGame);
                LoadDlcToListview(dlcList);
            }
            else
            {
                lsvDlc.Items.Clear();
            }

            lblDlcCount.Text = lsvDlc.Enabled ? $"Count: {lsvDlc.Items.Count}" : null;
        }
    }
}