using Postgrest.Attributes;
using Postgrest.Models;
using System;

namespace PrEParateApp.Model
{
    [Table("recordatorio")]
    public class Recordatorio : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("FRECUENCIA")]
        public string Frecuencia { get; set; }

        [Column("HORA")]
        public TimeSpan Hora { get; set; }

        [Column("USUARIO")]
        public int UsuarioId { get; set; }

        public Recordatorio() { }

        public Recordatorio(string nombre, string frecuencia, TimeSpan hora, int usuarioId)
        {
            this.Nombre = nombre;
            this.Frecuencia = frecuencia;
            this.Hora = hora;
            this.UsuarioId = usuarioId;
        }

        public Recordatorio(Recordatorio r)
        {
            this.Id = r.Id;
            this.Nombre = r.Nombre;
            this.Frecuencia = r.Frecuencia;
            this.Hora = r.Hora;
            this.UsuarioId = r.UsuarioId;
        }
    }
}
