namespace ClinicaEmergencia2025BDD.Modelo
{
    public class Afiliado
    {
        public ObraSocial obraSocial { get; set; }
        public string numeroAfiliado { get; set; }
        public string obtenerObraSocial(string obra)
        {
            obraSocial = new ObraSocial();
            
            return obraSocial.obtenerObraSocial(obra);
        }
    }
}