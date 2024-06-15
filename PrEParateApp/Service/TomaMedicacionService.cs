using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PrEParateApp.Model;

public class TomaMedicacionService
{
    private readonly TomaMedicacionRepository _tomaMedicacionRepository;

    public TomaMedicacionService(TomaMedicacionRepository tomaMedicacionRepository)
    {
        _tomaMedicacionRepository = tomaMedicacionRepository;
    }

    public async Task<bool> CrearTomaMedicacion(TomaMedicacion tomaMedicacion)
    {
        try
        {
            await _tomaMedicacionRepository.Insertar(tomaMedicacion);
            return true;
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al crear toma de medicación: {ex.Message}", "OK");
            return false;
        }
    }

    public async Task<bool> EliminarTomaMedicacion(TomaMedicacion tomaMedicacion)
    {
        try
        {
            await _tomaMedicacionRepository.Eliminar(tomaMedicacion);
            return true;
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al eliminar toma de medicación: {ex.Message}", "OK");
            return false;
        }
    }

    public async Task<bool> ActualizarTomaMedicacion(TomaMedicacion tomaMedicacion)
    {
        try
        {
            await _tomaMedicacionRepository.Actualizar(tomaMedicacion);
            return true;
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al actualizar toma de medicación: {ex.Message}", "OK");
            return false;
        }
    }

    public async Task<IEnumerable<TomaMedicacion>> ObtenerTodasLasTomas()
    {
        return await _tomaMedicacionRepository.GetAll();
    }

    public async Task<IEnumerable<TomaMedicacion>> ObtenerTomasPorUsuario(int userId)
    {
        return await _tomaMedicacionRepository.GetByUserId(userId);
    }
}
