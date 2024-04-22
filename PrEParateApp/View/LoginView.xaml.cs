using PrEParateApp.ViewModel;

namespace PrEParateApp.View;

public partial class LoginView : ContentPage
{
	public LoginView(LoginVM l)
	{
		InitializeComponent();
		BindingContext = l;
	}


}