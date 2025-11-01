using ClinicaEmergencia2025BDD.App;
using ClinicaEmergencia2025BDD.Modelo;
using ClinicaEmergencia2025BDD.StepDefinitions.Mock;
using NUnit.Framework;
using Reqnroll;
using System;

namespace ClinicaEmergencia2025BDD.StepDefinitions
{
    [Binding]
    public class ModuloDeAutenticacion_005StepDefinitions
    {
        private DBPruebaEnMemoria dbMockeada;
        private ServicioLogin servicioLogin;
        private Usuario usuario;
        private ServicioAutenticacion servicioAutenticacion;
        Exception ex;
        public ModuloDeAutenticacion_005StepDefinitions()
        {
            dbMockeada = new DBPruebaEnMemoria();
            servicioLogin = new ServicioLogin(dbMockeada);
            servicioAutenticacion = new ServicioAutenticacion();
        }

        [Given("existen los siguientes usuarios:")]
        public void GivenExistenLosSiguientesUsuarios(DataTable dataTable)
        {
            foreach (var item in dataTable.Rows)
            {
                var user = item["Email"];
                var clave = item["Contraseña"];
                var autoridad = item["Autoridad"];
                string password = servicioAutenticacion.CrearHash(clave);
                Usuario usuario = new Usuario(user, password, autoridad);
                this.servicioLogin.guardarUsuario(usuario);
                this.usuario = servicioLogin.getUsuario(password);
            }
        }

        [When("ingreso los siguientes datos para acceder al sistema:")]
        public void WhenIngresoLosSiguientesDatosParaAccederAlSistema(DataTable dataTable)
        {
            foreach(var item in dataTable.Rows)
            {
                var email = item["Email"];
                var password = item["Contraseña"];
                var autoridad = item["Autoridad"];
                var hash = servicioAutenticacion.CrearHash(password);
                Usuario usuario1 = new Usuario(email, hash, autoridad);
                if (usuario1 != usuario) 
                {
                    ex = new Exception("Usuario y Contraseña inválidos.");
                }
            }
        }

        [Then("puedo ver y ejecutar las historias correspondiente")]
        public void ThenPuedoVerYEjecutarLasHistoriasCorrespondiente()
        {
            Assert.IsNotNull(this.usuario);
        }

        [Then("se emite el siguiente mensaje: {string}")]
        public void ThenSeEmiteElSiguienteMensaje(string p0)
        {
            Assert.AreEqual(ex.Message.ToString(), p0);
        }


    }
}
