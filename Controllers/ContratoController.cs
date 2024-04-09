using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaOrtiz_Pascuali.Models;

namespace InmobiliariaOrtiz_Pascuali.Controllers;

public class ContratoController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public ContratoController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()


    {


        ContratoRepo Cr = new ContratoRepo();
        var listaContratos = Cr.ObtenerTodos();



        return View(listaContratos);
    }

    public IActionResult Registrar(int id)
    {
        InquilinoRepo Ir = new InquilinoRepo();

        ViewBag.Inquilinos = Ir.GetInquilinos();
        InmuebleRepo InmR = new InmuebleRepo();
        //obtener solo los inmuebles disponibles
        // ViewBag.Inmuebles = InmR.ObtenerTodos().Where(inmueble => inmueble.Estado==true).ToList();
        ViewBag.Inmuebles = InmR.ObtenerTodos();



        return View();


    }

    public IActionResult Insertar(Contrato contrato)

    {

        ContratoRepo Cr = new ContratoRepo();
        Cr.Insertar(contrato);
        return RedirectToAction(nameof(Index));


    }

    public IActionResult Eliminar(int id)

    {
        ContratoRepo repo = new ContratoRepo();
        repo.Eliminar(id);

        return RedirectToAction(nameof(Index));


    }
    public IActionResult Editar(int id)
    {
        InquilinoRepo repoInq = new InquilinoRepo();
        ViewBag.Inquilinos = repoInq.GetInquilinos();
        InmuebleRepo repoInm = new InmuebleRepo();
        ViewBag.Inmuebles = repoInm.ObtenerTodos();
        ContratoRepo repo = new ContratoRepo();
        Contrato contrato = repo.BuscarContrato(id);
        return View(contrato);


    }
    public IActionResult Modificar(Contrato contrato)
    {
        ContratoRepo repo = new ContratoRepo();


        repo.ModificarContrato(contrato);



        return RedirectToAction(nameof(Index));
    }

    public IActionResult Detalle(int id)
    {
        ContratoRepo repo = new ContratoRepo();
        Contrato contrato = repo.BuscarContrato(id);
        PropietarioRepo pr = new PropietarioRepo();
        ViewBag.Propietario = pr.buscarPropietario(contrato.inmueble.IdPropietario);

        return View(contrato);

    }



    //metodo para pagar un contrato de alquiler

    public IActionResult Pagar(int id)
    {

        ContratoRepo repo = new ContratoRepo();
        Contrato contrato = repo.BuscarContrato(id);
       

        if (contrato != null)
        {
            return View(contrato); // Pasar un solo objeto Contrato a la vista
        }
        else
        {
            
            return NotFound();
        }
    }


/*

    [HttpPost]
    public IActionResult RealizarPago(int contratoId)
    {
        // Aquí puedes agregar la lógica para procesar el pago del contrato
        // Puedes acceder al ID del contrato enviado desde el formulario a través del parámetro contratoId
        return RedirectToAction(nameof(Index)); // Redirige a la página de contratos después de realizar el pago
    }
*/


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
