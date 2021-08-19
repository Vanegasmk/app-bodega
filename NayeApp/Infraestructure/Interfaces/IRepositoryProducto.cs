using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IRepositoryProducto
    {
        IEnumerable<Producto> GetProductos();
        IEnumerable<Producto> GetProductoByName(string name);
        Producto GetProductoById(int id);
        void DeleteProducto(int id);
        Producto Save(Producto Producto, string[] selectedProducto);

        IEnumerable<Producto> GetBodegaProductoById(int id);

        Producto GetCategoriaProductoById(int id);

        IEnumerable<Producto> GetCategoriaProductosById(int id);
    }
}
