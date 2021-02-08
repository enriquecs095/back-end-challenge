using System;
using System.Collections.Generic;

#nullable disable

namespace Challenge_AWS.Models
{
    public partial class ListaCarrito
    {
        public int? Idusuario { get; set; }
        public int? Idproducto { get; set; }
        public int? Cantidad { get; set; }
        public int IdLista { get; set; }

        public virtual Producto IdproductoNavigation { get; set; }
        public virtual Usuario IdusuarioNavigation { get; set; }
    }
}
