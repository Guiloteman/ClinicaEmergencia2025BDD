using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaEmergencia2025BDD.Modelo
{
    public class Nivel
    {
        public Dictionary<NivelEmergencia, int> tiempoRespuesta = new Dictionary<NivelEmergencia, int>
        {
            { NivelEmergencia.Critica, 5 },          // 5 minutos
            { NivelEmergencia.Emergencia, 30 },     // 30 minutos
            { NivelEmergencia.Urgencia, 60 },       // 60 minutos
            { NivelEmergencia.UrgenciaMenor, 120 }, // 120 minutos
            { NivelEmergencia.SinUrgencia, 240 }    // 240 minutos
        };

        public int ObtenerTiempoRespuesta(NivelEmergencia nivel)
        {
            return tiempoRespuesta[nivel];
        }
    }
}
