using SteamDlcShopping.Dtos;
using SteamDlcShopping.Entities;
using SteamDlcShopping.Enums;
using SteamDlcShopping.Properties;
using SortOrder = SteamDlcShopping.Enums.SortOrder;

namespace SteamDlcShopping
{
    public static class Middleware
    {
        private static SteamProfile? _steamProfile;

        public static bool IsSessionActive()
        {
            bool active = !string.IsNullOrWhiteSpace(Settings.Default.SessionId) && !string.IsNullOrWhiteSpace(Settings.Default.SteamLoginSecure);

            if (active && _steamProfile is null)
            {
                Login();
            }

            return active;
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



        public static SteamProfileDto GetSteamProfile()
        {
            SteamProfileDto result = new()
            {
                Username = _steamProfile?.Username,
                AvatarUrl = _steamProfile?.AvatarUrl
            };

            return result;
        }

        public static LibraryDto GetLibrary(string? filterName = null, bool filterOnSale = false,
            SortField sortField = SortField.AppId, SortOrder sortOrder = SortOrder.Ascending)
        {
            LibraryDto result = new();
            result.Games = new();

            decimal totalCost = 0m;

            List<Game>? library = SortLibrary(sortField, sortOrder);

            if (library is null)
            {
                return new();
            }

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

                GameDto gameDto = new()
                {
                    AppId = game.AppId,
                    Name = game.Name,
                    DlcTotalPrice = $"{game.DlcTotalPrice}€",
                    DlcHighestPercentage = game.DlcHighestPercentage > 0 ? $"{game.DlcHighestPercentage}%" : null
                };

                totalCost += game.DlcTotalPrice ?? 0m;

                result.Games.Add(gameDto);
            }

            result.Size = result.Games.Count;
            result.TotalCost = totalCost;

            return result;
        }

        public static List<DlcDto> GetGame(int appId)
        {
            List<DlcDto> result = new();

            Game? game = _steamProfile?.Library?.Games?.FirstOrDefault(x => x.AppId == appId);

            if (game is null)
            {
                return result;
            }

            foreach (Dlc dlc in game.DlcList)
            {
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
                        price = $"{(dlc.OnSale ? dlc.Sale?.Price : dlc.Price)}€";
                    }
                }

                DlcDto dlcDto = new()
                {
                    Name = dlc.Name,
                    Price = price,
                    Discount = dlc.OnSale ? $"{dlc.Sale?.Percentage}%" : null,
                    IsOwned = dlc.IsOwned
                };

                result.Add(dlcDto);
            }

            return result;
        }



        public static void LoadGamesDlc()
        {
            _steamProfile?.Library?.LoadGamesDlc();
        }

        private static List<Game>? SortLibrary(SortField sortField = SortField.AppId, SortOrder sortOrder = SortOrder.Ascending)
        {
            List<Game>? result = _steamProfile?.Library?.Games;

            if (sortOrder == SortOrder.Ascending)
            {
                switch (sortField)
                {
                    case SortField.TotalCost:
                        result = _steamProfile?.Library?.Games?.OrderBy(x => x.DlcTotalPrice).ToList();
                        break;
                    case SortField.MaxDiscount:
                        result = _steamProfile?.Library?.Games?.OrderBy(x => x.DlcHighestPercentage).ToList();
                        break;
                }
            }

            if (sortOrder == SortOrder.Descending)
            {
                switch (sortField)
                {
                    case SortField.TotalCost:
                        result = _steamProfile?.Library?.Games?.OrderByDescending(x => x.DlcTotalPrice).ToList();
                        break;
                    case SortField.MaxDiscount:
                        result = _steamProfile?.Library?.Games?.OrderByDescending(x => x.DlcHighestPercentage).ToList();
                        break;
                }
            }

            return result;
        }



        public static SortedDictionary<int, string?>? GetBlacklist()
        {
            return _steamProfile?.Library?.Blacklist;
        }

        public static void BlacklistGames(List<int> appIds)
        {
            appIds.ForEach(x => _steamProfile?.Library?.BlacklistGame(x));

            _steamProfile?.Library?.ApplyBlacklist();
            _steamProfile?.Library?.SaveBlacklist();
        }

        public static void UnblacklistGames(List<int> appIds)
        {
            appIds.ForEach(x => _steamProfile?.Library?.UnBlacklistGame(x));

            _steamProfile?.Library?.SaveBlacklist();
        }

        public static Dictionary<int, string> GetFreeDlc()
        {
            Dictionary<int, string> result = new();

            List<Game>? games = _steamProfile?.Library?.Games?.Where(x => x.DlcList.Any(y => !y.IsOwned && y.IsFree)).ToList();

            if (games is null)
            {
                return result;
            }

            foreach (Game game in games)
            {
                List<Dlc> dlcList = game.DlcList.Where(x => !x.IsOwned && x.IsFree).ToList();

                dlcList.ForEach(x => result.Add(x.AppId, $"{game.Name} - {x.Name}"));
            }

            return result;
        }
    }
}