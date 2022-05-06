using SteamDlcShopping.Properties;

namespace SteamDlcShopping
{
    public partial class FrmSettings : Form
    {
        ErrorProvider erpSteamApiKey;
        private string nameSearch = string.Empty;
        private List<int> _unblacklist;

        public FrmSettings()
        {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            //Errors
            erpSteamApiKey = new()
            {
                BlinkStyle = ErrorBlinkStyle.NeverBlink
            };

            erpSteamApiKey.SetIconAlignment(txtSteamApiKey, ErrorIconAlignment.MiddleRight);
            erpSteamApiKey.SetIconPadding(txtSteamApiKey, 5);

            //Help icons
            pbtSteamApiKey.Image = SystemIcons.Information.ToBitmap();
            ptbSmartLoading.Image = SystemIcons.Information.ToBitmap();

            //Tooltips
            ToolTip toolTip = new();
            toolTip.SetToolTip(pbtSteamApiKey, "This key is required in order to retrieve the owned games information.");
            toolTip.SetToolTip(ptbSmartLoading, "Improves loading times by automatically blacklisting games without DLC after the first time they were loaded.");

            //Controls
            txtSteamApiKey.Text = Settings.Default.SteamApiKey;
            chkAutoBlacklist.Checked = Settings.Default.AutoBlacklist;

            Program._steamProfile.Library.LoadBlacklist();
            _unblacklist = new();

            LoadBlacklistToListbox();
        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Settings.Default.SteamApiKey))
            {
                erpSteamApiKey.SetError(txtSteamApiKey, "Steam API Key is required!");
                e.Cancel = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings.Default.SteamApiKey = txtSteamApiKey.Text;
            Settings.Default.AutoBlacklist = chkAutoBlacklist.Checked;
            Settings.Default.Save();

            foreach (int appId in _unblacklist)
            {
                Program._steamProfile.Library.UnBlacklistGame(appId);
            }

            if (_unblacklist.Count > 0)
            {
                Program._steamProfile.Library.SaveBlacklist();
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtSteamApiKey_TextChanged(object sender, EventArgs e)
        {
            erpSteamApiKey.Clear();
        }

        private void txtBlacklistSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtBlacklistSearch.Text.Length < 3)
            {
                if (string.IsNullOrWhiteSpace(nameSearch))
                {
                    return;
                }

                nameSearch = string.Empty;
            }
            else
            {
                nameSearch = txtBlacklistSearch.Text;
            }

            LoadBlacklistToListbox();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lsbBlacklist.SelectedItems.Count == 0)
            {
                return;
            }

            for (int idx = lsbBlacklist.SelectedItems.Count - 1; lsbBlacklist.SelectedItems.Count > 0; idx--)
            {
                KeyValuePair<int, string> item = (KeyValuePair<int, string>)lsbBlacklist.SelectedItems[idx];

                _unblacklist.Add(item.Key);
                lsbBlacklist.Items.Remove(item);
            }

            //Fill in metric fields
            lblGameCount.Text = $"Count: {lsbBlacklist.Items.Count}";
        }

        private void LoadBlacklistToListbox()
        {
            lsbBlacklist.DisplayMember = "Value";
            lsbBlacklist.ValueMember = "Key";

            lsbBlacklist.Items.Clear();

            lsbBlacklist.BeginUpdate();

            foreach (KeyValuePair<int, string> item in Program._steamProfile.Library.Blacklist)
            {
                //Filter by name search
                if (txtBlacklistSearch.Text.Length >= 3 && !item.Value.Contains(nameSearch, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                lsbBlacklist.Items.Add(item);
            }

            lsbBlacklist.EndUpdate();

            //Fill in metric fields
            lblGameCount.Text = $"Count: {lsbBlacklist.Items.Count}";
        }
    }
}