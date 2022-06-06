namespace SteamDlcShopping.ViewModels
{
    public class GameView
    {
        //Properties
        public int AppId { get; set; }

        public string? Name { get; set; }

        public string? DlcTotalPrice { get; set; }

        public int DlcLeft { get; set; }

        public string? DlcLowestPercentage { get; set; }

        public string? DlcHighestPercentage { get; set; }
    }
}