using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaOrtiz_Pascuali.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;


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




    /*
    public IActionResult editar(int id)
    {
        var usuario = ur.ObtenerPorId(id);

        if (usuario == null)
        {
            return NotFound();
        }
    ViewBag.Roles = Usuario.ObtenerRoles();
        // Obtener los roles como un diccionario de int y string
        var rolesDict = Usuario.ObtenerRoles();

        // Convertir el diccionario a una lista de SelectListItem
        var rolesList = rolesDict.Select(r => new SelectListItem { Value = r.Key.ToString(), Text = r.Value }).ToList();

        // Asignar la lista de roles a ViewBag.Roles
        ViewBag.Roles = rolesList;

        return View(usuario);
    }

    */
    public IActionResult editar(int id)
    {

        var usuario = ur.ObtenerPorId(id);

        if (usuario == null)
        {
            return NotFound();
        }
        // lo convierto pero no tira , larga error
        ViewBag.Roles = Usuario.ObtenerRoles()
            .Select(r => new SelectListItem { Value = r.Key.ToString(), Text = r.Value })
            .ToList();

        return View(usuario);
    }








    [HttpPost]
    public IActionResult editar(Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            //  modificación en la base de datos
            int resultado = ur.Modificacion(usuario);
            if (resultado > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // no se pudo modificar el usuario
                ModelState.AddModelError(string.Empty, "No se pudo guardar la modificación del usuario.");
            }
        }
        // Si hay errores de validación o si no se pudo modificar el usuario
        // vuelve a mostrar el formulario de edición con los errores
        ViewBag.Roles = Usuario.ObtenerRoles();
        return View(usuario);
    }





    //eliminar un usuario
    public IActionResult Eliminar(int id)
    {
        Console.WriteLine(id);
        var resultado = ur.Eliminar(id);
        if (resultado == -1)
        {
            TempData["Error"] = "Ocurrió un error al eliminar el usuario.";
        }
        return RedirectToAction(nameof(Index));
    }






}