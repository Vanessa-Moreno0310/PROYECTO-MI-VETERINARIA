using MiVeterinaria.Models;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MiVeterinaria.Services
{
    public class ProductoServices
    {
        // <summary>
        /// Consulta todos los productos
        /// </summary>
        /// <returns>Retorna todos los productos creados</returns>
        public List<ProductoDb> ConsultarProductos()
        {
            try
            {
                var document = MongoContext.Database().GetCollection<ProductoDb>("ProductoDb");

                var response = (from p in document.AsQueryable() select p).ToList();

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // <summary>
        /// Crea un nuevo producto
        /// </summary>
        /// <param name="producto">Objeto del producto</param>
        /// <returns>Retorna True si el producto se creo correctamente</returns>
        public bool CrearProducto(ProductoDb producto)
        {
            try
            {
                var document = MongoContext.Database().GetCollection<ProductoDb>("ProductoDb");

                document.InsertOne(producto);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // <summary>
        /// Actualiza un producto especifica
        /// </summary>
        /// <param name="mascota">Objeto del producto</param>
        /// <returns>Retorna True si el producto se actualizo correctamente y False cuando no se logro actualizar</returns>
        public ResponseDb ActualizarProducto(ProductoDb producto)
        {
            try
            {
                var document = MongoContext.Database().GetCollection<ProductoDb>("ProductoDb");
                var filter = Builders<ProductoDb>.Filter.Eq(c => c.Id, producto.Id);
                var update = Builders<ProductoDb>.Update
                    .Set(u => u.Nombre, producto.Nombre)
                    .Set(u => u.Caracteristica, producto.Caracteristica)
                    .Set(u => u.Valor, producto.Valor)
                    .Set(u => u.Imagen, producto.Imagen);

                var result = document.UpdateOne(filter, update);

                ResponseDb response = new ResponseDb() { IsSuccess = Convert.ToBoolean(result.IsAcknowledged) };
                if (response.IsSuccess)
                    response.Message = "Se elminnó correctamente el producto";
                else
                    response.Message = "Hubo un error al eliminar el producto, por favor vuelva a intentalo";

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // <summary>
        /// Elimina un producto
        /// </summary>
        /// <param name="idProducto">Id de la producto</param>
        /// <returns>Retorna True si la producto se elimino correctamente</returns>
        public ResponseDb EliminarProducto(string idProducto)
        {
            try
            {
                var document = MongoContext.Database().GetCollection<ProductoDb>("ProductoDb");
                var result = document.DeleteOne(x => x.Id == idProducto);

                ResponseDb response = new ResponseDb() { IsSuccess = Convert.ToBoolean(result.IsAcknowledged) };
                if (response.IsSuccess)
                    response.Message = "Se elminnó correctamente el producto";
                else
                    response.Message = "Hubo un error al eliminar el producto, por favor vuelva a intentalo";

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}