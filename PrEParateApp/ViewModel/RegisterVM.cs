using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PrEParateApp.Model;
using PrEParateApp.Utilities;
using PrEParateApp.View;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrEParateApp.ViewModel
{
    public partial class RegisterVM : ObservableObject
    {
        [ObservableProperty]
        private string dni;

        [ObservableProperty]
        private string nombreApellidos;

        [ObservableProperty]
        private DateTime fechaNacimiento = DateTime.Today;

        [ObservableProperty]
        private string numeroSS;

        [ObservableProperty]
        private string numeroSIP;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string repeatPassword;

        [ObservableProperty]
        private Medico medicoSeleccionado;

        public ObservableCollection<Medico> MedicosDisponibles { get; } = new ObservableCollection<Medico>();

        private readonly RegisterService _registerService;
        private readonly MedicoService _medicoService;

        public RegisterVM(RegisterService registerService, MedicoService medicoService)
        {
            _registerService = registerService;
            _medicoService = medicoService;
            CargarMedicos();
        }

        private async void CargarMedicos()
        {
            var medicos = await _medicoService.ObtenerMedicos();
            foreach (var medico in medicos)
            {
                MedicosDisponibles.Add(medico);
            }
        }

        [RelayCommand]
        public async Task Register()
        {
            if (String.IsNullOrEmpty(NombreApellidos))
            {
                await ShowErrorMessage("El Nombre y apelldios no puede estar vacío.");
                return;
            }

            if (!Utils.ValidarDNI(Dni) && !Utils.ValidarNIE(Dni))
            {
                await ShowErrorMessage("El DNI o NIE no es correcto.");
                return;
            }

            if (!Utils.EsNumeroSeguridadSocialValido(NumeroSS))
            {
                await ShowErrorMessage("El número de la Seguridad Social no es correcto.");
                return;
            }

            if (!Utils.EsNumeroTarjetaSIPValido(NumeroSIP))
            {
                await ShowErrorMessage("El número de la Tarjeta Sanitaria (SIP) no es correcto.");
                return;
            }

            if (String.IsNullOrEmpty(Password) || Password.Length < 8) 
            {
                await ShowErrorMessage("La contraseña debe tener un mínimo de 8 caracteres.");
                return;
            }


            if (Password != RepeatPassword)
            {
                await ShowErrorMessage("Las contraseñas no coinciden.");
                return;
            }

            var usuario = new Usuario
            {
                DNI = Dni,
                Nombre = NombreApellidos,
                FechaNacimiento = FechaNacimiento,
                NumeroSS = NumeroSS,
                NumeroSIP = NumeroSIP,
                Password = Password,
                MedicoID = MedicoSeleccionado.ID
            };

            bool isRegistered = await _registerService.RegisterUser(usuario);
            if (isRegistered)
            {
                await ShowErrorMessage("Registro exitoso. Su cuenta está pendiente de aprobación.");
                Application.Current.MainPage = MauiProgram.App.Services.GetService<LoginView>();
            }
            else
            {
                await ShowErrorMessage("Error al registrar el usuario. Inténtelo de nuevo.");
            }

            await Task.CompletedTask;
        }

        private async Task ShowErrorMessage(string message)
        {
            await Application.Current.MainPage.DisplayAlert("Error", message, "OK");
        }

        [RelayCommand]
        public async void Volver() 
        {
            Application.Current.MainPage = MauiProgram.App.Services.GetService<LoginView>();
        }
    }
}
