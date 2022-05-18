using System.Diagnostics;

namespace SteamDlcShopping
{
    public partial class FrmFreeDlc : Form
    {
        public FrmFreeDlc()
        {
            InitializeComponent();
        }

        private void FrmFreeDlc_Load(object sender, EventArgs e)
        {
            lnkStorePage.Enabled = false;

            lsbFreeDlc.DisplayMember = "Value";
            lsbFreeDlc.ValueMember = "Key";

            LoadDlcToListbox();
        }

        private void lsbFreeDlc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //No selected game
            if (lsbFreeDlc.SelectedIndices.Count == 0)
            {
                lnkStorePage.Enabled = false;
                return;
            }

            lnkStorePage.Enabled = true;
        }

        private void lnkStorePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            KeyValuePair<int, string> item = (KeyValuePair<int, string>)lsbFreeDlc.SelectedItems[0];

            Process process = new()
            {
                StartInfo = new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    FileName = "cmd.exe",
                    Arguments = $"/c start https://store.steampowered.com/app/{item.Key}"
                }
            };

            process.Start();
        }

        private void LoadDlcToListbox()
        {
            lsbFreeDlc.Items.Clear();

            Dictionary<int, string> dlcList = Middleware.GetFreeDlc();

            lsbFreeDlc.BeginUpdate();

            foreach (KeyValuePair<int, string> item in dlcList)
            {
                lsbFreeDlc.Items.Add(item);
            }

            lsbFreeDlc.EndUpdate();
        }
    }
}