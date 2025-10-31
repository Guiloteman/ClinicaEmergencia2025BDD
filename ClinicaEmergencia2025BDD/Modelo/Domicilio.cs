namespace ClinicaEmergencia2025BDD.Modelo
{
    public class Domicilio
    {
        public string calle { get; set; }
        public string numero { get; set; }
        public string localidad { get; set; }
        public Domicilio(string calle, string numero, string localidad)
        {
            this.calle = calle;
            this.numero = numero;
            this.localidad = localidad;
        }
    }
}