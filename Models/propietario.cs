namespace InmobiliariaOrtiz_Pascuali.Models;

public class Propietario
{
    public int idPropietario { get; set; }
    public string nombre { get; set; }= ""; //INICIALIZA CON UN VACIO 
    public string? apellido { get; set; }
    public string? dni { get; set; }
    public bool estado { get; set; }

    public Propietario() { } //Propietario


}
