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
    public class DisciplinasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisciplinasController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Professores
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;

            return View(_context.Disciplinas.ToList());
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
