using COSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace COSA.Controllers
{
    public class SesionController : Controller
    {

        private Database1Entities db = new Database1Entities();
        // GET: Sesion
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(int cedula, string contrasena )
        {
            var persona= (from d in db.Persona where d.Cedula == cedula && d.Contraseña == contrasena.Trim()
                          select d).FirstOrDefault(); ;

            if (persona == null) { 
         

                ViewBag.Error = "Los datos no Coinciden";
                return View("Index");
            }

            Session["Persona"] = persona;
            System.Web.HttpContext.Current.Session["CedulaPersona"] = persona.Cedula;
            if (persona.Admin == 1)
            {
                System.Web.HttpContext.Current.Session["Rol"] = persona.Admin;
                System.Web.HttpContext.Current.Session["Nombre"] = persona.Nombres;
                return RedirectToAction("Index", "Personas");
            }
            else
            {
                System.Web.HttpContext.Current.Session["Rol"] = persona.Admin;
                System.Web.HttpContext.Current.Session["Nombre"] = persona.Nombres;
                return RedirectToAction("Index", "Prueba");
            }
        }
        public ActionResult LogOut()
        {
            return View();
        }
    }
}