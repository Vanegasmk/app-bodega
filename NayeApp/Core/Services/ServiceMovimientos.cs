using Core.Interfaces;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Interfaces;
using Infraestructure.Repository;

namespace Core.Services
{
    public class ServiceMovimientos : IServiceMovimientos
    {
        public IEnumerable<Movimientos> GetMovimientos()
        {
            IRepositoryMovimientos repository = new RepositoryMovimientos();
            return repository.GetMovimientos();
        }
        public IEnumerable<Movimientos> GetMovimientosByName(string name)
        {
            IRepositoryMovimientos repository = new RepositoryMovimientos();
            return repository.GetMovimientosByName(name);
        }
        public Movimientos GetMovimientosById(int id)
        {
            IRepositoryMovimientos repository = new RepositoryMovimientos();
            return repository.GetMovimientosById(id);
        }
        public void DeleteMovimiento(int id)
        {
            IRepositoryMovimientos repository = new RepositoryMovimientos();
            repository.DeleteMovimiento(id);
        }
        public Movimientos Save(Movimientos movimientos)
        {
            IRepositoryMovimientos repository = new RepositoryMovimientos();
            return repository.Save(movimientos);
        }

        public Movimientos GetProductoMovimientoById(int id)
        {
            IRepositoryMovimientos repository = new RepositoryMovimientos();
            return repository.GetProductoMovimientoById(id);
        }

        public Movimientos GetTipoMovimientoById(int id)
        {
            IRepositoryMovimientos repository = new RepositoryMovimientos();
            return repository.GetTipoMovimientoById(id);
        }

        public IEnumerable<Movimientos> GetMovimientosEntradaFechas(DateTime? fecha1, DateTime? fecha2)
        {
            IRepositoryMovimientos repository = new RepositoryMovimientos();
            IEnumerable<Movimientos> movimientos = null;

            if (fecha1 != null && fecha2 != null)
            {
                movimientos = repository.GetMovimientosEntradaFechas(fecha1.Value, fecha2.Value);
            }
            else if (fecha1 != null && fecha2 == null)
            {
                movimientos = repository.GetMovimientosEntradaFechas(fecha1.Value, null);
            }

            return movimientos;
        }

        public IList<Movimientos> GetMovimientosMayorSalida()
        {
            IRepositoryMovimientos repository = new RepositoryMovimientos();
            return repository.GetMovimientosMayorSalida();

        }

        public IEnumerable<Movimientos> GetMovimientosSalidasFechas(DateTime? fecha1, DateTime? fecha2)
        {
            IRepositoryMovimientos repository = new RepositoryMovimientos();
            IEnumerable<Movimientos> movimientos = null;

            if (fecha1 != null && fecha2 != null)
            {
                movimientos = repository.GetMovimientosSalidasFechas(fecha1.Value, fecha2.Value);
            }
            else if (fecha1 != null && fecha2 == null)
            {
                movimientos = repository.GetMovimientosSalidasFechas(fecha1.Value, null);
            }

            return movimientos;
        }

    }
}
