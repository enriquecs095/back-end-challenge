using System;
using System.Collections.Generic;

#nullable disable

namespace Challenge_AWS.Models
{
    public partial class Administrador
    {
        public Administrador()
        {
            Chats = new HashSet<Chat>();
        }

        public int Idadministrador { get; set; }
        public int? Idusuario { get; set; }

        public virtual Usuario IdusuarioNavigation { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
    }
}
