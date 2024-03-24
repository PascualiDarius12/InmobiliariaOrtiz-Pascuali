using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaOrtiz_Pascuali.Models;

namespace InmobiliariaOrtiz_Pascuali.Controllers;

public class PropietarioController : Controller
{
    private readonly ILogger<PropietarioController> _logger;

    public PropietarioController(ILogger<PropietarioController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        PropietarioRepo pr = new PropietarioRepo();
        var listaPropietarios = pr.getPropietarios();
        return View(listaPropietarios);
    }
    public IActionResult editar(int id)
    {
        //logica para mostrar el formulario vacio o lleno con el propietario que queremos editar
        if (id > 0)
        {
            PropietarioRepo repo = new PropietarioRepo();
            var propietario = repo.buscarPropietario(id);
            return View(propietario);
        }
        else
        {
            return View();
        }


    }
    public IActionResult Insertar(Propietario propietario)
    {
        PropietarioRepo repo = new PropietarioRepo();
        if (propietario.IdPropietario > 0)
        {
            repo.Modificar(propietario);

        }
        else
        {
            repo.Insertar(propietario);
        }



        return RedirectToAction(nameof(Index));
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    
    public IActionResult Eliminar(int id)
    {
        Console.WriteLine(id);
        PropietarioRepo repo = new PropietarioRepo();
        var resultado = repo.Eliminar(id);
        if (resultado == -1)
        {
            TempData["Error"] = "Ocurrió un error al eliminar el propietario.";
        }
        return RedirectToAction(nameof(Index));
    }

}






