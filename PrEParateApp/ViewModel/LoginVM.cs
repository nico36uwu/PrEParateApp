using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PrEParateApp.Model;
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
                bool isSuccess = await _authService.Login(Dni, Password);
                if (isSuccess)
                {
                    // Navegar a la página principal
                    Console.WriteLine("Inicio de sesión exitoso.");
                    Application.Current.MainPage = new MainPageView();
                }
                else
                {
                    // Mostrar mensaje de error
                    Console.WriteLine("Credenciales incorrectas.");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante el inicio de sesión
                Console.WriteLine($"Error al iniciar sesión: {ex.Message}");
            }
        }
    }
}
