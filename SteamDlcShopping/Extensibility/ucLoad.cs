namespace SteamDlcShopping.Extensibility
{
    public partial class ucLoad : UserControl
    {
        public ucLoad()
        {
            InitializeComponent();
        }

        private void ucLoad_Load(object sender, EventArgs e)
        {
            if (Parent is null)
            {
                return;
            }

            Form form = (Form)Parent;

            Size = new Size(form.ClientSize.Width, form.ClientSize.Height);

            ptbLoading.Left = (ClientSize.Width - ptbLoading.Width) / 2;
            ptbLoading.Top = (ClientSize.Height - ptbLoading.Height) / 2;
        }
    }
}