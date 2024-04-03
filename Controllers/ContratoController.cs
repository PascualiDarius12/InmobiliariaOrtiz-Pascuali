using Microsoft.AspNetCore.Mvc;
using InmobiliariaOrtiz_Pascuali.Models;

namespace InmobiliariaOrtiz_Pascuali.Controllers
{
    public class ContratoController : Controller
    {
 
        // Declaración de una variable privada readonly de tipo ILogger<PropietarioController>.
        private readonly ILogger<ContratoController> _logger;
        private readonly InquilinoRepo repoInquilino;
        //private readonly PropietarioRepo repoPropietario;
        private readonly InmuebleRepo repoInmueble;

       
       

        // Constructor de la clase PropietarioController que 
        //recibe un parámetro de tipo ILogger<PropietarioController>.
        public ContratoController(ILogger<ContratoController> logger, InquilinoRepo repoInquilino, InmuebleRepo repoInmueble)
        {
            _logger = logger; // Asignación del parámetro logger a la variable _logger.
            this.repoInquilino = repoInquilino;
            this.repoInmueble = repoInmueble;
        }
        







        public IActionResult Index()
        {
            ContratoRepo cr = new ContratoRepo();
            var listaContratos = cr.ObtenerTodos();
            return View(listaContratos);
        }
    }





}//fin




/*
public IActionResult Registrar(){

    ContratoRepo contratoRepo = new ContratoRepo();
    ViewBag.inquilino = contratoRepo.ObtenerTodos();
    return View();
}




public IActionResult Editar(int id)
{
   

    PropietarioRepo repoProp = new PropietarioRepo();
    ViewBag.Propietarios = repoProp.getPropietarios();


    InmuebleRepo repoInmueble = new InmuebleRepo();
    ViewBag.Inmuebles = repoInmueble.GetInmuebles();

    InquilinoRepo repoInq = new InquilinoRepo();
    ViewBag.Inquilinos = repoInq.GetInquilinos();

    ContratoRepo repoContrato = new ContratoRepo();
    Contrato contrato= repoContrato.ObtenerTodos();
    return View(contrato);


}
public IActionResult Modificar(Inmueble inmueble)
{
    InmuebleRepo repo = new InmuebleRepo();
    // Inmueble inmueble = repo.BuscarInmueble(id);

    repo.ModificarInmueble(inmueble);



    return RedirectToAction(nameof(Index));
}



public IActionResult Insertar(Inmueble inmueble)

{
    Console.WriteLine(inmueble.IdPropietario);
    InmuebleRepo repoInmueble = new InmuebleRepo();
    repoInmueble.Insertar(inmueble);
    return RedirectToAction(nameof(Index));


}

public IActionResult Eliminar(int id)

{
    InmuebleRepo repo = new InmuebleRepo();
    repo.Eliminar(id);

    return RedirectToAction(nameof(Index));


}





[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public IActionResult Error()
{
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}

*/
