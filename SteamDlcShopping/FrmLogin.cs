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
                List<CoreWebView2Cookie> cookiesTask = await webLogin.CoreWebView2.CookieManager.GetCookiesAsync(null);

                Settings.Default.SessionId = cookiesTask.FirstOrDefault(x => x.Name == "sessionid").Value;
                Settings.Default.SteamLoginSecure = cookiesTask.FirstOrDefault(x => x.Name == "steamLoginSecure").Value;
                Settings.Default.Save();

                Close();
            }
        }
    }
}