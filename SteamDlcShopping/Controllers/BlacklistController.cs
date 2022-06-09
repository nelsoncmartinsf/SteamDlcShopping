using SteamDlcShopping.Models;
using SteamDlcShopping.ViewModels;

namespace SteamDlcShopping.Controllers
{
    public class BlacklistController
    {
        private static Blacklist? _blacklist;

        public static List<GameBlacklistView> GetBlacklist(string? filterName = null, bool _filterAutoBlacklisted = false)
        {
            List<GameBlacklistView> result = new();

            if (_blacklist is null)
            {
                return result;
            };

            if (_blacklist.Games is null)
            {
                return result;
            };

            foreach (GameBlacklist game in _blacklist.Games)
            {
                //Filter by name search
                if (string.IsNullOrWhiteSpace(game.Name) || !game.Name.Contains(filterName ?? string.Empty, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                //Filter by games not auto blacklisted
                if (_filterAutoBlacklisted && game.AutoBlacklisted)
                {
                    continue;
                }

                GameBlacklistView gameBlacklist = new()
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
            if (_blacklist is null)
            {
                return;
            }

            appIds.ForEach(x => _blacklist.BlacklistGame(x, LibraryController.GetGameName(x), false));
            _blacklist.SaveBlacklist();

            if (_blacklist.Games is null)
            {
                return;
            }

            LibraryController.ApplyBlacklist(appIds);
        }

        public static void UnblacklistGames(List<int> appIds)
        {
            if (_blacklist is null)
            {
                return;
            }

            appIds.ForEach(x => _blacklist.UnBlacklistGame(x));
            _blacklist.SaveBlacklist();
        }

        public static void ClearAutoBlacklist()
        {
            if (_blacklist is null)
            {
                return;
            }

            if (_blacklist.Games is null)
            {
                return;
            }

            for (int index = _blacklist.Games.Count - 1; index >= 0; index--)
            {
                GameBlacklist game = _blacklist.Games[index];

                if (!game.AutoBlacklisted)
                {
                    continue;
                }

                _blacklist.UnBlacklistGame(game.AppId);
            }

            _blacklist.SaveBlacklist();
        }
    }
}