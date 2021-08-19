using Infraestructure.Interfaces;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryProveedor : IRepositoryProveedor
    {
        public IEnumerable<Proveedor> GetProveedores()
        {
            IEnumerable<Proveedor> proveedores = null;

            using (MyContextNayeApp ctx = new MyContextNayeApp())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                proveedores = ctx.Proveedor.ToList();
            }
            return proveedores;

        }

        public IEnumerable<Proveedor> GetProveedoresByName(string name)
        {
            IEnumerable<Proveedor> proveedor = null;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    proveedor = ctx.Proveedor.Where(x => x.Nombre == name).
                                                    Include(x => x.Producto).
                                                    ToList();
                }
                return proveedor;
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



        public Proveedor GetProveedorById(int id)
        {
            Proveedor proveedor = null;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    proveedor = ctx.Proveedor.Where(x => x.Id == id).
                                              Include(x => x.Producto).
                                              Include(x => x.Contacto).
                                              FirstOrDefault();
                }
                return proveedor;
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





        



        public void DeleteProveedor(int id)
        {
            int retorno;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    ctx.Database.ExecuteSqlCommand("DELETE FROM ContactoProveedor WHERE Id_Proveedor = @id", new SqlParameter("@id", id));

                    Proveedor proveedor = new Proveedor()
                    {
                        Id = id
                    };
                    ctx.Entry(proveedor).State = EntityState.Deleted;
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
        public Proveedor Save(Proveedor proveedor, string[] selectedContacto)
        {
            int retorno = 0;
            Proveedor oProveedor;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProveedor = GetProveedorById(proveedor.Id);
                    IRepositoryContacto _RepositoryContacto = new RepositoryContacto();

                    if (oProveedor == null)
                    {

                        if (selectedContacto != null)
                        {
                            proveedor.Contacto = new List<Contacto>();

                            foreach (var contacto in selectedContacto)
                            {
                                var contactoToAdd = _RepositoryContacto.GetContactoById(int.Parse(contacto));
                                ctx.Contacto.Attach(contactoToAdd);
                                proveedor.Contacto.Add(contactoToAdd);
                            }

                        }

                        ctx.Proveedor.Add(proveedor);
                        retorno = ctx.SaveChanges();

                    }
                    else
                    {
                        ctx.Entry(proveedor).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                        
                        var selectedContactoID = new HashSet<string>(selectedContacto);
                        if (selectedContacto != null)
                        {
                            ctx.Entry(proveedor).Collection(x => x.Contacto).Load();
                            var newContactosForProveedor = ctx.Contacto.Where(x => selectedContactoID.Contains(x.Id.ToString())).ToList();
                            proveedor.Contacto = newContactosForProveedor;
                            ctx.Entry(proveedor).State = EntityState.Modified;
                            retorno = ctx.SaveChanges();
                        }
                    }
                    
                }

                if (retorno >= 0)
                    oProveedor = GetProveedorById(proveedor.Id);
                return oProveedor;
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

        public bool GetProveedorProductoById(int id)
        {
            Proveedor oProveedor;
            try
            {


                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oProveedor = GetProveedorById(id);

                    if (oProveedor.Producto.Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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


    }
}
