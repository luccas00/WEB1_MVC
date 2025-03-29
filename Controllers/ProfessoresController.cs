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
            ViewBag.Tipo = await _context.GetTipoAsync(userId);

            return View(_context.Professores.ToList());
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
