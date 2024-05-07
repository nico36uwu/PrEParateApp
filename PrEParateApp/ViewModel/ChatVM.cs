using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PrEParateApp.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PrEParateApp.ViewModel
{
    public partial class ChatVM : ObservableObject
    {
        private readonly AuthenticationService _authenticationService;
        private readonly MensajeRepository _mensajeRepository;

        [ObservableProperty]
        private string textoMensaje;

        public ObservableCollection<Mensaje> Mensajes { get; } = new ObservableCollection<Mensaje>();

        public ChatVM(AuthenticationService authenticationService, MensajeRepository mensajeRepository)
        {
            _authenticationService = authenticationService;
            _mensajeRepository = mensajeRepository;
            _mensajeRepository.OnMensajeInserted += MensajeRepository_OnMensajeInserted; // Suscribirse al evento
            LoadMensajes();
        }

        [RelayCommand]
        public async Task EnviarMensaje()
        {
            if (!string.IsNullOrWhiteSpace(TextoMensaje))
            {
                var nuevoMensaje = new Mensaje
                {
                    Texto = TextoMensaje,
                    Fecha = DateTime.Now,
                    ChatId = _authenticationService.ChatUsario.ID,
                    AutorUsuarioId = _authenticationService.UsuarioConectado.ID,
                    EsDeUsuario = true,
                };

                await _mensajeRepository.Insertar(nuevoMensaje);
                Mensajes.Add(nuevoMensaje);
                TextoMensaje = string.Empty;
            }
        }

        private async void LoadMensajes()
        {
            var mensajes = await _mensajeRepository.GetMensajesChat(_authenticationService.ChatUsario.ID);
            foreach (var mensaje in mensajes)
            {
                mensaje.EsDeUsuario = mensaje.AutorUsuarioId == _authenticationService.UsuarioConectado.ID;
                Mensajes.Add(mensaje);
            }
            await _mensajeRepository.InitializeAsync();
        }

        private void MensajeRepository_OnMensajeInserted(Mensaje mensaje)
        {
            if (mensaje.ChatId == _authenticationService.ChatUsario.ID)
            {
                mensaje.EsDeUsuario = mensaje.AutorUsuarioId == _authenticationService.UsuarioConectado.ID;
                Mensajes.Add(mensaje);
            }
        }
    }

}
