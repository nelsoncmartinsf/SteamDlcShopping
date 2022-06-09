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
            ptbLoading.ImageLocation = "C:\\Users\\VM\\Desktop\\FlGsjNI.gif";
            //ptbLoading.ImageLocation = "C:\\Users\\VM\\Desktop\\loading_large.gif";
        }
    }
}