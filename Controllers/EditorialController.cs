using AccentureAccademySchultenAlejandro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccentureAccademySchultenAlejandro.Controllers
{
    public class EditorialController : Controller
    {
        // GET: Editorial
        public ActionResult Mostrar()
        {
            ProyectoFinalSchultenAlejandroEntities db = new ProyectoFinalSchultenAlejandroEntities();
            List<Editorial> editoriales = db.Editorial.OrderBy(g => g.Editorial1).ToList();
            return View(editoriales);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(string Editorial1)
        {
            if (Editorial1 == null)
            {
                return Content("No puedo insertar los datos, falta agregar la editorial");
            }

            if (Editorial1.Length == 0)
            {
                return Content("No puedo insertar los datos, falta agregar la editorial");
            }

            ProyectoFinalSchultenAlejandroEntities db = new ProyectoFinalSchultenAlejandroEntities();

            Editorial nuevaEditorial = new Editorial();
            nuevaEditorial.Editorial1 = Editorial1;

            db.Editorial.Add(nuevaEditorial);

            db.SaveChanges();

            return RedirectToAction("Mostrar");
        }
    }
}