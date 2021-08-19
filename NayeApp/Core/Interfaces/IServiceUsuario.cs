using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IServiceUsuario
    {
        Usuario GetUsuario(string correo, string clave);
        Usuario GetUsuarioById(int id);
        Usuario Save(Usuario usuario);

        IEnumerable<Usuario> GetUsuarios();

        void DeleteUsuario(int id);

        Usuario EditarRolUsuario(int id);

        Usuario SuspenderRolUsuario(int id);


    }
}
