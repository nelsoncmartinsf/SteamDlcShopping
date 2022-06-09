using SteamDlcShopping.Models;
using SteamDlcShopping.Properties;
using SteamDlcShopping.ViewModels;

namespace SteamDlcShopping.Controllers
{
    public static class SteamProfileController
    {
        private static SteamProfile? _steamProfile;

        public static void Reset()
        {
            _steamProfile = null;
        }

        public static bool IsSessionActive()
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(Settings.Default.SessionId) || string.IsNullOrWhiteSpace(Settings.Default.SteamLoginSecure))
            {
                return result;
            }

            if (_steamProfile is null)
            {
                Login();
            }

            result = LibraryController.DynamicStoreIsFilled();

            return result;
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

            Reset();
            LibraryController.Reset();
            BlacklistController.Reset();
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

        public static long GetSteamId()
        {
            long result = 0;

            if (_steamProfile is null)
            {
                return result;
            }

            result = _steamProfile.Id;

            return result;
        }
    }
}