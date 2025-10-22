using ClinicaEmergencia2025BDD.App;
using ClinicaEmergencia2025BDD.Modelo;
using ClinicaEmergencia2025BDD.StepDefinitions.Mock;
using NUnit.Framework;
using Reqnroll;
using System;

namespace ClinicaEmergencia2025BDD.StepDefinitions
{
    [Binding]
    public class ModuloDeRegistroDePacientesStepDefinitions
    {
        private DBPruebaEnMemoria dbMockeada;
        private ServicioUrgencias servicioUrgencias;

        public ModuloDeRegistroDePacientesStepDefinitions()
        {
            dbMockeada = new DBPruebaEnMemoria();
            servicioUrgencias = new ServicioUrgencias(dbMockeada);
        }

        [Given("que no están cargados los pacientes en el sistema se emite el siguiente mensaje: {string}")]
        public void GivenQueNoEstanCargadosLosPacientesEnElSistemaSeEmiteElSiguienteMensaje(string p0, DataTable dataTable)
        {
            foreach (var item in dataTable.Rows) 
            {
                string res = servicioUrgencias.verificarPacienteRegistrado(item["Cuil"]);
                Assert.AreEqual(res, p0);
            }
        }

        [When("se cargan los siguientes pacientes:")]
        public void WhenSeCarganLosSiguientesPacientes(DataTable dataTable)
        {
            foreach (var row in dataTable.Rows)
            {
                string cuil = row["Cuil"];
                string apellido = row["Apellido"];
                string nombre = row["Nombre"];
                string calle = row["Calle"];
                string numero = row["Número"];
                string local = row["Localidad"];
                string obraSocial = row["Obra Social"];
                string numAfil = row["Número de Afiliación"];
                Paciente paciente = new Paciente();
                paciente.cuil = cuil;
                paciente.apellido = apellido;
                paciente.nombre = nombre;
                Domicilio domicilio = new Domicilio();
                domicilio.calle = calle;
                domicilio.numero = numero;
                domicilio.localidad = local;
                paciente.obtenerObraSocial(obraSocial);
                numAfil = "AFI-21/10/2025-20000";
                Afiliado afiliado = new Afiliado();
                afiliado.numeroAfiliado = numAfil;
                dbMockeada.GuardarPaciente(paciente);
            }
        }

        [Then("se muestra el siguiente mensaje: {string}")]
        public void ThenSeMuestraElSiguienteMensaje(string p0)
        {
            string mensaje = dbMockeada.mostrarMensaje();
            Assert.AreEqual(mensaje, p0);
        }
    }
}
