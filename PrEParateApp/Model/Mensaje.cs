using Postgrest.Attributes;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrEParateApp.Model
{
    [Table("mensaje")]
    public class Mensaje : BaseModel
    {

        [PrimaryKey("id", false)]
        public int ID { get; set; }

        [Column("TEXTO")]
        public string Texto { get; set; }

        [Column("FECHA")]
        public DateTime Fecha { get; set; }

        [Column("CHAT")]
        public int ChatId { get; set; }

        [Column("AUTOR_MEDICO")]
        public int? AutorMedicoId { get; set; }

        [Column("AUTOR_USUARIO")]
        public int? AutorUsuarioId { get; set; }

        [IgnoreDataMember]
        public bool EsDeUsuario { get; set; }

        public Mensaje() { }

        public Mensaje(string texto, DateTime fecha, int chatId, int autormedicoid, int autorusuarioid)
        {
            this.Texto = texto;
            this.Fecha = fecha;
            this.ChatId = chatId;
            this.AutorMedicoId = autormedicoid;
            this.AutorUsuarioId = autorusuarioid;
        }

        public Mensaje(Mensaje m)
        {
            this.ID = m.ID;
            this.Texto = m.Texto;
            this.Fecha = m.Fecha;
            this.ChatId = m.ChatId;
            this.AutorMedicoId= m.AutorMedicoId;
            this.AutorUsuarioId = m.AutorUsuarioId;
        }
    }
}

