using AccentureAccademySchultenAlejandro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccentureAccademySchultenAlejandro.Controllers
{
    public class HomeController : Controller
    {
        readonly ProyectoFinalSchultenAlejandroEntities db = new ProyectoFinalSchultenAlejandroEntities();

        public ActionResult Index()
        {
            List<Librito> libritos = new List<Librito>();
            List<Libro> libros = db.Libro.OrderBy(l => l.Titulo).ToList();
            List<EscritoPor> listadoex = db.EscritoPor.ToList();
            foreach (Libro l in libros)
            {
                Librito librito = new Librito();
                librito.Id_Libro = l.Id_Libro;
                librito.Titulo = l.Titulo;
                librito.Descripcion = l.Descripcion;
                librito.Editorial = db.Editorial.Where(e => e.Id_Editorial == l.Id_Editorial).FirstOrDefault().Editorial1;
                librito.Genero = db.Genero.Where(g => g.Id_Genero == l.Id_Genero).FirstOrDefault().Genero1;
                librito.Imagen = l.Imagen;
                librito.ISBN = l.ISBN;
                foreach (EscritoPor ex in listadoex)
                {
                    if (l.Id_Libro == ex.Id_Libro)
                    {
                        librito.Autores.Add(db.Autor.Find(ex.Id_Autor));
                    }
                }
                libritos.Add(librito);
            }
            return View(libritos);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}