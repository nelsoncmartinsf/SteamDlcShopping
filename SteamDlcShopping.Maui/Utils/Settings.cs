using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamDlcShopping.Maui.Utils;
internal static class Settings
{
    public static String SessionId
    {
        get => Preferences.Get(nameof(SessionId), "");
        set => Preferences.Set(nameof(SessionId), value);
    }
    public static String SteamLoginSecure
    {
        get => Preferences.Get(nameof(SteamLoginSecure), "");
        set => Preferences.Set(nameof(SteamLoginSecure), value);
    }
    public static String SteamApiKey
    {
        get => Preferences.Get(nameof(SteamApiKey), "");
        set => Preferences.Set(nameof(SteamApiKey), value);
    }

    public static Boolean AutoBlacklist
    {
        get => Preferences.Get(nameof(AutoBlacklist), false);
        set => Preferences.Set(nameof(AutoBlacklist), value);
    }
    public static Int32 AutoBlacklistReminder
    {
        get => Preferences.Get(nameof(AutoBlacklist), -1);
        set => Preferences.Set(nameof(AutoBlacklist), value);
    }
    public static DateTime AutoBlacklistLastReminder
    {
        get => Preferences.Get(nameof(AutoBlacklist), DateTime.MaxValue);
        set => Preferences.Set(nameof(AutoBlacklist), value);
    }

    public static Boolean HideGamesNotOnSale
    {
        get => Preferences.Get(nameof(HideGamesNotOnSale), false);
        set => Preferences.Set(nameof(HideGamesNotOnSale), value);
    }
    public static Boolean HideDlcNotOnSale
    {
        get => Preferences.Get(nameof(HideDlcNotOnSale), false);
        set => Preferences.Set(nameof(HideDlcNotOnSale), value);
    }
    public static Boolean HideOwnedDlc
    {
        get => Preferences.Get(nameof(HideOwnedDlc), false);
        set => Preferences.Set(nameof(HideOwnedDlc), value);
    }

    public static Boolean OpenPagesOnSteamClient
    {
        get => Preferences.Get(nameof(OpenPagesOnSteamClient), false);
        set => Preferences.Set(nameof(OpenPagesOnSteamClient), value);
    }
}
