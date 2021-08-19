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
    public class ServiceProveedor : IServiceProveedor
    {
        public IEnumerable<Proveedor> GetProveedores()
        {
            IRepositoryProveedor repository = new RepositoryProveedor();
            return repository.GetProveedores();
        }
        public IEnumerable<Proveedor> GetProveedoresByName(string name)
        {
            IRepositoryProveedor repository = new RepositoryProveedor();
            return repository.GetProveedoresByName(name);
        }
        public Proveedor GetProveedorById(int id)
        {
            IRepositoryProveedor repository = new RepositoryProveedor();
            return repository.GetProveedorById(id);
        }
        public void DeleteProveedor(int id)
        {
            IRepositoryProveedor repository = new RepositoryProveedor();
            repository.DeleteProveedor(id);
        }
        public Proveedor Save(Proveedor proveedor, string[] selectedContacto)
        {
            IRepositoryProveedor repository = new RepositoryProveedor();
            return repository.Save(proveedor,selectedContacto);
        }

        public bool GetProveedorProductoById(int id)
        {
            IRepositoryProveedor repository = new RepositoryProveedor();
            return repository.GetProveedorProductoById(id);
        }

        
    }
}
