using System.Threading.Tasks;
using PrEParateApp.Model;

public class AuthenticationService
{
    private UsuarioRepository _usuarioRepository;
    private Usuario _usuarioConectado;

    public Usuario UsuarioConectado => _usuarioConectado;

    private MedicoRepository _medicoRepository;
    private Medico _medicoUsuario;

    public Medico MedicoUsario => _medicoUsuario;

    public AuthenticationService(UsuarioRepository usuarioRepository, MedicoRepository medicoRepository)
    {
        _usuarioRepository = usuarioRepository;
        _medicoRepository = medicoRepository;
    }

    public async Task<bool> Login(string dni, string password)
    {
        var user = await _usuarioRepository.FindByDniAndPassword(dni, password);
        if (user != null) 
        {
            _usuarioConectado = user;
            await AsignarMedicoUsuario();
        }
        return _usuarioConectado != null;
    }

    private async Task AsignarMedicoUsuario()
    {
        var medico = await _medicoRepository.FindByUserID(UsuarioConectado.MedicoID);
        _medicoUsuario = medico;
    }

    public void Logout()
    {
        _usuarioConectado = null;
    }
}
