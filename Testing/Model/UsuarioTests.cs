using Xunit;
using PrEParateApp.Model;
using System;

namespace Model;

public class UsuarioTests
{
    [Fact]
    public void PuedeCrearUsuario()
    {
        var usuario = new Usuario("12345678A", "John Doe", "password123", "Aceptado", "123-SS", "123-SIP", "http://image.url", new DateTime(1990, 1, 1), 1);

        Assert.Equal("12345678A", usuario.DNI);
        Assert.Equal("John Doe", usuario.Nombre);
        Assert.Equal("password123", usuario.Password);
        Assert.Equal("Aceptado", usuario.EstadoPaciente);
        Assert.Equal("123-SS", usuario.NumeroSS);
        Assert.Equal("123-SIP", usuario.NumeroSIP);
        Assert.Equal("http://image.url", usuario.ImagenURL);
        Assert.Equal(new DateTime(1990, 1, 1), usuario.FechaNacimiento);
        Assert.Equal(1, usuario.MedicoID);
    }
}
