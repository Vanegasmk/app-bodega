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
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            IServiceCategoria _ServiceCategoria = new ServiceCategoria();
            IEnumerable<Producto> productos = _ServiceProducto.GetProductos();
            ViewBag.Categorias = _ServiceCategoria.GetCategorias();
            return View(productos);
        }

        public PartialViewResult CategoriaProducto(int? id)
        {
            IEnumerable<Producto> productos = null;
            IServiceProducto _ServiceProducto = new ServiceProducto();

            if (id != null)
            {
                productos = _ServiceProducto.GetCategoriaProductosById((int)id);
            }

            return PartialView("_PartialViewProducto",productos);

        }

        public PartialViewResult NombreProducto(string nombre)
        {
            IEnumerable<Producto> productos = null;
            IServiceProducto _ServiceProducto = new ServiceProducto();

            if (string.IsNullOrEmpty(nombre))
            {
                productos = _ServiceProducto.GetProductos();
            }
            else
            {
                productos = _ServiceProducto.GetProductoByName(nombre);
            }

            return PartialView("_PartialViewProducto", productos);
        }

        // GET: Producto/Details/5
        public ActionResult Details(int? id)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            Producto producto;
            try
            {
                if(id == null)
                {
                    return RedirectToAction("Index");
                }

                producto = _ServiceProducto.GetProductoById(id.Value);

                if(producto == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(producto);
        }


        


        
        public ActionResult Edit(int? id)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            Producto producto;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                producto = _ServiceProducto.GetProductoById(id.Value);

                if (producto == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            ViewBag.Id_Categoria = Categorias(producto.Id_Categoria);
            ViewBag.Id_Bodega = Bodegas(producto.Id_Bodega);
            ViewBag.Id_Proveedor = Proveedores(producto.Proveedor);
            return View(producto);
        }

        private SelectList Categorias(int idCategoria = 0)
        {
            IServiceCategoria _ServiceCategoria = new ServiceCategoria();
            IEnumerable<Categoria> categorias = _ServiceCategoria.GetCategorias();
            return new SelectList(categorias, "Id", "Nombre",idCategoria);
        }

        private SelectList Bodegas(int idBodega = 0)
        {
            IServiceBodega _ServiceBodega = new ServiceBodega();
            IEnumerable<Bodega> bodegas = _ServiceBodega.GetBodegas();
            return new SelectList(bodegas,"Id","Nombre",idBodega);
        }
        

        private MultiSelectList Proveedores(ICollection<Proveedor> proveedores)
        {
            IServiceProveedor _ServiceProveedor = new ServiceProveedor();
            IEnumerable<Proveedor> listaProveedores = _ServiceProveedor.GetProveedores();

            int[] listaProveedoresSelect = null;
            
            if(proveedores != null)
            {
                listaProveedoresSelect = proveedores.Select(x => x.Id).ToArray();
            }

            return new MultiSelectList(listaProveedores, "Id", "Nombre", listaProveedoresSelect);
        }

        public ActionResult Create()
        {
            ViewBag.Id_Categoria = Categorias();
            ViewBag.Id_Bodega = Bodegas();
            ViewBag.Id_Proveedor = Proveedores(null);
            return View();

        }

        [HttpPost]
        public ActionResult Save(Producto producto,string[] selectedProveedores)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            try
            {
                
                Producto oProducto = _ServiceProducto.Save(producto,selectedProveedores);
                
                return RedirectToAction("Index");
            }
            catch 
            {

                Utils.Utils.ValidateErrors(this);
                ViewBag.Id_Categoria = Categorias(producto.Id_Categoria);
                ViewBag.Id_Bodega = Bodegas(producto.Id_Bodega);
                ViewBag.Id_Proveedor = Proveedores(producto.Proveedor);
                return View("Create",producto);
            }
        
        }

        public ActionResult Delete(int? id)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            Producto producto;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                producto = _ServiceProducto.GetProductoById(id.Value);

                if (producto == null)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        [HttpPost]
        public ActionResult DeleteProducto(Producto producto)
        {
            IServiceProducto _ServiceProducto = new ServiceProducto();
            IServiceMovimientos _ServiceMovimientos = new ServiceMovimientos();
            Movimientos movimiento;

            try
            {
                movimiento = _ServiceMovimientos.GetProductoMovimientoById(producto.Id);
                if (movimiento == null)
                {
                    _ServiceProducto.DeleteProducto(producto.Id);
                }
                else
                {
                    return  ViewBag.NotificationMessage = SweetAlerts.Mensaje("Error", "No puede eliminar un producto con movimientos ", SweetAlerts.SweetAlertMessageType.error);
                }
            }
            catch 
            {

                Utils.Utils.ValidateErrors(this);
                return View("Delete", _ServiceProducto.GetProductoById(producto.Id));
            }

            return RedirectToAction("Index");
        }

        

    }
}
