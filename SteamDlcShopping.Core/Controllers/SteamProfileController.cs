using SteamDlcShopping.Core.Models;
using SteamDlcShopping.Core.ViewModels;

namespace SteamDlcShopping.Core.Controllers
{
    public static class SteamProfileController
    {
        private static SteamProfile? _steamProfile;

        public static bool IsSessionActive()
        {
            bool result = false;

            if (_steamProfile is null)
            {
                return result;
            }

            result = LibraryController.DynamicStoreIsFilled() && LibraryController.GamesIsFilled();

            return result;
        }

        public static void Login(string steamApiKey, string sessionId, string steamLoginSecure)
        {
            _steamProfile = new(steamLoginSecure);
            LibraryController.Login(steamApiKey, sessionId, steamLoginSecure);
        }

        public static void Logout()
        {
            _steamProfile = null;
            LibraryController.Logout();
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

        internal static long GetSteamId()
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