using SteamDlcShopping.Entities;
using SteamDlcShopping.Properties;
using System.Diagnostics;
using Timer = System.Threading.Timer;

namespace SteamDlcShopping
{
    public partial class FrmMain : Form
    {
        private readonly SteamProfile SteamProfile;
        private int selectedAppId;

        public FrmMain()
        {
            InitializeComponent();
            SteamProfile = new();
        }

        private void frmCatalog_Load(object sender, EventArgs e)
        {
            grbLibrary.Enabled = false;

            if (SteamProfile.IsLoggedIn)
            {
                ptbAvatar.LoadAsync(SteamProfile.AvatarUrl);
                lblUsername.Text = SteamProfile.Username;

                btnLogin.Enabled = false;
                btnLogout.Enabled = true;
                btnCalculate.Enabled = true;
            }
            else
            {
                grbLibrary.Enabled = false;

                ptbAvatar.Image = ptbAvatar.InitialImage;
                lblUsername.Text = null;

                btnLogin.Enabled = true;
                btnLogout.Enabled = false;
                btnCalculate.Enabled = false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            new FrmLogin().ShowDialog();

            if (SteamProfile.IsLoggedIn)
            {
                ptbAvatar.LoadAsync(SteamProfile.AvatarUrl);
                lblUsername.Text = SteamProfile.Username;

                btnLogin.Enabled = false;
                btnLogout.Enabled = true;
                btnCalculate.Enabled = true;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Settings.Default.SessionId = null;
            Settings.Default.SteamLoginSecure = null;
            Settings.Default.Save();

            ptbAvatar.Image = ptbAvatar.InitialImage;
            lblUsername.Text = null;

            btnLogin.Enabled = true;
            btnLogout.Enabled = false;
            btnCalculate.Enabled = false;

            txtLibrarySearch.Text = null;
            lsvGame.Items.Clear();
            lblGameCount.Text = null;

            lsvGame.Items.Clear();
            lblDlcCount.Text = null;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            Timer tmrLibrary = new(_ => tmrLibrary_Tick(), null, 0, Timeout.Infinite);
        }

        private void tmrLibrary_Tick()
        {
            btnLogout.Invoke(new Action(() => btnLogout.Enabled = false));
            btnCalculate.Invoke(new Action(() => btnCalculate.Enabled = false));

            Stopwatch timer = Stopwatch.StartNew();

            SteamProfile.LoadLibrary(Settings.Default.SteamApiKey);
            SteamProfile.Library.LoadGamesDlc();
            ddlLibrarySort.Invoke(new Action(() => ddlSort_SelectedIndexChanged(new(), new())));

            timer.Stop();
            lbldebug.Invoke(new Action(() => lbldebug.Text = $"{timer.Elapsed}"));

            lblGameCount.Invoke(new Action(() => lblGameCount.Text = $"Count: {SteamProfile.Library.Size}"));
            lsvLibrary.Invoke(new Action(LoadLibraryToListview));
            btnLogout.Invoke(new Action(() => btnLogout.Enabled = true));
            btnCalculate.Invoke(new Action(() => btnCalculate.Enabled = true));
            grbLibrary.Invoke(new Action(() => grbLibrary.Enabled = true));
        }

        private void grbLibrary_EnabledChanged(object sender, EventArgs e)
        {
            if (!grbLibrary.Enabled)
            {
                txtLibrarySearch.Text = null;
                ddlLibrarySort.SelectedIndex = 0;

                lsvLibrary.Items.Clear();
                lsvGame.Items.Clear();

                lblGameCount.Text = null;
                lblDlcCount.Text = null;
                lnkSteamPage.Visible = false;
            }
        }

        //////////////////////////////////////// LIBRARY ////////////////////////////////////////

        string nameSearch = null;
        int sort = 0;

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtLibrarySearch.Text.Length < 3)
            {
                if (string.IsNullOrWhiteSpace(nameSearch))
                {
                    return;
                }

                nameSearch = null;
            }
            else
            {
                nameSearch = txtLibrarySearch.Text;
            }

            LoadLibraryToListview();
        }

        private void chkHideGamesNotOnSale_CheckedChanged(object sender, EventArgs e)
        {
            LoadLibraryToListview();
        }

        private void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLibrarySort.SelectedIndex == sort)
            {
                return;
            }

