using CommunityToolkit.Mvvm.Input;
using Postgrest.Attributes;
using Postgrest.Models;
using PrEParateApp.View;
using System;

namespace PrEParateApp.Model
{
    [Table("evento")]
    public class Evento : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("TIPO")]
        public string Tipo { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("FECHA")]
        public DateTime Fecha { get; set; }

        [Column("USUARIO")]
        public int UsuarioId { get; set; }

        public Evento() { }

        public Evento(string tipo, string nombre, DateTime fecha, int usuarioId)
        {
            this.Tipo = tipo;
            this.Nombre = nombre;
            this.Fecha = fecha;
            this.UsuarioId = usuarioId;
        }

        public Evento(Evento e)
        {
            this.Id = e.Id;
            this.Tipo = e.Tipo;
            this.Nombre = e.Nombre;
            this.Fecha = e.Fecha;
            this.UsuarioId = e.UsuarioId;
        }
    }
}
