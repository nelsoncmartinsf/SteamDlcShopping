using Microsoft.Web.WebView2.Core;
using SteamDlcShopping.Properties;

namespace SteamDlcShopping.Views
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        //////////////////////////////////////// FORM ////////////////////////////////////////

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            webLogin.Stop();
            webLogin.Dispose();
        }

        //////////////////////////////////////// WEBVIEW2 ////////////////////////////////////////

        private void webLogin_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            webLogin.CoreWebView2.CookieManager.DeleteAllCookies();
            webLogin.CoreWebView2.Settings.AreDevToolsEnabled = false;
            webLogin.CoreWebView2.DOMContentLoaded += webLogin_CoreWebView2_DOMContentLoaded;
        }

        private void webLogin_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            if (e.Uri.Contains("HelpWithLoginInfo"))
            {
                e.Cancel = true;
            }

            if (e.Uri == "https://store.steampowered.com/")
            {
                //WindowState = FormWindowState.Minimized;
            }
        }

        private void webLogin_CoreWebView2_DOMContentLoaded(object? sender, CoreWebView2DOMContentLoadedEventArgs e)
        {
            if (webLogin.Source.AbsoluteUri == "https://store.steampowered.com/login")
            {
                Thread.Sleep(500);

                webLogin.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('responsive_header')[0].remove();");
                webLogin.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('login_bottom_row')[0].remove();");
                webLogin.CoreWebView2.ExecuteScriptAsync("document.querySelectorAll('[class^=newlogindialog_UseMobileAppForQR]')[0].remove();");
                webLogin.CoreWebView2.ExecuteScriptAsync("document.querySelector('[class^=newlogindialog_TextLink]').remove();");
                webLogin.CoreWebView2.ExecuteScriptAsync("document.body.style.overflow = 'hidden';");
                webLogin.CoreWebView2.ExecuteScriptAsync("document.getElementsByClassName('page_content')[0].scrollIntoView({behavior: 'auto',block: 'center',inline: 'center'});");

                webLogin.Visible = true;
            }
        }

        private async void webLogin_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (webLogin.Source.AbsoluteUri == "https://store.steampowered.com/")
            {
                List<CoreWebView2Cookie> cookies = await webLogin.CoreWebView2.CookieManager.GetCookiesAsync(null);

                Settings.Default.SessionId = GetCookieValue(cookies, "sessionid");
                Settings.Default.SteamLoginSecure = GetCookieValue(cookies, "steamLoginSecure");
                Settings.Default.Save();

                Close();
            }
        }

        //////////////////////////////////////// METHODS ////////////////////////////////////////

        private static string? GetCookieValue(List<CoreWebView2Cookie> cookies, string name)
        {
            foreach (CoreWebView2Cookie cookie in cookies)
            {
                if (cookie.Name == name)
                {
                    return cookie.Value;
                }
            }

            return null;
        }
    }
}