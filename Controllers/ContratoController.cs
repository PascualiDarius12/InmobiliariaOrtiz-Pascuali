using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaOrtiz_Pascuali.Models;
using Microsoft.AspNetCore.Authorization;

namespace InmobiliariaOrtiz_Pascuali.Controllers;
[Authorize]
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
       
        ViewBag.Inmuebles = InmR.ObtenerTodos();



        return View();


    }

    public IActionResult Insertar(Contrato contrato)

    {

        ContratoRepo Cr = new ContratoRepo();
        Cr.Insertar(contrato);
        return RedirectToAction(nameof(Index));


    }

    [Authorize]
    public IActionResult Eliminar(int id)

    {

        if (!User.IsInRole("Administrador"))
        {
            string mensaje = "No posee los permisos suficientes para realizar esta accion";
            ViewBag.mensaje = mensaje;
            Console.WriteLine(ViewBag.mensaje);

            return RedirectToAction(nameof(Index));



        }
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

    public IActionResult Pagos(int id)


    {


        ContratoRepo Cr = new ContratoRepo();
        var listaPagos = Cr.ObtenerPagos(id);




        return View(listaPagos);
    }



    //metodo para pagar un contrato de alquiler

    public IActionResult Pagar(int id)
    {

        ContratoRepo repo = new ContratoRepo();
        Pago pago = repo.BuscarPago(id);
        Console.WriteLine(pago.IdContrato);
        Contrato contrato = repo.BuscarContrato(id);




        if (pago != null)
        {
            repo.ActualizarPago(pago);
            return RedirectToAction(nameof(Pagos), new { id = pago.IdContrato });
        }
        else
        {

            return NotFound();
        }
    }





    public IActionResult BuscarPorDNI(string dni)
    {
        ContratoRepo Cr = new ContratoRepo();
        var listaContratos = Cr.BuscarPorDNI(dni);

        return View("Index", listaContratos);
    }






    


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
