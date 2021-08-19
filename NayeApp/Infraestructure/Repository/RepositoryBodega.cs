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
    public class RepositoryBodega : IRepositoryBodega
    {
        public IEnumerable<Bodega> GetBodegas()
        {
            IEnumerable<Bodega> bodegas = null;
            using (MyContextNayeApp ctx = new MyContextNayeApp())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                bodegas = ctx.Bodega.ToList();
            }
            return bodegas;
        }

        public IEnumerable<Bodega> GetBodegaByName(string name)
        {
            IEnumerable<Bodega> bodegas = null;

            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    bodegas = ctx.Bodega.Where(x => x.Nombre == name).ToList();
                }
                return bodegas;
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

        public Bodega GetBodegaById(int id)
        {
            Bodega bodega = null;

            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    bodega = ctx.Bodega.Where(x => x.Id == id).FirstOrDefault();
                }
                return bodega;
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
        public void DeleteBodega(int id)
        {
            int retorno;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Bodega bodega = new Bodega()
                    {
                        Id = id
                    };
                    ctx.Entry(bodega).State = EntityState.Deleted;
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


        public Bodega Save(Bodega bodega)
        {
            int retorno = 0;
            Bodega oBodega;

            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oBodega = GetBodegaById(bodega.Id);

                    if (oBodega == null)
                    {
                        ctx.Bodega.Add(bodega);
                    }
                    else
                    {
                        ctx.Entry(bodega).State = EntityState.Modified;
                    }

                    retorno = ctx.SaveChanges();

                }

                if (retorno >= 0)
                    oBodega = GetBodegaById(bodega.Id);
                return oBodega;
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
