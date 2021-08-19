using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IServiceMovimientos
    {
        IEnumerable<Movimientos> GetMovimientos();
        IEnumerable<Movimientos> GetMovimientosByName(string name);
        Movimientos GetMovimientosById(int id);
        void DeleteMovimiento(int id);
        Movimientos Save(Movimientos movimientos);
        Movimientos GetProductoMovimientoById(int id);

        Movimientos GetTipoMovimientoById(int id);

        IEnumerable<Movimientos> GetMovimientosEntradaFechas(DateTime? fecha1, DateTime? fecha2);

        IList<Movimientos> GetMovimientosMayorSalida();

        IEnumerable<Movimientos> GetMovimientosSalidasFechas(DateTime? fecha1, DateTime? fecha2);
    }
}
