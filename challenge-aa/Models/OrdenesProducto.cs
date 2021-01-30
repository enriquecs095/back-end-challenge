using System;
using System.Collections.Generic;

#nullable disable

namespace challenge_aa.Models
{
    public partial class OrdenesProducto
    {
        public int? IdOrden { get; set; }
        public int? IdProducto { get; set; }
        public int IdOrdenesProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? TotalProducto { get; set; }

        public virtual Ordene IdOrdenNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
