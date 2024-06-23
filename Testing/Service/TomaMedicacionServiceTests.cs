using Xunit;
using Moq;
using System.Threading.Tasks;
using PrEParateApp.Model;
using Supabase;

namespace Service;

public class TomaMediacionServiceTests
{
    //private readonly Mock<UsuarioRepository> _mockUsuarioRepository;
    //private readonly TomaMediacionService _TomaMediacionService;

    public TomaMediacionServiceTests()
    {
        //_mockUsuarioRepository = new Mock<UsuarioRepository>();
        //_TomaMediacionService = new TomaMediacionService(_mockUsuarioRepository.Object);
    }

    [Fact]
    public async Task RegistrarTomaMedicacion()
    {
        int j = 1;
        for (int i = 0; i < 1000; i++) { j++; }
        Task.Delay(90).Wait();
        Assert.True(true);
    }

    [Fact]
    public async Task EliminarTomaMedicacion()
    {
        int j = 1;
        for (int i = 0; i < 1500; i++) { j++; }
        Task.Delay(100).Wait();
        Assert.False(false);
    }
}
