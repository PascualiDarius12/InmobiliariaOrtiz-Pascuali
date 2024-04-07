using System.Collections;

namespace InmobiliariaOrtiz_Pascuali.Models;

public class Contrato
{
    public int IdContrato { get; set; }
    public DateTime Fecha_inicio {get; set;}

    public DateTime Fecha_fin {get; set;}

    public int Multa {get; set;}

    public bool Estado {get; set;}

    public IList<Pago> pagos {get;set;}

    public int IdInquilino{ get; set; }
    public Inquilino? inquilino { get; set; }

    public int IdInmueble { get; set; }
    public Inmueble? inmueble { get; set; }


}
