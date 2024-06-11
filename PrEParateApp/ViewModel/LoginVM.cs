using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PrEParateApp.Model;
using PrEParateApp.Utilities;
using PrEParateApp.View;

namespace PrEParateApp.ViewModel
{
    public partial class LoginVM : ObservableObject
    {

        private string _dni;
        private string _password;

        private AuthenticationService _authService;


        public string Dni
        {
            get => _dni;
            set => SetProperty(ref _dni, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public LoginVM() { }

        public LoginVM(AuthenticationService authService)
            : this()
        {
            _authService = authService;
        }

        [RelayCommand]
        public async void Login()
        {
            try
            {
                string loginResult = await _authService.Login(Dni, Password);
                switch (loginResult)
                {
                    case Constantes.ACEPTADO:
                        // Navegar a la página principal
                        Application.Current.MainPage = new MainPageView();
                        break;
                    case Constantes.PENDIENTE:
                        await ShowErrorMessage("Su cuenta está pendiente de aprobación.");
                        break;
                    case Constantes.RECHAZADO:
                        await ShowErrorMessage("Su cuenta ha sido rechazada.");
                        break;
                    default:
                        await ShowErrorMessage("Credenciales incorrectas.");
                        break;
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante el inicio de sesión
                await ShowErrorMessage($"Error al iniciar sesión: {ex.Message}");
            }
        }

        private async Task ShowErrorMessage(string message)
        {
            await Application.Current.MainPage.DisplayAlert("Información", message, "OK");
        }

        [RelayCommand]
        public async void Register() 
        {
            Application.Current.MainPage = MauiProgram.App.Services.GetService<RegisterView>();
        }
    }
}
