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

            //Library.Games = Library.Games.Take(1).ToList();
        }

        public void LoadGamesDlc()
        {
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

        private void ImproveGamesList()
        {
            for (int idx = 0; idx < Games.Count;)
            {
                Game game = Games[idx];

                //Remove games without dlc
                if (game.DlcList == null || game.DlcAmount == 0)
                {
                    Games.RemoveAt(idx);
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

                    //Special rule to consider not available dlc as owned
                    //This marks games that are only missing not available dlc as completed
                    if (dlc.IsNotAvailable)
                    {
                        continue;
                    }

                    allOwned = false;
                }

                //Remove games with all dlc owned
                if (allOwned)
                {
                    Games.RemoveAt(idx);
                    continue;
                }

                //Calculate dlc metrics
                game.CalculateDlcMetrics();

                idx++;
            }
        }
    }
}