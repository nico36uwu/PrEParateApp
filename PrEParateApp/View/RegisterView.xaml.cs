using PrEParateApp.ViewModel;

namespace PrEParateApp.View;

public partial class RegisterView : ContentPage
{
	public RegisterView(RegisterVM r)
	{
		InitializeComponent();
		BindingContext = r;
	}
}