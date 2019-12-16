using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Biblioteca.Entidades
{
    public class Elenco
    {
        public int idelenco { get; set; }
        [Required]
        public int idpelicula { get; set; }
        [Required]
        public int idactor { get; set; }
    }
}
