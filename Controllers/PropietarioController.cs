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
        PropietarioRepo repo = new PropietarioRepo();
        List<Propietario> propietario = repo.ObtenerPropietarios();

        return View(propietario);
    }

[HttpGet]
public ActionResult CrearPropietario()
    {
        return View();
    }



[HttpPost]
    public ActionResult CrearPropietario(Propietario p)
    {


        try
        {

            PropietarioRepo repositorio = new PropietarioRepo();
            repositorio.Alta(p);
            return RedirectToAction("Index");
        }
        catch (System.Exception)
        {
            throw;
        }
    }




}

