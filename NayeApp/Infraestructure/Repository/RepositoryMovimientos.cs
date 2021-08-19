using Infraestructure.Interfaces;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace Infraestructure.Repository
{
    public class RepositoryMovimientos : IRepositoryMovimientos 
    {

        
        public IEnumerable<Movimientos> GetMovimientos()
        {
            IEnumerable<Movimientos> movimientos = null;

            using (MyContextNayeApp ctx = new MyContextNayeApp())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                movimientos = ctx.Movimientos.Include(x => x.Producto).
                                              Include(x => x.TipoMovimiento).
                                              Include(x => x.Usuario).
                                              Include(x => x.Producto.Proveedor).ToList();


            }
            return movimientos;
        }


        public IEnumerable<Movimientos> GetMovimientosByName(string name)
        {
            IEnumerable<Movimientos> movimientos = null;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    movimientos = ctx.Movimientos.Where(x => x.Comentario == name).
                                                  Include(x => x.Producto).
                                                  Include(x => x.TipoMovimiento).
                                                  Include(x => x.Usuario).ToList();
                }
                return movimientos;
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


        public IEnumerable<Movimientos> GetMovimientosEntradaFechas(DateTime? fecha1, DateTime? fecha2)
        {
            IEnumerable<Movimientos> movimientos = null;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    if (fecha1 != null && fecha2 != null)
                    {
                        movimientos = ctx.Movimientos.Where(x => (x.Creado >= fecha1) || (x.Creado <= fecha2)).
                                                       Where(x => x.TipoMovimiento.Tipo == "Entrada").
                                                       Include(x => x.Producto).
                                                       Include(x => x.Producto.Proveedor).
                                                       Include(x => x.TipoMovimiento).
                                                       Include(x => x.Usuario).ToList();
                    }
                    else if(fecha1 != null && fecha2 == null)
                    {
                        movimientos = ctx.Movimientos.Where(x => (x.Creado >= fecha1)).
                                                       Where(x => x.TipoMovimiento.Tipo == "Entrada").
                                                       Include(x => x.Producto.Proveedor).
                                                       Include(x => x.Producto).
                                                       Include(x => x.TipoMovimiento).
                                                       Include(x => x.Usuario).ToList();
                    }
                }
                return movimientos;
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

        public IEnumerable<Movimientos> GetMovimientosSalidasFechas(DateTime? fecha1, DateTime? fecha2)
        {
            IEnumerable<Movimientos> movimientos = null;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    if (fecha2 != null)
                    {
                        movimientos = ctx.Movimientos.Where(x => (x.Creado >= fecha1) && (x.Creado <= fecha2)).
                                                        Where(x => x.TipoMovimiento.Tipo == "Salida").
                                                        Include(x => x.Producto).
                                                        Include(x => x.Producto.Proveedor).
                                                        Include(x => x.TipoMovimiento).
                                                        Include(x => x.Usuario).ToList();
                    }
                    else if (fecha1 != null && fecha2 == null)
                    {
                        movimientos = ctx.Movimientos.Where(x => (x.Creado >= fecha1)).
                                                        Where(x => x.TipoMovimiento.Tipo == "Salida").
                                                        Include(x => x.Producto).
                                                        Include(x => x.Producto.Proveedor).
                                                        Include(x => x.TipoMovimiento).
                                                        Include(x => x.Usuario).ToList();
                    }
                }
                return movimientos;
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

        public IList<Movimientos> GetMovimientosMayorSalida()
        {
            IList<Movimientos> movimientos;
            IList<Movimientos> movimientosFiltro = new List<Movimientos>(); 
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    movimientos = ctx.Movimientos.Where(x => x.TipoMovimiento.Tipo == "Salida").
                                                        Include(x => x.Producto).
                                                        Include(x => x.Producto.Proveedor).
                                                        Include(x => x.TipoMovimiento).
                                                        Include(x => x.Usuario).ToList();
                }

                foreach (var movimiento in movimientos)
                {
                    if (movimientosFiltro != null)
                    {
                        Movimientos productoFiltro = movimientosFiltro.Where(x => x.Id_Producto == movimiento.Id_Producto).SingleOrDefault();
                        
                        if (productoFiltro != null)
                        {
                            movimientosFiltro.Where(x => x.Id_Producto == movimiento.Id_Producto).Select(x => { x.Cantidad += movimiento.Cantidad; x.Id++; return x ; }).ToList();
                        }
                        else
                        {
                            Movimientos movimientoNuevo = GetMovimientosById(movimiento.Id);

                            movimientoNuevo.Id = 1;

                            movimientosFiltro.Add(movimientoNuevo);
                        }
                    }
                }
                return movimientosFiltro;
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

        public Movimientos GetMovimientosById(int id)
        {
            Movimientos movimiento = null;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    movimiento = ctx.Movimientos.Where(x => x.Id == id).
                                                  Include(x => x.Producto).
                                                  Include(x => x.TipoMovimiento).
                                                  Include(x => x.Usuario).
                                                  Include(x => x.Producto.Proveedor).FirstOrDefault();
                }
                return movimiento;
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

        public Movimientos GetProductoMovimientoById(int id)
        {
            Movimientos movimiento = null;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    movimiento = ctx.Movimientos.Where(x => x.Producto.Id == id).
                                                  Include(x => x.TipoMovimiento).
                                                  Include(x => x.Usuario).FirstOrDefault();
                }
                return movimiento;
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

        public Movimientos GetTipoMovimientoById(int id)
        {
            Movimientos movimiento = null;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    movimiento = ctx.Movimientos.Where(x => x.Id_TipoMovimiento == id).
                                                  Include(x => x.TipoMovimiento).
                                                  Include(x => x.Usuario).FirstOrDefault();
                }
                return movimiento;
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

        public void DeleteMovimiento(int id)
        {
            int retorno;
            
            IRepositoryTipoMovimiento repositoryTipoMovimiento = new RepositoryTipoMovimiento();
            IRepositoryProducto repositoryProducto = new RepositoryProducto();
            Movimientos oMovimiento;
            Producto oProducto;
            TipoMovimiento oTipoMovimiento;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oMovimiento = GetMovimientosById(id);
                    oProducto = repositoryProducto.GetProductoById(oMovimiento.Id_Producto);
                    oTipoMovimiento = repositoryTipoMovimiento.GetTipoMovimientoById(oMovimiento.Id_TipoMovimiento);

                    if (oTipoMovimiento.Tipo == "Salida")
                    {
                        oProducto.Unidades += oMovimiento.Cantidad;
                        ctx.Entry(oProducto).State = EntityState.Modified;
                    }
                    else if (oTipoMovimiento.Tipo == "Entrada")
                    {
                        oProducto.Unidades -= oMovimiento.Cantidad;
                        ctx.Entry(oProducto).State = EntityState.Modified;
                    }

                    Movimientos movimiento = new Movimientos()
                    {
                        Id = id
                    };


                    ctx.Entry(movimiento).State = EntityState.Deleted;
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

        
        public Movimientos Save(Movimientos movimiento)
        {
            int retorno = 0;
            IRepositoryProducto repositoryProducto = new RepositoryProducto();
            IRepositoryTipoMovimiento repositoryTipoMovimiento = new RepositoryTipoMovimiento();
            Movimientos oMovimiento;
            Producto oProducto;
            TipoMovimiento oTipoMovimiento;


            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oMovimiento = GetMovimientosById(movimiento.Id);
                    oProducto = repositoryProducto.GetProductoById(movimiento.Id_Producto);
                    oTipoMovimiento = repositoryTipoMovimiento.GetTipoMovimientoById(movimiento.Id_TipoMovimiento);
                    

                    if (oMovimiento == null)
                    {
                        if(oTipoMovimiento.Tipo == "Entrada")
                        {
                            oProducto.Unidades += movimiento.Cantidad;
                            ctx.Entry(oProducto).State = EntityState.Modified;
                        }else if(oTipoMovimiento.Tipo == "Salida")
                        {
                            oProducto.Unidades -= movimiento.Cantidad;
                            ctx.Entry(oProducto).State = EntityState.Modified;
                        }

                        movimiento.Total = movimiento.Cantidad * oProducto.Precio;
                        movimiento.Creado = DateTime.Now;

                        ctx.Movimientos.Add(movimiento);
                    }

                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oMovimiento = GetMovimientosById(movimiento.Id);
                return oMovimiento;
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
