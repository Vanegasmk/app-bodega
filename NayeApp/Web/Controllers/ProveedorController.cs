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
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {

            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            IEnumerable<Proveedor> proveedores = _ServiceProveedor.GetProveedores();

            return View(proveedores);
        }

        public ActionResult Details(int? id)
        {
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            Proveedor proveedor;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                proveedor = _ServiceProveedor.GetProveedorById(id.Value);

                if (proveedor == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(proveedor);
        }

        public ActionResult Create()
        {
            ViewBag.IdContacto = Contactos(null);
            return View();
        }




        private MultiSelectList Contactos(ICollection<Contacto> contactos)
        {
            IServiceContacto _ServiceContacto = new ServiceContacto();
            IEnumerable<Contacto> listaContactos = _ServiceContacto.GetContactos();

            int[] listaContactosSelect = null;

            if (contactos != null)
            {
                listaContactosSelect = contactos.Select(x => x.Id).ToArray();
            }

            return new MultiSelectList(listaContactos,"Id","Nombre",listaContactosSelect);
        }




        [HttpPost]
        public ActionResult Save(Proveedor proveedor,string[] selectedContactos)
        {
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();

            try
            {
                Proveedor oProveedor = _ServiceProveedor.Save(proveedor,selectedContactos);
                return RedirectToAction("Index");
            }
            catch 
            {
                ViewBag.IdContacto = Contactos(proveedor.Contacto);
                Utils.Utils.ValidateErrors(this);
                return View("Create", proveedor);
            }
        }

        public ActionResult Edit(int? id)
        {
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            Proveedor proveedor;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                proveedor = _ServiceProveedor.GetProveedorById(id.Value);

                if (proveedor == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            ViewBag.IdContacto = Contactos(proveedor.Contacto);
            return View(proveedor);
        }

        public ActionResult Delete(int? id)
        {
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            Proveedor proveedor;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                proveedor = _ServiceProveedor.GetProveedorById(id.Value);

                if (proveedor == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(proveedor);
        }

        [HttpPost]
        public ActionResult DeleteProveedor(Proveedor proveedor)
        {
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            try
            {
                bool oProveedor = _ServiceProveedor.GetProveedorProductoById(proveedor.Id);

                if (oProveedor)
                {
                    return ViewBag.NotificationMessage = SweetAlerts.Mensaje("Error", "No puede eliminar un proveedor con productos ", SweetAlerts.SweetAlertMessageType.error);
                }
                else
                {
                    _ServiceProveedor.DeleteProveedor(proveedor.Id);
                }

            }
            catch 
            {

                Utils.Utils.ValidateErrors(this);
                return View("Delete", _ServiceProveedor.GetProveedorById(proveedor.Id));
            }
            return RedirectToAction("Index");
        }


    }
}