﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vidioteca.Models
{
    public class Actor
    {
        public int idactor { get; set; }
        public string nombre { get; set; }
        public DateTime fechanac { get; set; }
        public string sexo { get; set; }
        public byte[] foto { get; set; }
    }
}
