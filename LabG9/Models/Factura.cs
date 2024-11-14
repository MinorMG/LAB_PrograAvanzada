using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabG9.Models
{
    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }

        public string ClienteId { get; set; }  

        [ForeignKey("ClienteId")]
        public virtual ApplicationUser Cliente { get; set; } 

        public DateTime FechaPedido { get; set; }
        public string DireccionEntrega { get; set; }

        public string MetodoPago {  get; set; }

        public decimal totalFactura {  get; set; }

        public string Estado { get; set; }

      
        public ICollection<DetalleFactura> DetalleFactura { get; set; }
    }
}
