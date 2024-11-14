using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabG9.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }

        [Required]
        public decimal Precio { get; set; } 

        [Required]
        public int cantidad {  get; set; }

        [MaxLength(255)]
        public string Descripcion { get; set; }

        public bool Estado { get; set; }  

        [MaxLength(255)]
        public string ImagenUrl { get; set; }  


        public ICollection<DetalleFactura> DetalleFacturas { get; set; }
    }
}