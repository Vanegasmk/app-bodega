
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
    public class BodegaController : Controller
    {
        // GET: Bodega
        public ActionResult Index()
        {
            IServiceBodega _ServiceBodega = new ServiceBodega();
            IEnumerable<Bodega> bodegas = _ServiceBodega.GetBodegas();
            return View(bodegas);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(Bodega bodega)
        {
            IServiceBodega _ServiceBodega = new ServiceBodega();

            try
            {
                Bodega oBodega = _ServiceBodega.Save(bodega);
                return RedirectToAction("Index");
            }
            catch 
            {
                Utils.Utils.ValidateErrors(this);
                return View("Create",bodega);

            }
        }

        public ActionResult Edit(int? id)
        {
            IServiceBodega _ServiceBodega = new ServiceBodega();
            Bodega bodega;
            try
            {
                if(id == null)
                {
                    return RedirectToAction("Index");
                }

                bodega = _ServiceBodega.GetBodegaById(id.Value);
                
                if(bodega == null)
                {
                    return RedirectToAction("Index");
                }

            }
            catch(Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(bodega);

        }

        public ActionResult Details(int? id)
        {
            IServiceBodega _ServiceBodega = new ServiceBodega();
            Bodega bodega;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                bodega = _ServiceBodega.GetBodegaById(id.Value);

                if (bodega == null)
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(bodega);

        }

        public ActionResult Delete(int? id)
        {
            IServiceBodega _ServiceBodega = new ServiceBodega();
            Bodega bodega;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                bodega = _ServiceBodega.GetBodegaById(id.Value);

                if (bodega == null)
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(bodega);

        }

        [HttpPost]
        public ActionResult DeleteBodega(Bodega bodega)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            IServiceBodega _ServiceBodega = new ServiceBodega();
            IEnumerable<Producto> productos = null;

            try
            {
                productos = _ServiceProducto.GetBodegaProductoById(bodega.Id);
                
                if(productos.Count() >= 0)
                {
                    return ViewBag.NotificationMessage = SweetAlerts.Mensaje("Error", "No puede eliminar una bodega con productos ", SweetAlerts.SweetAlertMessageType.error);
                }
                else
                {
                    _ServiceBodega.DeleteBodega(bodega.Id);
                }
                return RedirectToAction("Index");
            }
            catch 
            {

                Utils.Utils.ValidateErrors(this);
                return View("Delete", _ServiceBodega.GetBodegaById(bodega.Id));
            }
        }

    }

}