using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supabase;
using PrEParateApp.Model;
using Supabase.Interfaces;

public class UsuarioRepository
{
    private readonly Client _supabaseClient;

    public UsuarioRepository(Client client)
    {
        this._supabaseClient = client;
    }

    public async Task Insertar(Usuario usuario)
    {
        await _supabaseClient.From<Usuario>().Insert(usuario);
    }

    public async Task Eliminar(Usuario usuario)
    {
        await _supabaseClient.From<Usuario>().Delete(usuario);
    }

    public async Task Actualizar(Usuario usuario)
    {
        await _supabaseClient.From<Usuario>().Where(b => b.DNI == usuario.DNI)
            .Set(b => b.DNI, usuario.DNI)
            .Set(b => b.Nombre, usuario.Nombre)
            .Set(b => b.Password, usuario.Password)
            .Set(b => b.EstadoPaciente, usuario.EstadoPaciente)
            .Set(b => b.NumeroSIP, usuario.NumeroSIP)
            .Set(b => b.NumeroSS, usuario.NumeroSS)
            .Set(b => b.ImagenURL, usuario.ImagenURL)
            .Set(b => b.FechaNacimiento, usuario.FechaNacimiento)
            .Set(b => b.MedicoID, usuario.MedicoID)
            .Update();
    }

    public async Task<IEnumerable<Usuario>> GetAll()
    {
        var response = await _supabaseClient.From<Usuario>().Get();
        return response.Models.OrderByDescending(b => b.DNI);
    }

    public async Task<Usuario> FindByDniAndPassword(string dni, string password)
    {
        var response = await _supabaseClient.From<Usuario>().Where(b => b.DNI == dni && b.Password == password).Get();
        return response.Model;
    }
}
