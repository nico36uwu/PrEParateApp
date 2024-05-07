using PrEParateApp.ViewModel;

namespace PrEParateApp.View;

public partial class ChatView : ContentPage
{
    public ChatView(ChatVM c)
    {
        InitializeComponent();
        BindingContext = c;

        // Suscribirse al evento para desplazarse automáticamente
        ((ChatVM)BindingContext).ScrollToMessage += ScrollToLatestMessage;
    }

    private void ScrollToLatestMessage()
    {
        Dispatcher.Dispatch(async () =>
        {
            await MensajesScrollView.ScrollToAsync(0, MensajesScrollView.ContentSize.Height, true);
        });
    }
}
