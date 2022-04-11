using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamDlcShopping.Properties;
using System.Net;

namespace SteamDlcShopping.Entities
{
    public class Library
    {
        //Properties
        public int Size
        {
            get
            {
                return Games.Count;
            }
        }

        public List<Game> Games { get; set; }

        private List<int> OwnedApps { get; set; }

        private SortedDictionary<int, string> Blacklist { get; set; }

        //Constructor
        public Library()
        {
            Games = new();
            OwnedApps = new();
        }

        //Methods
        public void LoadGames(string apiKey, long steamId)
        {
            HttpClient httpClient = new();
            string response = httpClient.GetStringAsync($"https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key={apiKey}&steamid={steamId}&include_appinfo=true").Result;

            JObject jObject = JObject.Parse(response);
            Games = JsonConvert.DeserializeObject<List<Game>>(jObject["response"]["games"].ToString());
        }

        public void LoadGamesDlc()
        {
            ApplyBlacklist();

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

            //Load owned apps
            LoadDynamicStore();

            countdownEvent.Wait();

            ImproveGamesList();

            foreach (Game game in Games)
            {
                game.CalculateDlcMetrics();
            }
        }

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
            OwnedApps = JsonConvert.DeserializeObject<List<int>>(jObject["rgOwnedApps"].ToString());
        }

        private void ApplyBlacklist()
        {
            string content;

            if (File.Exists("blacklist.txt"))
            {
                content = File.ReadAllText("blacklist.txt");
                Blacklist = JsonConvert.DeserializeObject<SortedDictionary<int, string>>(content);

                if (Blacklist != null)
                {
                    Games.RemoveAll(x => Blacklist.ContainsKey(x.AppId));
                }
            }
        }

        private void ImproveGamesList()
        {
            List<int> gamesToRemove = new();

            foreach (Game game in Games)
            {
                //Mark to remove games without dlc
                if (game.DlcList == null || game.DlcAmount == 0)
                {
                    if (Settings.Default.AutoBlacklist)
                    {
                        Blacklist.Add(game.AppId, game.Name);
                    }

                    int index = Games.IndexOf(game);
                    gamesToRemove.Insert(0, index);
                    continue;
                }

                bool allOwned = true;

                //Mark dlc as owned
                foreach (Dlc dlc in game.DlcList)
                {
                    if (OwnedApps.Contains(dlc.AppId))
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
                string content = JsonConvert.SerializeObject(Blacklist);
                File.WriteAllText("blacklist.txt", content);
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