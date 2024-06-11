using System.Collections.Generic;
using System.Threading.Tasks;
using PrEParateApp.Model;

public class MedicoService
{
    private readonly MedicoRepository _medicoRepository;

    public MedicoService(MedicoRepository medicoRepository)
    {
        _medicoRepository = medicoRepository;
    }

    public async Task<List<Medico>> ObtenerMedicos()
    {
        var response = await _medicoRepository.GetAll();
        return response.ToList<Medico>();
    }
}
