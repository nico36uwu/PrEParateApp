using PrEParateApp.ViewModel;

namespace PrEParateApp.View;

public partial class AgendaPersonalView : ContentPage
{
	public AgendaPersonalView(AgendaPersonalVM a)
	{
		InitializeComponent();
		BindingContext = a;
	}
}