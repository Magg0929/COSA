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
    public class CostosController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Costos
        public ActionResult Index()
        {
            return View(db.Costos.ToList());
        }

        // GET: Costos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costos costos = db.Costos.Find(id);
            if (costos == null)
            {
                return HttpNotFound();
            }
            return View(costos);
        }

        // GET: Costos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Costos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Costo,Desde,Hasta")] Costos costos)
        {
            if (ModelState.IsValid)
            {
                db.Costos.Add(costos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(costos);
        }

        // GET: Costos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costos costos = db.Costos.Find(id);
            if (costos == null)
            {
                return HttpNotFound();
            }
            return View(costos);
        }

        // POST: Costos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Costo,Desde,Hasta")] Costos costos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(costos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(costos);
        }

        // GET: Costos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Costos costos = db.Costos.Find(id);
            if (costos == null)
            {
                return HttpNotFound();
            }
            return View(costos);
        }

        // POST: Costos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Costos costos = db.Costos.Find(id);
            db.Costos.Remove(costos);
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
