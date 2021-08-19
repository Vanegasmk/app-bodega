using Infraestructure.Interfaces;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryTipoMovimiento : IRepositoryTipoMovimiento
    {
        public IEnumerable<TipoMovimiento> GetTipoMovimientos()
        {
            IEnumerable<TipoMovimiento> tipoMovimientos = null;

            using (MyContextNayeApp ctx = new MyContextNayeApp())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                tipoMovimientos = ctx.TipoMovimiento.ToList();

            }
            return tipoMovimientos;
        }

        public IEnumerable<TipoMovimiento> GetTipoMovimientoByName(string name)
        {
            IEnumerable<TipoMovimiento> tipoMovimientos = null;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    tipoMovimientos = ctx.TipoMovimiento.Where(x => x.Nombre == name).
                                                         ToList();

                }
                return tipoMovimientos;
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
        public TipoMovimiento GetTipoMovimientoById(int id)
        {
            TipoMovimiento tipoMovimiento = null;

            using (MyContextNayeApp ctx = new MyContextNayeApp())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                tipoMovimiento = ctx.TipoMovimiento.Where(x => x.Id == id).
                                                    SingleOrDefault();
            }
            return tipoMovimiento;
        }
        public void DeleteTipoMovimiento(int id) 
        {
            int retorno;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    TipoMovimiento tipoMovimiento = new TipoMovimiento
                    {
                        Id = id
                    };
                    ctx.Entry(tipoMovimiento).State = EntityState.Deleted;
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
        public TipoMovimiento Save(TipoMovimiento tipoMovimiento)
        {
            int retorno = 0;
            TipoMovimiento oTipoMovimiento;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oTipoMovimiento = GetTipoMovimientoById(tipoMovimiento.Id);

                    if(oTipoMovimiento == null)
                    {
                        ctx.TipoMovimiento.Add(tipoMovimiento);
                    }
                    else
                    {
                        ctx.Entry(tipoMovimiento).State = EntityState.Modified;
                    }

                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oTipoMovimiento = GetTipoMovimientoById(tipoMovimiento.Id);
                return oTipoMovimiento;
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
