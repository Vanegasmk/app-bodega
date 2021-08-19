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
    public class ServiceBodega : IServiceBodega
    {
        public IEnumerable<Bodega> GetBodegas()
        {
            IRepositoryBodega repository = new RepositoryBodega();
            return repository.GetBodegas();
        }
        public IEnumerable<Bodega> GetBodegaByName(string name)
        {
            IRepositoryBodega repository = new RepositoryBodega();
            return repository.GetBodegaByName(name);
        }
        public Bodega GetBodegaById(int id)
        {
            IRepositoryBodega repository = new RepositoryBodega();
            return repository.GetBodegaById(id);
        }
        public void DeleteBodega(int id)
        {
            IRepositoryBodega repository = new RepositoryBodega();
            repository.DeleteBodega(id);
        }
        public Bodega Save(Bodega bodega)
        {
            IRepositoryBodega repository = new RepositoryBodega();
            return repository.Save(bodega);
        }
    }
}
