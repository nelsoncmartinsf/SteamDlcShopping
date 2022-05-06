using SteamDlcShopping.Properties;
using System.Net;
using System.Xml;

namespace SteamDlcShopping.Entities
{
    public class SteamProfile
    {
        //Fields
        private long? _id;

        private string? _username;

        private string? _avatarUrl;

        private Library? _library;

        //Properties
        public static bool IsLoggedIn
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Settings.Default.SessionId) && !string.IsNullOrWhiteSpace(Settings.Default.SteamLoginSecure);
            }
        }

        private long Id
        {
            get
            {
                if (!_id.HasValue)
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

        private string Url => $"https://steamcommunity.com/profiles/{Id}";

        public Library Library
        {
            get
            {
                if (_library is null)
                {
                    _library = new(Id);
                }

                return _library;
            }
        }
    }
}