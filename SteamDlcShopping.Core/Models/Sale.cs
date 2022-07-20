namespace SteamDlcShopping.Core.Models
{
    internal class Sale
    {
        //Properties
        internal int Percentage { get; }

        internal long Price { get; }

        //Constructor
        internal Sale(int percentage = default, long price = default)
        {
            Percentage = percentage;
            Price = price;
        }
    }
}