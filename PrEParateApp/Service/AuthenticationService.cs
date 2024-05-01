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

    private ChatRepository _chatRepository;
    private Chat _chatUsuario;

    public Chat ChatUsario => _chatUsuario;

    public AuthenticationService(UsuarioRepository usuarioRepository, MedicoRepository medicoRepository, ChatRepository chatRepository)
    {
        _usuarioRepository = usuarioRepository;
        _medicoRepository = medicoRepository;
        _chatRepository = chatRepository;
    }

    public async Task<bool> Login(string dni, string password)
    {
        var user = await _usuarioRepository.FindByDniAndPassword(dni, password);
        if (user != null) 
        {
            _usuarioConectado = user;
            await AsignarMedicoUsuario();
            await AsignarChatUsuario();
        }
        return _usuarioConectado != null;
    }

    private async Task AsignarMedicoUsuario()
    {
        var medico = await _medicoRepository.FindByUserID(UsuarioConectado.MedicoID);
        _medicoUsuario = medico;
    }

    private async Task AsignarChatUsuario() 
    {
        var chat = await _chatRepository.FindByUserID(UsuarioConectado.ID);
        _chatUsuario = chat;
    }

    public void Logout()
    {
        _chatUsuario = null;
        _medicoUsuario = null;
        _usuarioConectado = null;
    }

    
}
