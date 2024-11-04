namespace AITP3.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AITP3.Models;
    using AITP3.DAL;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System.Data.Entity.Validation;

    public sealed class Configuration : DbMigrationsConfiguration<BibliotecaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BibliotecaContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Verificar e adicionar o endereço, se não existir
            var endereco = context.Enderecos.FirstOrDefault(e => e.CEP == "12345-678") ?? new Endereco
            {
                Logradouro = "Rua das Flores",
                Numero = "123",
                Complemento = "Apto 1",
                Bairro = "Centro",
                Cidade = "São Paulo",
                Estado = "SP",
                CEP = "12345-678"
            };
            if (!context.Enderecos.Any(e => e.CEP == endereco.CEP))
                context.Enderecos.AddOrUpdate(e => e.CEP, endereco);

            // Adicionar administrador, se não existir
            if (!context.Users.Any(u => u.UserName == "admin@biblioteca.com"))
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@biblioteca.com",
                    Email = "admin@biblioteca.com",
                    Nome = "Admin",
                    Sobrenome = "Biblioteca",
                    DataNascimento = new DateTime(1985, 1, 1),
                    Endereco = endereco,
                    TipoUsuario = TipoUsuario.Administrador
                };
                userManager.Create(admin, "Admin123!");
            }

            // Adicionar usuário comum, se não existir
            if (!context.Users.Any(u => u.UserName == "usuario@biblioteca.com"))
            {
                var usuario = new ApplicationUser
                {
                    UserName = "usuario@biblioteca.com",
                    Email = "usuario@biblioteca.com",
                    Nome = "Usuario",
                    Sobrenome = "Comum",
                    DataNascimento = new DateTime(1995, 5, 5),
                    Endereco = endereco,
                    TipoUsuario = TipoUsuario.Usuario
                };
                userManager.Create(usuario, "Usuario123!");
            }

            context.SaveChanges();

            // Criar uma categoria de exemplo, se não existir
            var categoria = context.Categorias.FirstOrDefault(c => c.Nome == "Ficção Científica") ?? new Categoria
            {
                Nome = "Ficção Científica"
            };
            if (!context.Categorias.Any(c => c.Nome == categoria.Nome))
                context.Categorias.AddOrUpdate(c => c.Nome, categoria);

            context.SaveChanges();

            // Adicionar livros, se não existirem
            var livro1 = new Livro
            {
                Titulo = "Duna",
                Autor = "Frank Herbert",
                Editora = "Nova Editora",
                ISBN = "978-3-16-148410-0",
                AnoPublicacao = "1965",
                CategoriaId = categoria.Id,
                Valor = 39.90m
            };
            var livro2 = new Livro
            {
                Titulo = "Neuromancer",
                Autor = "William Gibson",
                Editora = "CyberEditora",
                ISBN = "978-3-16-148411-7",
                AnoPublicacao = "1984",
                CategoriaId = categoria.Id,
                Valor = 29.90m
            };

            context.Livros.AddOrUpdate(l => l.ISBN, livro1, livro2);
            context.SaveChanges();

            // Criar um empréstimo para o usuário, se não existir
            var usuarioExistente = context.Users.SingleOrDefault(u => u.UserName == "usuario@biblioteca.com");
            if (usuarioExistente != null && !context.Emprestimos.Any(e => e.UsuarioId == usuarioExistente.Id))
            {
                var emprestimo = new Emprestimo
                {
                    DataEmprestimo = DateTime.Now,
                    DataDevolucao = DateTime.Now.AddDays(14),
                    ValorTotal = 39.90m,
                    UsuarioId = usuarioExistente.Id,
                    EmprestimoLivros = new List<EmprestimoLivro> { new EmprestimoLivro { Livro = livro1 } }
                };
                context.Emprestimos.AddOrUpdate(e => e.Id, emprestimo);
            }

            context.SaveChanges();
        }


    }
}
