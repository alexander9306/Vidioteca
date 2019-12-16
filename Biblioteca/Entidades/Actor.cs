using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Biblioteca.Entidades
{
    
    public class Actor
    {
        public int idactor { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        [Required]
        public DateTime fechanac { get; set; }
        [Required]
        public char sexo { get; set; }
        [Required]
        public int idfoto { get; set; }
    }
}
