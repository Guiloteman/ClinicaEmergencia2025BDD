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

        public string GeneradorDeNumeroDeAfiliacion()
        {
            // Prefijo opcional
            string prefijo = "AFI";

            // Fecha actual en formato AAAAMMDD
            string fecha = DateTime.Now.ToString("ddMMyyyy");

            // Número aleatorio de 5 dígitos
            Random rnd = new Random();
            int numeroAleatorio = rnd.Next(10000, 99999);

            // Construir el número de afiliación
            return $"{prefijo}-{fecha}-{numeroAleatorio}";
        }
    }
}