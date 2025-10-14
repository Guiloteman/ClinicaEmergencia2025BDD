using ClinicaEmergencia2025BDD.App.Interfaces;
using ClinicaEmergencia2025BDD.Modelo;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaEmergencia2025BDD.App
{
    public class ServicioUrgencias
    {
        Exception ex;
        public RepositorioPacientes dbPacientes;
        public ServicioUrgencias(RepositorioPacientes repoPacientes)
        {
            dbPacientes = repoPacientes;
        }
        public void RegistrarUrgencia(string cuilPaciente,
                                      Enfermera enfermera,
                                      string nivelEmergencia,
                                      string informe,
                                      decimal temperatura,
                                      decimal frecuenciaCardiaca,
                                      decimal frecuenciaRespiratoria,
                                      decimal tensionSistolica,
                                      decimal tensionDiastolica
                                      )
        {
            try
            {
                Ingreso nuevoIngreso = new Ingreso();
                nuevoIngreso.paciente = dbPacientes.ObtenerPacientePorCuil(cuilPaciente);
                nuevoIngreso.enfermera = enfermera;
                nuevoIngreso.nivel = new Nivel(nivelEmergencia);
                nuevoIngreso.informe = informe;
                nuevoIngreso.temperatura = temperatura;
                nuevoIngreso.frecuenciaCardiaca = new FrecuenciaCardiaca(frecuenciaCardiaca);
                nuevoIngreso.frecuenciaRespiratoria = new FrecuenciaRespiratoria(frecuenciaRespiratoria);
                nuevoIngreso.tensionArterial = new TensionArterial(tensionSistolica, tensionDiastolica);
            }
            catch (Exception ex)
            {
                ex = new Exception(ex.Message.ToString());
            }
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
                ex = new Exception("Paciente no registrado. No se puede ingresar a urgencias.");
                return ex.Message.ToString();
            }
            return null;
        }

        public void ObtenerExcepcion(string p0, DataTable dataTable)
        {
            ex = new Exception(p0);
            foreach (var row in dataTable.Rows)
            {
                string cuil = row["Cuil"];
                string informe = row["Informe"];
                string nivel = row["Nivel de Emergencia"];
                string temperatura = row["Temperatura"];
                string fC = row["Frecuencia Cardíaca"];
                string fr = row["Frecuencia Respiratoria"];
                string ten = row["Tensión Arterial"];
                
                switch(p0)
                {
                    case "Faltan agregar algunos datos.":
                        if (string.IsNullOrWhiteSpace(cuil) || string.IsNullOrWhiteSpace(informe) || string.IsNullOrWhiteSpace(nivel) || string.IsNullOrWhiteSpace(temperatura) || string.IsNullOrWhiteSpace(fC) || string.IsNullOrWhiteSpace(fr) || string.IsNullOrWhiteSpace(ten))
                        {
                            Assert.AreEqual(p0, ex.Message.ToString());
                        }
                        break;
                    default:
                        break;
                }
            }
            
        }

        public void ObtenerExcepcionValoreNegativos(string p0, DataTable dataTable)
        {
            ex = new Exception(p0);
            foreach (var row in dataTable.Rows)
            {
                decimal fC = decimal.Parse(row["Frecuencia Cardíaca"]);
                decimal fr = decimal.Parse(row["Frecuencia Respiratoria"]);   

                switch (p0)
                {
                    case "Los datos cargados, correspondientes para los de frecuencia, no pueden ser negativos.":
                        if (decimal.IsNegative(fC) || decimal.IsNegative(fr))
                        {
                            Assert.AreEqual(p0, ex.Message.ToString());
                        }
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
