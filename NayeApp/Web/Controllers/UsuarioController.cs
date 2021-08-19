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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<Usuario> usuarios = _ServiceUsuario.GetUsuarios();
            return View(usuarios);
        }

        
        public ActionResult Details(int? id)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario usuario;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                usuario = _ServiceUsuario.GetUsuarioById(id.Value);

                if (usuario == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

       
        public ActionResult Edit(int? id)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario usuario;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                usuario = _ServiceUsuario.GetUsuarioById(id.Value);

                if (usuario == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        public ActionResult Delete(int? id)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario usuario;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                usuario = _ServiceUsuario.GetUsuarioById(id.Value);

                if (usuario == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(usuario);
        }


        [HttpPost]
        public ActionResult UpdateUsuario(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            try
            {
                _ServiceUsuario.EditarRolUsuario(usuario.Id);
                ViewBag.NotificationMessage = SweetAlerts.Mensaje("Success", "Usuario editado", SweetAlerts.SweetAlertMessageType.success);
            }
            catch
            {

                Utils.Utils.ValidateErrors(this);
                return View("Delete", _ServiceUsuario.GetUsuarioById(usuario.Id));
            }

            return RedirectToAction("Index");
        }


        public ActionResult Suspender(int? id)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario usuario;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                usuario = _ServiceUsuario.GetUsuarioById(id.Value);

                if (usuario == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        public ActionResult SuspenderUsuario(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            try
            {
                _ServiceUsuario.SuspenderRolUsuario(usuario.Id);
                ViewBag.NotificationMessage = SweetAlerts.Mensaje("Success", "Usuario editado", SweetAlerts.SweetAlertMessageType.success);
            }
            catch
            {

                Utils.Utils.ValidateErrors(this);
                return View("Delete", _ServiceUsuario.GetUsuarioById(usuario.Id));
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult DeleteUsuario(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario;

            try
            {
                oUsuario = _ServiceUsuario.GetUsuarioById(usuario.Id);
                if (oUsuario.Movimientos.Count() == 0)
                {
                    _ServiceUsuario.DeleteUsuario(usuario.Id);
                }
                else
                {
                    return ViewBag.NotificationMessage = SweetAlerts.Mensaje("Error", "No puede al usuario debido a que tiene movimientos enlazados", SweetAlerts.SweetAlertMessageType.error);
                }
            }
            catch
            {

                Utils.Utils.ValidateErrors(this);
                return View("Delete", _ServiceUsuario.GetUsuarioById(usuario.Id));
            }

            return RedirectToAction("Index");
        }
    }
}
