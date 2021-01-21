using System;
using System.Collections.Generic;

#nullable disable

namespace challenge_aa.Models
{
    public partial class Review
    {
        public int Idreview { get; set; }
        public int? Valoracion { get; set; }
        public string Mensaje { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Idproducto { get; set; }

        public virtual Producto IdproductoNavigation { get; set; }
    }
}
