using Microsoft.Extensions.Logging;
using Supabase;
using CommunityToolkit.Maui;
using PrEParateApp.View;
using PrEParateApp.Model;
using PrEParateApp.ViewModel;

namespace PrEParateApp;

public static class MauiProgram
{
    public static MauiApp App { get; private set; }

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            // Initialize the .NET MAUI Community Toolkit by adding the below line of code
            .UseMauiCommunityToolkit()
            // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Continue initializing your .NET MAUI App here

        //Supabase Config
        builder.Services.AddSingleton(provider => new Supabase.Client(AppConfig.SUPABASE_URL, AppConfig.SUPABASE_KEY));

        //View
        builder.Services.AddTransient<MainPageView>();
        builder.Services.AddTransient<ChatView>();
        builder.Services.AddTransient<AgendaPersonalView>();
        builder.Services.AddTransient<CalendarioView>();
        builder.Services.AddTransient<CuestionarioView>();
        builder.Services.AddTransient<PerfilUsuarioView>();
        builder.Services.AddTransient<LoginView>();
        builder.Services.AddTransient<RegisterView>();

        //ViewModel
        builder.Services.AddTransient<ChatVM>();
        builder.Services.AddTransient<AgendaPersonalVM>();
        builder.Services.AddTransient<CalendarioVM>();
        builder.Services.AddTransient<CuestionarioVM>();
        builder.Services.AddTransient<PerfilUsuarioVM>();
        builder.Services.AddTransient<LoginVM>();
        builder.Services.AddTransient<RegisterVM>();

        //Services
        builder.Services.AddSingleton<UsuarioRepository>();
        builder.Services.AddSingleton<MedicoRepository>();
        builder.Services.AddSingleton<ChatRepository>();
        builder.Services.AddSingleton<MensajeRepository>();
        builder.Services.AddSingleton<AuthenticationService>();

        App = builder.Build();
        return App;
    }
}
