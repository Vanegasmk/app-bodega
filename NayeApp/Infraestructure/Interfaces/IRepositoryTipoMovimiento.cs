using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IRepositoryTipoMovimiento
    {
        IEnumerable<TipoMovimiento> GetTipoMovimientos();
        IEnumerable<TipoMovimiento> GetTipoMovimientoByName(string name);
        TipoMovimiento GetTipoMovimientoById(int id);
        void DeleteTipoMovimiento(int id);
        TipoMovimiento Save(TipoMovimiento tipoMovimiento);
    }
}
