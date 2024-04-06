namespace InmobiliariaOrtiz_Pascuali.Models;

public class Inmueble
{
    public int IdInmueble { get; set; }
    public string Direccion { get; set; }
    public string Coordenadas { get; set; }
    public string Tipo { get; set; }
    public string Uso { get; set; }
    public DateTime[] FechaDisponible { get; set; }
    public int CantAmbientes { get; set; }
    public double Precio { get; set; }
    public bool Estado { get; set; }
    public int IdPropietario { get; set; }
    public Propietario? propietario { get; set; }


}
