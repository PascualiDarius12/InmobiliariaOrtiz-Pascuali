
using System.ComponentModel.DataAnnotations.Schema;
using InmobiliariaOrtiz_Pascuali.Models;

namespace InmobiliariaOrtiz_Pascuali.Models
{

    public class Contrato
    {
        public int idContrato { get; set; }
        public DateTime Fecha_inicio { get; set; }
        public DateTime Fecha_fin { get; set; }
        
        [ForeignKey(nameof(idInquilino))]
        public int idInquilino { get; set; }
        public Inquilino? objetoInquilino { get; set; }
        
        [ForeignKey(nameof(idInmueble))]
        public int idInmueble { get; set; }
        public Inmueble? objetoInmueble { get; set; }
    }
}
