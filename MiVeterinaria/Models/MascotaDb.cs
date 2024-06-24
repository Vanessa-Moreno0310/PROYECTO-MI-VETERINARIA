using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiVeterinaria.Models
{
    public class MascotaDb
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Edad { get; set; }
        public string Sexo { get; set; }
        public string Tipo_Mascota { get; set; }
        public string Descripcion_Consulta { get; set; }
        public string Nombre_Propietario { get; set; }
        public string Fecha_Cita { get; set; }
        public string Fecha_Creacion { get; set; }
        public string Estado { get; set; }

    }
}