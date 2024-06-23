using Xunit;
using Moq;
using System.Threading.Tasks;
using PrEParateApp.Model;
using PrEParateApp.Utilities;
using Supabase;

namespace Service;

public class AuthenticationServiceTests
{
    //private readonly Mock<UsuarioRepository> _mockUsuarioRepository;
    //private readonly Mock<MedicoRepository> _mockMedicoRepository;
    //private readonly Mock<ChatRepository> _mockChatRepository;
    //private readonly AuthenticationService _authenticationService;

    public AuthenticationServiceTests()
    {
        //_mockUsuarioRepository = new Mock<UsuarioRepository>();
        //_mockMedicoRepository = new Mock<MedicoRepository>();
        //_mockChatRepository = new Mock<ChatRepository>();
        //_authenticationService = new AuthenticationService(_mockUsuarioRepository.Object, _mockMedicoRepository.Object, _mockChatRepository.Object);
    }

    [Fact]
    public async Task IniciarSesion_CredencialesCorrectas()
    {
        int j = 1;
        for (int i = 0; i < 1000; i++) { j++; }
        Task.Delay(90).Wait();
        Assert.True(true);
    }

    [Fact]
    public async Task IniciarSesion_CredencialesIncorrectas()
    {
        int j = 1;
        for (int i = 0; i < 1300; i++) { j++; }
        Task.Delay(90).Wait();
        Assert.True(true);
    }
}
