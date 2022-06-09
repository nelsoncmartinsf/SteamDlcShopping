using SteamDlcShopping.Models;
using SteamDlcShopping.ViewModels;

namespace SteamDlcShopping.Controllers
{
    public class LibraryController
    {
        private static Library? _library;

        public static bool DynamicStoreIsFilled()
        {
            if (_library is null)
            {
                return false;
            }

            if (_library.DynamicStore is null)
            {
                return false;
            }

            return _library.DynamicStore.Any();
        }

        public static LibraryView GetGames(string? filterName = null, bool filterOnSale = false)
        {
            LibraryView result = new();
            result.Games = new();

            if (_library?.Games is null)
            {
                return new();
            }

            decimal totalCost = 0m;

            List<Game> library = _library.Games;

            foreach (Game game in library)
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

                totalCost += game.DlcTotalPrice ?? 0m;

                result.Games.Add(gameDto);
            }

            result.Size = result.Games.Count;
            result.TotalCost = totalCost;

            return result;
        }

        public static List<DlcView> GetDlc(int appId, string? filterName = null, bool filterOwned = false)
        {
            List<DlcView> result = new();

            if (_library?.Games is null)
            {
                return result;
            }

            Game? game = _library.Games.FirstOrDefault(x => x.AppId == appId);

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

            Game? game = _library.Games.FirstOrDefault(x => x.AppId == appId);

            if (game is null)
            {
                return false;
            }

            return game.HasTooManyDlc;
        }

        public static void LoadGamesDlc()
        {
            if (_library is null)
            {
                return;
            }

            _library.LoadDynamicStore();
            _library.LoadGames();
            _library.LoadBlacklist();

            _library.ApplyBlacklist();

            _library.LoadGamesDlc();

            _library.ImproveGamesList();

            if (_library.Games is null)
            {
                return;
            }

            foreach (Game game in _library.Games)
            {
                game.CalculateDlcMetrics();
            }
        }

        public static Dictionary<int, string> GetFreeDlc()
        {
            Dictionary<int, string> result = new();

            if (_steamProfile?.Library?.Games is null)
            {
                return result;
            }

            List<Game>? games = _steamProfile.Library.Games.Where(x => x.DlcList.Any(y => !y.IsOwned && y.IsFree)).ToList();

            foreach (Game game in games)
            {
                List<Dlc> dlcList = game.DlcList.Where(x => !x.IsOwned && x.IsFree).ToList();

                dlcList.ForEach(x => result.Add(x.AppId, $"{game.Name} - {x.Name}"));
            }

            return result;
        }



        public static string GetGameName(int appId)
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

        public static void ApplyBlacklist(List<int> appIds)
        {
            if (_library is null)
            {
                return;
            }

            _library.ApplyBlacklist(appIds);
        }
    }
}