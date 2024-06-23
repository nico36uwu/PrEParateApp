using Xunit;
using PrEParateApp.Model;

namespace Model;

public class ChatTests
{
    [Fact]
    public void PuedeCrearChat()
    {
        var chat = new Chat(true, 1, 2);

        Assert.True(chat.NotificacionPendiente);
        Assert.Equal(1, chat.MedicoId);
        Assert.Equal(2, chat.UsuarioId);
    }
}
