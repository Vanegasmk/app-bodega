using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IServiceContacto
    {
        IEnumerable<Contacto> GetContactos();
        Contacto GetContactoById(int id);
        void DeleteContacto(int id);
        Contacto Save(Contacto contacto);
    }
}
