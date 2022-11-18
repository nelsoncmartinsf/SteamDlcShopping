namespace SteamDlcShopping.Core.ViewModels
{
    public class LibraryView
    {
        //Properties
        public List<GameView>? Games { get; set; }

        public string? TotalCurrentPrice { get; set; }
        
        public string? TotalFullPrice { get; set; }
    }
}