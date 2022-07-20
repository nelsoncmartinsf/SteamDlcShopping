using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace SteamDlcShopping.Core.Models
{
    internal class Library
    {
        //Fields
        private readonly long _steamId;

        //Properties
        internal List<int>? DynamicStore { get; private set; }

        internal List<Game>? Games { get; private set; }

        internal long? TotalCost => Games?.Sum(x => x.DlcTotalPrice);

        //Constructor
        internal Library(long steamId)
        {
            _steamId = steamId;
        }

        //Methods
        internal void LoadDynamicStore(string sessionId, string steamLoginSecure)
        {
            HttpResponseMessage response;
            Uri uri = new("https://store.steampowered.com/dynamicstore/userdata/");

            using HttpClientHandler handler = new();
            handler.CookieContainer = new();
            handler.CookieContainer.Add(uri, new Cookie("sessionid", sessionId));
            handler.CookieContainer.Add(uri, new Cookie("steamLoginSecure", steamLoginSecure));

            HttpClient client = new(handler);
            response = client.GetAsync(uri).Result;

            JObject jObject = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            DynamicStore = JsonConvert.DeserializeObject<List<int>>($"{jObject["rgOwnedApps"]}");
        }

        internal void LoadGames(string steamApiKey)
        {
            HttpClient httpClient = new();

            Task<string> response = httpClient.GetStringAsync($"https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key={steamApiKey}&steamid={_steamId}&include_appinfo=true");
            response.Wait();

            if (!response.IsCompletedSuccessfully)
            {
                return;
            }

            JObject jObject = JObject.Parse(response.Result);

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
            int size = Games.Count / threads;

            using CountdownEvent countdownEvent = new(Games.Count % threads == 0 ? threads : threads + 1);

            for (int count = 0; count * size < Games.Count; count++)
            {
                ThreadPool.QueueUserWorkItem(delegate (object? count)
                {
                    for (int? index = (count as int?) * size; index < ((count as int?) + 1) * size; index++)
                    {
                        if (index is null)
                        {
                            continue;
                        }

                        if (index == Games.Count)
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

        internal void ApplyBlacklist(List<int> appIds)
        {
            if (Games is null)
            {
                return;
            }

            Games.RemoveAll(x => appIds.Contains(x.AppId));
        }
    }
}