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
    [Authorize]
    public class ComentariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComentariosController()
        {
            _context = new ApplicationDbContext();
        }

        
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;

            List<Comentario> comentarios = new List<Comentario>();

            if (User.Identity.GetUserName() == "professor@ufop.edu.br")
            {

                comentarios = _context.Comentarios.ToList();
                return View(comentarios);

            } else {

                comentarios = _context.Comentarios.Where(c => c.Autor == User.Identity.Name).ToList();

            }

            return View(comentarios);
        }

        // GET: Comentarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;

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
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;

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
                Professores professor = _context.Professores.Find(id.ToString());
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
        public async Task<ActionResult> Create(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            _context.SaveChanges();

            if (comentario.DisciplinaNome != null)
            {
                Disciplinas disciplinas = _context.Disciplinas.FirstOrDefault(d => d.Codigo == comentario.Disciplina);
                if (disciplinas != null)
                {
                    disciplinas.TotalComentarios++;
                    disciplinas.Media = (disciplinas.Media + comentario.Positivo) / disciplinas.TotalComentarios;
                    disciplinas.AvaliacaoGeral = await _context.GetAvaliacaoGeralDisciplina(comentario.Disciplina);
                    _context.SaveChanges();
                }
            }
            else
            {
                Professores professores = _context.Professores.FirstOrDefault(p => p.Id == comentario.Professor.ToString());
                if (professores != null)
                {
                    professores.TotalComentarios++;
                    professores.Media = (professores.Media + comentario.Positivo) / professores.TotalComentarios;
                    professores.AvaliacaoGeral = await _context.GetAvaliacaoGeralProfessor(comentario.Professor);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index");

        }

        // GET: Comentarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {

            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;

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
