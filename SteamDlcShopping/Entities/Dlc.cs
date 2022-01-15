namespace SteamDlcShopping.Entities
{
    public class Dlc
    {
        //Properties
        public int AppId { get; }

        public string Name { get; }

        public decimal Price { get; }

        public Sale Sale { get; }

        public bool OnSale
        {
            get
            {
                return Sale != null;
            }
        }

        public bool IsFree { get; }

        public bool IsNotAvailable { get; }

        public bool IsOwned { get; private set; }

        //Constructor
        public Dlc(int appId = default, string name = default, decimal price = default, Sale sale = default, bool isFree = default, bool isNotAvailable = default)
        {
            AppId = appId;
            Name = name;
            Price = price;
            Sale = sale;
            IsFree = isFree;
            IsNotAvailable = isNotAvailable;
        }

        //Methods
        public void MarkAsOwned()
        {
            IsOwned = true;
        }
    }
}