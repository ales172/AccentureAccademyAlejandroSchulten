using AccentureAccademySchultenAlejandro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccentureAccademySchultenAlejandro.Controllers
{
    public class GeneroController : Controller
    {
        // GET: Genero

        public ActionResult Mostrar()
        {
            ProyectoFinalSchultenAlejandroEntities db = new ProyectoFinalSchultenAlejandroEntities();
            List<Genero> generos = db.Genero.OrderBy(g => g.Genero1).ToList();
            return View(generos);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(string Genero1)
        {
            if (Genero1 == null)
            {
                return Content("No puedo insertar los datos falta el genero");
            }

            if (Genero1.Length == 0)
            {
                return Content("No puedo insertar los datos falta el genero");
            }

            ProyectoFinalSchultenAlejandroEntities db = new ProyectoFinalSchultenAlejandroEntities();

            Genero nuevoGenero = new Genero();
            nuevoGenero.Genero1 = Genero1;

            db.Genero.Add(nuevoGenero);

            db.SaveChanges();

            return RedirectToAction("Mostrar");
        }

    }
}