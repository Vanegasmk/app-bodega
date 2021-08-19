using Core.Interfaces;
using Core.Services;
using Infraestructure.Interfaces;
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
    public class MovimientoController : Controller
    {
        // GET: Movimiento
        public ActionResult Index()
        {
            IServiceMovimientos _ServiceMovimientos = new ServiceMovimientos();
            IEnumerable<Movimientos> movimientos = _ServiceMovimientos.GetMovimientos();

            return View(movimientos);
        }
        
        public ActionResult Details(int? id)
        {
            IServiceMovimientos _ServiceMovimientos = new ServiceMovimientos();
            Movimientos movimientos;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                movimientos = _ServiceMovimientos.GetMovimientosById(id.Value);

                if (movimientos == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(movimientos);
        }



        public ActionResult Edit(int? id)
        {
            IServiceMovimientos _ServiceMovimientos = new ServiceMovimientos();
            Movimientos movimientos;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                movimientos = _ServiceMovimientos.GetMovimientosById(id.Value);

                if (movimientos == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            ViewBag.Id_Producto = Productos(movimientos.Id_Producto);
            ViewBag.Id_TipoMovimiento = TipoMovimientos(movimientos.Id_TipoMovimiento);
            return View(movimientos);
        }



        public ActionResult Create()
        {
            ViewBag.Id_Producto = Productos();
            ViewBag.Id_TipoMovimiento = TipoMovimientos();
            return View();
        }

        

        public SelectList Productos(int id = 0)
        {
            IServiceProducto _ServiceProductos = new ServiceProducto();
            IEnumerable<Producto> productos = _ServiceProductos.GetProductos();
            return new SelectList(productos, "Id", "Nombre", id);
        }

        public SelectList TipoMovimientos(int id = 0)
        {
            IServiceTipoMovimiento _ServiceTipoMovimiento = new ServiceTipoMovimiento();
            IEnumerable<TipoMovimiento> tipoMovimientos = _ServiceTipoMovimiento.GetTipoMovimientos();
            return new SelectList(tipoMovimientos, "Id", "Nombre", id);
        }

       [HttpPost]
        public ActionResult Edit(Movimientos movimiento)
        {
            IServiceMovimientos _ServiceMovimientos = new ServiceMovimientos();

            try
            {
                _ServiceMovimientos.DeleteMovimiento(movimiento.Id);
                movimiento.Id = 0;
                Save(movimiento);
            }
            catch
            {

                Utils.Utils.ValidateErrors(this);
                ViewBag.Id_Producto = Productos();
                ViewBag.Id_TipoMovimiento = TipoMovimientos();
            }

            return RedirectToAction("Index");
        }
       



        [HttpPost]
        public ActionResult Save(Movimientos movimiento)
        {
            IServiceMovimientos _ServiceMovimientos = new ServiceMovimientos();
            IServiceProducto _ServiceProducto = new ServiceProducto();
            

            try
            {

                if(ModelState.IsValid)
                {
                    if (Session["User"] != null)
                    {
                        Producto oProducto = _ServiceProducto.GetProductoById(movimiento.Id_Producto);
                        Usuario oUsuario = Session["User"] as Usuario;

                        movimiento.Id_Usuario = oUsuario.Id;

                        if (oProducto.Unidades - movimiento.Cantidad < 0)
                        {
                           return  ViewBag.NotificationMessage = SweetAlerts.Mensaje("Error", "La cantidad no puede ser mayor a la cantidad del stock ", SweetAlerts.SweetAlertMessageType.error);
                            
                        }
                        else
                        {
                            Movimientos oMovimientos = _ServiceMovimientos.Save(movimiento);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {

                Utils.Utils.ValidateErrors(this);
                ViewBag.Id_Producto = Productos();
                ViewBag.Id_TipoMovimiento = TipoMovimientos();
                return View("Create", movimiento);
            }
        }

        public ActionResult Delete(int? id)
        {
            IServiceMovimientos _ServiceMovimientos = new ServiceMovimientos();
            _ServiceMovimientos.DeleteMovimiento(id.Value);
            return RedirectToAction("Index");
        }
    }
}