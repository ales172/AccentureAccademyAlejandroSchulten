using AccentureAccademySchultenAlejandro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccentureAccademySchultenAlejandro.Controllers
{
    public class AutorController : Controller
    {
        // GET: Autor
        public ActionResult Mostrar()
        {
            ProyectoFinalSchultenAlejandroEntities db = new ProyectoFinalSchultenAlejandroEntities();
            List<Autor> autores = db.Autor.OrderBy(a => a.Nombre).ToList();
            return View(autores);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(string Nombre)
        {
            if (Nombre == null)
            {
                return Content("No puedo insertar los datos, faltan datos");
            }

            if (Nombre.Length == 0)
            {
                return Content("No puedo insertar los datos, faltan datos");
            }

            ProyectoFinalSchultenAlejandroEntities db = new ProyectoFinalSchultenAlejandroEntities();

            Autor nuevoAutor = new Autor();
            nuevoAutor.Nombre = Nombre;
            db.Autor.Add(nuevoAutor);

            db.SaveChanges();

            return RedirectToAction("Mostrar");
        }
    }
}