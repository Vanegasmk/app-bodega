using Core.Interfaces;
using Core.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login (Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario;
            try
            {
                if(ModelState.IsValid)
                {
                    oUsuario = _ServiceUsuario.GetUsuario(usuario.Correo, usuario.Clave);
                    if(oUsuario != null)
                    {
                        Session["User"] = oUsuario;
                        Log.Info($"Accede {oUsuario.Nombre} {oUsuario.Apellido} con el rol {oUsuario.Tipo}");
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        Log.Warn($"{usuario.Correo} se intento conectar y fallo");
                        ViewBag.NotificationMessage = SweetAlerts.Mensaje("Login","Error al iniciar sesión", SweetAlerts.SweetAlertMessageType.warning);
                    }
                }
                return View("Index");
            }
            catch(Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default","Error");
            }
        }
        public  ActionResult NoAutorizado()
        {
            try
            {
                ViewBag.Message = "No Autorizado";
                if (Session["User"] != null)
                {
                    Usuario oUsuario = Session["User"] as Usuario;
                    Log.Info($"Accede {oUsuario.Nombre} {oUsuario.Apellido} con el rol {oUsuario.Tipo}");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "Login";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
            return View();
        }
        public ActionResult Salir()
        {
            try
            {
                Log.Info("Usuario desconectado");
                Session["User"] = null;
                return RedirectToAction("Index", "Login");
            }
            catch(Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "Login";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default","Error");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();

            try
            {
                Usuario oUsuario = _ServiceUsuario.Save(usuario);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                Utils.Utils.ValidateErrors(this);
                return View("Create",usuario);
            }
        }
    }
}