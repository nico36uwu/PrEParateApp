using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supabase;
using PrEParateApp.Model;
using Supabase.Interfaces;

public class MensajeRepository
{
    private readonly Client _supabaseClient;

    public MensajeRepository(Client client)
    {
        this._supabaseClient = client;
    }

    public async Task Insertar(Mensaje mensaje)
    {
        await _supabaseClient.From<Mensaje>().Insert(mensaje);
    }

    public async Task Eliminar(Mensaje mensaje)
    {
        await _supabaseClient.From<Mensaje>().Delete(mensaje);
    }

    public async Task Actualizar(Mensaje mensaje)
    {
        await _supabaseClient.From<Mensaje>().Where(b => b.ID == mensaje.ID)
            .Set(b => b.Texto, mensaje.Texto)
            .Set(b => b.Fecha, mensaje.Fecha)
            .Set(b => b.ChatId, mensaje.ChatId)
            .Set(b => b.AutorMedicoId, mensaje.AutorMedicoId)
            .Set(b => b.AutorUsuarioId, mensaje.AutorUsuarioId)
            .Update();
    }

    public async Task<IEnumerable<Mensaje>> GetAll()
    {
        var response = await _supabaseClient.From<Mensaje>().Get();
        return response.Models.OrderByDescending(b => b.Fecha);
    }

    public async Task<IEnumerable<Mensaje>> GetMensajesChat(int chatID)
    {
        var response = await _supabaseClient.From<Mensaje>().Where(b => b.ChatId == chatID).Get();
        return response.Models.OrderBy(b => b.Fecha);
    }
}
