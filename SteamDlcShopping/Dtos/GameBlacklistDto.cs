namespace SteamDlcShopping.Dtos
{
    public class GameBlacklistDto
    {
        //Properties
        public int AppId { get; set; }

        public string? Name { get; set; }

        public bool AutoBlacklisted { get; set; }
    }
}