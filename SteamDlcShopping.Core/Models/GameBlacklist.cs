namespace SteamDlcShopping.Core.Models
{
    internal class GameBlacklist
    {
        //Properties
        internal int AppId { get; }

        internal string? Name { get; }

        internal bool AutoBlacklisted { get; }

        //Constructor
        public GameBlacklist(int appId, string? name, bool autoBlacklisted)
        {
            AppId = appId;
            Name = name;
            AutoBlacklisted = autoBlacklisted;
        }
    }
}