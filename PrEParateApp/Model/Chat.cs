using Postgrest.Attributes;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrEParateApp.Model
{
    [Table("chat")]
    public class Chat : BaseModel
    {

        [PrimaryKey("id", false)]
        public int ID { get; set; }

        [Column("NOTIF_PENDIENTE")]
        public bool NotificacionPendiente { get; set; }

        [Column("MEDICO")]
        public int MedicoId { get; set; }

        [Column("USUARIO")]
        public int UsuarioId { get; set; }

        public Chat() { }

        public Chat(bool notificacionpendiente, int medicoid, int usuarioid)
        {
            this.NotificacionPendiente = notificacionpendiente;
            this.MedicoId = medicoid;
            this.UsuarioId = usuarioid;
        }

        public Chat(Chat c)
        {
            this.ID = c.ID;
            this.NotificacionPendiente = c.NotificacionPendiente;
            this.MedicoId = c.MedicoId;
            this.UsuarioId = c.UsuarioId;
        }
    }
}

