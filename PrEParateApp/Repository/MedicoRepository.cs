using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supabase;
using PrEParateApp.Model;
using Supabase.Interfaces;

public class MedicoRepository
{
    private readonly Client _supabaseClient;

    public MedicoRepository(Client client)
    {
        this._supabaseClient = client;
    }

    public async Task Insertar(Medico medico)
    {
        await _supabaseClient.From<Medico>().Insert(medico);
    }

    public async Task Eliminar(Medico medico)
    {
        await _supabaseClient.From<Medico>().Delete(medico);
    }

    public async Task Actualizar(Medico medico)
    {
        await _supabaseClient.From<Medico>().Where(b => b.DNI == medico.DNI)
            .Set(b => b.DNI, medico.DNI)
            .Set(b => b.Nombre, medico.Nombre)
            .Set(b => b.Password, medico.Password)
            .Update();
    }

    public async Task<IEnumerable<Medico>> GetAll()
    {
        var response = await _supabaseClient.From<Medico>().Get();
        return response.Models.OrderByDescending(b => b.DNI);
    }

    internal async Task<Medico> FindByUserID(int medicoID)
    {
        var response = await _supabaseClient.From<Medico>().Where(b => b.ID == medicoID).Get();
        return response.Model;
    }
}
