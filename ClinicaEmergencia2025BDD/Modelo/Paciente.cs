using Reqnroll;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClinicaEmergencia2025BDD.Modelo
{
    public class Paciente : Persona
    {
        public Afiliado afiliado { get; set; }
        public Domicilio domicilio { get; set; }
        public Paciente(string cuil, string nombre, string apellido, string numAfil, string obra, string calle, string numero, string localidad) : base(cuil, nombre, apellido)
        {
            if (string.IsNullOrWhiteSpace(cuil) ||
                string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(apellido) ||
                string.IsNullOrWhiteSpace(calle) ||
                string.IsNullOrWhiteSpace(numero) ||
                string.IsNullOrWhiteSpace(localidad))
            {
                throw new Exception("¡Se omitieron algunos datos mandatorios!");
            }
            this.afiliado = new Afiliado(numAfil, obra);

            bool obraSocialExiste = this.afiliado.obraSocial.obtenerObraSocial(obra);
            bool afiliadoValido = this.afiliado.obraSocial.corroborarNumeroDeAfiliacion(numAfil);

            if (!obraSocialExiste)
            {
                throw new Exception("¡No se puede registrar al paciente con una obra social inexistente!");
            }

            if (obraSocialExiste && !afiliadoValido)
            {
                throw new Exception("¡No se puede registrar al paciente dado que no está afiliado a la obra social!");
            }
            this.domicilio = new Domicilio(calle, numero, localidad);
        }

        public Paciente(string cuil, string nombre, string apellido) : base(cuil, nombre, apellido)
        {
            this.cuil = cuil;
            this.nombre = nombre;
            this.apellido = apellido;
        }

        public Paciente(string cuil, string nombre, string apellido, string calle, string numero, string local) : base(cuil, nombre, apellido)
        {
            this.cuil=cuil;
            this.nombre=nombre;
            this.apellido=apellido;
            this.domicilio = new Domicilio(calle, numero, local);
        }
    }
}