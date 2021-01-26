using System;
using System.Collections.Generic;

#nullable disable

namespace challenge_aa.Models
{
    public partial class Producto
    {
        public Producto()
        {
            ListaCarritos = new HashSet<ListaCarrito>();
            OrdenesProductos = new HashSet<OrdenesProducto>();
            Reviews = new HashSet<Review>();
        }

        public int Idproducto { get; set; }
        public decimal Precio { get; set; }
        public string Nombre { get; set; }
        public int? Idmenu { get; set; }
        public string Descripcion { get; set; }
        public string Url { get; set; }
        public int? Status { get; set; }

        public virtual Menu IdmenuNavigation { get; set; }
        public virtual ICollection<ListaCarrito> ListaCarritos { get; set; }
        public virtual ICollection<OrdenesProducto> OrdenesProductos { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
