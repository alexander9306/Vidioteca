using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Web.Models.Pelicula
{
    public class ActualizarViewModel
    {
        [Required]
        public int idpelicula { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "El titulo no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string titulo { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3,
            ErrorMessage = "El genero no debe de tener más de 60 caracteres, ni menos de 3 caracteres.")]
        public string genero { get; set; }
        [Required]
        public string fechaestreno { get; set; }
        [Required]
        public byte[] foto { get; set; }
    }
}
