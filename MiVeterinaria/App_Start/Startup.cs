using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using MiVeterinaria.Handler;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MiVeterinaria.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var secretKey = ConfigurationManager.AppSettings["JwtSecretKey"];
            var key = Convert.FromBase64String(secretKey);

            app.Use(typeof(TokenMiddleware));
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "yourissuer", // Reemplaza con tu issuer
                    ValidAudience = "youraudience", // Reemplaza con tu audiencia
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }
            });

        }

    }
}