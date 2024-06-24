using Microsoft.IdentityModel.Tokens;
using MiVeterinaria.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace MiVeterinaria.Services
{
    public class AccountServices
    {
    }

    public class AuthService
    {
        public string Authenticate(string username, string password)
        {
            var SecretKey = ConfigurationManager.AppSettings["JwtSecretKey"];
            var document = MongoContext.Database().GetCollection<UsuarioDb>("UsuarioDb");
            var user = document.Find(u => u.Usuario == username && u.Contraseña == password).FirstOrDefault();

            if (user == null)
            {
                return null;
            }
            // Generar un nuevo Guid para KeyId
            var keyId = Guid.NewGuid().ToString();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.Usuario),
                new Claim(ClaimTypes.Role, user.Rol)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }

}