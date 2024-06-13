using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supabase;
using PrEParateApp.Model;

public class RecordatorioRepository
{
    private readonly Client _supabaseClient;

    public RecordatorioRepository(Client client)
    {
        this._supabaseClient = client;
    }

    public async Task Insertar(Recordatorio recordatorio)
    {
        await _supabaseClient.From<Recordatorio>().Insert(recordatorio);
    }

    public async Task Eliminar(Recordatorio recordatorio)
    {
        await _supabaseClient.From<Recordatorio>().Delete(recordatorio);
    }

    public async Task Actualizar(Recordatorio recordatorio)
    {
        await _supabaseClient.From<Recordatorio>().Where(b => b.Id == recordatorio.Id)
            .Set(b => b.Nombre, recordatorio.Nombre)
            .Set(b => b.Frecuencia, recordatorio.Frecuencia)
            .Set(b => b.Hora, recordatorio.Hora)
            .Update();
    }

    public async Task<IEnumerable<Recordatorio>> GetAll()
    {
        var response = await _supabaseClient.From<Recordatorio>().Get();
        return response.Models.OrderByDescending(b => b.Id);
    }

    public async Task<IEnumerable<Recordatorio>> GetByUserId(int userId)
    {
        var response = await _supabaseClient.From<Recordatorio>().Where(b => b.UsuarioId == userId).Get();
        return response.Models;
    }
}
