using SteamDlcShopping.Core.Controllers;
using SteamDlcShopping.Properties;
using Timer = System.Threading.Timer;

namespace SteamDlcShopping.Extensibility
{
    public partial class ucLoading : UserControl
    {
        public ucLoading()
        {
            InitializeComponent();
        }

        private void ucLoading_Load(object sender, EventArgs e)
        {
            pgbLoading.Maximum = LibraryController.GetLibrarySize();
            ptbLoading.Image = Settings.Default.UseMemeLoading ? Resources.memeLoading : Resources.defaultLoading;
            Timer tmrLoading = new(_ => tmrLoading_Tick(), null, 0, 500);
        }

        private void tmrLoading_Tick()
        {
            Invoke(new Action(() =>
            {
                pgbLoading.Value = LibraryController.GetCurrentlyLoaded();
            }));
        }
    }
}