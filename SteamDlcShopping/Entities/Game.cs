using HtmlAgilityPack;
using System.Globalization;
using System.Net;

namespace SteamDlcShopping.Entities
{
    public class Game
    {
        //Properties
        public int AppId { get; }

        public string Name { get; }

        public decimal DlcTotalPrice { get; private set; }

        public int DlcHighestPercentage { get; private set; }

        public int DlcAmount
        {
            get
            {
                return DlcList.Count;
            }
        }

        public List<Dlc> DlcList { get; private set; }

        //Constructor
        public Game(int appId = default, string name = default)
        {
            AppId = appId;
            Name = name;
            DlcList = new();
        }

        //Methods
        public void LoadDlc()
        {
            try
            {
                HttpClient httpClient = new();
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
                    string name = WebUtility.HtmlDecode(node.SelectSingleNode("./div[@class='game_area_dlc_name']").InnerText?.Trim());
                    string price = priceNode.InnerText.Trim();

                    string originalPrice = priceNode.SelectSingleNode(".//div[@class='discount_original_price']")?.InnerText?.Trim();
                    string salePrice = priceNode.SelectSingleNode(".//div[@class='discount_final_price']")?.InnerText?.Trim();
                    string salePercentage = priceNode.SelectSingleNode(".//div[@class='discount_pct']")?.InnerText?.Trim();

                    bool isFree = false;
                    bool isNotAvailable = false;
                    Sale sale = null;

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
                            if (!string.IsNullOrWhiteSpace(salePrice))
                            {
                                price = originalPrice;
                                int iSalePercentage = Convert.ToInt32(salePercentage[1..^1]);

                                //Formatting of rounded values with -- on the decimal part
                                salePrice = salePrice.Replace('-', '0');
                                decimal dSalePrice = decimal.Parse(salePrice, NumberStyles.Currency, new CultureInfo("pt-PT"));

                                sale = new Sale(iSalePercentage, dSalePrice);
                            }

                            //Formatting of rounded values with -- on the decimal part
                            price = price.Replace('-', '0');
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

        public void CalculateDlcMetrics()
        {
            DlcTotalPrice = 0;
            DlcHighestPercentage = 0;

            foreach (Dlc dlc in DlcList)
            {
                if (dlc.IsOwned)
                {
                    continue;
                }

                if (!dlc.OnSale)
                {
                    DlcTotalPrice += dlc.Price;
                    continue;
                }

                DlcTotalPrice += dlc.Sale.Price;

                if (dlc.Sale.Percentage > DlcHighestPercentage)
                {
                    DlcHighestPercentage = dlc.Sale.Percentage;
                }
            }
        }
    }
}