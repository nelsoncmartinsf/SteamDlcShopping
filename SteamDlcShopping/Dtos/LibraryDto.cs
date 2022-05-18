namespace SteamDlcShopping.Dtos
{
    public class LibraryDto
    {
        //Properties
        public List<GameDto>? Games { get; set; }

        public int Size { get; set; }

        public decimal TotalCost { get; set; }
    }
}