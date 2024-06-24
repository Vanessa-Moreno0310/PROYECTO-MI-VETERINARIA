using MiVeterinaria.Models;
using MiVeterinaria.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiVeterinaria.Controllers
{
    [CustomAuthorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ConsultarProducto()
        {
            ProductoServices services = new ProductoServices();
            List<ProductoDb> response = services.ConsultarProductos();

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InsertarProducto(ProductoDb modelo, HttpPostedFileBase imagen)
        {
            ProductoServices services = new ProductoServices();

            if (imagen != null && imagen.ContentLength > 0)
            {
                var fileName = Path.GetFileName(imagen.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                imagen.SaveAs(path);
                modelo.Imagen = "/Content/Images/" + fileName; // Ruta relativa para la imagen
            }

            var response = services.CrearProducto(modelo);

            return Json(response);
        }

        [HttpPut]
        public ActionResult ActualizarProducto(ProductoDb modelo, string idProducto)
        {
            ProductoServices service = new ProductoServices();
            var response = service.ActualizarProducto(modelo, idProducto);

            return Json(response);
        }

        [HttpDelete]
        public ActionResult EliminarProducto(string idProducto)
        {
            ProductoServices service = new ProductoServices();
            var response = service.EliminarProducto(idProducto);

            return Json(response);
        }
    }
}