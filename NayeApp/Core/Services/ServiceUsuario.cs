using Core.Interfaces;
using Infraestructure.Models;
using Infraestructure.Interfaces;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utils;

namespace Core.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        public Usuario GetUsuario(string correo, string clave)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();

            string crytpPasswd = Cryptography.Encrypt(clave);

            return repository.GetUsuario(correo, crytpPasswd);
        }

        public Usuario GetUsuarioById(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();

            Usuario oUsuario = repository.GetUsuarioById(id);

            oUsuario.Clave = Cryptography.Decrypt(oUsuario.Clave);

            return oUsuario;
        }
        public Usuario Save(Usuario usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            
            usuario.Clave = Cryptography.Encrypt(usuario.Clave);
            usuario.Tipo = 0;

            return repository.Save(usuario);
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuarios();
        }
        public void DeleteUsuario(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            repository.DeleteUsuario(id);
        }

        public Usuario EditarRolUsuario(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.EditarRolUsuario(id);
        }

        public Usuario SuspenderRolUsuario(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.SuspenderRolUsuario(id);
        }
    }
}
