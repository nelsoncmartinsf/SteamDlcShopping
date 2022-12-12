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

        internal long? TotalCurrentPrice => Games?.Sum(x => x.DlcTotalCurrentPrice);

        internal long? TotalFullPrice => Games?.Sum(x => x.DlcTotalFullPrice);

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

            Parallel.ForEach(Games, game => { game.LoadDlc(); });
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