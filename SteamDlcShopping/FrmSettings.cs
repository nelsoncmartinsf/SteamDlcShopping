using SteamDlcShopping.Properties;

namespace SteamDlcShopping
{
    public partial class FrmSettings : Form
    {
        ErrorProvider _erpSteamApiKey;
        private string _filterName = string.Empty;
        private List<int> _unblacklist;
        public SortedDictionary<int, string> _blacklist;

        public FrmSettings()
        {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            //Errors
            _erpSteamApiKey = new()
            {
                BlinkStyle = ErrorBlinkStyle.NeverBlink
            };

            _erpSteamApiKey.SetIconAlignment(txtSteamApiKey, ErrorIconAlignment.MiddleRight);
            _erpSteamApiKey.SetIconPadding(txtSteamApiKey, 5);

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

            _blacklist = Middleware.GetBlacklist();
            _unblacklist = new();

            LoadBlacklistToListbox();
        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Settings.Default.SteamApiKey))
            {
                _erpSteamApiKey.SetError(txtSteamApiKey, "Steam API Key is required!");
                e.Cancel = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings.Default.SteamApiKey = txtSteamApiKey.Text;
            Settings.Default.AutoBlacklist = chkAutoBlacklist.Checked;
            Settings.Default.Save();

            Middleware.UnblacklistGames(_unblacklist);

            if (_unblacklist.Count > 0)
            {
                Middleware.SaveBlacklist();
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtSteamApiKey_TextChanged(object sender, EventArgs e)
        {
            _erpSteamApiKey.Clear();
        }

        private void txtBlacklistSearch_TextChanged(object sender, EventArgs e)
        {
            _filterName = txtBlacklistSearch.Text;
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

            foreach (KeyValuePair<int, string> item in _blacklist)
            {
                //Filter by name search
                if (txtBlacklistSearch.Text.Length >= 3 && !item.Value.Contains(_filterName, StringComparison.InvariantCultureIgnoreCase))
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