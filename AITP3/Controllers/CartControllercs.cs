using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AITP3.DAL;
using AITP3.Models;
using Microsoft.AspNet.Identity;

namespace AITP3.Controllers
{
    public class CartController : Controller
    {
        private BibliotecaContext db = new BibliotecaContext();

        public ActionResult Index()
        {
            // Obtém o ID do usuário autenticado
            string usuarioId = User.Identity.GetUserId();
            if (usuarioId == null) return RedirectToAction("Login", "Account");

            // Recupera o carrinho do usuário
            var cart = db.Carts.Include(c => c.Items).SingleOrDefault(c => c.UsuarioId == usuarioId);
            if (cart == null)
            {
                // Cria um carrinho vazio para o usuário, caso ainda não tenha um
                cart = new Cart { UsuarioId = usuarioId };
                db.Carts.Add(cart);
                db.SaveChanges();
            }

            return View(cart);
        }

        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            // Obtém o ID do usuário autenticado
            string usuarioId = User.Identity.GetUserId();
            if (usuarioId == null) return RedirectToAction("Login", "Account");

            // Verifica se o livro existe
            var livro = db.Livros.Find(id);
            if (livro == null) return HttpNotFound();

            // Recupera o carrinho do usuário ou cria um novo se ele não existir
            var cart = db.Carts.Include(c => c.Items).SingleOrDefault(c => c.UsuarioId == usuarioId);
            if (cart == null)
            {
                cart = new Cart { UsuarioId = usuarioId };
                db.Carts.Add(cart);
            }

            // Adiciona o livro ao carrinho e salva as alterações
            cart.AddItem(livro);
            db.SaveChanges();

            return RedirectToAction("Index", "Acervo");
        }

        public ActionResult RemoveFromCart(int livroId)
        {
            // Obtém o ID do usuário autenticado
            string usuarioId = User.Identity.GetUserId();
            if (usuarioId == null) return RedirectToAction("Login", "Account");

            // Recupera o carrinho do usuário
            var cart = db.Carts.Include(c => c.Items).SingleOrDefault(c => c.UsuarioId == usuarioId);
            if (cart != null)
            {
                // Busca o item a ser removido
                var itemToRemove = cart.Items.FirstOrDefault(i => i.Id == livroId);
                if (itemToRemove != null)
                {
                    // Remove o item
                    cart.Items.Remove(itemToRemove);
                    db.SaveChanges(); // Salva as alterações no banco de dados
                }
            }

            return RedirectToAction("Index"); // Redireciona de volta para a página do carrinho
        }
    }

    }
