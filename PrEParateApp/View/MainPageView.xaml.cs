using CommunityToolkit.Maui.Core.Platform;
using PrEParateApp.View;

namespace PrEParateApp.View
{
    public partial class MainPageView : Shell
    {

        public MainPageView()
        {
            InitializeComponent();
            SetDefaultTab();
        }

        private void SetDefaultTab()
        {
            // Encontrar el TabBar
            var tabBar = Items[0] as TabBar;
            if (tabBar != null)
            {
                // Establecer el ShellContent predeterminado como el segundo elemento (AgendaPersonalView)
                tabBar.CurrentItem = tabBar.Items[2];
            }
        }

    }

}
