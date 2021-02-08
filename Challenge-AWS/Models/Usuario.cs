using System;
using System.Collections.Generic;

#nullable disable

namespace Challenge_AWS.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Administradors = new HashSet<Administrador>();
            Clientes = new HashSet<Cliente>();
            ListaCarritos = new HashSet<ListaCarrito>();
        }

        public int Idusuario { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public int? Rol { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Administrador> Administradors { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<ListaCarrito> ListaCarritos { get; set; }
    }
}
