using ClinicaEmergencia2025BDD.App;
using ClinicaEmergencia2025BDD.App.Interfaces;
using ClinicaEmergencia2025BDD.Modelo;

using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace ClinicaEmergencia2025BDD.App.Tests
{
    [TestClass()]
    public class ServiciosUrgenciasTest
    {
        Usuario usuario;

        [TestMethod()]
        public void verificarPacienteRegistradoTest()
        {
            string cuilInexistente = "27-777777-9";

            var mockRepo = new Mock<RepositorioPacientes>();

            mockRepo.Setup(repo => repo.ObtenerPacientePorCuil(cuilInexistente))
                    .Returns((Paciente)null);

            var servicioUrgencias = new ServicioUrgencias(mockRepo.Object);

            string resultado = servicioUrgencias.verificarPacienteRegistrado(cuilInexistente);

            Assert.AreEqual(resultado, "Paciente no registrado. No se puede ingresar a urgencias.");

            mockRepo.Verify(repo => repo.ObtenerPacientePorCuil(cuilInexistente), Times.Once());
        }

        [TestMethod()]
        public void CrearHashTest()
        {
            string password = "MiClaveSegura123";
            var authService = new ClinicaEmergencia2025BDD.App.ServicioAutenticacion();

            string hash = authService.CrearHash(password);

            Assert.IsFalse(string.IsNullOrEmpty(hash));
            Assert.IsTrue(hash.Contains('|'));
        }

        [TestMethod()]
        public void VerificarHashTest()
        {
            string password = "ContraseñaDePrueba99";
            var authService = new ClinicaEmergencia2025BDD.App.ServicioAutenticacion();

            string hashAlmacenado = authService.CrearHash(password);
            bool resultado = authService.VerificarHash(password, hashAlmacenado);
            Assert.IsTrue(resultado, "La verificación debe ser exitosa con la contraseña correcta.");
        }

        [TestMethod()]
        public void VerificarHash_ContraseñaIncorrecta_DebeRetornarFalso()
        {
            string passwordOriginal = "ContraseñaDePrueba99";
            string passwordIncorrecta = "OtraContraseña123";
            var authService = new ClinicaEmergencia2025BDD.App.ServicioAutenticacion();
            string hashAlmacenado = authService.CrearHash(passwordOriginal);
            bool resultado = authService.VerificarHash(passwordIncorrecta, hashAlmacenado);
            Assert.IsFalse(resultado, "La verificación debe fallar con la contraseña incorrecta.");
        }

        [TestMethod()]
        public void CrearHash_DosHashesMismaClave_DebeSerDiferentePorElSalt()
        {
            string password = "password";
            var authService = new ClinicaEmergencia2025BDD.App.ServicioAutenticacion();

            string hash1 = authService.CrearHash(password);
            string hash2 = authService.CrearHash(password);

            Assert.AreNotEqual(hash1, hash2, "Dos hashes de la misma clave deben ser diferentes debido al salt aleatorio.");
        }

        [TestMethod()]
        public void guardarUsuarioTest()
        {
            var servicio = new ServicioAutenticacion();
            var hash = servicio.CrearHash("1234");
            Usuario usuario = new Usuario("emi_enfermera@gmail.com", hash, "enfermera");

            var mockRepo = new Mock<RepoUsuario>();

            var serv = new ServicioLogin(mockRepo.Object);

            mockRepo.Setup(repo => repo.ObtenerUsuarioPorClave(hash)).Returns(usuario);

            serv.GuardarUsuario(usuario);

            Assert.IsNotNull(serv.ObtenerUsuarioPorClave(hash));

            mockRepo.Verify(
                repo => repo.GuardarUsuario(
                    It.Is<Usuario>(u => u.user == "emi_enfermera@gmail.com" && u.password == hash)
                ),
                Times.Once());
        }
    }
}