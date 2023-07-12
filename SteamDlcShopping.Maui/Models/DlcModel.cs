using SteamDlcShopping.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamDlcShopping.Maui.Models;
public class DlcModel
{
    private DlcView _dlcView;

    public Int32 AppId => _dlcView.AppId;
    public String Name => _dlcView.Name;
    public String Price => _dlcView.Price;
    public String Discount => _dlcView.Discount;
    public Boolean IsOwned => _dlcView.IsOwned;

    public String Banner => $"https://cdn.cloudflare.steamstatic.com/steam/apps/{_dlcView.AppId}/capsule_231x87.jpg";

    public static implicit operator DlcView(DlcModel model)
    {
        return model._dlcView;
    }
    public static implicit operator DlcModel(DlcView view)
    {
        return new DlcModel()
        {
            _dlcView = view,
        };
    }
}
