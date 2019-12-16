using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Web.Models.Actor
{
    public class CrearViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        [Required]
        public string fechanac { get; set; }
        [Required]
        [StringLength(1, MinimumLength = 1,
            ErrorMessage = "El sexo debe ser M para masculino o F para femenino.")]
        public string sexo { get; set; }
        public byte[] foto { get; set; }
    }
}
