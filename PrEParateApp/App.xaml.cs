using PrEParateApp.Model;
using PrEParateApp.View;
using PrEParateApp.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Plugin.LocalNotification;  // Importar para usar GetRequiredService

namespace PrEParateApp
{
    public partial class App : Application
    {
        private IServiceProvider Services => MauiProgram.App.Services;

        public App()
        {
            InitializeComponent();
            RegisterRoutes();

            // Configurar la página principal usando DI
            MainPage = Services.GetService<LoginView>();
        }

        private void RegisterRoutes()
        {
            try
            {
                Routing.RegisterRoute(nameof(MainPageView), typeof(MainPageView));
                Routing.RegisterRoute(nameof(ChatView), typeof(ChatView));
                Routing.RegisterRoute(nameof(AgendaPersonalView), typeof(AgendaPersonalView));
                Routing.RegisterRoute(nameof(CalendarioView), typeof(CalendarioView));
                Routing.RegisterRoute(nameof(CuestionarioView), typeof(CuestionarioView));
                Routing.RegisterRoute(nameof(PerfilUsuarioView), typeof(PerfilUsuarioView));
                Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
                Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
                Routing.RegisterRoute(nameof(EventoView), typeof(EventoView));
                Routing.RegisterRoute(nameof(RecordatorioView), typeof(RecordatorioView));
                Routing.RegisterRoute(nameof(RegistroMedicacionView), typeof(RegistroMedicacionView));
            }
            catch (Exception e)
            {
                // Manejar la excepción de manera adecuada, por ejemplo, registrarla o mostrar un mensaje de error.
                Console.WriteLine($"Error al registrar rutas: {e.Message}");
            }
        }
    }
}
