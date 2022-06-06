using SteamDlcShopping.Controllers;
using System.Diagnostics;

namespace SteamDlcShopping
{
    public partial class FrmFreeDlc : Form
    {
        public FrmFreeDlc()
        {
            InitializeComponent();
        }

        //////////////////////////////////////// FORM ////////////////////////////////////////

        private void FrmFreeDlc_Load(object sender, EventArgs e)
        {
            lsbDlc.DisplayMember = "Value";
            lsbDlc.ValueMember = "Key";

            Dictionary<int, string> dlcList = Middleware.GetFreeDlc();

            lsbDlc.BeginUpdate();

            foreach (KeyValuePair<int, string> item in dlcList)
            {
                lsbDlc.Items.Add(item);
            }

            lsbDlc.EndUpdate();
        }

        //////////////////////////////////////// LISTBOX ////////////////////////////////////////

        private void lsbDlc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            KeyValuePair<int, string> item = (KeyValuePair<int, string>)lsbDlc.SelectedItems[0];

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
    }
}