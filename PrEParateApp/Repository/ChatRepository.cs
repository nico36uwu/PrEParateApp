using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supabase;
using PrEParateApp.Model;
using Supabase.Interfaces;

public class ChatRepository
{
    private readonly Client _supabaseClient;

    public ChatRepository(Client client)
    {
        this._supabaseClient = client;
    }

    public async Task Insertar(Chat chat)
    {
        await _supabaseClient.From<Chat>().Insert(chat);
    }

    public async Task Eliminar(Chat chat)
    {
        await _supabaseClient.From<Chat>().Delete(chat);
    }

    public async Task Actualizar(Chat chat)
    {
        await _supabaseClient.From<Chat>().Where(b => b.ID == chat.ID)
            .Set(b => b.NotificacionPendiente, chat.NotificacionPendiente)
            .Set(b => b.MedicoId, chat.MedicoId)
            .Set(b => b.UsuarioId, chat.UsuarioId)
            .Update();
    }

    public async Task<IEnumerable<Chat>> GetAll()
    {
        var response = await _supabaseClient.From<Chat>().Get();
        return response.Models.OrderByDescending(b => b.ID);
    }

    public async Task<Chat> FindByUserID(int usuarioID)
    {
        var response = await _supabaseClient.From<Chat>().Where(b => b.ID == usuarioID).Get();
        return response.Model;
    }
}
