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
        ProyectoFinalSchultenAlejandroEntities db = new ProyectoFinalSchultenAlejandroEntities();

        public ActionResult Mostrar()
        {

            List<Editorial> editoriales = db.Editorial.OrderBy(g => g.Editorial1).ToList();
            return View(editoriales);
        }

        public ActionResult Crear()
        {
            Editorial editorial = new Editorial();
            this.ViewBag.TituloPagina = "Agregar Editorial";
            return View("Editar", editorial);
        }

        [HttpPost]
        public ActionResult Crear(Editorial editorial)
        {
            if (editorial == null)
            {
                return Content("No puedo insertar los datos, falta agregar la editorial");
            }
            if (editorial.Editorial1.Length == 0)
            {
                return Content("No puedo insertar los datos, falta agregar la editorial");
            }
            db.Editorial.Add(editorial);

            db.SaveChanges();

            return RedirectToAction("Mostrar");
        }

        public ActionResult Editar(int Id)
        {
            this.ViewBag.TituloPagina = "Editar Editorial";
            Editorial aEditar = db.Editorial.Find(Id);
            return View(aEditar);
        }
        [HttpPost]
        public ActionResult Editar(Editorial editorial)
        {
            this.db.Editorial.Attach(editorial);
            this.db.Entry(editorial).State = System.Data.Entity.EntityState.Modified;
            this.db.SaveChanges();
            return RedirectToAction("Mostrar");
        }
        public ActionResult Eliminar(int Id)
        {
            Editorial editorial = this.db.Editorial.FirstOrDefault(e => e.Id_Editorial == Id);
            this.db.Editorial.Remove(editorial);
            this.db.SaveChanges();
            return RedirectToAction("Mostrar");
        }
    }
}