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

        private void webLogin_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            webLogin.CoreWebView2.CookieManager.DeleteAllCookies();
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

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            webLogin.Stop();
            webLogin.Dispose();
        }
    }
}