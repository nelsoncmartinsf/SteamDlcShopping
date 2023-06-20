using CommunityToolkit.Maui.Views;
using SteamDlcShopping.Maui.Views;

namespace SteamDlcShopping.Maui.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }


    private void ContentPage_LoadedEvent(object sender, EventArgs e)
    {
        // TODO: Load and apply settings to checkboxes
    }

    private void SignInButton_ClickedEvent(object sender, EventArgs e)
    {

    }
    private void SignOutButton_ClickedEvent(object sender, EventArgs e)
    {

    }

    private void SettingsButton_ClickedEvent(object sender, EventArgs e)
    {
        //Window settingsWindow = new Window(new SettingsPage()) {
        //    Width = 800,
        //    Height = 600,
        //};

        //Application.Current.OpenWindow(settingsWindow);
    }
    private void AboutButton_ClickedEvent(object sender, EventArgs e)
    {
        //string result = await DisplayPromptAsync("Question 1", "What's your name?");
    }

    private void LoadGamesButton_ClickedEvent(object sender, EventArgs e)
    {

    }

    private void HideGamesNotOnSaleCheckbox_CheckedChangedEvent(object sender, CheckedChangedEventArgs e)
    {

    }

    private void GameSearchBar_TextChangedEvent(object sender, TextChangedEventArgs e)
    {

    }

    private void HideDlcNotOnSaleCheckbox_CheckedChangedEvent(object sender, CheckedChangedEventArgs e)
    {

    }
    private void HideDlcOwnedCheckbox_CheckedChangedEvent(object sender, CheckedChangedEventArgs e)
    {

    }

    private void GameDlcSearchBar_TextChangedEvent(object sender, TextChangedEventArgs e)
    {

    }
}