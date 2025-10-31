using System;
using ClinicaEmergencia2025BDD.App;
using ClinicaEmergencia2025BDD.Modelo;
using ClinicaEmergencia2025BDD.StepDefinitions.Mock;
using Microsoft.IdentityModel.Tokens;
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
        private Exception excepcionEsperda;
        public ModuloDeUrgenciasStepDefinitions()
        {
            dbMockeada = new DBPruebaEnMemoria();
            servicioUrgencias = new ServicioUrgencias(dbMockeada);
        }

        [Given("que la siguiente enfermera esta registradaaa:")]
        public void GivenQueLaSiguienteEnfermeraEstaRegistradaaa(DataTable dataTable)
        {
            foreach (var item in dataTable.Rows)
            {
                var cuil = item["Cuil"];
                var nombre = item["Nombre"];
                var apellido = item["Apellido"];
                Enfermera enfermera = new Enfermera(cuil, nombre, apellido);
            }
        }

        [Given("que estan registrados los siguientes pacientes:")]
        public void GivenQueEstanRegistradosLosSiguientesPacientes(DataTable dataTable)
        {
            string obra = "";
            foreach (var row in dataTable.Rows)
            {
                string nombre = row["Nombre"];
                string apellido = row["Apellido"];
                string cuil = row["Cuil"];
                
               
                Paciente paciente = new Paciente(cuil, nombre, apellido);
                dbMockeada.GuardarPaciente(paciente);
            }
        }

        [When("ingresan a urgencias los siguientes pacientes:")]
        public void WhenIngresanAUrgenciasLosSiguientesPacientes(DataTable dataTable)
        {
            excepcionEsperda = null;
            foreach (var row in dataTable.Rows)
            {
                string cuil = row["Cuil"];
                string nivelEmergencia = row["Nivel de Emergencia"];
                string informe = row["Informe"];
                decimal temperatura = decimal.Parse(row["Temperatura"]);
                decimal frecuenciaCardiaca = decimal.Parse(row["Frecuencia Cardíaca"]);
                decimal frecuenciaRespiratoria = decimal.Parse(row["Frecuencia Respiratoria"]);
                decimal tensionSistolica = decimal.Parse(row["Tensión Arterial"].ToString().Split('/')[0]);
                decimal tensionDiastolica = decimal.Parse(row["Tensión Arterial"].ToString().Split('/')[1]);
                try
                {
                    servicioUrgencias.RegistrarUrgencia(cuil, enfermera, nivelEmergencia, informe, temperatura, frecuenciaCardiaca, frecuenciaRespiratoria, tensionSistolica, tensionDiastolica);
                }
                catch (Exception ex)
                {
                    excepcionEsperda = ex;
                    break;
                }
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

        [Then("se muestra el mensaje de error {string}")]
        public void ThenSeMuestraElMensajeDeError(string p0, DataTable dataTable)
        {
            string ex = servicioUrgencias.verificarPacienteRegistrado(dataTable.Rows[0]["Cuil"]);
            Assert.AreEqual(p0, ex);
        }

        [Then("se muestra el mensaje de error {string}.")]
        public void ThenSeMuestraElMensajeDeError_(string p0, DataTable dataTable)
        {
            servicioUrgencias.ObtenerExcepcion(p0, dataTable);
            servicioUrgencias.ObtenerExcepcionValoreNegativos(p0, dataTable);
        }

        [Then("los pacientes deben ser añadidos a la cola de atencion ordenados por prioridad de la siguiente manera:")]
        public void ThenLosPacientesDebenSerAnadidosALaColaDeAtencionOrdenadosPorPrioridadDeLaSiguienteManera(DataTable dataTable)
        {
            var ingresos = servicioUrgencias.ObtenerIngresos();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string cuilEsperado = dataTable.Rows[i]["Cuil"];
                Assert.AreEqual(cuilEsperado, ingresos[i].paciente.cuil);
            }
        }


    }
}
