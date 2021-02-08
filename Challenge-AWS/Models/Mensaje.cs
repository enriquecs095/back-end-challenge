using System;
using System.Collections.Generic;

#nullable disable

namespace Challenge_AWS.Models
{
    public partial class Mensaje
    {
        public int Idmensaje { get; set; }
        public int? Status { get; set; }
        public DateTime? Fechamensaje { get; set; }
        public string Mensaje1 { get; set; }
        public int? Idchat { get; set; }

        public virtual Chat IdchatNavigation { get; set; }
    }
}
