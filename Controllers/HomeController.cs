using LuccasCorpVX.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LuccasCorpVX.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;
            ViewBag.Tipo = await _context.GetTipoAsync(userId);

            return View();
        }

        public async Task<ActionResult> About()
        {
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;
            ViewBag.Tipo = await _context.GetTipoAsync(userId);
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public async Task<ActionResult> Learn()
        {
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;
            ViewBag.Tipo = await _context.GetTipoAsync(userId);
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> Contact()
        {
            var userId = User.Identity.GetUserId();
            var fullName = await ApplicationDbContext.GetFullNameAsync(userId);

            ViewBag.FullName = fullName;
            ViewBag.Tipo = await _context.GetTipoAsync(userId);
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
