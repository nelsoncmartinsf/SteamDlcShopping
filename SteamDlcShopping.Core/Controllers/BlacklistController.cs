﻿using SteamDlcShopping.Core.Models;
using SteamDlcShopping.Core.ViewModels;

namespace SteamDlcShopping.Core.Controllers
{
    public class BlacklistController
    {
        private static Blacklist? _blacklist;

        internal static void Reset()
        {
            _blacklist = null;
        }

        public static void Load()
        {
            if (_blacklist is null)
            {
                _blacklist = new Blacklist();
            }

            _blacklist.Load();
        }

        internal static void Save()
        {
            if (_blacklist is null)
            {
                return;
            }

            _blacklist.Save();
        }

        internal static List<int> Get()
        {
            List<int> result = new();

            if (_blacklist is null)
            {
                return result;
            };

            if (_blacklist.Games is null)
            {
                return result;
            };

            _blacklist.Games.ForEach(x => result.Add(x.AppId));

            return result;
        }

        public static List<GameBlacklistView> GetView(string? filterName = null, bool _filterAutoBlacklisted = false)
        {
            List<GameBlacklistView> result = new();

            if (_blacklist is null)
            {
                return result;
            }

            if (_blacklist.Games is null)
            {
                return result;
            }

            foreach (GameBlacklist game in _blacklist.Games)
            {
                //Filter by name search
                if (!string.IsNullOrWhiteSpace(game.Name) && !game.Name.Contains(filterName ?? string.Empty, StringComparison.InvariantCultureIgnoreCase))
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

        public static void AddGames(List<int> appIds, bool autoBlacklist)
        {
            if (_blacklist is null)
            {
                return;
            }

            appIds.ForEach(x => _blacklist.AddGame(x, LibraryController.GetGameName(x), autoBlacklist));
            _blacklist.Save();

            if (_blacklist.Games is null)
            {
                return;
            }

            LibraryController.ApplyBlacklist(appIds);
        }

        public static void RemoveGames(List<int> appIds)
        {
            if (_blacklist is null)
            {
                return;
            }

            appIds.ForEach(x => _blacklist.RemoveGame(x));
            _blacklist.Save();
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

                _blacklist.RemoveGame(game.AppId);
            }

            _blacklist.Save();
        }
    }
}