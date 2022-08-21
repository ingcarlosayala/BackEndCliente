using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ClienteDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [MaxLength(50)]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "La {0} es requerida")]
        [MaxLength(200)]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(12)]
        public string Telefono { get; set; }
    }
}