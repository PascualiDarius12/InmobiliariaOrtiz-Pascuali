namespace InmobiliariaOrtiz_Pascuali.Models;

public class Inquilino
{
    public int idInquilino { get; set; }
    public string? nombre { get; set; } // ? acepta valores nulos (null)
    public string? apellido { get; set; }
    public string? dni { get; set; }
    public bool estado { get; set; }
    
 

}
