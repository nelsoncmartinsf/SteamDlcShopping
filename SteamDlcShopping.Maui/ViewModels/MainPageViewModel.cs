using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SteamDlcShopping.Maui.ViewModels;
internal class MainPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public ICommand SignInCommand { get; init; }
    public ICommand SignOutCommand { get; init; }

    public ICommand SettingsCommand { get; init; }
    public ICommand AboutCommand { get; init; }


}
