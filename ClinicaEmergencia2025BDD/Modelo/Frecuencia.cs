using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaEmergencia2025BDD.Modelo
{
    public class Frecuencia
    {
        private decimal valor;

        public Frecuencia(decimal valor)
        {
            this.valor = verirficarValor(valor);
        }

        public decimal verirficarValor(decimal valor)
        {
            if (valor < 0)
            {
                throw new Exception("El valor de la frecuencia no puede ser negativo.");
            }
            return valor;
        }
    }
}
