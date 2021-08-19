using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Infraestructure.Interfaces;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Infraestructure.Repository
{
    public class RepositoryProducto : IRepositoryProducto
    {
        public IEnumerable<Producto> GetProductos()
        {

            IEnumerable<Producto> productos = null;

            using (MyContextNayeApp ctx = new MyContextNayeApp())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                productos = ctx.Producto.Include(x => x.Categoria).
                                         Include(x => x.Proveedor).
                                         Include(x => x.Bodega).ToList();
            }
            return productos;
        }

        public IEnumerable<Producto> GetProductoByName(string name)
        {
            IEnumerable<Producto> productos = null;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    productos = ctx.Producto.Where(x => x.Nombre == name).
                                            Include(x => x.Categoria).
                                            Include(x => x.Bodega).
                                            Include(x => x.Proveedor).ToList();
          
                }
                return productos;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        

        public Producto GetProductoById(int id)
        {
            Producto producto = null;

            using (MyContextNayeApp ctx = new MyContextNayeApp())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                producto = ctx.Producto.Where(x => x.Id == id).
                                        Include(x => x.Categoria).
                                        Include(x => x.Bodega).
                                        Include(x => x.Proveedor).
                                        SingleOrDefault();
            }

            return producto;
        }

        public IEnumerable<Producto> GetBodegaProductoById(int id)
        {
            IEnumerable<Producto>  productos = null;

            using (MyContextNayeApp ctx = new MyContextNayeApp())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                productos = ctx.Producto.Where(x => x.Id_Bodega == id).
                                        Include(x => x.Categoria).
                                        Include(x => x.Bodega).
                                        Include(x => x.Proveedor).ToList();
            }

            return productos;
        }

        public Producto GetCategoriaProductoById(int id)
        {
            Producto producto = null;

            using (MyContextNayeApp ctx = new MyContextNayeApp())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                producto = ctx.Producto.Where(x => x.Id_Categoria == id).
                                        Include(x => x.Categoria).
                                        Include(x => x.Bodega).
                                        Include(x => x.Proveedor).
                                        SingleOrDefault();
            }

            return producto;
        }

        public IEnumerable<Producto> GetCategoriaProductosById(int id)
        {

            IEnumerable<Producto> productos = null;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    productos = ctx.Producto.Where(x => x.Id_Categoria == id).
                                            Include(x => x.Categoria).
                                            Include(x => x.Bodega).
                                            Include(x => x.Proveedor).ToList();

                }
                return productos;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }

        }

   


        public void DeleteProducto(int id)
        {
            int retorno;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    ctx.Database.ExecuteSqlCommand("DELETE FROM ProveedorProducto WHERE Id_Producto = @id", new SqlParameter("@id", id));

                    Producto producto = new Producto()
                    {
                        Id = id
                    };
                    
                    ctx.Entry(producto).State = EntityState.Deleted;
                    
                    retorno = ctx.SaveChanges();

                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }



        public Producto Save(Producto producto,string[] selectedProveedor)
        {
            int retorno = 0;
            Producto oProducto;

            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProducto = GetProductoById((int)producto.Id);
                    IRepositoryProveedor _RepositoryProveedor = new RepositoryProveedor();

                    if (oProducto == null)
                    {
                        producto.Creado = DateTime.Now;
                        //ctx.Producto.Add(producto);
                        if (selectedProveedor != null)
                        {
                            producto.Proveedor = new List<Proveedor>();
                            foreach (var proveedor in selectedProveedor)
                            {
                                var proveedorToAdd = _RepositoryProveedor.GetProveedorById(int.Parse(proveedor));
                                ctx.Proveedor.Attach(proveedorToAdd);
                                producto.Proveedor.Add(proveedorToAdd);
                            }
                        }

                        ctx.Producto.Add(producto);
                        retorno = ctx.SaveChanges();
                    }
                    else
                    {


                        producto.Creado = oProducto.Creado;
                        ctx.Entry(producto).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();

                        var selectedProveedorID = new HashSet<string>(selectedProveedor);
                        if (selectedProveedor != null)
                        {
                            ctx.Entry(producto).Collection(x => x.Proveedor).Load();
                            var newProveedorForProducto = ctx.Proveedor.Where(x => selectedProveedorID.Contains(x.Id.ToString())).ToList();
                            producto.Proveedor = newProveedorForProducto;
                            ctx.Entry(producto).State = EntityState.Modified;
                            retorno = ctx.SaveChanges();
                        }
                    }
                    

                }

                if (retorno >= 0)
                    oProducto = GetProductoById(producto.Id);

                return oProducto;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }

        }


    }
}
