using AccentureAccademySchultenAlejandro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccentureAccademySchultenAlejandro.Controllers
{
    public class EscritoPorController : Controller
    {
        // GET: EscritoPor
        ProyectoFinalSchultenAlejandroEntities db = new ProyectoFinalSchultenAlejandroEntities();
        public ActionResult Mostrar()
        {
            List<EscritoPor> escritoPors = db.EscritoPor.ToList();
            return View(escritoPors);
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(int idAutor, int idLibro)
        {
            EscritoPor nuevo = new EscritoPor();
            nuevo.Id_Autor = idAutor;
            nuevo.Id_Libro = idLibro;

            db.EscritoPor.Add(nuevo);

            db.SaveChanges();

            return RedirectToAction("Mostrar");
        }
    }
}