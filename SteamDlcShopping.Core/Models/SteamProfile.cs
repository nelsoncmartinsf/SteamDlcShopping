using System.Net;
using System.Xml;

namespace SteamDlcShopping.Core.Models
{
    internal class SteamProfile
    {
        //Properties
        private const string _url = "https://steamcommunity.com/profiles";

        internal long Id { get; }

        internal string? Username { get; }

        internal string? AvatarUrl { get; }

        //Constructor
        internal SteamProfile(string steamLoginSecure)
        {
            if (string.IsNullOrWhiteSpace(steamLoginSecure))
            {
                return;
            }

            HttpClient httpClient;
            string xml;
            XmlDocument xmlDocument;
            XmlNode? xmlNode;

            //Id
            string steamId = WebUtility.UrlDecode(steamLoginSecure);
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
        }
    }
}