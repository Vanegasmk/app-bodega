using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IServiceProveedor
    {
        IEnumerable<Proveedor> GetProveedores();
        IEnumerable<Proveedor> GetProveedoresByName(string name);
        Proveedor GetProveedorById(int id);
        void DeleteProveedor(int id);
        Proveedor Save(Proveedor proveedor, string[] selectedContacto);

        bool GetProveedorProductoById(int id);

    }
}
