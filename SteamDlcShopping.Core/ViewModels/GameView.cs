namespace SteamDlcShopping.Core.ViewModels;

public class GameView
{
    //Properties
    public int AppId { get; set; }

    public string? Name { get; set; }

    public string? DlcTotalCurrentPrice { get; set; }

    public string? DlcTotalFullPrice { get; set; }

    public int DlcLeft { get; set; }

    public string? DlcHighestPercentage { get; set; }
}