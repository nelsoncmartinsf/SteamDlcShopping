using Microsoft.Web.WebView2.Core;

namespace SteamDlcShopping.Maui.Views;

public partial class SteamLogInView : ContentView
{
    public event EventHandler<EventArgs> LoggedIn;
    public SteamLogInView()
    {
        InitializeComponent();
    }

    public void OpenSteamLogIn()
    {
        RedirectToSteamLogin();

        this.IsVisible = true;
    }
    public void CloseSteamLogIn()
    {
        this.IsVisible = false;
    }

    private void RedirectToSteamLogin()
    {
        SteamLogInWebView.Source = new UrlWebViewSource()
        {
            Url = "https://store.steampowered.com/login",
        };
    }
    private async void GetCookies()
    {
        if (SteamLogInWebView.Handler.PlatformView is Microsoft.Maui.Platform.MauiWebView mauiWebView)
        {
            IReadOnlyList<CoreWebView2Cookie> cookies = await mauiWebView.CoreWebView2.CookieManager.GetCookiesAsync(null);

            // TODO: Since the web views automagically retains the cookies, should we retrieve directly from there instead?
            CoreWebView2Cookie sessionCookie = cookies.FirstOrDefault(q => q.Name.Equals("sessionid", StringComparison.InvariantCultureIgnoreCase));
            CoreWebView2Cookie loginCookie = cookies.FirstOrDefault(q => q.Name.Equals("steamloginsecure", StringComparison.InvariantCultureIgnoreCase));
            //1722447882.17095
            if (sessionCookie != null && loginCookie != null)
            {
                Utils.Settings.SessionId = sessionCookie.Value;
                Utils.Settings.SteamLoginSecure = loginCookie.Value;

                LoggedIn?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    private void SteamLogInWebView_Navigating(object sender, WebNavigatingEventArgs e)
    {
        if (e.Url.Equals("https://store.steampowered.com/login"))
        {
            return;
        }
        if (e.Url.Equals("https://store.steampowered.com/"))
        {
            e.Cancel = true;
            GetCookies();
            return;
        }

        if (SteamLogInWebView.Handler.PlatformView is Microsoft.Maui.Platform.MauiWebView mauiWebView)
        {
            mauiWebView.CoreWebView2.CookieManager.DeleteAllCookies();
        }

        RedirectToSteamLogin();
    }
    private void SteamLogInWebView_Navigated(object sender, WebNavigatedEventArgs e)
    {
        //GetCookies();

        base.OnHandlerChanged();
    }
}