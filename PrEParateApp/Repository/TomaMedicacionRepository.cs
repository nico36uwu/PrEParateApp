using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supabase;
using PrEParateApp.Model;

public class TomaMedicacionRepository
{
    private readonly Client _supabaseClient;

    public TomaMedicacionRepository(Client client)
    {
        this._supabaseClient = client;
    }

    public async Task Insertar(TomaMedicacion tomaMedicacion)
    {
        await _supabaseClient.From<TomaMedicacion>().Insert(tomaMedicacion);
    }

    public async Task Eliminar(TomaMedicacion tomaMedicacion)
    {
        await _supabaseClient.From<TomaMedicacion>().Delete(tomaMedicacion);
    }

    public async Task Actualizar(TomaMedicacion tomaMedicacion)
    {
        await _supabaseClient.From<TomaMedicacion>().Where(b => b.Id == tomaMedicacion.Id)
            .Set(b => b.Comentarios, tomaMedicacion.Comentarios)
            .Set(b => b.Fecha, tomaMedicacion.Fecha)
            .Set(b => b.Hora, tomaMedicacion.Hora)
            .Update();
    }

    public async Task<IEnumerable<TomaMedicacion>> GetAll()
    {
        var response = await _supabaseClient.From<TomaMedicacion>().Get();
        return response.Models.OrderByDescending(b => b.Id);
    }

    public async Task<IEnumerable<TomaMedicacion>> GetByUserId(int userId)
    {
        var response = await _supabaseClient.From<TomaMedicacion>().Where(b => b.UsuarioId == userId).Get();
        return response.Models;
    }
}
