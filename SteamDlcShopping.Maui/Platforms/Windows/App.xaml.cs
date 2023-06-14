using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Windows.Graphics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SteamDlcShopping.Maui.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            // Nothing like a bit of magic to make the window size fixed... What a nice start...
            //WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, _) =>
            //{
            //    handler.PlatformView.GetAppWindow().Resize(new SizeInt32(1280, 720));

            //    IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(handler.PlatformView);
            //    WindowId windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
            //    AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

            //    OverlappedPresenter presenter = appWindow.Presenter as OverlappedPresenter;
            //    presenter.IsResizable = false;
            //    presenter.IsMaximizable = false; // It can still maximize...
            //});
            // Actually, screw that, let the user mess with the size.
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}