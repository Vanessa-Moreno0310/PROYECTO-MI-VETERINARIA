using MiVeterinaria.Models;
using MiVeterinaria.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace MiVeterinaria.Domain.Services
{
    [CustomAuthorize]
    public class UsuarioService
    {
        // <summary>
        /// Consulta todos los usuarios
        /// </summary>
        /// <returns>Retorna todos los usuarios creados</returns>
        public List<UsuarioDb> ConsultarUsuarios()
        {
            try
            {
                var document = MongoContext.Database().GetCollection<UsuarioDb>("UsuarioDb");

                var response = (from p in document.AsQueryable() select p).ToList();

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // <summary>
        /// Crea un nuevo usuario
        /// </summary>
        /// <param name="usuario">Objeto del usuario</param>
        /// <returns>Retorna True si el usuario se creo correctamente</returns>
        public bool CrearUsuario(UsuarioDb usuario)
        {
            try
            {
                var document = MongoContext.Database().GetCollection<UsuarioDb>("UsuarioDb");

                document.InsertOne(usuario);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // <summary>
        /// Actualiza un usuario especifico
        /// </summary>
        /// <param name="usuario">Objeto del usuario</param>
        /// <returns>Retorna True si el usuario se actualizo correctamente y False cuando no se logro actualizar</returns>
        public ResponseDb ActualizarUsuario(UsuarioDb usuario, string idUser)
        {
            try
            {
                var document = MongoContext.Database().GetCollection<UsuarioDb>("UsuarioDb");
                var filter = Builders<UsuarioDb>.Filter.Eq(c => c.NumeroDoc, idUser);
                var update = Builders<UsuarioDb>.Update
                    .Set(u => u.Nombre, usuario.Nombre)       
                    .Set(u => u.Apellido, usuario.Apellido)
                    .Set(u => u.TipoDoc, usuario.TipoDoc)
                    .Set(u => u.NumeroDoc, usuario.NumeroDoc)
                    .Set(u => u.Usuario, usuario.Usuario)
                    .Set(u => u.Contraseña, usuario.Contraseña)
                    .Set(u => u.Telefono, usuario.Telefono);

                var result = document.UpdateOne(filter, update);

                ResponseDb response = new ResponseDb() { IsSuccess = Convert.ToBoolean(result.IsAcknowledged) };
                if (response.IsSuccess)
                    response.Message = "Se actualizo correctamente el usuario";
                else
                    response.Message = "Hubo un error al actualizar el usuario, por favor vuelva a intentalo";

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        /// <returns>Retorna True si el usuario se elimino correctamente</returns>
        public ResponseDb EliminarUsuario(string idUsuario)
        {
            try
            {
                var document = MongoContext.Database().GetCollection<UsuarioDb>("UsuarioDb");
                var result = document.DeleteOne(x => x.NumeroDoc == idUsuario);

                ResponseDb response = new ResponseDb() { IsSuccess = Convert.ToBoolean(result.IsAcknowledged) };
                if (response.IsSuccess)
                    response.Message = "Se elminnó correctamente el usuario";
                else
                    response.Message = "Hubo un error al eliminar el usuario, por favor vuelva a intentalo";

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
