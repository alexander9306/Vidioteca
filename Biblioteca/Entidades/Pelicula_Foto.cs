using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Biblioteca.Entidades
{
    public class Pelicula_Foto
    {
        [Required]
        public int idfoto { get; set; }
        public byte[] foto { get; set; }
    }
}