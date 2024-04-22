using PrEParateApp.Model;
using PrEParateApp.View;
using PrEParateApp.ViewModel;

namespace PrEParateApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            RegisterRoutes();
            UsuarioRepository usuarioRepository = new UsuarioRepository(new Supabase.Client(AppConfig.SUPABASE_URL, AppConfig.SUPABASE_KEY));
            LoginVM vm = new LoginVM(usuarioRepository);
            MainPage = new LoginView(vm);
            //MainPage = new MainPageView();
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
            }
            catch (Exception e)
            {
                // Manejar la excepción de manera adecuada, por ejemplo, registrarla o mostrar un mensaje de error.
            }
        }
    }
}
