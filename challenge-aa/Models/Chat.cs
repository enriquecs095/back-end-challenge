using System;
using System.Collections.Generic;

#nullable disable

namespace challenge_aa.Models
{
    public partial class Chat
    {
        public Chat()
        {
            Mensajes = new HashSet<Mensaje>();
        }

        public int Idchat { get; set; }
        public int? Status { get; set; }
        public int? Idcliente { get; set; }
        public int? Idadministrador { get; set; }

        public virtual Administrador IdadministradorNavigation { get; set; }
        public virtual Cliente IdclienteNavigation { get; set; }
        public virtual ICollection<Mensaje> Mensajes { get; set; }
    }
}
