using Postgrest.Attributes;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrEParateApp.Model
{
    [Table("usuario")]
    public class Usuario : BaseModel
    {
        [PrimaryKey("id", false)]
        public int ID { get; set; }

        [Column("DNI")]
        public string DNI { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("ESTADO_PACIENTE")]
        public string EstadoPaciente { get; set; }

        [Column("NUMERO_SS")]
        public string NumeroSS { get; set; }

        [Column("NUMERO_SIP")]
        public string NumeroSIP { get; set; }

        [Column("IMAGEN_URL")]
        public string ImagenURL { get; set; }

        [Column("FECHA_NACIMIENTO")]
        public DateTime FechaNacimiento { get; set; }

        [Column("MEDICO")]
        public int MedicoID { get; set; }

        public Usuario() {}

        public Usuario(string dni, string nombre, string password, string estadoPaciente, string numeroSS, string numeroSIP, string imagenURL, DateTime fechaNacimiento, int medicoId)
        {
            this.DNI = dni;
            this.Nombre = nombre;
            this.Password = password;
            this.EstadoPaciente = estadoPaciente;
            this.NumeroSS = numeroSS;
            this.NumeroSIP = numeroSIP;
            this.ImagenURL = imagenURL;
            this.FechaNacimiento = fechaNacimiento;
            this.MedicoID = medicoId;
        }


        public Usuario(Usuario u) 
        { 
            this.ID = u.ID;
            this.DNI = u.DNI;
            this.Nombre = u.Nombre;
            this.Password = u.Password;
            this.EstadoPaciente = u.EstadoPaciente;
            this.NumeroSS = u.NumeroSS;
            this.NumeroSIP = u.NumeroSIP;
            this.ImagenURL = u.ImagenURL;
            this.FechaNacimiento = u.FechaNacimiento;
            this.MedicoID = u.MedicoID;
        }
    }
}
