using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp06.Models;

namespace Tp06.Controllers;

public class  AccountController: Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }


     private BD bd = new BD();
     [HttpGet]
public IActionResult Login()
{
    return View();
}
    [HttpPost]
public IActionResult Login(string UserName, string password)
{
    List<Usuario> usuarios = bd.Usuarios(); 
    Usuario usuario = null;

    foreach (Usuario i in usuarios)
    {
        if (i.UserName == UserName && i.Password == password)
        {
            usuario = i;
        }
    }

    if (usuario != null)
    {
        HttpContext.Session.SetString("ID", usuario.ID.ToString());
   HttpContext.Session.SetString("UserName", usuario.UserName.ToString());
         usuario.UltimoLogin = DateTime.Now;
        return View("Principal");
    }

    ViewBag.Error = "UserName o contraseña incorrectos.";
    return View();
}
 [HttpGet]
public IActionResult Registro()
{
    return View();
}

[HttpPost]
public IActionResult Registro(string Nombre, string Apellido, string password, string Foto, string UserName)
{
    if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellido) || string.IsNullOrEmpty(password) ||string.IsNullOrEmpty(UserName) || bd.ObtenerUsuarioPorUsername(UserName)!=null )
    {
        ViewBag.Error = "Nombre, apellido, Username y contraseña son obligatorios. o Username ya utilizado";
        return View();
    }
    Usuario nuevoUsuario = new Usuario
    {
        Nombre = Nombre,
        Apellido = Apellido,
        Password = password,
        Foto = Foto,
        UserName = UserName,
        UltimoLogin = DateTime.Now
    };
    bd.AgregarUsuario(nuevoUsuario);
  return RedirectToAction("Login");

}
}