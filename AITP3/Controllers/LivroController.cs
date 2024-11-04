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

namespace AITP3
{
    public class LivroController : Controller
    {
        private BibliotecaContext db = new BibliotecaContext();

        // GET: Livro
        public ActionResult Index()
        {
            return View(db.Livros.ToList());
        }

        // GET: Livro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // GET: Livro/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome");
            return View();
        }

        // POST: Livro/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Autor,Editora,ISBN,AnoPublicacao,Valor,CategoriaId")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Livros.Add(livro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome", livro.CategoriaId);
            return View(livro);
        }

        // GET: Livro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }

            // Carregar as categorias para o DropDownList
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome", livro.CategoriaId);

            return View(livro);
        }

        // POST: Livro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Autor,CategoriaId,Editora,ISBN,AnoPublicacao,Valor")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Recarregar as categorias caso a validação falhe
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nome", livro.CategoriaId);

            return View(livro);
        }

        // GET: Livro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Livro livro = db.Livros.Find(id);
            db.Livros.Remove(livro);
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
