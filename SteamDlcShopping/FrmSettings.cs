using SteamDlcShopping.Properties;

namespace SteamDlcShopping
{
    public partial class FrmSettings : Form
    {
        ErrorProvider _erpSteamApiKey;

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
    }
}