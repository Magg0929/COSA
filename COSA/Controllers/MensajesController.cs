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
    public class MensajesController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Mensajes
        public ActionResult Index()
        {
            var mensaje = db.Mensaje.Include(m => m.Chat).Include(m => m.Costos).Include(m => m.Telefonos);
            return View(mensaje.ToList());
        }

        // GET: Mensajes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensaje mensaje = db.Mensaje.Find(id);
            if (mensaje == null)
            {
                return HttpNotFound();
            }
            return View(mensaje);
        }

        // GET: Mensajes/Create
        public ActionResult Create()
        {
            ViewBag.ChatId = new SelectList(db.Chat, "Id", "Id");
            ViewBag.CostoId = new SelectList(db.Costos, "Id", "Id");
            ViewBag.EmisorId = new SelectList(db.Telefonos, "Numero", "Numero");
            return View();
        }

        // POST: Mensajes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Mensaje1,CostoId,EmisorId,ChatId")] Mensaje mensaje)
        {
            if (ModelState.IsValid)
            {
                db.Mensaje.Add(mensaje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChatId = new SelectList(db.Chat, "Id", "Id", mensaje.ChatId);
            ViewBag.CostoId = new SelectList(db.Costos, "Id", "Id", mensaje.CostoId);
            ViewBag.EmisorId = new SelectList(db.Telefonos, "Numero", "Numero", mensaje.EmisorId);
            return View(mensaje);
        }

        // GET: Mensajes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensaje mensaje = db.Mensaje.Find(id);
            if (mensaje == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChatId = new SelectList(db.Chat, "Id", "Id", mensaje.ChatId);
            ViewBag.CostoId = new SelectList(db.Costos, "Id", "Id", mensaje.CostoId);
            ViewBag.EmisorId = new SelectList(db.Telefonos, "Numero", "Numero", mensaje.EmisorId);
            return View(mensaje);
        }

        // POST: Mensajes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Mensaje1,CostoId,EmisorId,ChatId")] Mensaje mensaje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mensaje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChatId = new SelectList(db.Chat, "Id", "Id", mensaje.ChatId);
            ViewBag.CostoId = new SelectList(db.Costos, "Id", "Id", mensaje.CostoId);
            ViewBag.EmisorId = new SelectList(db.Telefonos, "Numero", "Numero", mensaje.EmisorId);
            return View(mensaje);
        }

        // GET: Mensajes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensaje mensaje = db.Mensaje.Find(id);
            if (mensaje == null)
            {
                return HttpNotFound();
            }
            return View(mensaje);
        }

        // POST: Mensajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mensaje mensaje = db.Mensaje.Find(id);
            db.Mensaje.Remove(mensaje);
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
