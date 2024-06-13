using PrEParateApp.ViewModel;

namespace PrEParateApp.View;

public partial class RegistroMedicacionView : ContentPage
{
	public RegistroMedicacionView(RegistroMedicacionVM r)
	{
		InitializeComponent();
		BindingContext = r;
	}
}