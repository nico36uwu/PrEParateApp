using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Supabase;
using PrEParateApp.Model;
using static Supabase.Realtime.PostgresChanges.PostgresChangesOptions;
using Supabase.Realtime.PostgresChanges;
using static Supabase.Realtime.Constants;

public class MensajeRepository
{
    private readonly Client _supabaseClient;
    public event Action<Mensaje>? OnMensajeInserted;

    public MensajeRepository(Client client)
    {
        this._supabaseClient = client;
    }

    public async Task InitializeAsync()
    {
        await EnsureConnectedAsync();
        await InitializeRealtimeListenerAsync();
    }

    private async Task EnsureConnectedAsync()
    {
            await _supabaseClient.Realtime.ConnectAsync();
    }

    private async Task InitializeRealtimeListenerAsync()
    {
        var channel = _supabaseClient.Realtime.Channel("public-mensaje");
        channel.Register(new PostgresChangesOptions("public", "mensaje"));
        channel.AddPostgresChangeHandler(ListenType.All, (sender, change) =>
        {
            switch (change.Payload.Data.Type)
            {
                case EventType.Insert:
                    Mensaje? msg = change.Model<Mensaje>();
                    OnMensajeInserted?.Invoke(msg);
                    break;
                case EventType.Update:
                    // Mensaje actualizado
                    break;
                case EventType.Delete:
                    // Mensaje eliminado
                    break;
            }
        });

        await channel.Subscribe();
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
        await _supabaseClient.From<Mensaje>()
            .Where(b => b.ID == mensaje.ID)
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
