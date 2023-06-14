namespace SteamDlcShopping.Core.Models;

internal class GameBlacklist
{
    //Properties
    public int AppId { get; }

    public string? Name { get; }

    public bool AutoBlacklisted { get; }

    //Constructor
    public GameBlacklist(int appId, string? name, bool autoBlacklisted)
    {
        AppId = appId;
        Name = name;
        AutoBlacklisted = autoBlacklisted;
    }
}