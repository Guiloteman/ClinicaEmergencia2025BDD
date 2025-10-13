namespace ClinicaEmergencia2025BDD.Modelo
{
    public class ObraSocial
    {
        public Dictionary<int, string> obrasSociales = new Dictionary<int, string>()
        {
            { 1, "OSDE" },
            { 2, "Swiss Medical" },
            { 3, "Galeno" },
            { 4, "MedLife" },
            { 5, "PAMI" }
        };
        public string obtenerObraSocial(string obra)
        {
            string resultado = "";
            foreach (var item in obrasSociales)
            {
                if (item.Value == obra)
                {
                    resultado = item.Value;
                    break;
                }
            }
            return resultado;
        }
    }
}