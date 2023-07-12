using SteamDlcShopping.Core.ViewModels;

namespace SteamDlcShopping.Maui.Models;

public class GameModel
{
    private GameView _gameView;

    public Int32 AppId => _gameView.AppId;
    public String Name => _gameView.Name;
    public String Banner => $"https://cdn.cloudflare.steamstatic.com/steam/apps/{_gameView.AppId}/capsule_231x87.jpg";

    public static implicit operator GameView(GameModel model)
    {
        return model._gameView;
    }
    public static implicit operator GameModel(GameView view)
    {
        return new GameModel()
        {
            _gameView = view,
        };
    }
}
