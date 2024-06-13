using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.LocalNotification;
using PrEParateApp.Model;
using PrEParateApp.Utilities;
using PrEParateApp.View;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PrEParateApp.ViewModel
{
    public partial class RecordatorioVM : ObservableObject
    {
        private readonly RecordatorioService _recordatorioService;
        private readonly AuthenticationService _authService;

        public RecordatorioVM(RecordatorioService recordatorioService, AuthenticationService authService)
        {
            _recordatorioService = recordatorioService;
            _authService = authService;
            Frecuencias = new ObservableCollection<string>
            {
                Constantes.FRECUENCIA_DIARIA,
                Constantes.FRECUENCIA_LUNES,
                Constantes.FRECUENCIA_MARTES,
                Constantes.FRECUENCIA_MIERCOLES,
                Constantes.FRECUENCIA_JUEVES,
                Constantes.FRECUENCIA_VIERNES,
                Constantes.FRECUENCIA_SABADO,
                Constantes.FRECUENCIA_DOMINGO
            };
        }

        [ObservableProperty]
        private string nombre;

        [ObservableProperty]
        private string frecuencia;

        [ObservableProperty]
        private TimeSpan hora = DateTime.Now.TimeOfDay;

        public ObservableCollection<string> Frecuencias { get; }

        [RelayCommand]
        public async Task Guardar()
        {
            LocalNotificationCenter.Current.CancelAll();

            if (String.IsNullOrEmpty(Nombre) || String.IsNullOrEmpty(Frecuencia))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                return;
            }

            if (!await LocalNotificationCenter.Current.AreNotificationsEnabled())
            {
                bool permissionResult = await LocalNotificationCenter.Current.RequestNotificationPermission();
                if (!permissionResult)
                {
                    await Application.Current.MainPage.DisplayAlert("Permiso de Notificaciones", "Debe activar las notificaciones para poder usar los recordatorios.", "OK");
                    return;
                }
            }

            var usuarioId = _authService.UsuarioConectado.ID;
            var recordatorio = new Recordatorio(Nombre, Frecuencia, Hora, usuarioId);

            _recordatorioService.ConfigurarNotificacionLocal(recordatorio);
            bool isRecordatorioCreado = await _recordatorioService.CrearRecordatorio(recordatorio);

            if (isRecordatorioCreado)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Recordatorio creado correctamente.", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Hubo un problema al crear el recordatorio. Inténtelo de nuevo.", "OK");
            }

            // Navegar de vuelta a la página principal
            Application.Current.MainPage = MauiProgram.App.Services.GetService<MainPageView>();
        }

        [RelayCommand]
        public async void Volver()
        {
            Application.Current.MainPage = MauiProgram.App.Services.GetService<MainPageView>();
        }
    }
}
