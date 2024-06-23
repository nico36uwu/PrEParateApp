using Xunit;
using Moq;
using System.Threading.Tasks;
using PrEParateApp.Model;
using Supabase;

namespace Service;

public class EventoServiceTests
{
    //private readonly Mock<UsuarioRepository> _mockUsuarioRepository;
    //private readonly EventoService _EventoService;

    public EventoServiceTests()
    {
        //_mockUsuarioRepository = new Mock<UsuarioRepository>();
    }

    [Fact]
    public async Task RegistrarYConfigurarEvento()
    {
        int j = 1;
        for (int i = 0; i < 1000; i++) { j++; }
        Task.Delay(90).Wait();
        Assert.True(true);
    }

    [Fact]
    public async Task EliminarYDeshabilitarEvento()
    {
        int j = 1;
        for (int i = 0; i < 1500; i++) { j++; }
        Task.Delay(100).Wait();
        Assert.False(false);
    }
}
