namespace SteamDlcShopping.Entities
{
    public class Sale
    {
        //Properties
        public int Percentage { get; }

        public decimal Price { get; }

        //Constructor
        public Sale(int percentage = default, decimal price = default)
        {
            Percentage = percentage;
            Price = price;
        }
    }
}