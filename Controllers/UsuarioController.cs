using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaOrtiz_Pascuali.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace InmobiliariaOrtiz_Pascuali.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<PropietarioController> _logger;
    private readonly UsuarioRepo ur = new UsuarioRepo();
    private readonly IConfiguration configuration;
    private readonly IWebHostEnvironment environment;

    public UsuarioController(IConfiguration configuration, IWebHostEnvironment environment, ILogger<PropietarioController> logger)
    {
        _logger = logger;
        this.configuration = configuration;
        this.environment = environment;

    }

    public IActionResult Index()
    {

        var listaUsuarios = ur.getUsuarios();
        return View(listaUsuarios);
    }

    public IActionResult Registro()

    {
        ViewBag.Roles = Usuario.ObtenerRoles();


        return View();


    }

    [HttpPost]
    public IActionResult Registro(Usuario usuario)

    {
        // if (ModelState.IsValid) //este para saber que el objeto q trae el formulario  corresponde al modelo
        // {
        usuario = ur.crearClave(usuario, configuration);

        // var nbreRnd = Guid.NewGuid();//posible nombre aleatorio
        usuario.IdUsuario = ur.Crear(usuario);

        if (usuario.AvatarFile != null && usuario.IdUsuario > 0)
        {
            Console.WriteLine("entro");
            string wwwPath = environment.WebRootPath;
            string path = Path.Combine(wwwPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = "avatar_" + usuario.IdUsuario + Path.GetExtension(usuario.AvatarFile.FileName);
            string pathCompleto = Path.Combine(path, fileName);
            usuario.Avatar = Path.Combine("/Uploads", fileName);

            // Esta operación guarda la foto en memoria en la ruta que necesitamos
            using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
            {
                usuario.AvatarFile.CopyTo(stream);
            }
            ur.Modificacion(usuario);
        }
        return RedirectToAction(nameof(Index));
        // }


        // return RedirectToAction(nameof(Registro));


    }

    // GET: Usuarios/Login/
    public ActionResult Login(string returnUrl)
    {
        TempData["returnUrl"] = returnUrl;
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginView login)
    {

        Usuario usuario = new Usuario();
        usuario.Email = login.Email;
        usuario.Clave = login.Clave;
        try
        {
            //esto permite que el usuario cuando se logue lo redireccione a la url q estaba y no al home como defecto
            // var returnUrl = String.IsNullOrEmpty(TempData["returnUrl"] as string) ? "/Home" : TempData["returnUrl"].ToString();

            usuario = ur.crearClave(usuario, configuration);

            var e = ur.ObtenerPorEmail(usuario.Email);
            if (e == null || e.Clave != usuario.Clave)
            {
                ModelState.AddModelError("", "El email o la clave no son correctos");
                // TempData["returnUrl"] = returnUrl;
                return View();
            }

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, e.Email),
                        new Claim("FullName", e.Nombre + " " + e.Apellido),
                        new Claim(ClaimTypes.Role, e.RolNombre),
                        new Claim("urlAvatar",e.Avatar)
                    };

            var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
            TempData.Remove("returnUrl");
            return RedirectToAction(nameof(Index));


        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }
    }
    // public IActionResult editar(int id)
    // {
    //     //logica para mostrar el formulario vacio o lleno con el propietario que queremos editar
    //     if (id > 0)
    //     {
    //         PropietarioRepo repo = new PropietarioRepo();
    //         var propietario = repo.buscarPropietario(id);
    //         return View(propietario);
    //     }
    //     else
    //     {
    //         return View();
    //     }


    // }
    // public IActionResult Insertar(Propietario propietario)
    // {
    //     PropietarioRepo repo = new PropietarioRepo();
    //     if (propietario.IdPropietario > 0)
    //     {
    //         repo.Modificar(propietario);

    //     }
    //     else
    //     {
    //         repo.Insertar(propietario);
    //     }



    //     return RedirectToAction(nameof(Index));
    // }


    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }



    // public IActionResult Eliminar(int id)
    // {
    //     Console.WriteLine(id);
    //     PropietarioRepo repo = new PropietarioRepo();
    //     var resultado = repo.Eliminar(id);
    //     if (resultado == -1)
    //     {
    //         TempData["Error"] = "Ocurrió un error al eliminar el propietario.";
    //     }
    //     return RedirectToAction(nameof(Index));
    // }





    public IActionResult BuscarPorDNI(string dni)
    {
        PropietarioRepo repo = new PropietarioRepo();
        var propietarios = repo.BuscarPorDNI(dni);
        return View("Index", propietarios);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Usuario"); // Redirige al inicio de sesión
    }
}









