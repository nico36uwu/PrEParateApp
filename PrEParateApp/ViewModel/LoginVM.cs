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
        private UsuarioRepository _usuarioRepository;
        private string _dni;
        private string _password;

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

        public LoginVM(UsuarioRepository usuarioRepository)
            : this()
        {
            _usuarioRepository = usuarioRepository;
        }

        [RelayCommand]
        public async void Login()
        {
            try
            {
                // Aquí puedes añadir la lógica para verificar el DNI y la contraseña del usuario
                var user = await _usuarioRepository.FindByDniAndPassword(Dni, Password);
                if (user != null)
                {
                    // Implementar la lógica de navegación o mostrar un mensaje de éxito
                    Console.WriteLine("Inicio de sesión exitoso.");
                    Application.Current.MainPage = new MainPageView();
                }
                else
                {
                    // Manejar el caso de credenciales incorrectas
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
