﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Capitulo02.Models
{
    public class Instituicao
    {
        public long? InstituicaoID { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }

        public virtual ICollection<Departamento>? Departamentos { get; set; }
    }
}