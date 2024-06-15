using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.LocalNotification;
using PrEParateApp.Model;

public class EventoService
{
    private readonly EventoRepository _eventoRepository;

    public EventoService(EventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    public async Task<bool> CrearEvento(Evento evento)
    {
        try
        {
            await _eventoRepository.Insertar(evento);
            ConfigurarNotificacionLocal(evento);
            return true;
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al crear evento: {ex.Message}", "OK");
            return false;
        }
    }

    public async Task<bool> EliminarEvento(Evento evento)
    {
        try
        {
            await _eventoRepository.Eliminar(evento);
            CancelarNotificacionLocal(evento);
            return true;
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al eliminar evento: {ex.Message}", "OK");
            return false;
        }
    }

    public async Task<bool> ActualizarEvento(Evento evento)
    {
        try
        {
            await _eventoRepository.Actualizar(evento);
            return true;
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al actualizar evento: {ex.Message}", "OK");
            return false;
        }
    }

    public async Task<IEnumerable<Evento>> ObtenerTodosLosEventos()
    {
        return await _eventoRepository.GetAll();
    }

    public async Task<IEnumerable<Evento>> ObtenerEventosPorUsuario(int userId)
    {
        return await _eventoRepository.GetByUserId(userId);
    }

    private void ConfigurarNotificacionLocal(Evento evento)
    {
        var notification1 = new NotificationRequest
        {
            NotificationId = 10000 + evento.Id,
            Title = $"Recordatorio de Evento: {evento.Tipo}",
            Description = $"Mañana tienes el evento: {evento.Nombre}",
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = evento.Fecha.AddDays(-1).Date.AddHours(8)
            }
        };

        var notification2 = new NotificationRequest
        {
            NotificationId = 10000 + evento.Id + 1,
            Title = $"Hoy es el Evento: {evento.Tipo}",
            Description = $"Hoy tienes el evento: {evento.Nombre}",
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = evento.Fecha.Date.AddHours(8)
            }
        };

        LocalNotificationCenter.Current.Show(notification1);
        LocalNotificationCenter.Current.Show(notification2);
    }

    private void CancelarNotificacionLocal(Evento evento)
    {
        LocalNotificationCenter.Current.Cancel(10000 + evento.Id);
        LocalNotificationCenter.Current.Cancel(10000 + evento.Id + 1);
    }
}
