using NUnit.Framework;

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

        public Dictionary<int, string> numeroAfiliacionObrasSociales = new Dictionary<int, string>()
        {
            { 1, "AFI_OSDE_01/10/2025_0000000001" },
            { 2, "AFI_Swiss_Medical_03/10/2025_0000000002" },
            { 3, "AFI_Galeno_11/10/2025_0000000003" },
            { 4, "AFI_MedLife_15/10/2025_0000000004" },
            { 5, "AFI_PAMI_19/10/2025_0000000005" }
        };

        public ObraSocial(string numAfil, string obra)
        {
        }
        public bool obtenerObraSocial(string obra)
        {
            return obrasSociales.ContainsValue(obra);
        }

        public bool corroborarNumeroDeAfiliacion(string numAfil) 
        {
            return numeroAfiliacionObrasSociales.ContainsValue(numAfil);
        }

        public string obraSocial(string obra)
        {
            string resultado = "";
            try
            {
                foreach (var item in obrasSociales)
                {
                    resultado = item.Value;
                    break;
                }
                return resultado;
            }
            catch (Exception ex) 
            {
                new Exception(ex.ToString());
            }
            return resultado;
        }
    }
}