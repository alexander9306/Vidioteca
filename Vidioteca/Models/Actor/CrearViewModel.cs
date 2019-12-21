using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Vidioteca.Models.Actor
{
    public class CrearViewModel
    {
        [Required(ErrorMessage = "El campo nombre es requerido.")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El campo fecha es requerido.")]
        public DateTime fechanac { get; set; }
        [Required(ErrorMessage = "El campo sexo es requerido.")]
        public string sexo { get; set; }
        public IFormFile foto { get; set; }
    }
}
