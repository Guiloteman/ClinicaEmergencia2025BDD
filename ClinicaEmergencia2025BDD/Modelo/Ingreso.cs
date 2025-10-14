using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaEmergencia2025BDD.Modelo
{
    internal class Ingreso
    {
        public int Id { get; set; }
        public Atencion atencion;
        public Paciente paciente;
        public Enfermera enfermera;
        public Nivel nivel;
        public EstadoIngreso estadoIngreso = EstadoIngreso.Pendiente;
        public string informe;
        public DateTime FechaIngreso { get; set; } = DateTime.Now;
        public decimal temperatura;
        public TensionArterial tensionArterial;
        public FrecuenciaCardiaca frecuenciaCardiaca;
        public FrecuenciaRespiratoria frecuenciaRespiratoria;
    }
}
