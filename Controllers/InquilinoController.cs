using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaOrtiz_Pascuali.Models;

namespace InmobiliariaOrtiz_Pascuali.Controllers;

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
        var listaInquilino = pr.getInquilino();
        return View(listaInquilino);
    }
    public IActionResult editar(int id)
    {
        //logica para mostrar el formulario vacio o lleno con el propietario que queremos editar
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



    [HttpPost]
    public IActionResult Eliminar(int id)
    {
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
}
