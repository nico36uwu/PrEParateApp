using Postgrest.Attributes;
using Postgrest.Models;
using System;

namespace PrEParateApp.Model
{
    [Table("toma_medicacion")]
    public class TomaMedicacion : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("COMENTARIOS")]
        public string Comentarios { get; set; }

        [Column("FECHA")]
        public DateTime Fecha { get; set; }

        [Column("HORA")]
        public TimeSpan Hora { get; set; }

        [Column("USUARIO")]
        public int UsuarioId { get; set; }

        public TomaMedicacion() { }

        public TomaMedicacion(string comentarios, DateTime fecha, TimeSpan hora, int usuarioId)
        {
            this.Comentarios = comentarios;
            this.Fecha = fecha;
            this.Hora = hora;
            this.UsuarioId = usuarioId;
        }

        public TomaMedicacion(TomaMedicacion tm)
        {
            this.Id = tm.Id;
            this.Comentarios = tm.Comentarios;
            this.Fecha = tm.Fecha;
            this.Hora = tm.Hora;
            this.UsuarioId = tm.UsuarioId;
        }
    }
}
