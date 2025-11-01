using ClinicaEmergencia2025BDD.App.Interfaces;
using ClinicaEmergencia2025BDD.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaEmergencia2025BDD.App
{
    public class ServicioLogin
    {
        Exception ex;

        private readonly RepoUsuario dbUsuario;

        private List<Usuario> usuarios;

        public Usuario usuario;

        public ServicioLogin(RepoUsuario repoUsuario)
        {
            this.dbUsuario = repoUsuario;
            this.usuarios = new List<Usuario>();
        }

        public void guardarUsuario(Usuario usuario)
        {
            usuarios.Add(usuario);
        }

        public Usuario getUsuario(string clave)
        {
            foreach (var usuario in usuarios)
            {
                if (usuario.password == clave)
                {
                    return usuario;
                }
            }
            return null;
        }

        public void GuardarUsuario(Usuario usuario)
        {
            dbUsuario.GuardarUsuario(usuario);
        }

        public Usuario ObtenerUsuarioPorClave(string password)
        {
            return dbUsuario.ObtenerUsuarioPorClave(password);
        }
    }
}
