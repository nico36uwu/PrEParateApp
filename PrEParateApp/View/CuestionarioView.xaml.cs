using PrEParateApp.ViewModel;

namespace PrEParateApp.View;

public partial class CuestionarioView : ContentPage
{
	public CuestionarioView(CuestionarioVM c)
	{
		InitializeComponent();
		BindingContext = c;
	}
}