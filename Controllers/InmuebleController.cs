using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaOrtiz_Pascuali.Models;
using Microsoft.AspNetCore.Authorization;

namespace InmobiliariaOrtiz_Pascuali.Controllers;
[Authorize]
public class InmuebleController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public InmuebleController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.mensaje = TempData["mensaje"];
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
    [Authorize]
    public IActionResult Eliminar(int id)

    {
        
        if (!User.IsInRole("Administrador"))
    {
        string mensaje = "No posee los permisos suficientes para realizar esta accion";
            
            TempData["mensaje"] = mensaje;
        
        return RedirectToAction(nameof(Index));
        
       
        
    }
        InmuebleRepo repo = new InmuebleRepo();
        repo.Eliminar(id);

        return RedirectToAction(nameof(Index));


    }





    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }





public IActionResult BuscarPorDireccion(string direccion)
{
    InmuebleRepo repo = new InmuebleRepo();
    var inmuebles = repo.BuscarPorDireccion(direccion);
    return View("Index", inmuebles);
}

   

}
