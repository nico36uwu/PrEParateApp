using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PrEParateApp.Model;
using PrEParateApp.Utilities;
using PrEParateApp.View;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace PrEParateApp.ViewModel
{
    public partial class CalendarioVM : ObservableObject
    {
        private readonly EventoService _eventoService;
        private readonly TomaMedicacionService _tomaMedicacionService;
        private readonly AuthenticationService _authService;

        public CalendarioVM(EventoService eventoService, TomaMedicacionService tomaMedicacionService, AuthenticationService authService)
        {
            _eventoService = eventoService;
            _tomaMedicacionService = tomaMedicacionService;
            _authService = authService;
            Eventos = new ObservableCollection<Evento>();
            TomasDeMedicacion = new ObservableCollection<TomaMedicacion>();
            CurrentDate = DateTime.Today;
            DisplayMonth = CurrentDate.ToString("MMMM yyyy", CultureInfo.CurrentCulture).ToUpper();
            CargarItems();
        }

        [ObservableProperty]
        private string displayMonth;

        [ObservableProperty]
        private string resumenEventos;

        [ObservableProperty]
        private string resumenTomasMedicacion;

        public ObservableCollection<Evento> Eventos { get; }
        public ObservableCollection<TomaMedicacion> TomasDeMedicacion { get; }

        [ObservableProperty]
        private DateTime currentDate;

        public Grid CalendarGrid { get; set; }

        [RelayCommand]
        public async Task PlanificarNuevoEvento()
        {
            var view = MauiProgram.App.Services.GetService<EventoView>();
            Application.Current.MainPage = view;
        }

        private async void CargarItems()
        {
            var usuarioId = _authService.UsuarioConectado.ID;
            var eventos = await _eventoService.ObtenerEventosPorUsuario(usuarioId);
            var tomasDeMedicacion = await _tomaMedicacionService.ObtenerTomasPorUsuario(usuarioId);
            Eventos.Clear();
            TomasDeMedicacion.Clear();
            foreach (var evento in eventos)
            {
                Eventos.Add(evento);
            }
            foreach (var toma in tomasDeMedicacion)
            {
                TomasDeMedicacion.Add(toma);
            }
            CargarCalendario();
            ActualizarResumen();
        }

        private void CargarCalendario()
        {
            // Limpiar el calendario
            CalendarGrid.Children.Clear();
            CalendarGrid.HorizontalOptions = LayoutOptions.Center;
            CalendarGrid.VerticalOptions = LayoutOptions.Center;
            // Añadir encabezados
            var headers = new string[] { "L", "M", "X", "J", "V", "S", "D" };
            for (int col = 0; col < 7; col++)
            {
                var label = new Label
                {
                    Text = headers[col],
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 25,
                    FontAttributes = FontAttributes.Bold,
                    Padding = new Microsoft.Maui.Thickness(0, 0, 0, 10)
                };
                CalendarGrid.Children.Add(label);
                Grid.SetRow(label, 0);
                Grid.SetColumn(label, col);
            }

            // Obtener el primer día del mes
            var firstDayOfMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            var daysInMonth = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
            var startDayOfWeek = ((int)firstDayOfMonth.DayOfWeek + 6) % 7; // Ajustar el domingo como el último día de la semana

            // Rellenar el calendario con los días del mes
            int row = 1;
            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateTime(CurrentDate.Year, CurrentDate.Month, day);
                var label = new Label
                {
                    Text = day.ToString(),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 22,
                    BackgroundColor = Colors.Transparent,
                    TextColor = Colors.Black
                };

                var dayContainer = new StackLayout
                {
                    Children = { label },
                    HeightRequest = 35,
                    WidthRequest = 35,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    BackgroundColor = Colors.Transparent
                };

                // Colorear el día según sea necesario
                if (date == DateTime.Today)
                {
                    dayContainer.BackgroundColor = Colors.Blue;
                    label.TextColor = Colors.White;
                }
                else if (TomasDeMedicacion.Any(t => t.Fecha.Date == date.Date))
                {
                    dayContainer.BackgroundColor = Colors.Green;
                    label.TextColor = Colors.White;
                }
                else if (Eventos.Any(e => e.Fecha.Date == date.Date))
                {
                    dayContainer.BackgroundColor = Colors.Red;
                    label.TextColor = Colors.White;
                }

                CalendarGrid.Children.Add(dayContainer);
                Grid.SetRow(dayContainer, row);
                Grid.SetColumn(dayContainer, (startDayOfWeek + day - 1) % 7);

                // Mover a la siguiente fila si es necesario
                if ((startDayOfWeek + day) % 7 == 0)
                {
                    row++;
                }
            }
        }

        [RelayCommand]
        public void MesAnterior()
        {
            CurrentDate = CurrentDate.AddMonths(-1);
            DisplayMonth = CurrentDate.ToString("MMMM yyyy", CultureInfo.CurrentCulture).ToUpper();
            CargarCalendario();
            ActualizarResumen();
        }

        [RelayCommand]
        public void MesSiguiente()
        {
            CurrentDate = CurrentDate.AddMonths(1);
            DisplayMonth = CurrentDate.ToString("MMMM yyyy", CultureInfo.CurrentCulture).ToUpper();
            CargarCalendario();
            ActualizarResumen();
        }

        private void ActualizarResumen()
        {
            var eventosPendientes = Eventos.Count(e => e.Fecha >= DateTime.Today && e.Fecha.Month == CurrentDate.Month && e.Fecha.Year == CurrentDate.Year);
            var tomasMes = TomasDeMedicacion.Count(t => t.Fecha.Month == CurrentDate.Month && t.Fecha.Year == CurrentDate.Year);

            ResumenEventos = $"Este mes tiene {eventosPendientes} eventos pendientes";
            ResumenTomasMedicacion = $"Este mes ha registrado {tomasMes} tomas de medicación";
        }
    }
}
