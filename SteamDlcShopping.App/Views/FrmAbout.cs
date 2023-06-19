namespace SteamDlcShopping.App.Views;

public partial class FrmAbout : Form
{
    private string? _latestVersion;

    public FrmAbout() => InitializeComponent();

    private async void FrmAbout_Load(object sender, EventArgs e)
    {
        _latestVersion = await CoreController.GetLatestVersionName(Application.ProductVersion);

        if (_latestVersion is null)
        {
            return;
        }

        lnkNewVersion.VisitedLinkColor = lnkNewVersion.LinkColor;
        lnkNewVersion.Visible = true;
    }

    private void LnkNewVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        //CoreController.OpenLink($"https://github.com/DiogoABDias/SteamDlcShopping/releases/tag/{_latestVersion}");

        Settings.Default.UpdateIgnored = false;
        Settings.Default.Save();
        Close();
    }

    private void LnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => CoreController.OpenLink("https://github.com/DiogoABDias/SteamDlcShopping");
}