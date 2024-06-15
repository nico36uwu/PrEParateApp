using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PrEParateApp.Model;
using System;
using System.Threading.Tasks;

namespace PrEParateApp.ViewModel
{
    public partial class RegistroMedicacionVM : ObservableObject
    {
        private readonly TomaMedicacionService _tomaMedicacionService;
        private readonly AuthenticationService _authService;
        private Popup _popUp;


        public RegistroMedicacionVM(TomaMedicacionService tomaMedicacionService, AuthenticationService authService)
        {
            _tomaMedicacionService = tomaMedicacionService;
            _authService = authService;
            Fecha = DateTime.Now;
            Hora = DateTime.Now.TimeOfDay;
            MaxFecha = DateTime.Now;
        }

        [ObservableProperty]
        private DateTime fecha;

        [ObservableProperty]
        private TimeSpan hora;

        [ObservableProperty]
        private string comentarios;

        [ObservableProperty]
        private DateTime maxFecha;

        public void SetPopup(Popup popup)
        {
            _popUp = popup;
        }

        [RelayCommand]
        public async Task Guardar()
        {
            if (string.IsNullOrEmpty(Comentarios))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Los comentarios no pueden estar vacíos.", "OK");
                return;
            }

            if (Fecha > DateTime.Now)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La fecha de la toma de medicación no puede ser en el futuro.", "OK");
                return;
            }

            var usuarioId = _authService.UsuarioConectado.ID;
            var tomaMedicacion = new TomaMedicacion(Comentarios, Fecha, Hora, usuarioId);

            bool isTomaCreada = await _tomaMedicacionService.CrearTomaMedicacion(tomaMedicacion);

            if (isTomaCreada)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Toma de medicación registrada correctamente.", "OK");
                MessagingCenter.Send(this, "NuevaTomaMedicacionRegistrada");
                // Reiniciar campos
                Fecha = DateTime.Now;
                Hora = DateTime.Now.TimeOfDay;
                Comentarios = string.Empty;
                ClosePopup();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Hubo un problema al registrar la toma de medicación. Inténtelo de nuevo.", "OK");
            }
        }

        [RelayCommand]
        public void Volver()
        {
            ClosePopup();
        }

        private void ClosePopup()
        {
            _popUp?.Close();
        }
    }
}
