namespace ClinicaEmergencia2025BDD.Modelo
{
    public class Enfermera : Persona
    {
        public string matricula { get; set; }
        public Enfermera(string cuil, string nombre, string apellido) : base(cuil, nombre, apellido)
        {
        }
    }
}