using Microsoft.Web.WebView2.Core;
using SteamDlcShopping.Properties;

namespace SteamDlcShopping
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
            webLogin.CoreWebView2.DOMContentLoaded += webLogin_CoreWebView2_DOMContentLoaded;
        }

        private void webLogin_CoreWebView2_DOMContentLoaded(object? sender, CoreWebView2DOMContentLoadedEventArgs e)
        {
            if (webLogin.Source.AbsoluteUri == "https://store.steampowered.com/login")
            {
                webLogin.ExecuteScriptAsync("document.getElementsByClassName('responsive_header')[0].remove();");
                webLogin.ExecuteScriptAsync("document.getElementsByClassName('login_right_col')[0].remove();");
                webLogin.ExecuteScriptAsync("document.getElementById('link_forgot_password').remove();");
                webLogin.ExecuteScriptAsync("document.body.style.overflow = 'hidden';");
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