using SteamDlcShopping.Properties;

namespace SteamDlcShopping
{
    public partial class FrmSettings : Form
    {
        ErrorProvider erpSteamApiKey;

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

            //Tooltips
            ToolTip toolTip = new();
            toolTip.SetToolTip(lblSteamApiKey, "This is the tooltip");
            toolTip.SetToolTip(lblSteamPath, "This is the tooltip 2");

            //Controls
            txtSteamApiKey.Text = Settings.Default.SteamApiKey;
            chkSteamIsInstalled.Checked = Settings.Default.SteamIsInstalled;
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
            Settings.Default.SteamIsInstalled = chkSteamIsInstalled.Checked;
            Settings.Default.SteamPath = lnkSteamPath.Text;
            Settings.Default.Save();

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

        private void chkSteamIsInstalled_CheckedChanged(object sender, EventArgs e)
        {
            lblSteamPath.Enabled = chkSteamIsInstalled.Checked;
            lnkSteamPath.Enabled = chkSteamIsInstalled.Checked;
        }

        private void lnkSteamPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using FolderBrowserDialog fbd = new();
            fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                lnkSteamPath.Text = fbd.SelectedPath;
            }
        }
    }
}