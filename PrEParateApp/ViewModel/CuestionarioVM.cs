using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace PrEParateApp.ViewModel
{
    public partial class CuestionarioVM : ObservableObject
    {
        public CuestionarioVM()
        {
            TestConductalCommand = new AsyncRelayCommand(ShowDevelopmentAlert);
            CuestionarioCommand = new AsyncRelayCommand(ShowDevelopmentAlert);
        }

        public IAsyncRelayCommand TestConductalCommand { get; }
        public IAsyncRelayCommand CuestionarioCommand { get; }

        private async Task ShowDevelopmentAlert()
        {
            await Application.Current.MainPage.DisplayAlert("En desarrollo", "Esta funcionalidad está en desarrollo y de momento no está disponible.", "OK");
        }
    }
}
