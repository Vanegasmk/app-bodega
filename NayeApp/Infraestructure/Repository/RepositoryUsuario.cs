using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Infraestructure.Interfaces;

namespace Infraestructure.Repository
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        public IEnumerable<Usuario> GetUsuarios()
        {
            IEnumerable<Usuario> usuarios = null;

            using (MyContextNayeApp ctx = new MyContextNayeApp())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                usuarios = ctx.Usuario.ToList();
            }

            return usuarios;
        }

        public Usuario Save(Usuario usuario)
        {
            int retorno = 0;
            
            Usuario oUsuario = null;

            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oUsuario = GetUsuarioById(usuario.Id);
                    if(oUsuario == null)
                    {
                        ctx.Usuario.Add(usuario);
                    }else
                    {
                        ctx.Entry(usuario).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }

                if(retorno >= 0) 
                    oUsuario = GetUsuarioById(usuario.Id);
                

                return oUsuario;

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

        public Usuario GetUsuarioById(int id)
        {
            Usuario usuario = null;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    usuario = ctx.Usuario.Where(x => x.Id == id).
                                          Include(x => x.Movimientos).  
                                            FirstOrDefault();
                }

                return usuario;

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

        public Usuario EditarRolUsuario(int id)
        {
            Usuario usuario = null;
            int retorno = 0;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    usuario = ctx.Usuario.Where(x => x.Id == id).FirstOrDefault();

                    usuario.Tipo = 2;

                    ctx.Entry(usuario).State = EntityState.Modified;

                    retorno = ctx.SaveChanges();

                }

                if (retorno >= 0)
                    usuario = GetUsuarioById(id);
                return usuario;


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

        public Usuario SuspenderRolUsuario(int id)
        {
            Usuario usuario = null;
            int retorno = 0;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    usuario = ctx.Usuario.Where(x => x.Id == id).FirstOrDefault();

                    usuario.Tipo = 0;

                    ctx.Entry(usuario).State = EntityState.Modified;

                    retorno = ctx.SaveChanges();

                }

                if (retorno >= 0)
                    usuario = GetUsuarioById(id);
                return usuario;


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



        public Usuario GetUsuario(string correo,string clave)
        {
            Usuario usuario = null;

            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    usuario = ctx.Usuario.Where(x => x.Correo.Equals(correo) && x.Clave == clave).
                                                              FirstOrDefault<Usuario>();
                }

                return usuario;
            }
            catch(DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch(Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public void DeleteUsuario(int id)
        {
            int retorno;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Usuario usuario = new Usuario()
                    {
                        Id = id
                    };
                    ctx.Entry(usuario).State = EntityState.Deleted;
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







    }
}
