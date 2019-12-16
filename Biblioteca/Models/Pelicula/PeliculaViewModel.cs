using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Web.Models.Pelicula
{
    public class PeliculaViewModel
    {
        public int idpelicula { get; set; }
        public string titulo { get; set; }
        public string genero { get; set; }
        public DateTime fechaestreno { get; set; }
        public byte[] foto { get; set; }
    }
}
