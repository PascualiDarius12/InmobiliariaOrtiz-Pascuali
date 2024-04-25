using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaOrtiz_Pascuali.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Authorization;


namespace InmobiliariaOrtiz_Pascuali.Controllers;
[Authorize]
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










    [Authorize]
    public IActionResult Registro()

    {
        if (!User.IsInRole("Administrador"))
        {
            string mensaje = "No posee los permisos suficientes para realizar esta accion";
            ViewBag.mensaje = mensaje;
            Console.WriteLine(ViewBag.mensaje);

            return RedirectToAction(nameof(Index));



        }
        ViewBag.Roles = Usuario.ObtenerRoles();


        return View();


    }

    [HttpPost]
    [Authorize]
    public IActionResult Registro(Usuario usuario)

    {
        if (!User.IsInRole("Administrador"))
        {
            string mensaje = "No posee los permisos suficientes para realizar esta accion";
            ViewBag.mensaje = mensaje;
            Console.WriteLine(ViewBag.mensaje);

            return RedirectToAction(nameof(Index));



        }
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
[AllowAnonymous]
    public ActionResult Login(string returnUrl)
    {
        TempData["returnUrl"] = returnUrl;
        return View();
    }


    [HttpPost]
[AllowAnonymous]
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
                ViewBag.Mensaje = "El email o la clave no son correctos";
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
    [Authorize]
    public IActionResult Editar(int id)
    {
        if (!User.IsInRole("Administrador"))
        {
            string mensaje = "No posee los permisos suficientes para realizar esta accion";
            ViewBag.mensaje = mensaje;
            Console.WriteLine(ViewBag.mensaje);

            return RedirectToAction(nameof(Index));



        }

        var usuario = ur.ObtenerPorId(id);

        if (usuario.Email == User.Identity.Name)
        {
            return View("Perfil",usuario);
        }

        ViewBag.Roles = Usuario.ObtenerRoles();


        return View(usuario);
    }








    [HttpPost]
    [Authorize]
    public IActionResult Editar(Usuario usuario)
    {
        if (!User.IsInRole("Administrador"))
        {
            string mensaje = "No posee los permisos suficientes para realizar esta accion";
            ViewBag.mensaje = mensaje;
            Console.WriteLine(ViewBag.mensaje);

            return RedirectToAction(nameof(Index));



        }
        //hasheamos nueva contrasena si se modifico
        Usuario UsuarioBuscado = ur.ObtenerPorId(usuario.IdUsuario);
        if (usuario.Clave != UsuarioBuscado.Clave)
        {
            usuario = ur.crearClave(usuario, configuration);


        }
        // Verificar si el usuario tiene un avatar existente con la misma ruta
        if (usuario.AvatarFile != null)
        {
            string wwwPath = environment.WebRootPath;
            string path = Path.Combine(wwwPath, "Uploads");
            string fileName = "avatar_" + usuario.IdUsuario + Path.GetExtension(usuario.AvatarFile.FileName);
            string pathCompleto = Path.Combine(path, fileName);

            // Si el avatar existente tiene la misma ruta, sobrescribe el archivo
            if (System.IO.File.Exists(pathCompleto))
            {
                // Borra el archivo existente antes de guardar el nuevo
                System.IO.File.Delete(pathCompleto);
            }

            // Guarda el nuevo archivo de avatar
            using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
            {
                usuario.AvatarFile.CopyTo(stream);
            }

            usuario.Avatar = Path.Combine("/Uploads", fileName);
        }
        else
        {
            usuario.Avatar = "~/Uploads/usuario.jpg";
            
        }

        //agregamos el rol si se dejo en blanco o edito perfil sin rol
        if(usuario.Rol==0){
          usuario.Rol = UsuarioBuscado.Rol;
        }
        

        //  modificación en la base de datos
        int resultado = ur.Modificacion(usuario);
        if (resultado > 0)
        {


            ViewBag.Roles = Usuario.ObtenerRoles();

            return RedirectToAction(nameof(Index));
        }
        else
        {
            ViewBag.Roles = Usuario.ObtenerRoles();
            ViewBag.mensaje = "No se puedo guardar los datos";
            return View(usuario);

        }





    }

    [Authorize]
    public ActionResult Perfil()
    {
        if (!User.IsInRole("Administrador"))
        {

            ViewData["Title"] = "Mi perfil";
            var u = ur.ObtenerPorEmail(User.Identity.Name);
            return View("Perfil", u);


        }
        else
        {
            ViewData["Title"] = "Mi perfil";
            var u = ur.ObtenerPorEmail(User.Identity.Name);
            ViewBag.Roles = Usuario.ObtenerRoles();
            return View("Editar", u);
        }
    }

   

    //eliminar un usuario
    [Authorize]
    public async Task<IActionResult> Eliminar(int id)
    {

        if (!User.IsInRole("Administrador"))
        {
            string mensaje = "No posee los permisos suficientes para realizar esta accion";
            ViewBag.mensaje = mensaje;
            Console.WriteLine(ViewBag.mensaje);

            return RedirectToAction(nameof(Index));



        }
        Console.WriteLine(id);
        var resultado = ur.Eliminar(id);
        if (resultado == -1)
        {
            TempData["Error"] = "Ocurrió un error al eliminar el usuario.";
        }

        // Cerrar la sesión actual
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // aca  nos redirige al inicio de sesion

        return RedirectToAction(nameof(Login));
    }


    //buscador por nombre
    [HttpGet]
    public IActionResult BuscarPorNombre(string nombre)
    {
        var listaUsuarios = ur.BuscarPorNombre(nombre);
        return View("Index", listaUsuarios);
    }




}
