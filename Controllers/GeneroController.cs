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
        ProyectoFinalSchultenAlejandroEntities db = new ProyectoFinalSchultenAlejandroEntities();

        public ActionResult Mostrar()
        {
            List<Genero> generos = db.Genero.OrderBy(g => g.Genero1).ToList();
            return View(generos);
        }

        public ActionResult Crear()
        {
            this.ViewBag.TituloPagina = "Agregar Genero";
            Genero genero = new Genero();
            return View("Editar",genero);
        }

        [HttpPost]
        public ActionResult Crear(Genero genero)
        {
            if (genero == null)
            {
                return Content("No puedo insertar los datos falta el genero");
            }

            if (genero.Genero1.Length == 0)
            {
                return Content("No puedo insertar los datos falta el genero");
            }
            
            db.Genero.Add(genero);

            db.SaveChanges();

            return RedirectToAction("Mostrar");
        }
        public ActionResult Editar(int Id)
        {
            this.ViewBag.TituloPagina = "Editar Genero";
            Genero aEditar = db.Genero.Find(Id);
            return View(aEditar);
        }
        [HttpPost]
        public ActionResult Editar(Genero genero)
        {
            this.db.Genero.Attach(genero);
            this.db.Entry(genero).State = System.Data.Entity.EntityState.Modified;
            this.db.SaveChanges();
            return RedirectToAction("Mostrar");
        }
        public ActionResult Eliminar(int Id)
        {
            Genero genero = this.db.Genero.FirstOrDefault(g => g.Id_Genero == Id);
            this.db.Genero.Remove(genero);
            this.db.SaveChanges();
            return RedirectToAction("Mostrar");
        }

    }
}