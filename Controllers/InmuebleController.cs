using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaOrtiz_Pascuali.Models;

namespace InmobiliariaOrtiz_Pascuali.Controllers;

public class InmuebleController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public InmuebleController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        InmuebleRepo Ir = new InmuebleRepo();
        var listaInmuebles = Ir.ObtenerTodos();
        return View(listaInmuebles);
    }

    public IActionResult Registrar(int id)
    { 
        PropietarioRepo repoProp = new PropietarioRepo();
        ViewBag.Propietarios = repoProp.getPropietarios();
        return View();


    }

     public IActionResult Editar(int id)
    { 
        PropietarioRepo repoProp = new PropietarioRepo();
        ViewBag.Propietarios = repoProp.getPropietarios();
        InmuebleRepo repo = new InmuebleRepo();
        Inmueble inmueble = repo.BuscarInmueble(id);
        return View(inmueble);


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
}
