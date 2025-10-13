namespace ClinicaEmergencia2025BDD.Modelo
{
    public class TensionArterial
    {
        public Frecuencia frecuanciaSistolica { get; set; }
        public Frecuencia frecuenciaDiastolica { get; set; }
        public TensionArterial(decimal sistolica, decimal diastolica)
        {
            frecuanciaSistolica = new Frecuencia(sistolica);
            frecuenciaDiastolica = new Frecuencia(diastolica);
        }
    }
}