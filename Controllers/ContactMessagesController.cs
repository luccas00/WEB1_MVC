using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LuccasCorp.Models;

namespace LuccasCorp.Controllers
{
    public class ContactMessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContactMessages
        public ActionResult Index()
        {
            return View();
        }


        // GET: ContactMessages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactMessages/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Message,DateSent")] ContactMessage contactMessage)
        {
            if (ModelState.IsValid)
            {
                contactMessage.DateSent = DateTime.Now;
                db.ContactMessages.Add(contactMessage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactMessage);
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
