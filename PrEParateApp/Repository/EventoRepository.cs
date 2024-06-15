using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supabase;
using PrEParateApp.Model;

public class EventoRepository
{
    private readonly Client _supabaseClient;

    public EventoRepository(Client client)
    {
        this._supabaseClient = client;
    }

    public async Task Insertar(Evento evento)
    {
        await _supabaseClient.From<Evento>().Insert(evento);
    }

    public async Task Eliminar(Evento evento)
    {
        await _supabaseClient.From<Evento>().Delete(evento);
    }

    public async Task Actualizar(Evento evento)
    {
        await _supabaseClient.From<Evento>().Where(b => b.Id == evento.Id)
            .Set(b => b.Tipo, evento.Tipo)
            .Set(b => b.Nombre, evento.Nombre)
            .Set(b => b.Fecha, evento.Fecha)
            .Update();
    }

    public async Task<IEnumerable<Evento>> GetAll()
    {
        var response = await _supabaseClient.From<Evento>().Get();
        return response.Models.OrderByDescending(b => b.Id);
    }

    public async Task<IEnumerable<Evento>> GetByUserId(int userId)
    {
        var response = await _supabaseClient.From<Evento>().Where(b => b.UsuarioId == userId).Get();
        return response.Models;
    }
}
