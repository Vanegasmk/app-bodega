using Infraestructure.Interfaces;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Infraestructure.Repository
{
    public class RepositoryCategoria : IRepositoryCategoria
    {
        public IEnumerable<Categoria> GetCategorias()
        {
            IEnumerable<Categoria> categorias = null;
            using (MyContextNayeApp ctx = new MyContextNayeApp())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                categorias = ctx.Categoria.ToList();
            }
            return categorias;
        }

        public IEnumerable<Categoria> GetCategoriaByName(string name)
        {
            IEnumerable<Categoria> categorias = null;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    categorias = ctx.Categoria.Where(x => x.Nombre == name).ToList();
                }
                return categorias;
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
        public Categoria GetCategoriaById(int id)
        {
            Categoria categoria = null;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    categoria = ctx.Categoria.Where(x => x.Id == id).SingleOrDefault();
                }
                return categoria;
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
        public void DeleteCategoria(int id)
        {
            int retorno;
            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Categoria categoria = new Categoria()
                    {
                        Id = id
                    };
                    ctx.Entry(categoria).State = EntityState.Deleted;
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
        public Categoria Save(Categoria categoria)
        {
            int retorno = 0;
            Categoria oCategoria;

            try
            {
                using (MyContextNayeApp ctx = new MyContextNayeApp())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oCategoria = GetCategoriaById(categoria.Id);

                    if (oCategoria == null)
                    {
                        ctx.Categoria.Add(categoria);
                    }
                    else
                    {
                        ctx.Entry(categoria).State = EntityState.Modified;
                    }

                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oCategoria = GetCategoriaById(categoria.Id);
                return oCategoria;
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
