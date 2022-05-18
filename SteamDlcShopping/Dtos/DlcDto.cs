namespace SteamDlcShopping.Dtos
{
    public class DlcDto
    {
        //Properties
        public string? Name { get; set; }

        public string? Price { get; set; }

        public string? Discount { get; set; }

        public bool IsOwned { get; set; }
    }
}