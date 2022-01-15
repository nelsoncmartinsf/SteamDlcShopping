using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamDlcShopping.Properties;
using System.Net;

namespace SteamDlcShopping.Entities
{
    public class Library
    {
        //Properties
        public bool HasGames
        {
            get
            {
                return Games != null && Games.Any();
            }
        }

        public int Size
        {
            get
            {
                return Games.Count;
            }
        }

        [JsonProperty("games")]
        public List<Game> Games { get; set; }

        private List<int> OwnedApps { get; set; }

        //Methods
        public int FindGameIndex(int appId)
        {
            int idx = 0;

            foreach (Game game in Games)
            {
                if (game.AppId != appId)
                {
                    idx++;
                    continue;
                }

                return idx;
            }

            return -1;
        }

        public void LoadGamesDlc()
        {
            using CountdownEvent countdownEvent = new(Size);

            //Load all dlc for all games
            foreach (Game game in Games)
            {
                ThreadPool.QueueUserWorkItem(x =>
                {
                    game.LoadDlc();
                    countdownEvent.Signal();
                });
            }

            countdownEvent.Wait();

            //Load owned apps
            LoadDynamicStore();

            int idx = 0;
            while (idx < Games.Count)
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
    }
}