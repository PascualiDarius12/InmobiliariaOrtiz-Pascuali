/*

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InmobiliariaOrtiz_Pascuali.Models;
using System.Diagnostics;

namespace InmobiliariaOrtiz_Pascuali.Controllers
{
    public class ContratoController : Controller
    {
        private readonly ILogger<ContratoController> _logger;
        private readonly InquilinoRepo repoInquilino;
        private readonly InmuebleRepo repoInmueble;

        public ContratoController(ILogger<ContratoController> logger, InquilinoRepo repoInquilino, InmuebleRepo repoInmueble)
        {
            _logger = logger;
            this.repoInquilino = repoInquilino;
            this.repoInmueble = repoInmueble;
        }

        public IActionResult Index()
        {
            ContratoRepo cr = new ContratoRepo();
            var listaContratos = cr.ObtenerTodos();
            return View(listaContratos);
        }

        public IActionResult Registrar()
        {
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
            Contrato contrato = repoContrato.ObtenerTodos();
            return View(contrato);
        }

        public IActionResult Modificar(Contrato contrato)
        {
            ContratoRepo repo = new ContratoRepo();
            repo.ModificarContrato(contrato);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Insertar(Contrato contrato)
        {
            ContratoRepo repo = new ContratoRepo();
            repoInmueble.Insertar(contrato);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Eliminar(int id)
        {
            ContratoRepo repo = new ContratoRepo();
            repo.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InmobiliariaOrtiz_Pascuali.Models;
using System.Diagnostics;
using System.Collections.Generic;

namespace InmobiliariaOrtiz_Pascuali.Controllers
{



 public class ContratoController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public ContratoController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

/*
    public class ContratoController : Controller
    {
        private readonly ILogger<ContratoController> _logger;
        private readonly InquilinoRepo repoInquilino;
        private readonly InmuebleRepo repoInmueble;

        public ContratoController(ILogger<ContratoController> logger, InquilinoRepo repoInquilino, InmuebleRepo repoInmueble)
        {
            _logger = logger;
            this.repoInquilino = repoInquilino;
            this.repoInmueble = repoInmueble;
        }
*/





        public IActionResult Index()
        {
            ContratoRepo cr = new ContratoRepo();
            IList<Contrato> listaContratos = cr.ObtenerTodos();
            return View(listaContratos);
        }

        public IActionResult Registrar(Contrato contrato)
        {
             InquilinoRepo repoInq = new InquilinoRepo();
             ViewBag.Inquilinos = repoInq.GetInquilinos();
        //ViewBag.Propietarios = repoProp.getPropietarios();
        return View();
            //ContratoRepo contratoRepo = new ContratoRepo();
            //contratoRepo.Insertar(contrato);
            //ViewBag.inquilino = contratoRepo.ObtenerTodos();
            //return View();
        }


/*
        public IActionResult Editar(int id)
        {
            PropietarioRepo repoProp = new PropietarioRepo();
            ViewBag.Propietarios = repoProp.getPropietarios();

            InmuebleRepo repoInmueble = new InmuebleRepo();
            ViewBag.Inmuebles = repoInmueble.GetInmuebles();

            InquilinoRepo repoInq = new InquilinoRepo();
            ViewBag.Inquilinos = repoInq.GetInquilinos();

            ContratoRepo repoContrato = new ContratoRepo();
            Contrato contrato = repoContrato.BuscarContrato(id);
            return View(contrato);
        }
*/

public IActionResult Editar(int id)
{
    PropietarioRepo repoProp = new PropietarioRepo();
    ViewBag.Propietarios = repoProp.getPropietarios();

    InmuebleRepo repoInmueble = new InmuebleRepo();
    ViewBag.Inmuebles = repoInmueble.GetInmuebles();

    InquilinoRepo repoInq = new InquilinoRepo();
    ViewBag.Inquilinos = repoInq.GetInquilinos();

    ContratoRepo repoContrato = new ContratoRepo();
    Contrato contrato = repoContrato.BuscarContrato(id); // Pasar el id del contrato
    return View(contrato);
}



        [HttpPost]
        public IActionResult Modificar(Contrato contrato)
        {
            ContratoRepo repo = new ContratoRepo();
            repo.ModificarContrato(contrato);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Insertar(Contrato contrato)
        {
            ContratoRepo repo = new ContratoRepo();
            repo.Insertar(contrato);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            ContratoRepo repo = new ContratoRepo();
            repo.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
