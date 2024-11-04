using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AITP3.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public string ISBN { get; set; }
        public string AnoPublicacao { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
        public decimal Valor { get; set; }

        public virtual ICollection<EmprestimoLivro> EmprestimoLivros { get; set; }
    }

}