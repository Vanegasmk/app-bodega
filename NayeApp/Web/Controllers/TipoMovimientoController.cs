using Core.Interfaces;
using Core.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class TipoMovimientoController : Controller
    {
        // GET: TipoMovimiento
        public ActionResult Index()
        {
            IServiceTipoMovimiento _ServiceTipoMovimientos = new ServiceTipoMovimiento();
            IEnumerable<TipoMovimiento> _TipoMovimientos = _ServiceTipoMovimientos.GetTipoMovimientos();

            return View(_TipoMovimientos);
        }

        public ActionResult Details(int? id)
        {
            IServiceTipoMovimiento _ServiceTipoMovimiento = new ServiceTipoMovimiento();
            TipoMovimiento tipoMovimiento;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                tipoMovimiento = _ServiceTipoMovimiento.GetTipoMovimientoById(id.Value);

                if (tipoMovimiento == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(tipoMovimiento);
        }

        public ActionResult Create(TipoMovimiento tipoMovimiento)
        {
            ViewBag.Tipo = Tipo();
            return View();
        }

        [HttpPost]
        public ActionResult Save(TipoMovimiento tipoMovimiento)
        {
            IServiceTipoMovimiento _ServiceTipoMovimiento = new ServiceTipoMovimiento();

            try
            {
                TipoMovimiento oTipoMovimiento = _ServiceTipoMovimiento.Save(tipoMovimiento);
                return RedirectToAction("Index");
            }
            catch
            {
                Utils.Utils.ValidateErrors(this);
                return View("Create",tipoMovimiento);
            }

        }

        public ActionResult Edit(int? id)
        {
            IServiceTipoMovimiento _ServiceTipoMovimiento = new ServiceTipoMovimiento();
            TipoMovimiento tipoMovimiento;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                tipoMovimiento = _ServiceTipoMovimiento.GetTipoMovimientoById(id.Value);

                if(tipoMovimiento == null)
                {
                    return RedirectToAction("Index");
                }    
            }
            catch (Exception ex)
            {

                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            ViewBag.Tipo = Tipo(tipoMovimiento.Tipo);
            return View(tipoMovimiento);

        }

        public ActionResult Delete(int? id)
        {
            IServiceTipoMovimiento _ServiceTipoMovimiento = new ServiceTipoMovimiento();
            TipoMovimiento tipoMovimiento;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                tipoMovimiento = _ServiceTipoMovimiento.GetTipoMovimientoById(id.Value);

                if (tipoMovimiento == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(tipoMovimiento);

        }

        [HttpPost]

        public ActionResult DeleteTipoMovimiento(TipoMovimiento tipoMovimiento)
        {
            IServiceMovimientos _ServiceMovimientos = new ServiceMovimientos();
            IServiceTipoMovimiento _ServiceTipoMovimiento = new ServiceTipoMovimiento();
            Movimientos movimiento;
            try
            {
                movimiento = _ServiceMovimientos.GetTipoMovimientoById(tipoMovimiento.Id);
                if (movimiento == null)
                {
                    _ServiceTipoMovimiento.DeleteTipoMovimiento(tipoMovimiento.Id);
                }
                else
                {
                    return ViewBag.NotificationMessage = SweetAlerts.Mensaje("Error", "No puede eliminar un tipo de movimiento con movimientos", SweetAlerts.SweetAlertMessageType.error);
                }
            }
            catch
            {

                Utils.Utils.ValidateErrors(this);
                return View("Delete", _ServiceTipoMovimiento.GetTipoMovimientoById(tipoMovimiento.Id));
            }
            return RedirectToAction("Index");
        }

        private SelectList Tipo(string tipo = null)
        {

            List<Object> oTipo = new List<Object>();
            oTipo.Add(new SelectListItem() { Text = "Entrada", Value = "Entrada" });
            oTipo.Add(new SelectListItem() { Text = "Salida", Value = "Salida" });

            return new SelectList(oTipo,"Value","Text",tipo);

        }
    }
}