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
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            IServiceCategoria _ServiceCategoria = new ServiceCategoria();
            IEnumerable<Categoria> categorias = _ServiceCategoria.GetCategorias();

            return View(categorias);
        }

        public ActionResult Details(int? id)
        {
            IServiceCategoria _ServiceCategoria = new ServiceCategoria();
            Categoria categoria;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                categoria = _ServiceCategoria.GetCategoriaById(id.Value);

                if (categoria == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(Categoria categoria)
        {
            IServiceCategoria _ServiceCategoria = new ServiceCategoria();

            try
            {
                Categoria oCategoria = _ServiceCategoria.Save(categoria);

                return RedirectToAction("Index");
            }
            catch
            {
                Utils.Utils.ValidateErrors(this);
                return View("Create", categoria);
            }

        }

        public ActionResult Edit(int? id)
        {
            IServiceCategoria _ServiceCategoria = new ServiceCategoria();
            Categoria categoria;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                categoria = _ServiceCategoria.GetCategoriaById(id.Value);

                if (categoria == null)
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        public ActionResult Delete(int? id)
        {
            IServiceCategoria _ServiceCategoria = new ServiceCategoria();
            Categoria categoria;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                categoria = _ServiceCategoria.GetCategoriaById(id.Value);

                if (categoria == null)
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        [HttpPost]
        public ActionResult DeleteCategoria(Categoria categoria)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            IServiceCategoria _ServiceCategoria = new ServiceCategoria();

            Producto producto;

            try
            {
                producto = _ServiceProducto.GetCategoriaProductoById(categoria.Id);

                if (producto == null)
                {
                    _ServiceCategoria.DeleteCategoria(categoria.Id);
                }
                else
                {
                    return ViewBag.NotificationMessage = SweetAlerts.Mensaje("Error", "No puede eliminar una categoria con productos ", SweetAlerts.SweetAlertMessageType.error);
                }
            }
            catch
            {

                Utils.Utils.ValidateErrors(this);
                return View("Delete", _ServiceCategoria.GetCategoriaById(categoria.Id));
            }
            return RedirectToAction("Index");
        }
    }
}