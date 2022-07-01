using SteamDlcShopping.Properties;

namespace SteamDlcShopping.Extensibility
{
    public partial class ucCalculate : UserControl
    {
        public ucCalculate()
        {
            InitializeComponent();
        }

        private void ucLoading_Load(object sender, EventArgs e)
        {
            ptbLoading.Image = Settings.Default.UseMemeLoading ? Resources.memeLoading : Resources.defaultLoading;
        }
    }
}