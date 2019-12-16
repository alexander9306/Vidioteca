using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Web.Models.Pelicula
{
    public class ActorViewModel
    {
        public int idactor { get; set; }
        public string nombre { get; set; }
        public DateTime fechanac { get; set; }
        public char sexo { get; set; }
        public byte[] foto { get; set; }
    }
}
