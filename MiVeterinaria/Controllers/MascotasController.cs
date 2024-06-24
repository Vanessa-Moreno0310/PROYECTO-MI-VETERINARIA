using MiVeterinaria.Domain.Services;
using MiVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiVeterinaria.Controllers
{
    public class MascotasController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult VerMascotas()
        {
            MascotaService mascotaService = new MascotaService();

            List<MascotaDb> mascotas = new List<MascotaDb>();
            mascotas = mascotaService.ConsultarMascotas();
            return Json(mascotas, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult InsertarMascota(MascotaDb mascota)
        {
            MascotaService mascotaService = new MascotaService();

            var response = mascotaService.CrearMascota(mascota);

            return Json(response);
        }

        [CustomAuthorize]
        [HttpPut]
        public ActionResult ActualizarMascota(MascotaDb model, string idMascota)
        {
            MascotaService mascotaService = new MascotaService();
            ResponseDb response = mascotaService.ActualizarMascota(model, idMascota);

            return Json(response);
        }

        [CustomAuthorize]
        [HttpDelete]
        public ActionResult EliminarMascota(string idMascota)
        {
            MascotaService mascotaService = new MascotaService();
            ResponseDb response = mascotaService.EliminarMascota(idMascota);

            return Json(response);
        }
    }
}
