using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AITP3.DAL;
using AITP3.Models;
using Microsoft.AspNet.Identity;

namespace AITP3
{
    [Authorize]
    public class EmprestimoController : Controller
    {
        private BibliotecaContext db = new BibliotecaContext();
        public ActionResult Index()
        {
            // Obtém o ID do usuário logado
            var userId = User.Identity.GetUserId();

            // Filtra os empréstimos pelo ID do usuário logado
            var emprestimos = db.Emprestimos
                .Include(e => e.EmprestimoLivros.Select(el => el.Livro)) // Inclui detalhes dos livros
                .Where(e => e.UsuarioId == userId) // Garante que apenas empréstimos do usuário logado sejam buscados
                .ToList();

            return View(emprestimos);
        }
        // GET: Emprestimo/Details/5
        public ActionResult Details(int? id)
{
    if (id == null)
    {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    }

    string userId = User.Identity.GetUserId();
    Emprestimo emprestimo = db.Emprestimos
        .Include(e => e.EmprestimoLivros.Select(el => el.Livro)) // Include book details
        .SingleOrDefault(e => e.Id == id && e.UsuarioId == userId); // Check if the loan belongs to the logged-in user

    if (emprestimo == null)
    {
        return HttpNotFound();
    }
    return View(emprestimo);
}
        public ActionResult Create()
        {
            ViewBag.Livros = new MultiSelectList(db.Livros, "Id", "Titulo");
            ViewBag.Usuarios = new SelectList(db.Users, "Id", "Nome");
            return View();
        }

        public ActionResult CreateEmprestimo(string usuarioId, int[] selectedLivros)
        {
            if (string.IsNullOrEmpty(usuarioId))
            {
                ModelState.AddModelError("", "ID do usuário não encontrado.");
                return RedirectToAction("Index", "Cart");
            }

            var emprestimo = new Emprestimo
            {
                DataEmprestimo = DateTime.Now,
                DataDevolucao = DateTime.Now.AddDays(7),
                UsuarioId = usuarioId
            };

            if (selectedLivros != null && selectedLivros.Length > 0)
            {
                ViewBag.Livros = db.Livros.Where(l => selectedLivros.Contains(l.Id))
                    .Select(l => new SelectListItem
                    {
                        Value = l.Id.ToString(),
                        Text = l.Titulo
                    }).ToList();
            }
            else
            {
                ViewBag.Livros = new List<SelectListItem>();
            }

            ViewBag.UsuarioId = usuarioId;
            return View("Create", emprestimo);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DataEmprestimo,DataDevolucao,ValorTotal")] Emprestimo emprestimo, int[] selectedLivros)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Define o UsuarioId do usuário logado
                    var usuarioId = User.Identity.GetUserId();
                    emprestimo.UsuarioId = usuarioId;

                    if (selectedLivros != null)
                    {
                        emprestimo.EmprestimoLivros = new List<EmprestimoLivro>();
                        foreach (var livroId in selectedLivros)
                        {
                            var livro = db.Livros.Find(livroId);
                            if (livro != null)
                            {
                                emprestimo.EmprestimoLivros.Add(new EmprestimoLivro
                                {
                                    Emprestimo = emprestimo,
                                    Livro = livro
                                });
                                emprestimo.ValorTotal += livro.Valor;
                            }
                        }
                    }

                    db.Emprestimos.Add(emprestimo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Não foi possível salvar as mudanças. Tente novamente.");
            }

            ViewBag.Livros = new MultiSelectList(db.Livros, "Id", "Titulo", selectedLivros);
            return View(emprestimo);
        }




        // GET: Emprestimo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            Emprestimo emprestimo = db.Emprestimos.SingleOrDefault(e => e.Id == id && e.UsuarioId == userId);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimo/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DataEmprestimo,DataDevolucao,ValorTotal")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emprestimo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emprestimo);
        }

        // GET: Emprestimo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            db.Emprestimos.Remove(emprestimo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
