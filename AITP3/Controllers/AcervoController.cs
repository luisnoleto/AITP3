using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AITP3.DAL;
using AITP3.Models;
using Microsoft.AspNet.Identity;

namespace AITP3.Controllers
{
    public class AcervoController : Controller
    {
        private BibliotecaContext db = new BibliotecaContext();

        // GET: Acervo
        public ActionResult Index()
        {
            var livros = db.Livros.ToList();
            return View(livros);
        }

        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            string usuarioId = User.Identity.GetUserId();
            if (usuarioId == null) return RedirectToAction("Login", "Account");

            var livro = db.Livros.Find(id);
            if (livro == null) return HttpNotFound();

            var user = db.Users.Include(u => u.Carrinho).SingleOrDefault(u => u.Id == usuarioId);
            if (user == null) return HttpNotFound("Usuário não encontrado");

            var cart = user.Carrinho ?? new Cart { UsuarioId = usuarioId, Usuario = user };
            if (user.Carrinho == null)
            {
                db.Carts.Add(cart);
                user.Carrinho = cart;
            }

            cart.AddItem(livro);
            db.SaveChanges();

            return RedirectToAction("Index", "Acervo");
        }
    }
}