            switch (ddlLibrarySort.SelectedIndex)
            {
                case 0:
                    sort = 0;
                    SteamProfile.Library.Games = SteamProfile.Library.Games.OrderBy(x => x.AppId).ToList();
                    break;
                case 1:
                    sort = 1;
                    SteamProfile.Library.Games = SteamProfile.Library.Games.OrderByDescending(x => x.AppId).ToList();
                    break;
                case 2:
                    sort = 2;
                    SteamProfile.Library.Games = SteamProfile.Library.Games.OrderBy(x => x.Name).ToList();
                    break;
                case 3:
                    sort = 3;
                    SteamProfile.Library.Games = SteamProfile.Library.Games.OrderByDescending(x => x.Name).ToList();
                    break;
                case 4:
                    sort = 4;
                    SteamProfile.Library.Games = SteamProfile.Library.Games.OrderBy(x => x.DlcTotalPrice).ToList();
                    break;
                case 5:
                    sort = 5;
                    SteamProfile.Library.Games = SteamProfile.Library.Games.OrderByDescending(x => x.DlcTotalPrice).ToList();
                    break;
                case 6:
                    sort = 6;
                    SteamProfile.Library.Games = SteamProfile.Library.Games.OrderBy(x => x.DlcHighestPercentage).ToList();
                    break;
                case 7:
                    sort = 7;
                    SteamProfile.Library.Games = SteamProfile.Library.Games.OrderByDescending(x => x.DlcHighestPercentage).ToList();
                    break;
            }

            LoadLibraryToListview();
        }

        private void LoadLibraryToListview()
        {
            lsvLibrary.Items.Clear();

            lsvLibrary.BeginUpdate();

            foreach (Game game in SteamProfile.Library.Games)
            {
                //Filter by name search
                if (txtLibrarySearch.Text.Length >= 3 && !game.Name.Contains(nameSearch, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                //Filter by games on sale
                if (chkHideGamesNotOnSale.Checked && !game.DlcList.Any(x => x.OnSale))
                {
                    continue;
                }

                ListViewItem item = new()
                {
                    Text = game.Name
                };

                ListViewItem.ListViewSubItem subItem;

                subItem = new()
                {
                    Text = $"{game.DlcTotalPrice}€"
                };

                item.SubItems.Add(subItem);

                subItem = new()
                {
                    Text = game.DlcHighestPercentage != 0 ? $"{game.DlcHighestPercentage}%" : null
                };

                item.SubItems.Add(subItem);

                lsvLibrary.Items.Add(item);
            }

            lsvLibrary.EndUpdate();

            lblGameCount.Text = $"Count: {lsvLibrary.Items.Count}";
        }

        private void lsvLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDlcCount.Text = null;
            lnkSteamPage.Visible = false;

            //Selected game validation
            if (lsvLibrary.SelectedIndices.Count == 0)
            {
                selectedAppId = 0;
                return;
            }

            lnkSteamPage.Visible = true;

            ListViewItem item = lsvLibrary.SelectedItems[0];
            Game game = SteamProfile.Library.Games.FirstOrDefault(x => x.Name == item.Text);
            selectedAppId = game.AppId;
            lblDlcCount.Text = $"Count: {game.DlcAmount}";

            LoadDlcToListview();
        }

        //////////////////////////////////////// DLC ////////////////////////////////////////

        private void LoadDlcToListview()
        {
            int idx = SteamProfile.Library.FindGameIndex(selectedAppId);
            Game game = SteamProfile.Library.Games[idx];

            lsvGame.Items.Clear();

            if (game.DlcList != null)
            {
                lsvGame.BeginUpdate();

                foreach (Dlc dlc in game.DlcList)
                {
                    ListViewItem item = new()
                    {
                        Text = dlc.Name
                    };

                    string price;

                    if (dlc.IsFree)
                    {
                        price = "Free";
                    }
                    else
                    {
                        if (dlc.IsNotAvailable)
                        {
                            price = "N/A";
                        }
                        else
                        {
                            price = $"{(dlc.OnSale ? dlc.Sale.Price : dlc.Price)}€";
                        }
                    }

                    ListViewItem.ListViewSubItem subItem;

                    subItem = new()
                    {
                        Text = price
                    };

                    item.SubItems.Add(subItem);

                    subItem = new()
                    {
                        Text = dlc.OnSale ? $"{dlc.Sale.Percentage}%" : null
                    };

                    item.SubItems.Add(subItem);

                    if (dlc.IsOwned)
                    {
                        item.BackColor = Color.LightGreen;
                    }

                    lsvGame.Items.Add(item);
                }

                lsvGame.EndUpdate();
            }

            lblDlcCount.Text = $"Count: {lsvGame.Items.Count}";
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
                    Arguments = $"/c start https://store.steampowered.com/app/{selectedAppId}"
                }
            };

            process.Start();
        }

        private void smiSettings_Click(object sender, EventArgs e)
        {
            new FrmSettings().ShowDialog();
        }
    }
}