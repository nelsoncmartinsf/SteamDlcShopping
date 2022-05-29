using SteamDlcShopping.Properties;
using System.Net;
using System.Xml;

namespace SteamDlcShopping.Entities
{
    internal class SteamProfile
    {
        //Properties
        private const string _url = "https://steamcommunity.com/profiles";

        internal long Id { get; }

        internal string? Username { get; }

        internal string? AvatarUrl { get; }

        internal Library? Library { get; }

        //Constructor
        internal SteamProfile()
        {
            if (string.IsNullOrWhiteSpace(Settings.Default.SessionId) || string.IsNullOrWhiteSpace(Settings.Default.SteamLoginSecure))
            {
                return;
            }

            HttpClient httpClient;
            string xml;
            XmlDocument xmlDocument;
            XmlNode? xmlNode;

            //Id
            string steamId = WebUtility.UrlDecode(Settings.Default.SteamLoginSecure);
            int index = steamId.IndexOf('|', 0);

            if (index != -1)
            {
                steamId = steamId.Remove(index);
            }

            Id = Convert.ToInt64(steamId);

            //Username
            httpClient = new();
            xml = httpClient.GetStringAsync($"{_url}/{Id}/?xml=1").Result;

            xmlDocument = new();
            xmlDocument.LoadXml(xml);

            xmlNode = xmlDocument.SelectSingleNode("//steamID");
            Username = xmlNode is not null ? WebUtility.HtmlDecode(xmlNode.InnerText) : null;

            //AvatarUrl
            httpClient = new();
            xml = httpClient.GetStringAsync($"{_url}/{Id}/?xml=1").Result;

            xmlDocument = new();
            xmlDocument.LoadXml(xml);

            xmlNode = xmlDocument.SelectSingleNode("//avatarMedium");
            AvatarUrl = xmlNode is not null ? WebUtility.HtmlDecode(xmlNode.InnerText) : null;

            //Library
            Library = new(Id);
        }
    }
}