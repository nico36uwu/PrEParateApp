using PrEParateApp.ViewModel;

namespace PrEParateApp.View;

public partial class CalendarioView : ContentPage
{
	public CalendarioView(CalendarioVM c)
	{
		InitializeComponent();
		BindingContext = c;
	}
}