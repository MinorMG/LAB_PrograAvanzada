using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabG9.Models
{
    public class DetalleFactura
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Factura")]
        public int IdFactura { get; set; }
        public Factura Factura { get; set; }

        [ForeignKey("Producto")]
        public int IdProducto { get; set; }
        public Producto Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; } 
    }
}