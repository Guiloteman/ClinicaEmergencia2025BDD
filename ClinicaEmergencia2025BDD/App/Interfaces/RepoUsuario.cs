using ClinicaEmergencia2025BDD.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaEmergencia2025BDD.App.Interfaces
{
    public interface RepoUsuario
    {
        public void GuardarUsuario(Usuario usuario);
        public Usuario ObtenerUsuarioPorClave(string password);
    }
}
