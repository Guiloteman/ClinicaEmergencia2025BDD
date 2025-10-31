using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaEmergencia2025BDD.Modelo
{
    public class Medico: Persona
    {
        public string matricula { get; set; }
        public Medico(string cuil, string nombre, string apellido, string matricula) : base(cuil, nombre , apellido) 
        {
            this.matricula = matricula;
        }
    }
}
