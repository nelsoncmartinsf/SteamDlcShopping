namespace SteamDlcShopping.Models
{
    internal class Sale
    {
        //Properties
        internal int Percentage { get; }

        internal decimal Price { get; }

        //Constructor
        internal Sale(int percentage = default, decimal price = default)
        {
            Percentage = percentage;
            Price = price;
        }
    }
}