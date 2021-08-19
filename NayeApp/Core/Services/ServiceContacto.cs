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
    public class ServiceContacto : IServiceContacto
    {
        public IEnumerable<Contacto> GetContactos()
        {
            IRepositoryContacto repository = new RepositoryContacto();
            return repository.GetContactos();
        }
        
        public Contacto GetContactoById(int id)
        {
            IRepositoryContacto repository = new RepositoryContacto();
            return repository.GetContactoById(id);
        }
        
        public void DeleteContacto(int id)
        {
            IRepositoryContacto repository = new RepositoryContacto();
            repository.DeleteContacto(id);
        }
        public Contacto Save(Contacto contacto)
        {
            IRepositoryContacto repository = new RepositoryContacto();
            return repository.Save(contacto);
        }
    }
}
