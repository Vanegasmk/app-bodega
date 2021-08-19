using Infraestructure.Interfaces;
using Infraestructure.Repository;
using Core.Interfaces;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ServiceProducto : IServiceProducto
    {
        public IEnumerable<Producto> GetProductos()
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductos();
        }

        public IEnumerable<Producto> GetProductoByName(string name)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoByName(name);
        }

        public Producto GetProductoById(int id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetProductoById(id);
        }
        
        public void DeleteProducto(int id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            repository.DeleteProducto(id);
        }

        public Producto Save(Producto producto, string[] selectedProveedor)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.Save(producto,selectedProveedor);
        }

        public IEnumerable<Producto> GetBodegaProductoById(int id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetBodegaProductoById(id);
        }

        public Producto GetCategoriaProductoById(int id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetCategoriaProductoById(id);
        }

        public IEnumerable<Producto> GetCategoriaProductosById(int id)
        {
            IRepositoryProducto repository = new RepositoryProducto();
            return repository.GetCategoriaProductosById(id);
        }

    }
}
