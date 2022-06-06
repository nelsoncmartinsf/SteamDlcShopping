using HtmlAgilityPack;
using System.Globalization;
using System.Net;
using System.Web;

namespace SteamDlcShopping.Models
{
    internal class Game
    {
        //Properties
        internal int AppId { get; }

        internal string? Name { get; }

        internal decimal? DlcTotalPrice { get; private set; }

        internal int DlcLeft { get; private set; }

        internal int DlcLowestPercentage { get; private set; }

        internal int DlcHighestPercentage { get; private set; }

        internal bool TooManyDlc { get; set; }

        internal List<Dlc> DlcList { get; private set; }

        //Constructor
        public Game(int appId = default, string? name = default)
        {
            AppId = appId;
            Name = name;
            DlcList = new();
        }

        //Methods
        internal void LoadDlc()
        {
            try
            {
                Uri uri = new("https://store.steampowered.com");

                using HttpClientHandler handler = new();
                handler.CookieContainer = new CookieContainer();
                handler.CookieContainer.Add(uri, new Cookie("birthtime", HttpUtility.UrlEncode("birthtime=0; path=/; max-age=315360000")));

                HttpClient httpClient = new(handler);
                string response;

                using (HttpResponseMessage httpResponseMessage = httpClient.GetAsync($"https://store.steampowered.com/app/{AppId}").Result)
                {
                    using HttpContent content = httpResponseMessage.Content;
                    response = content.ReadAsStringAsync().Result;
                }

                //The html dlc area was not found
                if (!response.Contains("gameAreaDLCSection"))
                {
                    return;
                }

                if (response.Contains("game_area_dlc_excluded_by_preferences"))
                {
                    TooManyDlc = true;
                }

                HtmlAgilityPack.HtmlDocument htmlDoc = new();
                htmlDoc.LoadHtml(response);

                //The html parse failed
                if (htmlDoc.DocumentNode is null)
                {
                    return;
                }

                HtmlNodeCollection dlcList = htmlDoc.DocumentNode.SelectNodes("//a[contains(@class, 'game_area_dlc_row')]");

                //The node selection found no results
                if (dlcList is null || dlcList.Count == 0)
                {
                    return;
                }

                DlcList = new List<Dlc>();

                foreach (HtmlNode node in dlcList)
                {
                    HtmlNode priceNode = node.SelectSingleNode("./div[@class='game_area_dlc_price']");

                    string appId = node.Attributes["data-ds-appid"].Value;
                    string? name = WebUtility.HtmlDecode(node.SelectSingleNode("./div[@class='game_area_dlc_name']").InnerText?.Trim());
                    string? price = priceNode.InnerText.Trim();

                    string? originalPrice = priceNode.SelectSingleNode(".//div[@class='discount_original_price']")?.InnerText?.Trim();
                    string? salePrice = priceNode.SelectSingleNode(".//div[@class='discount_final_price']")?.InnerText?.Trim();
                    string? salePercentage = priceNode.SelectSingleNode(".//div[@class='discount_pct']")?.InnerText?.Trim();

                    bool isFree = false;
                    bool isNotAvailable = false;
                    Sale? sale = null;

                    decimal dPrice = 0;

                    switch (price.ToLower())
                    {
                        case "free":
                            isFree = true;
                            break;
                        case "n/a":
                            isNotAvailable = true;
                            break;
                        default:
                            //Dlc is currently on sale
                            if (!string.IsNullOrWhiteSpace(salePrice) && !string.IsNullOrWhiteSpace(salePercentage))
                            {
                                price = originalPrice;
                                int iSalePercentage = Convert.ToInt32(salePercentage[1..^1]);

                                //Formatting of rounded values with -- on the decimal part
                                salePrice = salePrice.Replace('-', '0');
                                decimal dSalePrice = decimal.Parse(salePrice, NumberStyles.Currency, new CultureInfo("pt-PT"));

                                sale = new Sale(iSalePercentage, dSalePrice);
                            }

                            //Formatting of rounded values with -- on the decimal part
                            price = (price ?? "").Replace('-', '0');
                            dPrice = decimal.Parse(price, NumberStyles.Currency, new CultureInfo("pt-PT"));

                            break;
                    }

                    Dlc dlc = new(Convert.ToInt32(appId), name, dPrice, sale, isFree, isNotAvailable);
                    DlcList.Add(dlc);
                }
            }
            catch
            {
                //TODO
            }
        }

        internal void CalculateDlcMetrics()
        {
            DlcTotalPrice = 0;
            DlcLeft = 0;
            DlcLowestPercentage = 0;
            DlcHighestPercentage = 0;

            foreach (Dlc dlc in DlcList)
            {
                if (dlc.IsOwned)
                {
                    continue;
                }

                DlcLeft++;

                if (dlc.Sale is null)
                {
                    DlcTotalPrice += dlc.Price;
                    continue;
                }

                DlcTotalPrice += dlc.Sale.Price;

                if (dlc.Sale.Percentage < DlcLowestPercentage || DlcLowestPercentage == 0)
                {
                    DlcLowestPercentage = dlc.Sale.Percentage;
                }

                if (dlc.Sale?.Percentage > DlcHighestPercentage)
                {
                    DlcHighestPercentage = dlc.Sale.Percentage;
                }
            }
        }
    }
}