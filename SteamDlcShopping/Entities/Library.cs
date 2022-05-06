using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamDlcShopping.Enums;
using SteamDlcShopping.Properties;
using System.Net;

namespace SteamDlcShopping.Entities
{
    public class Library
    {
        //Fields
        private long _steamId;

        private List<int>? _dynamicStore;

        private List<Game>? _games;

        //Properties

        public List<Game> Games => _games;

        public SortedDictionary<int, string> Blacklist { get; private set; }

        public int Size => Games.Count;

        public decimal TotalCost => Games.Sum(x => x.DlcTotalPrice);

        //Constructor
        public Library(long steamId)
        {
            _steamId = steamId;
        }

        //Methods
        private void LoadDynamicStore()
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
            _dynamicStore = JsonConvert.DeserializeObject<List<int>>(jObject["rgOwnedApps"].ToString());
        }
        
        private void LoadGames()
        {
            HttpClient httpClient = new();
            string response = httpClient.GetStringAsync($"https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key={Settings.Default.SteamApiKey}&steamid={_steamId}&include_appinfo=true").Result;

            JObject jObject = JObject.Parse(response);
            _games = JsonConvert.DeserializeObject<List<Game>>(jObject["response"]["games"].ToString());
        }



        public void LoadGamesDlc()
        {
            LoadDynamicStore();
            LoadGames();
            LoadBlacklist();

            ApplyBlacklist();
            SortGamesBy(SortField.AppId);

            //Load all dlc for all games
            int threads = 10;
            int size = Size / threads;

            using CountdownEvent countdownEvent = new(Size % threads == 0 ? threads : threads + 1);

            for (int count = 0; (count * size) < Size; count++)
            {
                ThreadPool.QueueUserWorkItem(delegate (object count)
                {
                    for (int? idx = (count as int?) * size; idx < ((count as int?) + 1) * size; idx++)
                    {
                        if (idx == Size)
                        {
                            break;
                        }

                        Games[idx.Value].LoadDlc();
                    }

                    countdownEvent.Signal();
                }, count);
            }

            countdownEvent.Wait();

            ImproveGamesList();

            foreach (Game game in Games)
            {
                game.CalculateDlcMetrics();
            }
        }

        private void ImproveGamesList()
        {
            List<int> gamesToRemove = new();

            foreach (Game game in Games)
            {
                //Mark to remove games without dlc
                if (game.DlcList is null || game.DlcAmount == 0)
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
                    if (_dynamicStore.Contains(dlc.AppId))
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



        public void BlacklistGame(int appId)
        {
            Game game = Games.First(x => x.AppId == appId);
            Blacklist.Add(game.AppId, game.Name);
        }

        public void UnBlacklistGame(int appId)
        {
            Blacklist.Remove(appId);
        }

        public void ApplyBlacklist()
        {
            Games.RemoveAll(x => Blacklist.ContainsKey(x.AppId));
        }

        public void LoadBlacklist()
        {
            if (!File.Exists("blacklist.txt"))
            {
                Blacklist = new();
                return;
            }

            string content = File.ReadAllText("blacklist.txt");
            Blacklist = JsonConvert.DeserializeObject<SortedDictionary<int, string>>(content);

            if (Blacklist is null)
            {
                Blacklist = new();
            }
        }

        public void SaveBlacklist()
        {
            string content = JsonConvert.SerializeObject(Blacklist);
            File.WriteAllText("blacklist.txt", content);
        }



        public void SortGamesBy(SortField sortField, Enums.SortOrder sortOrder = Enums.SortOrder.Ascending)
        {
            if (sortOrder == Enums.SortOrder.Ascending)
            {
                switch (sortField)
                {
                    case SortField.TotalCost:
                        _games = Games.OrderBy(x => x.DlcTotalPrice).ToList();
                        break;
                    case SortField.MaxDiscount:
                        _games = Games.OrderBy(x => x.DlcHighestPercentage).ToList();
                        break;
                }
            }

            if (sortOrder == Enums.SortOrder.Descending)
            {
                switch (sortField)
                {
                    case SortField.TotalCost:
                        _games = Games.OrderByDescending(x => x.DlcTotalPrice).ToList();
                        break;
                    case SortField.MaxDiscount:
                        _games = Games.OrderByDescending(x => x.DlcHighestPercentage).ToList();
                        break;
                }
            }
        }

        public Game GetGameByAppId(int appId)
        {
            foreach (Game game in Games)
            {
                if (game.AppId == appId)
                {
                    return game;
                }
            }

            return null;
        }

        public Game GetGameByName(string name)
        {
            foreach (Game game in Games)
            {
                if (game.Name == name)
                {
                    return game;
                }
            }

            return null;
        }
    }
}