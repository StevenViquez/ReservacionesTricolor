using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservacionesTricolor.Controllers
{
    public class LoginController : Controller
    {
        private ReservacionesTricolorEntities db = new ReservacionesTricolorEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(Usuario usuario)
        {
            Usuario oUsuario = new Usuario();
            
                if (ModelState.IsValid)
                {
                    oUsuario = db.Usuario
                        .Include("UsuarioRol")
                        .Where(x => x.Email == usuario.Email && x.Contrasena == usuario.Contrasena)
                        .FirstOrDefault();

                    if (oUsuario != null)
                    {
                        Session["User"] = oUsuario;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Message"] = "Error al autenticarse";
                    }
                }

                return View("Index");
        }

        public ActionResult UnAuthorized()
        {
            
                ViewBag.Message = "Un Authorized Page!";

                if (Session["User"] != null)
                {
                    Usuario oUsuario = Session["User"] as Usuario;
                }

                return View();
            
        }


        public ActionResult Logout()
        {
                Session["User"] = null;
                return RedirectToAction("Index", "Login");
        }
    }
}