using System;
using System.Collections.Generic;

#nullable disable

namespace Challenge_AWS.Models
{
    public partial class Ordene
    {
        public Ordene()
        {
            OrdenesProductos = new HashSet<OrdenesProducto>();
        }

        public int Idorden { get; set; }
        public DateTime? Fechaorden { get; set; }
        public decimal? Totalorden { get; set; }
        public int? Idcliente { get; set; }

        public virtual Cliente IdclienteNavigation { get; set; }
        public virtual ICollection<OrdenesProducto> OrdenesProductos { get; set; }
    }
}
