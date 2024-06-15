using CommunityToolkit.Mvvm.Input;
using PrEParateApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrEParateApp.ViewModel
{
    public partial class CalendarioVM
    {
        private readonly IServiceProvider _serviceProvider;

        public CalendarioVM(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [RelayCommand]
        public async Task PlanificarNuevoEvento()
        {
            Application.Current.MainPage = _serviceProvider.GetService<EventoView>();
        }
    }
}
