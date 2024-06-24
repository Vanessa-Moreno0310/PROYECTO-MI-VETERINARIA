using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiVeterinaria.Models
{
    public class ProductoDb
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Caracteristica { get; set; }
        public decimal Valor { get; set; }
        public string Imagen { get; set; }
    }
}