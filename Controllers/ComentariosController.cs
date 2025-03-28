using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LuccasCorpVX.Models;
using Microsoft.AspNet.Identity;

namespace LuccasCorpVX.Controllers
{
    public class ComentariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComentariosController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;

            return View(_context.Comentarios.ToList());
        }

        // GET: Comentarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = _context.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // GET: Comentarios/Create
        public async Task<ActionResult> Create(int? id, string tipo)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string disciplinaString = string.Empty;
            string professorString = string.Empty;

            if (tipo.Equals("Disciplina"))
            {
                Disciplinas disciplina = _context.Disciplinas.Find(id);
                disciplinaString = disciplina.Nome;
                if (disciplina == null)
                {
                    return HttpNotFound();
                }
            }
            else
            {
                Professores professor = _context.Professores.Find(id);
                professorString = professor.FirstName + " " + professor.LastName;
                if (professor == null)
                {
                    return HttpNotFound();
                }
            }

            ViewBag.Id = id;
            ViewBag.Tipo = tipo;
            ViewBag.AutorName = await ApplicationDbContext.GetFullNameAsync(User.Identity.GetUserId());
            ViewBag.AutorID = User.Identity.GetUserId();
            ViewBag.AutorEmail = User.Identity.GetUserName();
            ViewBag.Disciplina = disciplinaString;
            ViewBag.Professor = professorString;

            Comentario comentario = new Comentario();
            comentario.CreatedOn = DateTime.Now;
            comentario.Ativo = true;
            comentario.Autor = User.Identity.GetUserName();

            return View("Create", comentario);
        }

        // POST: Comentarios/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Comentarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = _context.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentario comentario = _context.Comentarios.Find(id);
            _context.Comentarios.Remove(comentario);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
