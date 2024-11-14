using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabG9.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required]
        [MaxLength(50)]
        public string NombreCategoria { get; set; }

        [MaxLength(255)]
        public string Descripcion { get; set; }

        public ICollection<Producto> Productos { get; set; }
    }
}