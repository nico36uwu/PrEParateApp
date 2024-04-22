using PrEParateApp.View;

namespace PrEParateApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            RegisterRoutes();
            MainPage = new View.MainPageView();
        }

        private void RegisterRoutes()
        {
            try
            {
                Routing.RegisterRoute(nameof(MainPageView), typeof(View.MainPageView));
                Routing.RegisterRoute(nameof(ChatView), typeof(ChatView));
                Routing.RegisterRoute(nameof(AgendaPersonalView), typeof(AgendaPersonalView));
                Routing.RegisterRoute(nameof(CalendarioView), typeof(CalendarioView));
                Routing.RegisterRoute(nameof(CuestionarioView), typeof(CuestionarioView));
                Routing.RegisterRoute(nameof(PerfilUsuarioView), typeof(PerfilUsuarioView));
            }
            catch (Exception e)
            {
                // Manejar la excepción de manera adecuada, por ejemplo, registrarla o mostrar un mensaje de error.
            }
        }
    }
}
