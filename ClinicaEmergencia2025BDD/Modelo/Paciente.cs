namespace ClinicaEmergencia2025BDD.Modelo
{
    public class Paciente : Persona
    {
        public Afiliado afiliado { get; set; }
        public Domicilio domicilio { get; set; }
        public string obtenerObraSocial(string obra)
        {
            this.afiliado = new Afiliado();
            
            return afiliado.obtenerObraSocial(obra);
        }
        public string GeneradorDeNumeroDeAfiliacion()
        {
            this.afiliado = new Afiliado();
            var resultado = afiliado.GeneradorDeNumeroDeAfiliacion();
            return resultado;
        }
    }
}