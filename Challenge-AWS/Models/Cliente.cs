using System;
using System.Collections.Generic;

#nullable disable

namespace Challenge_AWS.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Chats = new HashSet<Chat>();
            Ordenes = new HashSet<Ordene>();
        }

        public int Idcliente { get; set; }
        public int? Idusuario { get; set; }
        public string Telefono { get; set; }

        public virtual Usuario IdusuarioNavigation { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<Ordene> Ordenes { get; set; }
    }
}
