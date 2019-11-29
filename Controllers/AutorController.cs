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
        ProyectoFinalSchultenAlejandroEntities db = new ProyectoFinalSchultenAlejandroEntities();

        public ActionResult Mostrar()
        {
            List<Autor> autores = db.Autor.OrderBy(a => a.Nombre).ToList();
            return View(autores);
        }
        public ActionResult Crear()
        {
            this.ViewBag.TituloPagina = "Agregar Autor";
            Autor autor = new Autor();
            return View("Editar", autor);
        }

        [HttpPost]
        public ActionResult Crear(Autor autor)
        {
            if (autor == null)
            {
                return Content("No puedo insertar los datos, faltan datos");
            }

            if (autor.Nombre.Length == 0)
            {
                return Content("No puedo insertar los datos, faltan datos");
            }
           
            db.Autor.Add(autor);

            db.SaveChanges();

            return RedirectToAction("Mostrar");
        }
        public ActionResult Editar(int Id)
        {
            this.ViewBag.TituloPagina = "Editar Autor";
            Autor aEditar = db.Autor.Find(Id);
            return View(aEditar);
        }
        [HttpPost]
        public ActionResult Editar(Autor autor)
        {
            this.db.Autor.Attach(autor);
            this.db.Entry(autor).State = System.Data.Entity.EntityState.Modified;
            this.db.SaveChanges();
            return RedirectToAction("Mostrar");
        }
        public ActionResult Eliminar(int Id)
        {
            Autor autor = this.db.Autor.FirstOrDefault(a => a.Id_Autor == Id);
            this.db.Autor.Remove(autor);
            this.db.SaveChanges();
            return RedirectToAction("Mostrar");
        }
    }
}