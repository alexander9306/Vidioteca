using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Web.Models.Elenco
{
    public class CrearViewModel
    {
        public int idelenco { get; set; }
        [Required]
        public int idpelicula { get; set; }
        [Required]
        public int idactor { get; set; }
    }
}
