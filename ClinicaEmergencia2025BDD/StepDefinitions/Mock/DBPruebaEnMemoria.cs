using ClinicaEmergencia2025BDD.App.Interfaces;
using ClinicaEmergencia2025BDD.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaEmergencia2025BDD.StepDefinitions.Mock
{
    public class DBPruebaEnMemoria : RepositorioPacientes
    {
        public Dictionary<string, Paciente> pacientes;
        public DBPruebaEnMemoria()
        {
            pacientes = new Dictionary<string, Paciente>();
        }
        public void GuardarPaciente(Paciente paciente)
        {
            pacientes.Add(paciente.cuil, paciente);
        }
        public Paciente ObtenerPacientePorCuil(string cuil)
        {
            if (pacientes.ContainsKey(cuil))
            {
                return pacientes[cuil];
            }
            return null;
        }
    }
}
