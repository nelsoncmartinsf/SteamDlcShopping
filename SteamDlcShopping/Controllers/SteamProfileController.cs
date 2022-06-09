using SteamDlcShopping.Models;
using SteamDlcShopping.Properties;
using SteamDlcShopping.ViewModels;

namespace SteamDlcShopping.Controllers
{
    public static class SteamProfileController
    {
        private static SteamProfile? _steamProfile;

        public static bool IsSessionActive()
        {
            if (string.IsNullOrWhiteSpace(Settings.Default.SessionId) || string.IsNullOrWhiteSpace(Settings.Default.SteamLoginSecure))
            {
                return false;
            }

            if (_steamProfile is null)
            {
                Login();
            }

            return LibraryController.DynamicStoreIsFilled();
        }

        public static void Login()
        {
            _steamProfile = new();
        }

        public static void Logout()
        {
            Settings.Default.SessionId = null;
            Settings.Default.SteamLoginSecure = null;
            Settings.Default.Save();

            _steamProfile = null;
        }

        public static SteamProfileView GetSteamProfile()
        {
            SteamProfileView result = new();

            if (_steamProfile is null)
            {
                return result;
            }

            result.Username = _steamProfile?.Username;
            result.AvatarUrl = _steamProfile?.AvatarUrl;

            return result;
        }
    }
}