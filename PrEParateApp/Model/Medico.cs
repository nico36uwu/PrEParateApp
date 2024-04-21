using Postgrest.Attributes;
using Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrEParateApp.Model
{
    [Table("medico")]
    public class Medico : BaseModel
    {

        [PrimaryKey("id", false)]
        public int ID { get; set; }

        [Column("DNI")]
        public string DNI { get; set; }

        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        public Medico() {}

        public Medico(string dni, string nombre, string password) 
        {
            this.DNI = dni;
            this.Nombre = nombre;
            this.Password = password;
        }

        public Medico(Medico m) 
        {
            this.ID = m.ID;
            this.DNI = m.DNI;
            this.Nombre = m.Nombre;
            this.Password = m.Password;
        }
    }
}
