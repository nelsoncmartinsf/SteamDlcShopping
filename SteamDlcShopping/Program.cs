using SteamDlcShopping.Entities;
using SteamDlcShopping.Properties;

namespace SteamDlcShopping
{
    internal static class Program
    {
        internal static SteamProfile _steamProfile;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            _steamProfile = new();

            if (string.IsNullOrWhiteSpace(Settings.Default.SteamApiKey))
            {
                Application.Run(new FrmSettings());
            }

            Application.Run(new FrmMain());
        }
    }
}