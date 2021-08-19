using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IServiceCategoria
    {
        IEnumerable<Categoria> GetCategorias();
        IEnumerable<Categoria> GetCategoriaByName(string name);
        Categoria GetCategoriaById(int id);
        void DeleteCategoria(int id);
        Categoria Save(Categoria categoria);
    }
}
