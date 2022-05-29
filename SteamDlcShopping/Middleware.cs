using SteamDlcShopping.Dtos;
using SteamDlcShopping.Entities;
using SteamDlcShopping.Properties;

namespace SteamDlcShopping
{
    public static class Middleware
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

            return _steamProfile?.Library?.DynamicStore?.Count > 0;
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

        public static LibraryDto GetGames(string? filterName = null, bool filterOnSale = false)
        {
            LibraryDto result = new();
            result.Games = new();

            decimal totalCost = 0m;

            List<Game>? library = _steamProfile?.Library?.Games;

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

        public static List<DlcDto> GetDlc(int appId, string? filterName = null, bool filterOwned = false)
        {
            List<DlcDto> result = new();

            Game? game = _steamProfile?.Library?.Games?.FirstOrDefault(x => x.AppId == appId);

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

                DlcDto dlcDto = new()
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
            Game? game = _steamProfile?.Library?.Games?.FirstOrDefault(x => x.AppId == appId);
            return game is not null && game.TooManyDlc;
        }

        public static void LoadGamesDlc()
        {
            _steamProfile?.Library?.LoadDynamicStore();
            _steamProfile?.Library?.LoadGames();
            _steamProfile?.Library?.LoadBlacklist();

            _steamProfile?.Library?.ApplyBlacklist();

            _steamProfile?.Library?.LoadGamesDlc();

            _steamProfile?.Library?.ImproveGamesList();

            if (_steamProfile?.Library?.Games is null)
            {
                return;
            }

            foreach (Game game in _steamProfile.Library.Games)
            {
                game.CalculateDlcMetrics();
            }
        }



        public static List<GameBlacklistDto> GetBlacklist()
        {
            List<GameBlacklistDto> result = new();

            if (_steamProfile?.Library?.Blacklist is null)
            {
                return result;
            };

            foreach (GameBlacklist game in _steamProfile.Library.Blacklist)
            {
                GameBlacklistDto gameBlacklist = new()
                {
                    AppId = game.AppId,
                    Name = game.AutoBlacklisted ? $"{game.Name} (Auto Blacklist)" : game.Name,
                    AutoBlacklisted = game.AutoBlacklisted
                };

                result.Add(gameBlacklist);
            }

            return result;
        }

        public static void BlacklistGames(List<int> appIds)
        {
            appIds.ForEach(x => _steamProfile?.Library?.BlacklistGame(x, false));

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