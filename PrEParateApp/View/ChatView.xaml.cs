using PrEParateApp.ViewModel;

namespace PrEParateApp.View;

public partial class ChatView : ContentPage
{
	public ChatView(ChatVM c)
	{
		InitializeComponent();
		BindingContext = c;
	}
}