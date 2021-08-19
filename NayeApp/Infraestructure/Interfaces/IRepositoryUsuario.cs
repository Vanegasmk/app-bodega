using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Interfaces
{
    public interface IRepositoryUsuario
    {
        IEnumerable<Usuario> GetUsuarios();
        Usuario Save(Usuario usuario);
        Usuario GetUsuario(string email, string clave);
        Usuario GetUsuarioById(int id);

        void DeleteUsuario(int id);
        
        Usuario EditarRolUsuario(int id);

        Usuario SuspenderRolUsuario(int id);

    }
}
