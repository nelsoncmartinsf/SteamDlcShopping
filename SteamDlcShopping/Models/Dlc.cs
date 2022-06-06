namespace SteamDlcShopping.Models
{
    public class Dlc
    {
        //Properties
        internal int AppId { get; }

        internal string? Name { get; }

        internal decimal Price { get; }

        internal Sale? Sale { get; }

        internal bool IsFree { get; }

        internal bool IsNotAvailable { get; }

        internal bool IsOwned { get; private set; }

        //Constructor
        internal Dlc(int appId = default, string? name = default, decimal price = default, Sale? sale = default, bool isFree = default, bool isNotAvailable = default)
        {
            AppId = appId;
            Name = name;
            Price = price;
            Sale = sale;
            IsFree = isFree;
            IsNotAvailable = isNotAvailable;
        }

        //Methods
        internal void MarkAsOwned()
        {
            IsOwned = true;
        }
    }
}