using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITP3.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public virtual ICollection<Livro> Livros { get; set; }
    }
}