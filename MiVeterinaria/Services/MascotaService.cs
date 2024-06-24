using MiVeterinaria.Models;
using MiVeterinaria.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiVeterinaria.Domain.Services
{
    public class MascotaService
    {
        // <summary>
        /// Consulta todas las mascotas 
        /// </summary>
        /// <returns>Retorna todas las mascotas ordenadas descendientemente por la fecha de creación</returns>
        public List<MascotaDb> ConsultarMascotas()
        {
            try
            {
                var document = MongoContext.Database().GetCollection<MascotaDb>("MascotaDb");

                //var response = (from p in document.AsQueryable()select p).ToList();
                var response = (from p in document.AsQueryable() select p).OrderByDescending(x => x.Fecha_Creacion).ToList();

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // <summary>
        /// Crea una mascota nueva
        /// </summary>
        /// <param name="mascota">Objeto de la mascota</param>
        /// <returns>Retorna True si la mascota se creo correctamente</returns>
        public bool CrearMascota(MascotaDb mascota)
        {
            try
            {
                var document = MongoContext.Database().GetCollection<MascotaDb>("MascotaDb");

                document.InsertOne(mascota);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // <summary>
        /// Actualiza una mascota especifica
        /// </summary>
        /// <param name="mascota">Objeto de la mascota</param>
        /// <returns>Retorna True si la mascota se actualizo correctamente y False cuando no se logro actualizar</returns>
        public ResponseDb ActualizarMascota(MascotaDb mascota, string idMascota)
        {
            try
            {
                var document = MongoContext.Database().GetCollection<MascotaDb>("MascotaDb");
                var filter = Builders<MascotaDb>.Filter.Eq(c => c.Id, idMascota);
                var update = Builders<MascotaDb>.Update
                    .Set(u => u.Nombre, mascota.Nombre)
                    .Set(u => u.Edad, mascota.Edad)
                    .Set(u => u.Sexo, mascota.Sexo)
                    .Set(u => u.Tipo_Mascota, mascota.Tipo_Mascota)
                    .Set(u => u.Descripcion_Consulta, mascota.Descripcion_Consulta)
                    .Set(u => u.Nombre_Propietario, mascota.Nombre_Propietario)
                    .Set(u => u.Fecha_Cita, mascota.Fecha_Cita)
                    .Set(u => u.Fecha_Creacion, mascota.Fecha_Creacion)
                    .Set(u => u.Estado, mascota.Estado);

                var result = document.UpdateOne(filter, update);

                ResponseDb response = new ResponseDb() { IsSuccess = Convert.ToBoolean(result.IsAcknowledged) };
                if (response.IsSuccess)
                    response.Message = "Se actualizo correctamente la cita de la mascota";
                else
                    response.Message = "Hubo un error al actualizar la cita de la mascota, por favor vuelva a intentalo";

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // <summary>
        /// Elimina una mascota
        /// </summary>
        /// <param name="idMascota">Id de la mascota</param>
        /// <returns>Retorna True si la mascota se elimino correctamente</returns>
        public ResponseDb EliminarMascota(string idMascota)
        {
            try
            {
                var document = MongoContext.Database().GetCollection<MascotaDb>("MascotaDb");
                var result = document.DeleteOne(x => x.Id == idMascota);

                ResponseDb response = new ResponseDb() { IsSuccess = Convert.ToBoolean(result.IsAcknowledged) };
                if (response.IsSuccess)
                    response.Message = "Se elminnó correctamente la cita de la mascota";
                else
                    response.Message = "Hubo un error al eliminar la cita de la mascota, por favor vuelva a intentalo";

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
