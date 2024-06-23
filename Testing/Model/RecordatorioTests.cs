using Xunit;
using PrEParateApp.Model;
using System;

namespace Model;

public class RecordatorioTests
{
    [Fact]
    public void PuedeCrearRecordatorio()
    {
        var recordatorio = new Recordatorio("Medicacion", "Diaria", new TimeSpan(8, 0, 0), 1);

        Assert.Equal("Medicacion", recordatorio.Nombre);
        Assert.Equal("Diaria", recordatorio.Frecuencia);
        Assert.Equal(new TimeSpan(8, 0, 0), recordatorio.Hora);
        Assert.Equal(1, recordatorio.UsuarioId);
    }
}
