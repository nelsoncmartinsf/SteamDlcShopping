using SteamDlcShopping.Properties;
using System.Net;
using System.Xml;

namespace SteamDlcShopping.Entities
{
    public class SteamProfile
    {
        //Fields
        private long? _id;

        private long? _id3;

        private string _username;

        private string _avatarUrl;

        //Properties
        public long Id
        {
            get
            {
                if (_id == null)
                {
                    string steamId = WebUtility.UrlDecode(Settings.Default.SteamLoginSecure);
                    int index = steamId.IndexOf('|', 0);

                    if (index != -1)
                    {
                        steamId = steamId.Remove(index);
                    }

                    _id = Convert.ToInt64(steamId);
                }

                return _id.Value;
            }
        }

        public long Id3
        {
            get
            {
                if (_id3 == null)
                {
                    //Conversion from SteamID64 to the numeric part of SteamID3
                    _id3 = Id - 0x0110000100000000;
                }

                return _id3.Value;
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
                if (string.IsNullOrWhiteSpace(_avatarUrl))
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

        public Library Library { get; }

        public SortedDictionary<string, List<string>> Collections { get; set; }

        //Constructor
        public SteamProfile()
        {
            _username = string.Empty;
            _avatarUrl = string.Empty;
            Library = new();
            Collections = new();
        }
    }
}