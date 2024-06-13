using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.LocalNotification;
using PrEParateApp.Model;
using PrEParateApp.Utilities;
using PrEParateApp.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PrEParateApp.ViewModel
{
    public partial class RecordatorioVM : ObservableObject
    {
        private readonly RecordatorioService _recordatorioService;
        private readonly AuthenticationService _authService;
        private const int PageSize = 6;

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
            Recordatorios = new ObservableCollection<Recordatorio>();
            CargarRecordatorios();
        }

        [ObservableProperty]
        private string nombre;

        [ObservableProperty]
        private string frecuencia;

        [ObservableProperty]
        private TimeSpan hora = DateTime.Now.TimeOfDay;

        public ObservableCollection<string> Frecuencias { get; }

        [ObservableProperty]
        private ObservableCollection<Recordatorio> recordatorios;

        [ObservableProperty]
        private ObservableCollection<Recordatorio> recordatoriosPaginados;

        [ObservableProperty]
        private int paginaActual;

        [ObservableProperty]
        private bool puedeAvanzar;

        [ObservableProperty]
        private bool puedeRetroceder;

        [RelayCommand]
        public async Task Guardar()
        {
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
                Recordatorios.Add(recordatorio);
                CargarRecordatoriosPaginados();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Hubo un problema al crear el recordatorio. Inténtelo de nuevo.", "OK");
            }
        }

        [RelayCommand]
        public async Task Eliminar(Recordatorio recordatorio)
        {
            bool isRecordatorioEliminado = await _recordatorioService.EliminarRecordatorio(recordatorio);
            if (isRecordatorioEliminado)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Recordatorio eliminado correctamente.", "OK");
                Recordatorios.Remove(recordatorio);
                CargarRecordatoriosPaginados();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Hubo un problema al eliminar el recordatorio. Inténtelo de nuevo.", "OK");
            }
        }

        [RelayCommand]
        public void SiguientePagina()
        {
            if (PuedeAvanzar)
            {
                PaginaActual++;
                CargarRecordatoriosPaginados();
            }
        }

        [RelayCommand]
        public void AnteriorPagina()
        {
            if (PuedeRetroceder)
            {
                PaginaActual--;
                CargarRecordatoriosPaginados();
            }
        }

        private async void CargarRecordatorios()
        {
            var usuarioId = _authService.UsuarioConectado.ID;
            var recordatorios = await _recordatorioService.ObtenerRecordatoriosPorUsuario(usuarioId);
            Recordatorios = new ObservableCollection<Recordatorio>(recordatorios);
            PaginaActual = 0;
            CargarRecordatoriosPaginados();
        }

        private void CargarRecordatoriosPaginados()
        {
            var recordatoriosPaginados = Recordatorios.Skip(PaginaActual * PageSize).Take(PageSize);
            RecordatoriosPaginados = new ObservableCollection<Recordatorio>(recordatoriosPaginados);
            PuedeAvanzar = Recordatorios.Count > (PaginaActual + 1) * PageSize;
            PuedeRetroceder = PaginaActual > 0;
        }

        [RelayCommand]
        public void Volver()
        {
            Application.Current.MainPage = MauiProgram.App.Services.GetService<MainPageView>();
        }
    }
}
