using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AITP3.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }

        [Display(Name = "Data de Empréstimo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataEmprestimo { get; set; }

        [Display(Name = "Data de Devolução")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataDevolucao { get; set; }
        [Display(Name = "Valor do Emprestimo")]
        public decimal ValorTotal { get; set; }

        public virtual ICollection<EmprestimoLivro> EmprestimoLivros { get; set; }

        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }

}