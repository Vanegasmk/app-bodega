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
    public class RepositoryContacto : IRepositoryContacto
    {
        public IEnumerable<Contacto> GetContactos()
        {
            IEnumerable<Contacto> contactos;

            using (MyContextNayeApp ctx = new MyContextNayeApp())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                contactos = ctx.Contacto.ToList();
            }
            return contactos;
        }


        public Contacto GetContactoById(int id)
        {
            Contacto contacto = null;

            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    contacto = ctx.Contacto.Where(x => x.Id == id).
                                            Include(x => x.Proveedor).FirstOrDefault();
                }

                return contacto;
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



        public void DeleteContacto(int id)
        {
            using (MyContextNayeApp ctx = new MyContextNayeApp())
            {
                int retorno;
                try
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    Contacto contacto = new Contacto()
                    {
                        Id = id
                    };

                    ctx.Entry(contacto).State = EntityState.Deleted;
                    retorno = ctx.SaveChanges();
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

        public Contacto Save(Contacto contacto)
        {
            int retorno = 0;
            Contacto oContacto;

            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oContacto = GetContactoById(contacto.Id);

                    if (oContacto == null)
                    {
                        ctx.Contacto.Add(contacto);
                    }
                    else
                    {
                        ctx.Entry(contacto).State = EntityState.Modified;
                    }

                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oContacto = GetContactoById(contacto.Id);
                return oContacto;
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
