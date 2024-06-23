using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PrEParateApp.Model;
using PrEParateApp.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PrEParateApp.Utilities;

namespace PrEParateApp.ViewModel
{
    public partial class AgendaPersonalVM : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EventoService _eventoService;
        private readonly TomaMedicacionService _tomaMedicacionService;
        private readonly AuthenticationService _authService;
        private const int PageSize = 5;

        public AgendaPersonalVM(IServiceProvider serviceProvider, EventoService eventoService, TomaMedicacionService tomaMedicacionService, AuthenticationService authService)
        {
            _serviceProvider = serviceProvider;
            _eventoService = eventoService;
            _tomaMedicacionService = tomaMedicacionService;
            _authService = authService;

            TiposDeItems = new ObservableCollection<string>
            {
                Constantes.TOMA_MEDICACION,
                Constantes.EVENTO
            };
            tipoSeleccionado = Constantes.TOMA_MEDICACION;
            Items = new ObservableCollection<AgendaItem>();
            CargarItems();

            MessagingCenter.Subscribe<RegistroMedicacionVM>(this, "NuevaTomaMedicacionRegistrada", async (sender) =>
            {
                CargarItems();
            });
        }

        [ObservableProperty]
        private string tipoSeleccionado;

        public ObservableCollection<string> TiposDeItems { get; }

        [ObservableProperty]
        private ObservableCollection<AgendaItem> items;

        [ObservableProperty]
        private ObservableCollection<AgendaItem> itemsPaginados;

        [ObservableProperty]
        private int paginaActual;

        [ObservableProperty]
        private bool puedeAvanzar;

        [ObservableProperty]
        private bool puedeRetroceder;

        [ObservableProperty]
        private int totalPaginas;

        [ObservableProperty]
        private string totalItemsText;

        partial void OnTipoSeleccionadoChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                CargarItems();
            }
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

        [RelayCommand]
        public void SiguientePagina()
        {
            if (PuedeAvanzar)
            {
                PaginaActual++;
                CargarItemsPaginados();
            }
        }

        [RelayCommand]
        public void AnteriorPagina()
        {
            if (PuedeRetroceder)
            {
                PaginaActual--;
                CargarItemsPaginados();
            }
        }

        [RelayCommand]
        public async Task EliminarItem(AgendaItem item)
        {
            if (TipoSeleccionado == Constantes.TOMA_MEDICACION)
            {
                bool confirm = await Application.Current.MainPage.
                    DisplayAlert("Confirmar", "¿Está seguro de eliminar esta toma de medicación?", "Sí", "No");
                if (confirm)
                {
                    var toma = await _tomaMedicacionService.ObtenerTomaPorId(item.Id);
                    if (toma != null)
                    {
                        await Application.Current.MainPage.
                            DisplayAlert("Éxito", "Toma de medicación eliminada correctamente.", "OK");
                        await _tomaMedicacionService.EliminarTomaMedicacion(toma);
                        Items.Remove(item);
                        CargarItemsPaginados();
                    }
                }
            }
        }

        public async void CargarItems()
        {
            var usuarioId = _authService.UsuarioConectado.ID;
            var items = new ObservableCollection<AgendaItem>();

            if (TipoSeleccionado == Constantes.TOMA_MEDICACION)
            {
                var tomas = await _tomaMedicacionService.ObtenerTomasPorUsuario(usuarioId);
                items = new ObservableCollection<AgendaItem>(tomas.Select(t => new AgendaItem
                {
                    Id = t.Id,
                    Fecha = t.Fecha,
                    Descripcion = t.Comentarios,
                    Tipo = Constantes.TOMA_MEDICACION
                }).OrderByDescending(t => t.Fecha));
            }
            else if (TipoSeleccionado == Constantes.EVENTO)
            {
                var eventos = await _eventoService.ObtenerEventosPorUsuario(usuarioId);
                items = new ObservableCollection<AgendaItem>(eventos.Select(e => new AgendaItem
                {
                    Id = e.Id,
                    Fecha = e.Fecha,
                    Descripcion = e.Nombre,
                    Tipo = Constantes.EVENTO
                }).OrderByDescending(e => e.Fecha));
            }
            Items = items;
            PaginaActual = 1;
            CargarItemsPaginados();
        }



        private void CargarItemsPaginados()
        {
            var itemsPaginados = Items.Skip((PaginaActual - 1) * PageSize).Take(PageSize);
            ItemsPaginados = new ObservableCollection<AgendaItem>(itemsPaginados);
            PuedeAvanzar = Items.Count > PaginaActual * PageSize;
            PuedeRetroceder = PaginaActual > 1;

            TotalPaginas = (int)Math.Ceiling((double)Items.Count / PageSize);
            TotalItemsText = $"Total de Items: {Items.Count}";
        }
    }

    public class AgendaItem
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
    }
}
