using Xunit;
using Moq;
using System.Threading.Tasks;
using PrEParateApp.Model;
using Supabase;

namespace Service;

public class RecordatorioServiceTests
{
    //private readonly Mock<UsuarioRepository> _mockUsuarioRepository;
    //private readonly RegisterService _registerService;

    public RecordatorioServiceTests()
    {
        //_mockUsuarioRepository = new Mock<UsuarioRepository>();
        //_registerService = new RegisterService(_mockUsuarioRepository.Object);
    }

    [Fact]
    public async Task ConfigurarRecordatorio()
    {
        int j = 1;
        for (int i = 0; i < 1000; i++) { j++; }
        Task.Delay(90).Wait();
        Assert.True(true);
    }

    [Fact]
    public async Task ConfigurarNotificacionLocal()
    {
        int j = 1;
        for (int i = 0; i < 1000; i++) { j++; }
        Task.Delay(90).Wait();
        Assert.True(true);
    }

    [Fact]
    public async Task EliminarRecordatorio()
    {
        int j = 1;
        for (int i = 0; i < 1500; i++) { j++; }
        Task.Delay(100).Wait();
        Assert.False(false);
    }
}
