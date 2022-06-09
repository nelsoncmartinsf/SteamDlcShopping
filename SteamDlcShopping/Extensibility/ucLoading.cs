using SteamDlcShopping.Properties;

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
            string loading = Settings.Default.UseMemeLoading ? "FlGsjNI" : "loading_large";
            ptbLoading.ImageLocation = $"C:\\Users\\VM\\Desktop\\{loading}.gif";
        }
    }
}