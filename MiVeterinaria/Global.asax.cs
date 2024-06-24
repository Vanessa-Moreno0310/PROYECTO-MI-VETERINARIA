using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Builder;
using MiVeterinaria.App_Start;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MiVeterinaria
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Inicia OWIN al inicio de la aplicación
            OwinStartup();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void OwinStartup()
        {
            var startup = new Startup();
            IAppBuilder app = new AppBuilder();
            startup.Configuration(app);
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            // Borrar cookies de autenticación al inicio de la sesión
            if (HttpContext.Current.Request.Cookies["AuthToken"] != null)
            {
                var cookie = new HttpCookie("AuthToken");
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        //protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        //{
        //    HttpCookie authCookie = Request.Cookies["AuthToken"];
        //    if (authCookie != null)
        //    {
        //        var token = authCookie.Value;
        //        if (!string.IsNullOrEmpty(token))
        //        {
        //            var tokenHandler = new JwtSecurityTokenHandler();
        //            var key = Convert.FromBase64String(ConfigurationManager.AppSettings["JwtSecretKey"]);
        //            SecurityToken validatedToken;
        //            var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
        //            {
        //                ValidateIssuer = true,
        //                ValidateAudience = true,
        //                ValidIssuer = "yourissuer", // Reemplaza con tu issuer
        //                ValidAudience = "youraudience", // Reemplaza con tu audiencia
        //                IssuerSigningKey = new SymmetricSecurityKey(key),
        //                ValidateIssuerSigningKey = true
        //            }, out validatedToken);

        //            HttpContext.Current.User = claimsPrincipal;
        //        }
        //    }
        //}
    }
}
