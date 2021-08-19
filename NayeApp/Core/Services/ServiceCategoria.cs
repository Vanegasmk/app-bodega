using Infraestructure.Interfaces;
using Infraestructure.Repository;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Models;

namespace Core.Services
{
    public class ServiceCategoria : IServiceCategoria
    {
        public IEnumerable<Categoria> GetCategorias()
        {
            IRepositoryCategoria repository = new RepositoryCategoria();
            return repository.GetCategorias();
        }
        public IEnumerable<Categoria> GetCategoriaByName(string name)
        {
            IRepositoryCategoria repository = new RepositoryCategoria();
            return repository.GetCategoriaByName(name);
        }
        public Categoria GetCategoriaById(int id)
        {
            IRepositoryCategoria repository = new RepositoryCategoria();
            return repository.GetCategoriaById(id);
        }
        public void DeleteCategoria(int id)
        {
            IRepositoryCategoria repository = new RepositoryCategoria();
            repository.DeleteCategoria(id);
        }    
        public Categoria Save(Categoria categoria)
        {
            IRepositoryCategoria repository = new RepositoryCategoria();
            return repository.Save(categoria);
        }

    }
}
