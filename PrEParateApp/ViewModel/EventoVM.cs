using CommunityToolkit.Mvvm.Input;
using PrEParateApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrEParateApp.ViewModel
{
    public partial class EventoVM
    {

        public EventoVM() { }

        [RelayCommand]
        public async void Volver()
        {
            Application.Current.MainPage = MauiProgram.App.Services.GetService<MainPageView>();
        }
    }
}
