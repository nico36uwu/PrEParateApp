using Xunit;
using PrEParateApp.Model;
using System;

namespace Model;

public class TomaMedicacionTests
{
    [Fact]
    public void PuedeCrearTomaMedicacion()
    {
        var tomaMedicacion = new TomaMedicacion("Tomar con comida", new DateTime(2024, 6, 17), new TimeSpan(8, 0, 0), 1);

        Assert.Equal("Tomar con comida", tomaMedicacion.Comentarios);
        Assert.Equal(new DateTime(2024, 6, 17), tomaMedicacion.Fecha);
        Assert.Equal(new TimeSpan(8, 0, 0), tomaMedicacion.Hora);
        Assert.Equal(1, tomaMedicacion.UsuarioId);
    }
}
