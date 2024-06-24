using MiVeterinaria.Models;
using MiVeterinaria.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Web;
using System;
using System.Web.Mvc;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Configuration;
using Newtonsoft.Json;

public class AccountController : Controller
{
    private readonly AuthService _authService;

    public AccountController()
    {
        _authService = new AuthService();
    }

    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login(AccountDb model)
    {
        if (ModelState.IsValid)
        {
            var token = _authService.Authenticate(model.NombreUsuario, model.Contraseña);
            if (token == null)
            {
                ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos");
                return Json(new { success = false });
            }

            // Guardar el token en una cookie o en el local storage
            var cookie = new HttpCookie("AuthToken", token)
            {
                HttpOnly = true,
                Secure = Request.IsSecureConnection,
                Expires = DateTime.UtcNow.AddHours(1)
            };
            Response.Cookies.Add(cookie);

            // Redirigir a la página específica según el rol
            CustomAuthorizeAttribute customAuthorize = new CustomAuthorizeAttribute();
            var secretKey = ConfigurationManager.AppSettings["JwtSecretKey"];
            var userRole = customAuthorize.ValidateTokenAndGetRole(token, secretKey);

            if (userRole == "Administrador" || userRole == "Cliente")
            {
                return Json(new { success = true, token = token });
            }

            // Si no se encuentra el rol o no es específico, redirigir al logueo por defecto
            return Json(new { success = false });
        }

        return Json(new { success = false });
    }

    [HttpGet]
    public ActionResult Logout()
    {
        // Eliminar la cookie de autenticación
        if (Request.Cookies["AuthToken"] != null)
        {
            var cookie = new HttpCookie("AuthToken")
            {
                Expires = DateTime.Now.AddDays(-1),
                Value = null
            };
            Response.Cookies.Add(cookie);
        }

        return RedirectToAction(nameof(Index));
    }
}
