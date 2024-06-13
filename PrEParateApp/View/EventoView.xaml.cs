using PrEParateApp.ViewModel;

namespace PrEParateApp.View;

public partial class EventoView : ContentPage
{
	public EventoView(EventoVM e)
	{
		InitializeComponent();
		BindingContext = e;
	}
}