using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaOrtiz_Pascuali.Models;
using Microsoft.AspNetCore.Authorization;

namespace InmobiliariaOrtiz_Pascuali.Controllers;
[Authorize]
public class InquilinoController : Controller
{
    private readonly ILogger<InquilinoController> _logger;

    public InquilinoController(ILogger<InquilinoController> logger)
    {
        _logger = logger;
    }

   public IActionResult Index()
    {
        InquilinoRepo pr = new InquilinoRepo();
        var listaInquilinos = pr.GetInquilinos();
        return View(listaInquilinos);
    }
    public IActionResult editar(int id)
    {
        Console.WriteLine(id);
        
        if (id > 0)
        {
            InquilinoRepo repo = new InquilinoRepo();
            var inquilino = repo.buscarInquilino(id);
            return View(inquilino);
        }
        else
        {
            return View();
        }


    }
    public IActionResult Insertar(Inquilino inquilino)
    {
        InquilinoRepo repo = new InquilinoRepo();
        if (inquilino.IdInquilino > 0)
        {
            repo.Modificar(inquilino);

        }
        else
        {
            repo.Insertar(inquilino);
        }



        return RedirectToAction(nameof(Index));
    }



 [Authorize]
    public IActionResult Eliminar(int id)
    
    {
        
        if (!User.IsInRole("Administrador"))
    {
        string mensaje =  "No posee los permisos suficientes para realizar esta accion";
        ViewBag.mensaje = mensaje;
        Console.WriteLine(ViewBag.mensaje);
        
        return RedirectToAction(nameof(Index));
        
       
        
    }
        InquilinoRepo repo = new InquilinoRepo();
        var resultado = repo.Eliminar(id);
        if (resultado == -1)
        {
            TempData["Error"] = "Ocurrió un error al eliminar el propietario.";
        }
        return RedirectToAction(nameof(Index));
    }

    





    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }





    public IActionResult BuscarPorDNI(string dni)
{
    InquilinoRepo repo = new InquilinoRepo();
    var inquilinos = repo.BuscarPorDNI(dni);
    return View("Index", inquilinos);
}

}
