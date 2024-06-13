using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.LocalNotification;
using PrEParateApp.Model;
using PrEParateApp.Utilities;


public class RecordatorioService
{
    private readonly RecordatorioRepository _recordatorioRepository;

    public RecordatorioService(RecordatorioRepository recordatorioRepository)
    {
        _recordatorioRepository = recordatorioRepository;
    }

    public async Task<bool> CrearRecordatorio(Recordatorio recordatorio)
    {
        try
        {
            await _recordatorioRepository.Insertar(recordatorio);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al crear recordatorio: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> EliminarRecordatorio(Recordatorio recordatorio)
    {
        try
        {
            await _recordatorioRepository.Eliminar(recordatorio);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar recordatorio: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> ActualizarRecordatorio(Recordatorio recordatorio)
    {
        try
        {
            await _recordatorioRepository.Actualizar(recordatorio);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al actualizar recordatorio: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<Recordatorio>> ObtenerTodosLosRecordatorios()
    {
        return await _recordatorioRepository.GetAll();
    }

    public async Task<IEnumerable<Recordatorio>> ObtenerRecordatoriosPorUsuario(int userId)
    {
        return await _recordatorioRepository.GetByUserId(userId);
    }

    public void ConfigurarNotificacionLocal(Recordatorio recordatorio)
    {
        var notification = new NotificationRequest
        {
            NotificationId = recordatorio.Id,
            Title = "Recordatorio de PrEParate",
            Description = recordatorio.Nombre,
            CategoryType = NotificationCategoryType.Reminder,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(10) // Esto debe ser configurado según la hora y frecuencia seleccionada
            }
        };

        switch (recordatorio.Frecuencia)
        {
            case Constantes.FRECUENCIA_DIARIA:
                notification.Schedule.RepeatType = NotificationRepeat.Daily;
                notification.Schedule.NotifyTime = DateTime.Today.Add(recordatorio.Hora);
                break;
            case Constantes.FRECUENCIA_LUNES:
                notification.Schedule.RepeatType = NotificationRepeat.Weekly;
                notification.Schedule.NotifyTime = GetNextWeekday(DateTime.Now, DayOfWeek.Monday, recordatorio.Hora);
                break;
            case Constantes.FRECUENCIA_MARTES:
                notification.Schedule.RepeatType = NotificationRepeat.Weekly;
                notification.Schedule.NotifyTime = GetNextWeekday(DateTime.Now, DayOfWeek.Tuesday, recordatorio.Hora);
                break;
            case Constantes.FRECUENCIA_MIERCOLES:
                notification.Schedule.RepeatType = NotificationRepeat.Weekly;
                notification.Schedule.NotifyTime = GetNextWeekday(DateTime.Now, DayOfWeek.Wednesday, recordatorio.Hora);
                break;
            case Constantes.FRECUENCIA_JUEVES:
                notification.Schedule.RepeatType = NotificationRepeat.Weekly;
                notification.Schedule.NotifyTime = GetNextWeekday(DateTime.Now, DayOfWeek.Thursday, recordatorio.Hora);
                break;
            case Constantes.FRECUENCIA_VIERNES:
                notification.Schedule.RepeatType = NotificationRepeat.Weekly;
                notification.Schedule.NotifyTime = GetNextWeekday(DateTime.Now, DayOfWeek.Friday, recordatorio.Hora);
                break;
            case Constantes.FRECUENCIA_SABADO:
                notification.Schedule.RepeatType = NotificationRepeat.Weekly;
                notification.Schedule.NotifyTime = GetNextWeekday(DateTime.Now, DayOfWeek.Saturday, recordatorio.Hora);
                break;
            case Constantes.FRECUENCIA_DOMINGO:
                notification.Schedule.RepeatType = NotificationRepeat.Weekly;
                notification.Schedule.NotifyTime = GetNextWeekday(DateTime.Now, DayOfWeek.Sunday, recordatorio.Hora);
                break;
            default:
                throw new ArgumentException("Frecuencia no válida.");
        }

        LocalNotificationCenter.Current.Show(notification);
    }

    private DateTime GetNextWeekday(DateTime start, DayOfWeek day, TimeSpan time)
    {
        int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
        if (daysToAdd == 0 && start.TimeOfDay > time)
        {
            daysToAdd = 7; // Si la hora actual ya pasó, programar para la próxima semana
        }
        return start.AddDays(daysToAdd).Date.Add(time);
    }

    public void CancelarNotificacionLocal(Recordatorio recordatorio)
    {
        LocalNotificationCenter.Current.Cancel(recordatorio.Id);
    }

}
