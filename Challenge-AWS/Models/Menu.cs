using System;
using System.Collections.Generic;

#nullable disable

namespace Challenge_AWS.Models
{
    public partial class Menu
    {
        public Menu()
        {
            Productos = new HashSet<Producto>();
        }

        public int Idmenu { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
