namespace SteamDlcShopping.Maui.Views;

public partial class SettingsView : ContentView
{
    public SettingsView()
    {
        InitializeComponent();
    }

    public void OpenSettingsView()
    {
        LoadSettings();
        this.IsVisible = true;
    }
    public void CloseSettingsView(Boolean saveSettings)
    {
        this.IsVisible = false;

        if (saveSettings)
        {
            SaveSettings();
        }
    }
    public void LoadSettings()
    {
        SteamApiKeyEntry.Text = Utils.Settings.SteamApiKey;
        AutoBlacklistCheckBox.IsChecked = Utils.Settings.AutoBlacklist;
    }
    public void SaveSettings()
    {
        Utils.Settings.SteamApiKey = SteamApiKeyEntry.Text;
        Utils.Settings.AutoBlacklist = AutoBlacklistCheckBox.IsChecked;
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Launcher.OpenAsync("https://steamcommunity.com/dev/apikey");
    }

    private void SaveChangesButton_ClickedEvent(object sender, EventArgs e)
    {
        CloseSettingsView(true);
    }

    private void DiscardChangesButton_ClickedEvent(object sender, EventArgs e)
    {
        CloseSettingsView(false);
    }
}