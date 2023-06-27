//using CoreWebView2 = Microsoft.Web.WebView2.Core.CoreWebView2;
//using MauiWebView = Microsoft.Maui.Platform.MauiWebView;

namespace SteamDlcShopping.Maui.Views;

public partial class SteamLogInView : ContentView
{
    public SteamLogInView()
    {
        InitializeComponent();
    }

    public void OpenSteamLogIn()
    {
        this.IsVisible = true;

        SteamLogInWebView.Cookies = new System.Net.CookieContainer();
        SteamLogInWebView.Source = new UrlWebViewSource()
        {
            Url = "https://store.steampowered.com/login",
        };
    }
    public void CloseSteamLogIn()
    {
        this.IsVisible = false;
    }

    private void SteamLogInWebView_Navigating(object sender, WebNavigatingEventArgs e)
    {
        var a = e.Url;
    }

    private void SteamLogInWebView_Navigated(object sender, WebNavigatedEventArgs e)
    {
        var a = e.Url;

        var kujihl = SteamLogInWebView.Handler.PlatformView;

        if(SteamLogInWebView.Handler.PlatformView is Microsoft.Maui.Platform.MauiWebView mauiWebView)
        {

            //if(mauiWebView.)
        }

        

        //MauiWebView mauiWebView = SteamLogInWebView.Handler.PlatformView as MauiWebView;
        //CoreWebView2 f = mauiWebView.CoreWebView2 as CoreWebView2;//.CookieManager.GetCookiesAsync();
        //var asd = f.CookieManager.GetCookiesAsync(null).Result;

        var bv = SteamLogInWebView.Handler.PlatformView;
    }

    private void SteamLogInWebView_Loaded(object sender, EventArgs e)
    {
        var a = e;

    }
}