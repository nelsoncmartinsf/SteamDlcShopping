namespace SteamDlcShopping.Entities
{
    internal class GameBlacklist
    {
        //Properties
        public int AppId { get; set; }

        public string? Name { get; set; }

        public bool AutoBlacklisted { get; set; }
    }
}