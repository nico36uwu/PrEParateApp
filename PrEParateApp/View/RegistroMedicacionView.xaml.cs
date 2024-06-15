using CommunityToolkit.Maui.Views;
using PrEParateApp.ViewModel;

namespace PrEParateApp.View
{
    public partial class RegistroMedicacionView : Popup
    {
        public RegistroMedicacionView(RegistroMedicacionVM viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            viewModel.SetPopup(this);
        }
    }
}
