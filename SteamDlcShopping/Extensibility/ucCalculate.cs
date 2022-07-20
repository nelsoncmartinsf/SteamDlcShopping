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
            Control control = Parent.Controls["grbLibrary"];
            Size = new Size(control.ClientSize.Width, control.ClientSize.Height);

            ptbLoading.Left = (ClientSize.Width - ptbLoading.Width) / 2;
            ptbLoading.Top = (ClientSize.Height - ptbLoading.Height) / 2;
            ptbLoading.Image = Settings.Default.UseMemeLoading ? Resources.memeLoading : Resources.defaultLoading;
        }
    }
}