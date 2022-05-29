using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamDlcShopping.Properties;
using System.Net;

namespace SteamDlcShopping.Entities
{
    public class Library
    {
        //Fields
        private readonly long _steamId;

        //Properties
        internal List<int>? DynamicStore { get; private set; }

        internal List<Game>? Games { get; private set; }

        internal List<GameBlacklist>? Blacklist { get; private set; }

        internal int? Size => Games?.Count;

        internal decimal? TotalCost => Games?.Sum(x => x.DlcTotalPrice);

        //Constructor
        internal Library(long steamId)
        {
            _steamId = steamId;

            LoadDynamicStore();
            LoadBlacklist();
        }

        //Methods
        internal void LoadDynamicStore()
        {
            HttpResponseMessage response;
            Uri uri = new("https://store.steampowered.com/dynamicstore/userdata/");

            using HttpClientHandler handler = new();
            handler.CookieContainer = new CookieContainer();
            handler.CookieContainer.Add(uri, new Cookie("sessionid", Settings.Default.SessionId));
            handler.CookieContainer.Add(uri, new Cookie("steamLoginSecure", Settings.Default.SteamLoginSecure));

            HttpClient client = new(handler);
            response = client.GetAsync(uri).Result;

            JObject jObject = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            DynamicStore = JsonConvert.DeserializeObject<List<int>>($"{jObject["rgOwnedApps"]}");
        }

        internal void LoadGames()
        {
            HttpClient httpClient = new();
            string response = httpClient.GetStringAsync($"https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key={Settings.Default.SteamApiKey}&steamid={_steamId}&include_appinfo=true").Result;

            JObject jObject = JObject.Parse(response);

            Games = JsonConvert.DeserializeObject<List<Game>>($"{jObject["response"]?["games"]}");
        }



        internal void LoadGamesDlc()
        {
            if (Games is null)
            {
                return;
            }

            //Load all dlc for all games
            int threads = 10;
            int size = (Size ?? 0) / threads;

            using CountdownEvent countdownEvent = new(Size % threads == 0 ? threads : threads + 1);

            for (int count = 0; (count * size) < Size; count++)
            {
                ThreadPool.QueueUserWorkItem(delegate (object? count)
                {
                    for (int? index = (count as int?) * size; index < ((count as int?) + 1) * size; index++)
                    {
                        if (index is null)
                        {
                            continue;
                        }

                        if (index == Size)
                        {
                            break;
                        }

                        Games[index.Value].LoadDlc();
                    }

                    countdownEvent.Signal();
                }, count);
            }

            countdownEvent.Wait();
        }

        internal void ImproveGamesList()
        {
            List<int> gamesToRemove = new();

            if (Games is null)
            {
                return;
            }

            foreach (Game game in Games)
            {
                //Mark to remove games without dlc
                if (game.DlcList is null || game.DlcList.Count == 0)
                {
                    if (Settings.Default.AutoBlacklist)
                    {
                        BlacklistGame(game.AppId);
                    }

                    int index = Games.IndexOf(game);
                    gamesToRemove.Insert(0, index);
                    continue;
                }

                bool allOwned = true;

                foreach (Dlc dlc in game.DlcList)
                {
                    //Mark dlc as owned
                    if (DynamicStore is not null && DynamicStore.Contains(dlc.AppId))
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
                    int index = Games.IndexOf(game);
                    gamesToRemove.Insert(0, index);
                    continue;
                }
            }

            //Remove marked games
            //gamesToRemove id order is reversed so that deleting indexes doesn't break Games
            foreach (int index in gamesToRemove)
            {
                Games.RemoveAt(index);
            }

            if (Settings.Default.AutoBlacklist)
            {
                SaveBlacklist();
            }
        }



        internal void BlacklistGame(int appId, bool autoBlacklisted = true)
        {
            Game? game = Games?.First(x => x.AppId == appId);

            if (game is null)
            {
                return;
            }

            GameBlacklist gameBlacklist = new()
            {
                AppId = game.AppId,
                Name = game.Name,
                AutoBlacklisted = autoBlacklisted
            };

            Blacklist?.Add(gameBlacklist);
        }

        internal void UnBlacklistGame(int appId)
        {
            if (Blacklist is null)
            {
                return;
            }

            if (!Blacklist.Any(x => x.AppId == appId))
            {
                return;
            }

            int index = Blacklist.FindIndex(x => x.AppId == appId);
            Blacklist?.RemoveAt(index);
        }

        internal void ApplyBlacklist()
        {
            if (Blacklist is null)
            {
                return;
            }

            Games?.RemoveAll(x => Blacklist.Any(y => x.AppId == y.AppId));
        }

        internal void LoadBlacklist()
        {
            if (!File.Exists("blacklist.txt"))
            {
                Blacklist = new();
                return;
            }

            string content = File.ReadAllText("blacklist.txt");
            Blacklist = JsonConvert.DeserializeObject<List<GameBlacklist>>(content);

            if (Blacklist is null)
            {
                Blacklist = new();
            }
        }

        internal void SaveBlacklist()
        {
            string content = JsonConvert.SerializeObject(Blacklist);
            File.WriteAllText("blacklist.txt", content);
        }
    }
}