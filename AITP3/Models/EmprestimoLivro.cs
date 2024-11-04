using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITP3.Models
{
    public class EmprestimoLivro
    {
        public int EmprestimoId { get; set; }
        public Emprestimo Emprestimo { get; set; }

        public int LivroId { get; set; }
        public Livro Livro { get; set; }
    }
}
