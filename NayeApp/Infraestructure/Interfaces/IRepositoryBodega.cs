using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IRepositoryBodega
    {
        IEnumerable<Bodega> GetBodegas();
        IEnumerable<Bodega> GetBodegaByName(string name);
        Bodega GetBodegaById(int id);
        void DeleteBodega(int id);
        Bodega Save(Bodega bodega);
    }
}
