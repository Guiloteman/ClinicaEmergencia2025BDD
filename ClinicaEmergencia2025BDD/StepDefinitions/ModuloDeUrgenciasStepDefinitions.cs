using System;
using ClinicaEmergencia2025BDD.App;
using ClinicaEmergencia2025BDD.Modelo;
using ClinicaEmergencia2025BDD.StepDefinitions.Mock;
using NUnit.Framework;
using Reqnroll;

namespace ClinicaEmergencia2025BDD.StepDefinitions
{
    [Binding]
    public class ModuloDeUrgenciasStepDefinitions
    {
        private Enfermera enfermera;
        private DBPruebaEnMemoria dbMockeada;
        private ServicioUrgencias servicioUrgencias;
        public ModuloDeUrgenciasStepDefinitions()
        {
            dbMockeada = new DBPruebaEnMemoria();
            servicioUrgencias = new ServicioUrgencias(dbMockeada);
        }

        [Given("que la siguiente enfermera esta registrada:")]
        public void GivenQueLaSiguienteEnfermeraEstaRegistrada(DataTable dataTable)
        {
            string nombre = dataTable.Rows[0]["Nombre"];
            string apellido = dataTable.Rows[0]["Apellido"];
            enfermera = new Enfermera();
            enfermera.nombre = nombre;
            enfermera.apellido = apellido;
        }

        [Given("que estan registrados los siguientes pacientes:")]
        public void GivenQueEstanRegistradosLosSiguientesPacientes(DataTable dataTable)
        {
            foreach (var row in dataTable.Rows)
            {
                string nombre = row["Nombre"];
                string apellido = row["Apellido"];
                string cuil = row["Cuil"];
                string obraSocial = row["Obra Social"];
                Paciente paciente = new Paciente();
                paciente.nombre = nombre;
                paciente.apellido = apellido;
                paciente.cuil = cuil;
                paciente.obtenerObraSocial(obraSocial);
                dbMockeada.GuardarPaciente(paciente);
            }
        }

        [When("ingresan a urgencias los siguientes pacientes:")]
        public void WhenIngresanAUrgenciasLosSiguientesPacientes(DataTable dataTable)
        {
            foreach (var row in dataTable.Rows)
            {
                string cuil = row["Cuil"];
                NivelEmergencia nivelEmergencia = (NivelEmergencia)Enum.Parse(typeof(NivelEmergencia), row["Nivel de Emergencia"]);
                string informe = row["Informe"];
                decimal temperatura = decimal.Parse(row["Temperatura"]);
                decimal frecuenciaCardiaca = decimal.Parse(row["Frecuencia Cardíaca"]);
                decimal frecuenciaRespiratoria = decimal.Parse(row["Frecuencia Respiratoria"]);
                decimal tensionSistolica = decimal.Parse(row["Tensión Arterial"].ToString().Split('/')[0]);
                decimal tensionDiastolica = decimal.Parse(row["Tensión Arterial"].ToString().Split('/')[1]);
                servicioUrgencias.RegistrarUrgencia(cuil, enfermera, nivelEmergencia, informe, temperatura, frecuenciaCardiaca, frecuenciaRespiratoria, tensionSistolica, tensionDiastolica);
            }
        }

        [Then("los pacientes deben ser añadidos a la cola de atencion ordenados por cuil de la siguiente manera:")]
        public void ThenLosPacientesDebenSerAnadidosALaColaDeAtencionOrdenadosPorCuilDeLaSiguienteManera(DataTable dataTable)
        {
            foreach (var row in dataTable.Rows)
            {
                string cuilEsperado = row["Cuil"];
                Paciente pacienteEnCola = servicioUrgencias.ObtenerPacienteEnCola(cuilEsperado);
                Assert.IsTrue(pacienteEnCola.cuil.Equals(cuilEsperado.ToString()));
            }
        }
    }
}
