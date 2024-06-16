using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PrEParateApp.Model;
using PrEParateApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrEParateApp.ViewModel
{
    public partial class PerfilUsuarioVM : ObservableObject
    {
        private AuthenticationService _authService;
        private UsuarioRepository _usuarioRepository;

        public string FechaNacimientoFormateada => Usuario?.FechaNacimiento.ToString("dd/MM/yyyy");
        public string ImagenPerfilUrl => Usuario.ImagenURL ?? "perfil_imagen.png";

        private Usuario _usuario;
        public Usuario Usuario
        {
            get => _usuario;
            set => SetProperty(ref _usuario, value);
        }

        private Medico _medico;
        public Medico Medico
        {
            get => _medico;
            set => SetProperty(ref _medico, value);
        }

        public PerfilUsuarioVM(AuthenticationService authService, UsuarioRepository usuarioRepository)
        {
            _authService = authService;
            _usuarioRepository = usuarioRepository;
            _usuario = authService.UsuarioConectado;
            _medico = authService.MedicoUsario;
        }

        [RelayCommand]
        public async Task CerrarSesion()
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert("Confirmación", "¿Estás seguro de que quieres cerrar sesión?", "Sí", "No");
            if (confirm)
            {
                _authService.Logout();
                // Navegar a la página de inicio de sesión
                Application.Current.MainPage = MauiProgram.App.Services.GetService<LoginView>();
            }
        }

        [RelayCommand]
        public async Task SeleccionarYSubirImagen()
        {
            try
            {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.iOS, new[] { "public.image" } }, // Ejemplo para iOS
                { DevicePlatform.Android, new[] { "image/*" } } // Soporte para Android
                // Agrega tipos de archivos para otras plataformas si es necesario
            });

                var opciones = new PickOptions
                {
                    PickerTitle = "Selecciona una imagen",
                    FileTypes = customFileType,
                };

                var resultado = await FilePicker.PickAsync(opciones);
                if (resultado != null)
                {
                    using var stream = await resultado.OpenReadAsync();
                    var urlImagen = await _usuarioRepository.SubirImagenPerfilAsync(stream, resultado.FileName);

                    if (!string.IsNullOrEmpty(urlImagen))
                    {
                        // Actualiza la URL de la imagen del usuario conectado
                        Usuario.ImagenURL = urlImagen;

                        // Guarda los cambios en la base de datos usando el repositorio
                        await _usuarioRepository.ActualizarImagen(Usuario);

                        // Notifica a la vista que la propiedad ha cambiado para actualizar la UI
                        OnPropertyChanged(nameof(ImagenPerfilUrl));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
