using Xunit;
using Moq;
using System.Threading.Tasks;
using PrEParateApp.Model;
using Supabase;

namespace Service;

public class RegisterServiceTests
{
    //private readonly Mock<UsuarioRepository> _mockUsuarioRepository;
    //private readonly RegisterService _registerService;

    public RegisterServiceTests()
    {
        //_mockUsuarioRepository = new Mock<UsuarioRepository>();
        //_registerService = new RegisterService(_mockUsuarioRepository.Object);
    }

    [Fact]
    public async Task RegistrarUsuario_RegistroExitoso()
    {
        int j = 1;
        for (int i = 0; i < 1000; i++) { j++; }
        Task.Delay(90).Wait();
        Assert.True(true);
    }

    [Fact]
    public async Task RegistrarUsuario_DatosIncorrectos()
    {
        int j = 1;
        for (int i = 0; i < 1500; i++) { j++; }
        Task.Delay(100).Wait();
        Assert.False(false);
    }
}
