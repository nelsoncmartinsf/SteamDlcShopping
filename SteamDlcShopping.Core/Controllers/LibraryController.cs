using SteamDlcShopping.Core.Models;
using SteamDlcShopping.Core.ViewModels;

namespace SteamDlcShopping.Core.Controllers
{
    public class LibraryController
    {
        private static Library? _library;

        internal static void Login(string steamApiKey, string sessionId, string steamLoginSecure)
        {
            _library = new(SteamProfileController.GetSteamId());
            _library.LoadDynamicStore(sessionId, steamLoginSecure);
            _library.LoadGames(steamApiKey);

            BlacklistController.Load();

            _library.ApplyBlacklist(BlacklistController.Get());
        }

        internal static void Logout()
        {
            _library = null;
        }

        internal static bool DynamicStoreIsFilled()
        {
            bool result = false;

            if (_library is null)
            {
                return result;
            }

            if (_library.DynamicStore is null)
            {
                return result;
            }

            result = _library.DynamicStore.Any();

            return result;
        }

        internal static bool GamesIsFilled()
        {
            bool result = false;

            if (_library is null)
            {
                return result;
            }

            if (_library.Games is null)
            {
                return result;
            }

            result = _library.Games.Any();

            return result;
        }

        public static void Calculate(string steamApiKey, string sessionId, string steamLoginSecure, bool autoBlacklist)
        {
            if (_library is null)
            {
                return;
            }

            _library.LoadDynamicStore(sessionId, steamLoginSecure);
            _library.LoadGames(steamApiKey);
            BlacklistController.Load();

            _library.ApplyBlacklist(BlacklistController.Get());

            _library.LoadGamesDlc();

            if (_library.Games is null)
            {
                return;
            }

            List<int> gamesToRemove = new();

            foreach (Game game in _library.Games)
            {
                //Mark to remove games without dlc
                if (game.DlcList is null || game.DlcList.Count == 0)
                {
                    gamesToRemove.Add(game.AppId);
                    continue;
                }

                bool allOwned = true;

                foreach (Dlc dlc in game.DlcList)
                {
                    //Mark dlc as owned
                    if (_library.DynamicStore is not null && _library.DynamicStore.Contains(dlc.AppId))
                    {
                        dlc.MarkAsOwned();
                        continue;
                    }

                    //Special rule to consider N/A dlc as owned
                    //This marks games that are only missing N/A dlc as completed
                    if (dlc.IsNotAvailable)
                    {
                        continue;
                    }

                    allOwned = false;
                }

                //Mark to remove games with all dlc owned
                if (allOwned)
                {
                    gamesToRemove.Add(game.AppId);
                    continue;
                }
            }

            if (autoBlacklist)
            {
                BlacklistController.AddGames(gamesToRemove, true);
                BlacklistController.Save();
            }

            _library.ApplyBlacklist(gamesToRemove);

            _library.Games.ForEach(x => x.CalculateDlcMetrics());
        }

        public static int GetCurrentlyLoaded()
        {
            if (_library is null)
            {
                return 0;
            }

            return _library.CurrentlyLoaded;
        }

        public static LibraryView GetGames(string? filterName = null, bool filterOnSale = false)
        {
            LibraryView result = new()
            {
                Games = new()
            };

            if (_library is null)
            {
                return result;
            }

            if (_library.Games is null)
            {
                return result;
            }

            foreach (Game game in _library.Games)
            {
                //Filter by name search
                if (!string.IsNullOrWhiteSpace(game.Name) && !game.Name.Contains(filterName ?? string.Empty, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                //Filter by games on sale
                if (filterOnSale && game.DlcHighestPercentage == 0)
                {
                    continue;
                }

                GameView gameDto = new()
                {
                    AppId = game.AppId,
                    Name = game.Name,
                    DlcTotalPrice = $"{game.DlcTotalPrice}€",
                    DlcLeft = game.DlcLeft,
                    DlcLowestPercentage = game.DlcLowestPercentage > 0 ? $"{game.DlcLowestPercentage}%" : null,
                    DlcHighestPercentage = game.DlcHighestPercentage > 0 ? $"{game.DlcHighestPercentage}%" : null
                };

                result.TotalCost += game.DlcTotalPrice ?? 0m;

                result.Games.Add(gameDto);
            }

            return result;
        }

        public static List<DlcView> GetDlc(int appId, string? filterName = null, bool filterOwned = false)
        {
            List<DlcView> result = new();

            if (_library is null)
            {
                return result;
            }

            if (_library.Games is null)
            {
                return result;
            }

            Game game = _library.Games.First(x => x.AppId == appId);

            if (game is null)
            {
                return result;
            }

            foreach (Dlc dlc in game.DlcList)
            {
                //Filter by name search
                if (!string.IsNullOrWhiteSpace(dlc.Name) && !dlc.Name.Contains(filterName ?? string.Empty, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                //Filter by owned dlc
                if (filterOwned && dlc.IsOwned)
                {
                    continue;
                }

                string price;

                if (dlc.IsFree)
                {
                    price = "Free";
                }
                else
                {
                    if (dlc.IsNotAvailable)
                    {
                        price = "N/A";
                    }
                    else
                    {
                        price = $"{(dlc.Sale is not null ? dlc.Sale?.Price : dlc.Price)}€";
                    }
                }

                DlcView dlcDto = new()
                {
                    AppId = dlc.AppId,
                    Name = dlc.Name,
                    Price = price,
                    Discount = dlc.Sale is not null ? $"{dlc.Sale?.Percentage}%" : null,
                    IsOwned = dlc.IsOwned
                };

                result.Add(dlcDto);
            }

            return result;
        }

        public static Dictionary<int, string> GetFreeDlc()
        {
            Dictionary<int, string> result = new();

            if (_library is null)
            {
                return result;
            }

            if (_library.Games is null)
            {
                return result;
            }

            List<Game> games = _library.Games.Where(x => x.DlcList.Any(y => !y.IsOwned && y.IsFree)).ToList();

            foreach (Game game in games)
            {
                List<Dlc> dlcList = game.DlcList.Where(x => !x.IsOwned && x.IsFree).ToList();

                dlcList.ForEach(x => result.Add(x.AppId, $"{game.Name} - {x.Name}"));
            }

            return result;
        }

        public static bool GameHasTooManyDlc(int appId)
        {
            if (_library is null)
            {
                return false;
            }

            if (_library.Games is null)
            {
                return false;
            }

            Game game = _library.Games.First(x => x.AppId == appId);

            if (game is null)
            {
                return false;
            }

            return game.HasTooManyDlc;
        }

        internal static string GetGameName(int appId)
        {
            string result = string.Empty;

            if (_library is null)
            {
                return result;
            }

            if (_library.Games is null)
            {
                return result;
            }

            if (!_library.Games.Any(x => x.AppId == appId))
            {
                return result;
            }

            Game game = _library.Games.First(x => x.AppId == appId);
            result = game.Name ?? result;

            return result;
        }

        public static int GetLibrarySize()
        {
            int result = 0;

            if (_library is null)
            {
                return result;
            }

            if (_library.Games is null)
            {
                return result;
            }

            result = _library.Games.Count;

            return result;
        }

        internal static void ApplyBlacklist(List<int> appIds)
        {
            if (_library is null)
            {
                return;
            }

            _library.ApplyBlacklist(appIds);
        }
    }
}