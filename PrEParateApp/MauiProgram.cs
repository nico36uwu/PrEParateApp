using Microsoft.Extensions.Logging;
using Supabase;
using CommunityToolkit.Maui;
using PrEParateApp.View;
using PrEParateApp.Model;
using PrEParateApp.ViewModel;
using System.Globalization;
using Plugin.LocalNotification;

namespace PrEParateApp;

public static class MauiProgram
{
    public static MauiApp App { get; private set; }

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseLocalNotification()
            // Initialize the .NET MAUI Community Toolkit by adding the below line of code
            .UseMauiCommunityToolkit()
            // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Continue initializing your .NET MAUI App here

        // Establecer la cultura en español
        var culture = new CultureInfo("es-ES");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

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
        builder.Services.AddTransient<EventoView>();
        builder.Services.AddTransient<RecordatorioView>();
        builder.Services.AddTransient<RegistroMedicacionView>();


        //ViewModel
        builder.Services.AddTransient<ChatVM>();
        builder.Services.AddTransient<AgendaPersonalVM>();
        builder.Services.AddTransient<CalendarioVM>();
        builder.Services.AddTransient<CuestionarioVM>();
        builder.Services.AddTransient<PerfilUsuarioVM>();
        builder.Services.AddTransient<LoginVM>();
        builder.Services.AddTransient<RegisterVM>();
        builder.Services.AddTransient<EventoVM>();
        builder.Services.AddTransient<RecordatorioVM>();
        builder.Services.AddTransient<RegistroMedicacionVM>();

        //Services
        builder.Services.AddSingleton<UsuarioRepository>();
        builder.Services.AddSingleton<MedicoRepository>();
        builder.Services.AddSingleton<ChatRepository>();
        builder.Services.AddSingleton<MensajeRepository>();
        builder.Services.AddSingleton<RecordatorioRepository>();
        builder.Services.AddSingleton<AuthenticationService>();
        builder.Services.AddSingleton<RegisterService>();
        builder.Services.AddSingleton<MedicoService>();
        builder.Services.AddSingleton<RecordatorioService>();

        App = builder.Build();
        return App;
    }
}
