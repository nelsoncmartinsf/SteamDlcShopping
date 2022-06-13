using SteamDlcShopping.Core.Models;
using SteamDlcShopping.Core.ViewModels;

namespace SteamDlcShopping.Core.Controllers
{
    public static class SteamProfileController
    {
        private static SteamProfile? _steamProfile;

        private static void Reset()
        {
            _steamProfile = null;
        }

        public static bool IsSessionActive(string sessionId, string steamLoginSecure)
        {
            if (_steamProfile is null)
            {
                Login(steamLoginSecure);
            }

            bool result = LibraryController.DynamicStoreIsFilled(sessionId, steamLoginSecure);

            return result;
        }

        public static void Login(string steamLoginSecure)
        {
            _steamProfile = new(steamLoginSecure);
        }

        public static void Logout()
        {
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