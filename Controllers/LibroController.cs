using AccentureAccademySchultenAlejandro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccentureAccademySchultenAlejandro.Controllers
{
    public class LibroController : Controller
    {
        // GET: Libro
        private readonly GeneroController generoController = new GeneroController();
        private readonly EditorialController editorialController = new EditorialController();
        private readonly AutorController autorController = new AutorController();
        readonly ProyectoFinalSchultenAlejandroEntities db = new ProyectoFinalSchultenAlejandroEntities();

        public ActionResult Mostrar()
        {
            List<Librito> libritos = new List<Librito>();
            List<Libro> libros = db.Libro.OrderBy(l => l.Titulo).ToList();
            List<EscritoPor> listadoex = db.EscritoPor.ToList();
            foreach(Libro l in libros)
            {
                Librito librito = new Librito();
                librito.Titulo = l.Titulo;
                foreach(EscritoPor ex in listadoex)
                {
                    if(l.Id_Libro == ex.Id_Libro)
                    {
                        librito.Autores.Add(db.Autor.Find(ex.Id_Autor));
                    }
                }
                librito.Editorial = db.Editorial.Find(l.Id_Editorial);
                libritos.Add(librito);
            }

            return View(libritos);
        }

        public ActionResult Crear()
        {
            List<Autor> Autores = new List<Autor>();
            return View(Autores);
        }

        [HttpPost]
        public ActionResult Crear(string ISBN, string Titulo, IEnumerable<string> Autores, string Genero,string Editorial, string Imagen, string Descripcion)
        {
            if (ISBN == null || Titulo == null || Genero == null || Editorial == null)
            {
                return Content("No puedo insertar el libro, faltan datos");
            }

            if (ISBN.Length == 0 || Titulo.Length == 0 || Genero.Length == 0 || Editorial.Length == 0)
            {
                return Content("No puedo insertar el libro, faltan datos");
            }

            int idEditorial = -1;
            int idGenero = -1;
            EscritoPor escritoPor = new EscritoPor();
            Libro nuevoLibro = new Libro();
           
            //busco el genero, si no esta lo creo y lo asigno, sino lo asigno directamente
            List<Genero> generos = db.Genero.ToList();
            foreach(Genero g in generos)
            {
                if(g.Genero1 == Genero)
                {
                    idGenero = g.Id_Genero;
                }
            }
            if (idGenero != -1)
            {
                nuevoLibro.Id_Genero = idGenero;
            }
            else
            {
                generoController.Crear(Genero);
                Genero nuevo = db.Genero.Where(g => g.Genero1 == Genero).FirstOrDefault();
                nuevoLibro.Id_Genero = nuevo.Id_Genero;
            }
            //busco la editorial, si esta la asigno, sino la agrego y asigno
            List<Editorial> editoriales = db.Editorial.ToList();
            foreach (Editorial e in editoriales)
            {
                if (e.Editorial1 == Editorial)
                {
                    idEditorial = e.Id_Editorial;
                }
            }
            if (idEditorial != -1)
            {
                nuevoLibro.Id_Editorial = idEditorial;
            }
            else
            {
                editorialController.Crear(Editorial);
                Editorial nueva = db.Editorial.Where(e => e.Editorial1 == Editorial).FirstOrDefault();
                nuevoLibro.Id_Editorial = nueva.Id_Editorial;
            }
            nuevoLibro.Descripcion = Descripcion;
            nuevoLibro.ISBN = ISBN;
            nuevoLibro.Titulo = Titulo;
            nuevoLibro.Imagen = Imagen;
          
            db.Libro.Add(nuevoLibro);
            db.SaveChanges();
            // me fijo si el autor existe o no y lo asigno a escritoPor
            List<Autor> autoresAux = db.Autor.ToList();
            List<int> idAutores = new List<int>();
            foreach (string autor in Autores)//Autores es la lista que entra por parametro al metodo
            {
                int idAutor = -1;

                foreach (Autor autorAux in autoresAux)//autoresAux es la lista de los autores de la base
                {
                if (autor == autorAux.Nombre)
                {
                    idAutor = autorAux.Id_Autor;
                    }
                }
                if (idAutor != -1)
                {
                    escritoPor.Id_Autor = idAutor;
                }else
                    {
                        autorController.Crear(autor);
                        escritoPor.Id_Autor = db.Autor.Where(aut => aut.Nombre == autor).FirstOrDefault().Id_Autor;
                    }
                //busco id del libro asi lo asigno a escritoPor
                escritoPor.Id_Libro = db.Libro.Where(l => l.Titulo == Titulo).FirstOrDefault().Id_Libro;
                db.EscritoPor.Add(escritoPor);
                db.SaveChanges();
            }

            return RedirectToAction("Mostrar");
        }
        [HttpPost]
        public ActionResult Eliminar(int Id)
        {
            List<EscritoPor> listadoex = db.EscritoPor.ToList();
            Libro aEliminar = db.Libro.Find(Id);
            foreach(EscritoPor e in listadoex)
            {
                if (e.Id_Libro == Id)
                {
                    db.EscritoPor.Remove(e);
                    db.Libro.Remove(aEliminar);
                }
            }

            return RedirectToAction("Mostrar");
        }
    }
}