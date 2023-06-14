namespace SteamDlcShopping.App.Views;

public partial class FrmAbout : Form
{
    public FrmAbout() => InitializeComponent();

    private void LnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => LibraryController.OpenLink("https://github.com/DiogoABDias/SteamDlcShopping");
}