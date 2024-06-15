using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PrEParateApp.View;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PrEParateApp.ViewModel
{
    public partial class AgendaPersonalVM : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        public AgendaPersonalVM(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [RelayCommand]
        public async Task RegistrarToma()
        {
            var viewModel = _serviceProvider.GetRequiredService<RegistroMedicacionVM>();
            var popup = new RegistroMedicacionView(viewModel);
            await Application.Current.MainPage.ShowPopupAsync(popup);
        }

        [RelayCommand]
        public async Task ConfigurarRecordatorio()
        {
            Application.Current.MainPage = _serviceProvider.GetService<RecordatorioView>();
        }
    }
}
