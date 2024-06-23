using Xunit;
using PrEParateApp.Model;
using System;

namespace Model;

public class MensajeTests
{
    [Fact]
    public void PuedeCrearMensaje()
    {
        var mensaje = new Mensaje("Hola", new DateTime(2024, 6, 17), 1, 1, 1);

        Assert.Equal("Hola", mensaje.Texto);
        Assert.Equal(new DateTime(2024, 6, 17), mensaje.Fecha);
        Assert.Equal(1, mensaje.ChatId);
        Assert.Equal(1, mensaje.AutorMedicoId);
        Assert.Equal(1, mensaje.AutorUsuarioId);
    }
}
