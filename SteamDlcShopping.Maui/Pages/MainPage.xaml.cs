using SteamDlcShopping.Core.Controllers;
using SteamDlcShopping.Core.ViewModels;
using SteamDlcShopping.Maui.Models;
using SteamDlcShopping.Maui.Utils;
using SteamDlcShopping.Maui.Views;
using System.Linq;
namespace SteamDlcShopping.Maui.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async Task HandleLogIn()
    {
        await PrepareSteamProfileController();

        UpdateUI();

        await LoadGames();
    }

    private async Task PrepareSteamProfileController()
    {
        if (SteamProfileController.IsSessionActive())
        {
            return;
        }

        await SteamProfileController.LoginAsync(Settings.SteamApiKey, Settings.SessionId, Settings.SteamLoginSecure);

        await LibraryController.CalculateAsync(Settings.SteamApiKey, Settings.SessionId, Settings.SteamLoginSecure, false);
    }

    private void UpdateUI()
    {
        if (!SteamProfileController.IsSessionActive())
        {
            return;
        }

        SteamProfileView steamProfile = SteamProfileController.GetSteamProfile();

        SteamProfileImage.Source = ImageSource.FromUri(new Uri(steamProfile.AvatarUrl));
        SteamProfileLabel.Text = steamProfile.Username;
    }

    private async Task LoadGames()
    {
        if (!SteamProfileController.IsSessionActive())
        {
            // TODO: Show error message
            return;
        }

        await Task.Yield();

        String gameFilter = GameSearchBar.Text;
        Boolean hideNotOnSale = HideGamesNotOnSaleCheckbox.IsChecked;

        LibraryView library = LibraryController.GetGames(gameFilter, hideNotOnSale);

        List<GameModel> gameList = library.Games.Select(query => (GameModel)query).ToList();

        GamesListView.ItemsSource = gameList;
    }

    private async Task LoadDlcs(Int32 appId)
    {
        if (!SteamProfileController.IsSessionActive())
        {
            // TODO: Show error message
            return;
        }

        await Task.Yield();

        List<DlcView> dlcList = LibraryController.GetDlcs(appId, filterName: null, filterOnSale: false, filterOwned: false);

        List<DlcModel> dlcModels = dlcList.Select(query => (DlcModel)query).ToList();

        DlcListView.ItemsSource = dlcModels;
    }

    private async void ContentPage_LoadedEvent(object sender, EventArgs e)
    {
        await HandleLogIn();
    }

    private void SignInButton_ClickedEvent(object sender, EventArgs e)
    {
        SteamLogInView.OpenSteamLogIn();
    }
    private void SignOutButton_ClickedEvent(object sender, EventArgs e)
    {
        SteamProfileController.Logout();
    }

    private void SettingsButton_ClickedEvent(object sender, EventArgs e)
    {
        SettingsView.OpenSettingsView();
    }
    private void AboutButton_ClickedEvent(object sender, EventArgs e)
    {
        //string result = await DisplayPromptAsync("Question 1", "What's your name?");
    }

    private async void LoadGamesButton_ClickedEvent(object sender, EventArgs e)
    {
        await LoadGames();
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

    private async void SteamLogInView_LoggedIn(object sender, EventArgs e)
    {
        SteamLogInView.CloseSteamLogIn();

        await HandleLogIn();
    }

    private async void GamesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if(e.SelectedItem is GameModel model)
        {
            await LoadDlcs(model.AppId);
        }
    }
}