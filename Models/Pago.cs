namespace InmobiliariaOrtiz_Pascuali.Models;

public class Pago
{
    public int IdPago { get; set; }
    public DateTime Fecha_pago { get; set; }
    public double Monto { get; set; }
    public bool Estado {get; set;}
    public int IdContrato{get;set;}
}
