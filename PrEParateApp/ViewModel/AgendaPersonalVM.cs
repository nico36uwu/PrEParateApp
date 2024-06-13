using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PrEParateApp.View;
using System.Threading.Tasks;

namespace PrEParateApp.ViewModel
{
    public partial class AgendaPersonalVM : ObservableObject
    {
        [RelayCommand]
        public async Task RegistrarToma()
        {
            // Lógica para registrar toma de medicación
            Application.Current.MainPage = MauiProgram.App.Services.GetService<RegistroMedicacionView>();
        }

        [RelayCommand]
        public async Task ConfigurarRecordatorio()
        {
            // Lógica para configurar recordatorio
            Application.Current.MainPage = MauiProgram.App.Services.GetService<RecordatorioView>();
        }
    }
}
