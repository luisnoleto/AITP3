namespace AITP3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioId = c.String(),
                        Usuario_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Usuario_Id, cascadeDelete: true)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.Livro",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Autor = c.String(),
                        Editora = c.String(),
                        ISBN = c.String(),
                        AnoPublicacao = c.String(),
                        CategoriaId = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categoria", t => t.CategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.Cart", t => t.Cart_Id)
                .Index(t => t.CategoriaId)
                .Index(t => t.Cart_Id);
            
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmprestimoLivro",
                c => new
                    {
                        EmprestimoId = c.Int(nullable: false),
                        LivroId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmprestimoId, t.LivroId })
                .ForeignKey("dbo.Emprestimo", t => t.EmprestimoId, cascadeDelete: true)
                .ForeignKey("dbo.Livro", t => t.LivroId, cascadeDelete: true)
                .Index(t => t.EmprestimoId)
                .Index(t => t.LivroId);
            
            CreateTable(
                "dbo.Emprestimo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataEmprestimo = c.DateTime(nullable: false),
                        DataDevolucao = c.DateTime(nullable: false),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UsuarioId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nome = c.String(),
                        Sobrenome = c.String(),
                        DataNascimento = c.DateTime(nullable: false),
                        TipoUsuario = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Endereco_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Endereco", t => t.Endereco_ID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Endereco_ID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Logradouro = c.String(),
                        Numero = c.String(),
                        Complemento = c.String(),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        Estado = c.String(),
                        CEP = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Livro", "Cart_Id", "dbo.Cart");
            DropForeignKey("dbo.EmprestimoLivro", "LivroId", "dbo.Livro");
            DropForeignKey("dbo.EmprestimoLivro", "EmprestimoId", "dbo.Emprestimo");
            DropForeignKey("dbo.Emprestimo", "UsuarioId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Endereco_ID", "dbo.Endereco");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cart", "Usuario_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Livro", "CategoriaId", "dbo.Categoria");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Endereco_ID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Emprestimo", new[] { "UsuarioId" });
            DropIndex("dbo.EmprestimoLivro", new[] { "LivroId" });
            DropIndex("dbo.EmprestimoLivro", new[] { "EmprestimoId" });
            DropIndex("dbo.Livro", new[] { "Cart_Id" });
            DropIndex("dbo.Livro", new[] { "CategoriaId" });
            DropIndex("dbo.Cart", new[] { "Usuario_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Endereco");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Emprestimo");
            DropTable("dbo.EmprestimoLivro");
            DropTable("dbo.Categoria");
            DropTable("dbo.Livro");
            DropTable("dbo.Cart");
        }
    }
}
