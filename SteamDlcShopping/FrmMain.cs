using SteamDlcShopping.Entities;
using SteamDlcShopping.Enums;
using SteamDlcShopping.Properties;
using System.Diagnostics;
using Timer = System.Threading.Timer;

namespace SteamDlcShopping
{
    public partial class FrmMain : Form
    {
        private int selectedAppId;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //Remove and add the change event in order to not trigger it
            ddlLibrarySort.SelectedIndexChanged -= ddlLibrarySort_SelectedIndexChanged;
            ddlLibrarySort.SelectedIndex = 0;
            ddlLibrarySort.SelectedIndexChanged += ddlLibrarySort_SelectedIndexChanged;

            //Fill in with profile information or set to default state
            if (SteamProfile.IsLoggedIn)
            {
                ptbAvatar.LoadAsync(Program._steamProfile.AvatarUrl);
                lblUsername.Text = Program._steamProfile.Username;
            }
            else
            {
                ptbAvatar.Image = ptbAvatar.InitialImage;
                lblUsername.Text = null;
            }
        }

        private void smiSettings_Click(object sender, EventArgs e)
        {
            new FrmSettings().ShowDialog();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            new FrmLogin().ShowDialog();

            //Fill in profile information
            if (SteamProfile.IsLoggedIn)
            {
                ptbAvatar.LoadAsync(Program._steamProfile.AvatarUrl);
                lblUsername.Text = Program._steamProfile.Username;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //Clear session settings
            Settings.Default.SessionId = null;
            Settings.Default.SteamLoginSecure = null;
            Settings.Default.Save();

            //Set controls to default state
            ptbAvatar.Image = ptbAvatar.InitialImage;
            lblUsername.Text = null;

            Program._steamProfile = new();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //Set a timer worker thread
            Timer tmrLibrary = new(_ => tmrLibrary_Tick(), null, 0, Timeout.Infinite);
        }

        private void tmrLibrary_Tick()
        {
            Stopwatch timer = Stopwatch.StartNew();//DEBUG

            //Load dlc for all the games in the library
            Program._steamProfile.Library.LoadGamesDlc();

            //Trigger the sort dropdown to fill in the listview
            ddlLibrarySort.Invoke(new Action(() => ddlLibrarySort_SelectedIndexChanged(new(), new())));

            timer.Stop();//DEBUG

            lbldebug.Invoke(new Action(() => lbldebug.Text = $"{timer.Elapsed}"));//DEBUG
        }

        //////////////////////////////////////// FILTERS ////////////////////////////////////////

        private string nameSearch = string.Empty;

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Ignore filtering below the minimum character amount
            if (txtLibrarySearch.Text.Length < 3)
            {
                //Validate if the filter string is already empty to prevent repeated listview loads
                if (string.IsNullOrWhiteSpace(nameSearch))
                {
                    return;
                }

                nameSearch = null;
            }
            else
            {
                //Set the filter string
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
            //Sort the library accordingly
            switch (ddlLibrarySort.SelectedIndex)
            {
                case 0:
                    Program._steamProfile.Library.SortGamesBy(SortField.TotalCost);
                    break;
                case 1:
                    Program._steamProfile.Library.SortGamesBy(SortField.MaxDiscount, Enums.SortOrder.Descending);
                    break;
            }

            LoadLibraryToListview();
        }

        //////////////////////////////////////// LIBRARY ////////////////////////////////////////

        private void LoadLibraryToListview()
        {
            //Clear the listview
            lsvLibrary.Items.Clear();

            //Open window for updating, can greatly improve performance 
            lsvLibrary.BeginUpdate();

            foreach (Game game in Program._steamProfile.Library.Games)
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

                //Add item to listview
                lsvLibrary.Items.Add(item);
            }

            lsvLibrary.EndUpdate();

            //Fill in metric fields
            lblGameCount.Text = $"Count: {Program._steamProfile.Library.Games.Count}";
            lblLibraryCost.Text = $"Cost: {Program._steamProfile.Library.TotalCost}€";
        }

        private void lsvLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            //No selected game
            if (lsvLibrary.SelectedIndices.Count < 1)
            {
                return;
            }

            //Multiple games selected
            if (lsvLibrary.SelectedIndices.Count > 1)
            {
                lsvGame.Items.Clear();
                lblDlcCount.Text = null;
                return;
            }

            ListViewItem item = lsvLibrary.SelectedItems[0];
            Game game = Program._steamProfile.Library.GetGameByName(item.Text);
            selectedAppId = game.AppId;
            lblDlcCount.Text = $"Count: {game.DlcAmount}";

            LoadDlcToListview();
        }

        private void btnBlacklist_Click(object sender, EventArgs e)
        {
            //Selected games validation
            if (lsvLibrary.SelectedItems.Count == 0)
            {
                return;
            }

            foreach (ListViewItem item in lsvLibrary.SelectedItems)
            {
                if (int.TryParse(item.Tag.ToString(), out int appId))
                {
                    Program._steamProfile.Library.BlacklistGame(appId);
                    lsvLibrary.Items.Remove(item);
                }
            }

            Program._steamProfile.Library.ApplyBlacklist();
            Program._steamProfile.Library.SaveBlacklist();
        }

        //////////////////////////////////////// DLC ////////////////////////////////////////

        private void LoadDlcToListview()
        {
            Game game = Program._steamProfile.Library.GetGameByAppId(selectedAppId);

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