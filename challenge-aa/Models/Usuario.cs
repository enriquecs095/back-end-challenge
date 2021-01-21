using System;
using System.Collections.Generic;

#nullable disable

namespace challenge_aa.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Administradors = new HashSet<Administrador>();
            Clientes = new HashSet<Cliente>();
        }

        public int Idusuario { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public int? Rol { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Administrador> Administradors { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
