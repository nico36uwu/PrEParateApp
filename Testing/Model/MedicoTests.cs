using Xunit;
using PrEParateApp.Model;

namespace Model;

public class MedicoTests
{
    [Fact]
    public void PuedeCrearMedico()
    {
        var medico = new Medico("12345678A", "Dr. Smith", "password");

        Assert.Equal("12345678A", medico.DNI);
        Assert.Equal("Dr. Smith", medico.Nombre);
        Assert.Equal("password", medico.Password);
    }
}
