using Microsoft.AspNetCore.Mvc;
using InmobiliariaOrtiz_Pascuali.Models;

namespace InmobiliariaOrtiz_Pascuali.Controllers;

    public class PagoController : Controller
    {
        //private readonly PagoRepo _pagoRepo;
     private readonly ILogger<HomeController> _logger;

        public PagoController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            PagoRepo pagoRepo = new PagoRepo();
            var listaPagos = pagoRepo.ObtenerPagos(1);
            return View(listaPagos);
        }
    }

