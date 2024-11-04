using System.Collections.Generic;

namespace AITP3.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public List<Livro> Items { get; set; } = new List<Livro>();

        public string UsuarioId { get; set; }
        public virtual ApplicationUser Usuario { get; set; }

        public void AddItem(Livro livro)
        {
            Items.Add(livro);
        }
    }
}
