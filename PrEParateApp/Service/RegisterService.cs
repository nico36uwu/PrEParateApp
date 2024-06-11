using System.Threading.Tasks;
using PrEParateApp.Model;
using PrEParateApp.Utilities;
using Supabase;

public class RegisterService
{
    private UsuarioRepository _usuarioRepository;

    public RegisterService(UsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<bool> RegisterUser(Usuario usuario)
    {
        try
        {
            usuario.EstadoPaciente = Constantes.PENDIENTE; // Estado inicial al registrar un nuevo usuario
            await _usuarioRepository.Insertar(usuario);
            var user = _usuarioRepository.GetById(usuario);
            return user != null;
        }

        catch (Exception ex)
        {
            // Manejar excepciones si es necesario
            Console.WriteLine($"Error al registrar el usuario: {ex.Message}");
            return false;
        }
    }
}
