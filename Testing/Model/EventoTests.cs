using Xunit;
using PrEParateApp.Model;
using System;

namespace Model;

public class EventoTests
{
    [Fact]
    public void PuedeCrearEvento()
    {
        var evento = new Evento("Tipo1", "Evento1", new DateTime(2024, 6, 17), 1);

        Assert.Equal("Tipo1", evento.Tipo);
        Assert.Equal("Evento1", evento.Nombre);
        Assert.Equal(new DateTime(2024, 6, 17), evento.Fecha);
        Assert.Equal(1, evento.UsuarioId);
    }
}
