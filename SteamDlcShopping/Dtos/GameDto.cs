namespace SteamDlcShopping.Dtos
{
    public class GameDto
    {
        //Properties
        public int AppId { get; set; }

        public string? Name { get; set; }

        public string? DlcTotalPrice { get; set; }

        public string? DlcHighestPercentage { get; set; }
    }
}