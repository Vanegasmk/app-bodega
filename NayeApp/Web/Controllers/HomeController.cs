using Core.Interfaces;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            IServiceMovimientos _ServiceMovimientos = new ServiceMovimientos();

            ViewBag.CantidadEntradas = _ServiceMovimientos.GetMovimientos().Where(x => (x.Creado.ToString("yyyy/MM/dd") == DateTime.Now.ToString("yyyy/MM/dd"))  && (x.TipoMovimiento.Tipo == "Entrada")).Count();
            ViewBag.CantidadSalidas = _ServiceMovimientos.GetMovimientos().Where(x => (x.Creado.ToString("yyyy/MM/dd") == DateTime.Now.ToString("yyyy/MM/dd")) && (x.TipoMovimiento.Tipo == "Salida")).Count();
            ViewBag.BajoStock = _ServiceProducto.GetProductos().Where(x => x.Unidades <= 5);
            ViewBag.CantidadProductos = _ServiceProducto.GetProductos().Count();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



    }
}