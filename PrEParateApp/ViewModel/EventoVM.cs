using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PrEParateApp.Model;
using PrEParateApp.Utilities;
using PrEParateApp.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace PrEParateApp.ViewModel
{
    public partial class EventoVM : ObservableObject
    {
        private readonly EventoService _eventoService;
        private readonly AuthenticationService _authService;
        private const int PageSize = 5;

        public EventoVM(EventoService eventoService, AuthenticationService authService)
        {
            _eventoService = eventoService;
            _authService = authService;

            TiposDeEvento = new ObservableCollection<string>
            {
                Constantes.TIPO_CITA_MEDICA,
                Constantes.TIPO_VACUNA,
                Constantes.TIPO_CITA_CONTROL,
                Constantes.TIPO_ANALISIS_CLINICO,
                Constantes.TIPO_TRATAMIENTO_ESPECIFICO
            };

            MinFecha = DateTime.Now.AddDays(1); // Fecha mínima: mañana
            Eventos = new ObservableCollection<Evento>();
            CargarEventos();
        }

        [ObservableProperty]
        private string nombre;

        [ObservableProperty]
        private string tipoSeleccionado;

        [ObservableProperty]
        private DateTime fecha = DateTime.Now;

        public ObservableCollection<string> TiposDeEvento { get; }

        [ObservableProperty]
        private ObservableCollection<Evento> eventos;

        [ObservableProperty]
        private ObservableCollection<Evento> eventosPaginados;

        [ObservableProperty]
        private int paginaActual;

        [ObservableProperty]
        private bool puedeAvanzar;

        [ObservableProperty]
        private bool puedeRetroceder;

        [ObservableProperty]
        private int totalPaginas;

        [ObservableProperty]
        private string totalEventosText;

        [ObservableProperty]
        private DateTime minFecha;

        [RelayCommand]
        public async Task Guardar()
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(TipoSeleccionado))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                return;
            }

            if (Fecha < DateTime.Now.AddDays(1))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La fecha del evento debe ser a partir de mañana.", "OK");
                return;
            }

            var usuarioId = _authService.UsuarioConectado.ID;
            var evento = new Evento(TipoSeleccionado, Nombre, Fecha, usuarioId);

            bool isEventoCreado = await _eventoService.CrearEvento(evento);

            if (isEventoCreado)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Evento creado correctamente.", "OK");
                Eventos.Add(evento);
                CargarEventosPaginados();
                LimpiarCampos();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Hubo un problema al crear el evento. Inténtelo de nuevo.", "OK");
            }
        }

        [RelayCommand]
        public async Task Eliminar(Evento evento)
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert("Confirmar", "¿Está seguro de eliminar esta toma de medicación?", "Sí", "No");
            if (confirm)
            {
                bool isEventoEliminado = await _eventoService.EliminarEvento(evento);
                if (isEventoEliminado)
                {
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Evento eliminado correctamente.", "OK");
                    Eventos.Remove(evento);
                    CargarEventosPaginados();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Hubo un problema al eliminar el evento. Inténtelo de nuevo.", "OK");
                }
            }
        }

        [RelayCommand]
        public void SiguientePagina()
        {
            if (PuedeAvanzar)
            {
                PaginaActual++;
                CargarEventosPaginados();
            }
        }

        [RelayCommand]
        public void AnteriorPagina()
        {
            if (PuedeRetroceder)
            {
                PaginaActual--;
                CargarEventosPaginados();
            }
        }

        private async void CargarEventos()
        {
            var usuarioId = _authService.UsuarioConectado.ID;
            var eventos = await _eventoService.ObtenerEventosPorUsuario(usuarioId);
            Eventos = new ObservableCollection<Evento>(eventos);
            PaginaActual = 1;
            CargarEventosPaginados();
        }

        private void CargarEventosPaginados()
        {
            var eventosPaginados = Eventos.Skip((PaginaActual - 1) * PageSize).Take(PageSize);
            EventosPaginados = new ObservableCollection<Evento>(eventosPaginados);
            PuedeAvanzar = Eventos.Count > PaginaActual * PageSize;
            PuedeRetroceder = PaginaActual > 1;

            TotalPaginas = (int)Math.Ceiling((double)Eventos.Count / PageSize);
            TotalEventosText = $"Total de Eventos: {Eventos.Count}";
        }

        private void LimpiarCampos()
        {
            Nombre = string.Empty;
            TipoSeleccionado = string.Empty;
            Fecha = DateTime.Now.AddDays(1); // Reiniciar la fecha a mañana
        }

        [RelayCommand]
        public void Volver()
        {
            var view = MauiProgram.App.Services.GetService<MainPageView>();
            view.SetCalendarioTab();
            Application.Current.MainPage = view;
        }
    }
}
