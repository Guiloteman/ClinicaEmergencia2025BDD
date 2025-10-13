using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaEmergencia2025BDD.Modelo;

namespace ClinicaEmergencia2025BDD.App.Interfaces
{
    public interface RepositorioPacientes
    {
        public void GuardarPaciente(Paciente paciente);
        public Paciente ObtenerPacientePorCuil(string cuil);
    }
}
