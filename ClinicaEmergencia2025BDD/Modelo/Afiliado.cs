namespace ClinicaEmergencia2025BDD.Modelo
{
    public class Afiliado
    {
        public ObraSocial obraSocial { get; set; }

        public Afiliado(string numAfil, string obra) 
        {
            obraSocial = new ObraSocial(numAfil, obra);
        }
    }
}