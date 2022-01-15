using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SteamDlcShopping.Properties;
using System.Net;
using System.Xml;

namespace SteamDlcShopping.Entities
{
    public class SteamProfile
    {
        //Fields
        private string _id;

        private string _username;

        private string _avatarUrl;

        //Properties
        public string Id
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_id))
                {
                    string steamId = WebUtility.UrlDecode(Settings.Default.SteamLoginSecure);
                    int index = steamId.IndexOf('|', 0);

                    if (index != -1)
                    {
                        steamId = steamId.Remove(index);
                    }

                    _id = steamId;
                }

                return _id;
            }
        }

        public string Username
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_username))
                {
                    HttpClient httpClient = new();
                    string xml = httpClient.GetStringAsync($"{Url}/?xml=1").Result;

                    XmlDocument xmlDocument = new();
                    xmlDocument.LoadXml(xml);

                    XmlNode xmlNode = xmlDocument.SelectSingleNode("//steamID");
                    _username = WebUtility.HtmlDecode(xmlNode.InnerText);
                }

                return _username;
            }
        }

        public string AvatarUrl
        {
            get
            {
                if (_avatarUrl == null)
                {
                    HttpClient httpClient = new();
                    string xml = httpClient.GetStringAsync($"{Url}/?xml=1").Result;

                    XmlDocument xmlDocument = new();
                    xmlDocument.LoadXml(xml);

                    XmlNode xmlNode = xmlDocument.SelectSingleNode("//avatarMedium");
                    _avatarUrl = xmlNode.InnerText;
                }

                return _avatarUrl;
            }
        }

        public string Url
        {
            get
            {
                return $"https://steamcommunity.com/profiles/{Id}";
            }
        }

        public static bool IsLoggedIn
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Settings.Default.SessionId) && !string.IsNullOrWhiteSpace(Settings.Default.SteamLoginSecure);
            }
        }

        public Library Library { get; private set; }

        //Methods
        public void LoadLibrary(string apiKey)
        {
            HttpClient httpClient = new();
            string response = httpClient.GetStringAsync($"https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key={apiKey}&steamid={Id}&include_appinfo=true").Result;

            JObject jObject = JObject.Parse(response);
            Library = JsonConvert.DeserializeObject<Library>(jObject["response"].ToString());

            Library.Games = Library.Games.Take(100).ToList();
        }
    }
}