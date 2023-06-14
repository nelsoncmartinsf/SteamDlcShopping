namespace SteamDlcShopping.App.Extensibility;

public partial class UcLoad : UserControl
{
    public UcLoad() => InitializeComponent();

    private void UcLoad_Load(object sender, EventArgs e)
    {
        if (Parent is null)
        {
            return;
        }

        Form form = (Form)Parent;

        Size = new Size(form.ClientSize.Width, form.ClientSize.Height);

        PtbLoading.Left = (ClientSize.Width - PtbLoading.Width) / 2;
        PtbLoading.Top = (ClientSize.Height - PtbLoading.Height) / 2;
    }
}