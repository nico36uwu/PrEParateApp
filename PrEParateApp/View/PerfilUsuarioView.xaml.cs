using PrEParateApp.ViewModel;

namespace PrEParateApp.View;

public partial class PerfilUsuarioView : ContentPage
{
	public PerfilUsuarioView(PerfilUsuarioVM p)
	{
		InitializeComponent();
		BindingContext = p;
	}
}