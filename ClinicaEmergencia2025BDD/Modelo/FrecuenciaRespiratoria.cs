namespace ClinicaEmergencia2025BDD.Modelo
{
    public class FrecuenciaRespiratoria: Frecuencia
    {
        public FrecuenciaRespiratoria(decimal valor) : base(valor)
        {
        }

        public decimal Valor { get; set; }
        
    }
}