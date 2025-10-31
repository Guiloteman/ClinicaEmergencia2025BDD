using ClinicaEmergencia2025BDD.App;
using ClinicaEmergencia2025BDD.Modelo;
using ClinicaEmergencia2025BDD.StepDefinitions.Mock;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Configuration;
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
        private string mensaje = "";
        private string mensaje1 = "";
        
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
                try
                {
                    string cuil = row["Cuil"];
                    string apellido = row["Apellido"];
                    string nombre = row["Nombre"];
                    string calle = row["Calle"];
                    string numero = row["Número"];
                    string local = row["Localidad"];
                    string obraSocial = row["Obra Social"];
                    string numAfil = row["Número de Afiliación"];

                    Paciente paciente = new Paciente(cuil, nombre, apellido, numAfil, obraSocial, calle, numero, local);
                    
                    dbMockeada.GuardarPaciente(paciente);
                    this.mensaje = "¡Se Cargó con éxito!";
                }
                catch (Exception e) 
                {
                    string obraSocial = row["Obra Social"];
                    string numAfil = row["Número de Afiliación"];
                    Afiliado afiliado = new Afiliado(numAfil, obraSocial);
                    
                    
                    if (!afiliado.obraSocial.obtenerObraSocial(obraSocial))
                    {
                        this.mensaje1 = "¡No se puede registrar al paciente con una obra social inexistente!";
                    }
                    else if(!afiliado.obraSocial.corroborarNumeroDeAfiliacion(numAfil))
                    {
                        this.mensaje1 = "¡No se puede registrar al paciente dado que no está afiliado a la obra social!";
                    }
                    
                }
            }
        }

        [When("se cargan los siguientes pacientes sin obra social:")]
        public void WhenSeCarganLosSiguientesPacientesSinObraSocial(DataTable dataTable)
        {
            foreach (var row in dataTable.Rows)
            {
                try
                {
                    string cuil = row["Cuil"];
                    string apellido = row["Apellido"];
                    string nombre = row["Nombre"];
                    string calle = row["Calle"];
                    string numero = row["Número"];
                    string local = row["Localidad"];

                    Paciente paciente = new Paciente(cuil, nombre, apellido, calle, numero, local);

                    dbMockeada.GuardarPaciente(paciente);
                    this.mensaje = "¡Se Cargó con éxito!";
                }
                catch (Exception e)
                {
                    this.mensaje1 = "¡No se puede registrar al paciente con una obra social inexistente!";
                }
            }
        }

        [Then("se muestra el siguiente mensaje: {string}")]
        public void ThenSeMuestraElSiguienteMensaje(string p0)
        {
            Assert.AreEqual(this.mensaje, p0);
        }

        [Then("se muestra el siguiente mensaje de error: {string}")]
        public void ThenSeMuestraElSiguienteMensajeDeError(string p0)
        {
            Assert.AreEqual(this.mensaje1, p0);
        }
    }
}
