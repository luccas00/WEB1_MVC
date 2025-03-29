using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuccasCorpVX.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace LuccasCorpVX.Controllers
{
    [Authorize]
    public class ProfessoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfessoresController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Professores
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;

            return View(_context.Professores.ToList());
        }

        // GET: Professores/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professores professores = _context.Professores.Find(id);
            if (professores == null)
            {
                return HttpNotFound();
            }
            return View(professores);
        }

        // POST: Professores/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,FirstName,LastName,Foto,Campus,Departamento,CreatedOn,Ativo")] Professores professores)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(professores).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(professores);
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
