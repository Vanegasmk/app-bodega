using Core.Interfaces;
using Infraestructure.Interfaces;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ServiceTipoMovimiento : IServiceTipoMovimiento
    {
        public IEnumerable<TipoMovimiento> GetTipoMovimientos()
        {
            IRepositoryTipoMovimiento repository = new RepositoryTipoMovimiento();
            return repository.GetTipoMovimientos();
        }
        public IEnumerable<TipoMovimiento> GetTipoMovimientoByName(string name)
        {
            IRepositoryTipoMovimiento repository = new RepositoryTipoMovimiento();
            return repository.GetTipoMovimientoByName(name);
        }
        public TipoMovimiento GetTipoMovimientoById(int id)
        {
            IRepositoryTipoMovimiento repository = new RepositoryTipoMovimiento();
            return repository.GetTipoMovimientoById(id);
        }
        public void DeleteTipoMovimiento(int id)
        {
            IRepositoryTipoMovimiento repository = new RepositoryTipoMovimiento();
            repository.DeleteTipoMovimiento(id);
        }
        public TipoMovimiento Save(TipoMovimiento tipoMovimiento)
        {
            IRepositoryTipoMovimiento repository = new RepositoryTipoMovimiento();
            return repository.Save(tipoMovimiento);
        }
    }
}
