using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaEmergencia2025BDD.Modelo
{
    public class Usuario
    {
        public string user { get; set; }
        public string password { get; set; }
        public Enfermera enfermera { get; set; }
        public Medico medico { get; set; }
    }
}
