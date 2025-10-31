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
            this.afiliado = new Afiliado(numAfil, obra);
            if (afiliado.obraSocial.obtenerObraSocial(obra) && afiliado.obraSocial.corroborarNumeroDeAfiliacion(numAfil))
            {
                this.cuil = cuil;
                this.nombre = nombre;
                this.apellido = apellido;
                this.afiliado = new Afiliado(numAfil, obra);
                this.domicilio = new Domicilio(calle, numero, localidad);
                
            }
            else
            {
                throw new Exception("Error al intentar cargar los datos del paciente: Obra Social o número de afiliación inválido.");
            }
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