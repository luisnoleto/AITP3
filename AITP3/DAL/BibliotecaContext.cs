using AITP3.Migrations;
using AITP3.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AITP3.DAL
{
    public class BibliotecaContext : IdentityDbContext<ApplicationUser>
    {
        public BibliotecaContext() : base("BibliotecaContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BibliotecaContext, Configuration>());
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Adicione essa linha para configurar o Identity

            // Remove a convenção de pluralização de nomes de tabela
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Configurar a relação entre Livro e Categoria
            modelBuilder.Entity<Livro>()
                .HasRequired(l => l.Categoria)
                .WithMany(c => c.Livros)
                .HasForeignKey(l => l.CategoriaId);

            // Configurar a relação entre Emprestimo e Usuario
            modelBuilder.Entity<Emprestimo>()
                .HasRequired(e => e.Usuario)
                .WithMany(u => u.Emprestimos)
                .HasForeignKey(e => e.UsuarioId);

            modelBuilder.Entity<ApplicationUser>()
                .HasOptional(u => u.Endereco)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
     .HasOptional(u => u.Carrinho)     // Cada usuário pode ter um carrinho opcional
     .WithRequired(c => c.Usuario)     // Cada carrinho é obrigatório e tem um único usuário
     .WillCascadeOnDelete();           // Configura a deleção em cascata

            modelBuilder.Entity<Models.EmprestimoLivro>()
       .HasKey(el => new { el.EmprestimoId, el.LivroId });

            modelBuilder.Entity<Models.EmprestimoLivro>()
                .HasRequired(el => el.Emprestimo)
                .WithMany(e => e.EmprestimoLivros)
                .HasForeignKey(el => el.EmprestimoId);

            modelBuilder.Entity<Models.EmprestimoLivro>()
                .HasRequired(el => el.Livro)
                .WithMany(l => l.EmprestimoLivros)
                .HasForeignKey(el => el.LivroId);
        }
    
        


        public static BibliotecaContext Create()
        {
            return new BibliotecaContext();
        }
    }
}
