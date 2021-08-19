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
    public class ContactoController : Controller
    {
        // GET: Contacto
        public ActionResult Index()
        {
            IServiceContacto _ServiceContacto = new ServiceContacto();
            IEnumerable<Contacto> contactos = _ServiceContacto.GetContactos();
            return View(contactos);
        }

        // GET: Contacto/Details/5
        public ActionResult Details(int? id)
        {
            IServiceContacto _ServiceContacto = new ServiceContacto();
            Contacto contacto;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                contacto = _ServiceContacto.GetContactoById(id.Value);

                if (contacto == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(contacto);
        }





        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacto/Create
        [HttpPost]
        public ActionResult Save(Contacto contacto)
        {
            IServiceContacto _ServiceContacto = new ServiceContacto();
            try
            {
                Contacto oContacto = _ServiceContacto.Save(contacto);

                return RedirectToAction("Index");
            }
            catch
            {
                Utils.Utils.ValidateErrors(this);
                return View("Create", contacto);
            }
        }

        // GET: Contacto/Edit/5
        public ActionResult Edit(int? id)
        {
            IServiceContacto _ServiceContacto = new ServiceContacto();
            Contacto contacto;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                contacto = _ServiceContacto.GetContactoById(id.Value);

                if (contacto == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(contacto);
        }

       

        // GET: Contacto/Delete/5
        public ActionResult Delete(int? id)
        {
            IServiceContacto _ServiceContacto = new ServiceContacto();
            Contacto contacto;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                contacto = _ServiceContacto.GetContactoById(id.Value);

                if (contacto == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(contacto);
        }

        // POST: Contacto/Delete/5
        [HttpPost]
        public ActionResult DeleteContacto(Contacto contacto)
        {
            IServiceContacto _ServiceContacto = new ServiceContacto();
            Contacto oContacto;
            try
            {
                oContacto = _ServiceContacto.GetContactoById(contacto.Id);
                if (oContacto.Proveedor.Count() > 0)
                {
                    return ViewBag.NotificationMessage = SweetAlerts.Mensaje("Error", "No puede eliminar un contacto asignado a un proveedor ", SweetAlerts.SweetAlertMessageType.error);
                }
                else
                {
                    _ServiceContacto.DeleteContacto(contacto.Id);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                Utils.Utils.ValidateErrors(this);
                return View("Delete", _ServiceContacto.GetContactoById(contacto.Id));
            }
            
        }
    }
}
