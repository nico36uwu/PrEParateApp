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
        }

        private void CargarCalendario()
        {
            // Limpiar el calendario
            CalendarGrid.Children.Clear();

            // Añadir encabezados
            var headers = new string[] { "L", "M", "X", "J", "V", "S", "D" };
            for (int col = 0; col < 7; col++)
            {
                var label = new Label { Text = headers[col], HorizontalOptions = LayoutOptions.Center };
                CalendarGrid.Children.Add(label);
                Grid.SetRow(label, 0);
                Grid.SetColumn(label, col);
            }

            // Obtener el primer día del mes
            var firstDayOfMonth = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            var daysInMonth = DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);
            var startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;
            startDayOfWeek = startDayOfWeek == 0 ? 7 : startDayOfWeek; // Ajustar el domingo como el último día de la semana

            // Rellenar el calendario con los días del mes
            int row = 1;
            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateTime(CurrentDate.Year, CurrentDate.Month, day);
                var label = new Label { Text = day.ToString(), HorizontalOptions = LayoutOptions.Center };

                // Colorear el día según sea necesario
                if (date == DateTime.Today)
                {
                    label.BackgroundColor = Colors.Blue;
                }
                else if (TomasDeMedicacion.Any(t => t.Fecha.Date == date.Date))
                {
                    label.BackgroundColor = Colors.Green;
                }
                else if (Eventos.Any(e => e.Fecha.Date == date.Date))
                {
                    label.BackgroundColor = Colors.Red;
                }

                CalendarGrid.Children.Add(label);
                Grid.SetRow(label, row);
                Grid.SetColumn(label, (startDayOfWeek + day - 1) % 7);

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
        }

        [RelayCommand]
        public void MesSiguiente()
        {
            CurrentDate = CurrentDate.AddMonths(1);
            DisplayMonth = CurrentDate.ToString("MMMM yyyy", CultureInfo.CurrentCulture).ToUpper();
            CargarCalendario();
        }
    }
}
