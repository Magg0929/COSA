using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using COSA.Models;

namespace COSA.Controllers
{
    public class TelefonosController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Telefonos
        public ActionResult Index()
        {
            var telefonos = db.Telefonos.Include(t => t.Persona);
            return View(telefonos.ToList());
        }

        // GET: Telefonos/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefonos telefonos = db.Telefonos.Find(id);
            if (telefonos == null)
            {
                return HttpNotFound();
            }
            return View(telefonos);
        }

        // GET: Telefonos/Create
        public ActionResult Create()
        {
            ViewBag.UsuarioId = new SelectList(db.Persona, "Id", "Nombres");
            return View();
        }

        // POST: Telefonos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Numero,UsuarioId,Saldo")] Telefonos telefonos)
        {
            if (ModelState.IsValid)
            {
                db.Telefonos.Add(telefonos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsuarioId = new SelectList(db.Persona, "Id", "Nombres", telefonos.UsuarioId);
            return View(telefonos);
        }

        // GET: Telefonos/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefonos telefonos = db.Telefonos.Find(id);
            if (telefonos == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuarioId = new SelectList(db.Persona, "Id", "Nombres", telefonos.UsuarioId);
            return View(telefonos);
        }

        // POST: Telefonos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Numero,UsuarioId,Saldo")] Telefonos telefonos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telefonos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsuarioId = new SelectList(db.Persona, "Id", "Nombres", telefonos.UsuarioId);
            return View(telefonos);
        }

        // GET: Telefonos/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefonos telefonos = db.Telefonos.Find(id);
            if (telefonos == null)
            {
                return HttpNotFound();
            }
            return View(telefonos);
        }

        // POST: Telefonos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            //Telefonos telefonos = db.Telefonos.Find(id);
            //db.Telefonos.Remove(telefonos);
            //db.SaveChanges();
            Telefonos telefonos = db.Telefonos.Find(id);
            var id_P = telefonos.UsuarioId;
            Persona persona = db.Persona.Find(id_P);
            int count = db.Telefonos.Where(x => x.UsuarioId == persona.Id).Count();
            if (count > 1)
            {
                db.Telefonos.Remove(telefonos);
                db.SaveChanges();
            }
            else
            {
                db.Telefonos.Remove(telefonos);
                db.Persona.Remove(persona);
                db.SaveChanges();
            }
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
