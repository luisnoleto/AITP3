using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AITP3.Models
{
    public enum TipoUsuario
    {
        Administrador,
        Usuario
    }

    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual ICollection<Emprestimo> Emprestimos { get; set; }
        public virtual Cart Carrinho { get; set; } 
        public TipoUsuario TipoUsuario { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
