using PrEParateApp.ViewModel;

namespace PrEParateApp.View;

public partial class RecordatorioView : ContentPage
{
	public RecordatorioView(RecordatorioVM r)
	{
		InitializeComponent();
		BindingContext = r;
	}
}