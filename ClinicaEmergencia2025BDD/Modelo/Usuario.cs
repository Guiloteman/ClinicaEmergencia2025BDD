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
        public string enfermera { get; set; } = "enfermera";
        public string medico { get; set; } = "médico";

        public Usuario(string user, string password, string autoridad) 
        {
            this.user = user;
            this.password = password;
            if (autoridad.Contains(autoridad)) 
            {
                this.enfermera = autoridad;
            }
            else
            {
                this.medico = autoridad;
            }
        }
    }
}
