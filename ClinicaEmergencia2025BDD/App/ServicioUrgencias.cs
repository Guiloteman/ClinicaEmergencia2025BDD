using ClinicaEmergencia2025BDD.App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaEmergencia2025BDD.Modelo;

namespace ClinicaEmergencia2025BDD.App
{
    public class ServicioUrgencias
    {
        public RepositorioPacientes dbPacientes;
        public ServicioUrgencias(RepositorioPacientes repoPacientes)
        {
            dbPacientes = repoPacientes;
        }
        public void RegistrarUrgencia(string cuilPaciente,
                                      Enfermera enfermera,
                                      NivelEmergencia nivelEmergencia,
                                      string informe,
                                      decimal temperatura,
                                      decimal frecuenciaCardiaca,
                                      decimal frecuenciaRespiratoria,
                                      decimal tensionSistolica,
                                      decimal tensionDiastolica
                                      )
        {
            Ingreso nuevoIngreso = new Ingreso();
            nuevoIngreso.paciente = dbPacientes.ObtenerPacientePorCuil(cuilPaciente);
            nuevoIngreso.enfermera = enfermera;
            nuevoIngreso.nivelEmergencia = nivelEmergencia;
            nuevoIngreso.informe = informe;
            nuevoIngreso.temperatura = temperatura;
            nuevoIngreso.frecuenciaCardiaca = new FrecuenciaCardiaca(frecuenciaCardiaca);
            nuevoIngreso.frecuenciaRespiratoria = new FrecuenciaRespiratoria(frecuenciaRespiratoria);
            nuevoIngreso.tensionArterial = new TensionArterial(tensionSistolica, tensionDiastolica);
        }
        public Paciente ObtenerPacienteEnCola(string cuil)
        {
            return dbPacientes.ObtenerPacientePorCuil(cuil);
        }

        public string verificarPacienteRegistrado(string cuil)
        {
            var paciente = dbPacientes.ObtenerPacientePorCuil(cuil);
            if (paciente == null)
            {
                Exception ex = new Exception("Paciente no registrado. No se puede ingresar a urgencias.");
                return ex.Message.ToString();

            }
            return null;
        }
    }
}
