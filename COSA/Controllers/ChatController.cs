using COSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COSA.Controllers
{

    public class MensajeController : Controller
    {

        private Database1Entities db = new Database1Entities();

        // GET: Mensaje
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.Session["CedulaPersona"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<SelectList> lst = new List<SelectList>();
            var id = System.Web.HttpContext.Current.Session["CedulaPersona"].ToString();
            var cedula = int.Parse(id);
            var per = (from persona in db.Persona
                       where persona.Cedula == cedula
                       select persona).FirstOrDefault();

            var tel = (from telefono in db.Telefonos
                       where telefono.UsuarioId == per.Id
                       select telefono).ToList<Telefonos>();
            SelectList TelSelectList = new SelectList(tel, "Numero", "Numero");

            ViewBag.Origen = TelSelectList;




            var tel2 = (from telefono in db.Telefonos
                        where telefono.UsuarioId != per.Id
                        select telefono).ToList<Telefonos>();
            SelectList TelSelectList2 = new SelectList(tel2, "Numero", "Numero");

            ViewBag.Origen2 = TelSelectList2;


            ViewBag.msg = System.Web.HttpContext.Current.Session["msgD"];
            return View();
        }

        [HttpPost]
        public ActionResult EnvioMensaje(int origen, int destino, string mensaje)
        {



            var msg = (from op in db.Chat
                       where (op.Emisor == destino && op.Receptor == origen) || (op.Receptor == destino && op.Emisor == origen)
                       select op).FirstOrDefault();


            if (msg == null)
            {

                Chat sdf = new Chat();
                //Mensaje sdf = new Mensaje();
                sdf.Emisor = origen;
                sdf.Receptor = destino;

                db.Chat.Add(sdf);
                db.SaveChanges();
                var id = db.Chat.Max(e => e.Id);
                Session["msg"] = mensaje;
                Session["msgId"] = msg.Id;
                Session["origen"] = origen;
                return RedirectToAction("DetalleM");
            }
            else
            {

                Session["msg"] = mensaje;
                Session["msgId"] = msg.Id;
                Session["origen"] = origen;
                return RedirectToAction("DetalleM");


            }

        }

        public ActionResult DetalleM()
        {



            var mensajeo = System.Web.HttpContext.Current.Session["msgId"].ToString();
            var idMSg = int.Parse(mensajeo);

            var msg = (from op in db.Mensaje
                       where (op.ChatId == idMSg)
                       select op).ToList<Mensaje>();

            if (msg != null)
            {
                var id = System.Web.HttpContext.Current.Session["CedulaPersona"].ToString();
                var cedula = int.Parse(id);
                var per = (from persona in db.Persona
                           where persona.Cedula == cedula
                           select persona).FirstOrDefault();

                Mensaje Dm = new Mensaje();
                Dm.ChatId = idMSg;
                Dm.Mensaje1 = Session["msg"].ToString();
                Dm.CostoId = 1;
                Dm.EmisorId = int.Parse(System.Web.HttpContext.Current.Session["origen"].ToString());
                db.Mensaje.Add(Dm);

                db.SaveChanges();


                System.Web.HttpContext.Current.Session.Add("msgD", msg);
                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");

        }
        public ActionResult Actualizacion()
        {



            var msg = (from op in db.Mensaje

                       select op).ToList<Mensaje>();
            if (msg == null)
            {
                System.Web.HttpContext.Current.Session.Add("msgD", msg);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}