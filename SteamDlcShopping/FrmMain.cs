using Newtonsoft.Json;
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
            ddlLibrarySort.SelectedIndexChanged -= ddlLibrarySort_SelectedIndexChanged;
            ddlLibrarySort.SelectedIndex = 0;
            ddlLibrarySort.SelectedIndexChanged += ddlLibrarySort_SelectedIndexChanged;

            if (SteamProfile.IsLoggedIn)
            {
                ptbAvatar.LoadAsync(SteamProfile.AvatarUrl);
                lblUsername.Text = SteamProfile.Username;
            }
            else
            {
                ptbAvatar.Image = ptbAvatar.InitialImage;
                lblUsername.Text = "lblUsername";
            }
        }

        private void smiSettings_Click(object sender, EventArgs e)
        {
            new FrmSettings().ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            new FrmLogin().ShowDialog();

            if (SteamProfile.IsLoggedIn)
            {
                ptbAvatar.LoadAsync(SteamProfile.AvatarUrl);
                lblUsername.Text = SteamProfile.Username;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Settings.Default.SessionId = null;
            Settings.Default.SteamLoginSecure = null;
            Settings.Default.Save();

            ptbAvatar.Image = ptbAvatar.InitialImage;
            lblUsername.Text = "lblUsername";
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            Timer tmrLibrary = new(_ => tmrLibrary_Tick(), null, 0, Timeout.Infinite);
        }

        private void tmrLibrary_Tick()
        {
            Stopwatch timer = Stopwatch.StartNew();

            SteamProfile.Library.LoadGames(Settings.Default.SteamApiKey, SteamProfile.Id);
            SteamProfile.Library.LoadGamesDlc();
            ddlLibrarySort.Invoke(new Action(() => ddlLibrarySort_SelectedIndexChanged(new(), new())));

            timer.Stop();
            lbldebug.Invoke(new Action(() => lbldebug.Text = $"{timer.Elapsed}"));
            lsvLibrary.Invoke(new Action(LoadLibraryToListview));
        }

        //////////////////////////////////////// FILTERS ////////////////////////////////////////

        private string nameSearch = string.Empty;

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

        private void ddlLibrarySort_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlLibrarySort.SelectedIndex)
            {
                case 0:
                    SteamProfile.Library.Games = SteamProfile.Library.Games.OrderBy(x => x.DlcTotalPrice).ToList();
                    break;
                case 1:
                    SteamProfile.Library.Games = SteamProfile.Library.Games.OrderByDescending(x => x.DlcHighestPercentage).ToList();
                    break;
            }

            LoadLibraryToListview();
        }

        //////////////////////////////////////// LIBRARY ////////////////////////////////////////

        private void LoadLibraryToListview()
        {
            decimal sum = 0m;

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
                if (chkHideGamesNotOnSale.Checked && game.DlcHighestPercentage == 0)
                {
                    continue;
                }

                //Prices sum
                sum += game.DlcTotalPrice;

                //Game column
                ListViewItem item = new()
                {
                    Tag = game.AppId,
                    Text = game.Name
                };

                ListViewItem.ListViewSubItem subItem;

                //Cost column
                subItem = new()
                {
                    Text = $"{game.DlcTotalPrice}€"
                };

                item.SubItems.Add(subItem);

                //Max Discount column
                subItem = new()
                {
                    Text = game.DlcHighestPercentage != 0 ? $"{game.DlcHighestPercentage}%" : null
                };

                item.SubItems.Add(subItem);

                lsvLibrary.Items.Add(item);
            }

            lsvLibrary.EndUpdate();

            lblGameCount.Text = $"Count: {lsvLibrary.Items.Count}";
            lblLibraryCost.Text = $"Cost: {sum}€";
        }

        private void lsvLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Selected game validation
            if (lsvLibrary.SelectedIndices.Count != 1)
            {
                return;
            }

            ListViewItem item = lsvLibrary.SelectedItems[0];
            Game game = SteamProfile.Library.GetGameByName(item.Text);
            selectedAppId = game.AppId;
            lblDlcCount.Text = $"Count: {game.DlcAmount}";

            LoadDlcToListview();
        }

        private void btnBlacklist_Click(object sender, EventArgs e)
        {
            if (lsvLibrary.SelectedItems.Count == 0)
            {
                return;
            }

            string content;
            SortedDictionary<int, string> blacklist = null;

            if (File.Exists("blacklist.txt"))
            {
                content = File.ReadAllText("blacklist.txt");
                blacklist = JsonConvert.DeserializeObject<SortedDictionary<int, string>>(content);
            }

            if (blacklist == null)
            {
                blacklist = new();
            }

            foreach (ListViewItem item in lsvLibrary.SelectedItems)
            {
                if (int.TryParse(item.Tag.ToString(), out int appId))
                {
                    blacklist.Add(appId, item.Text);
                    lsvLibrary.Items.Remove(item);
                }
            }

            content = JsonConvert.SerializeObject(blacklist);
            File.WriteAllText("blacklist.txt", content);
        }

        //////////////////////////////////////// DLC ////////////////////////////////////////

        private void LoadDlcToListview()
        {
            Game game = SteamProfile.Library.GetGameByAppId(selectedAppId);

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
    }
}