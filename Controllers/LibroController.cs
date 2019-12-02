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
                foreach(EscritoPor ex in listadoex)
                {
                    if(l.Id_Libro == ex.Id_Libro)
                    {
                        librito.Autores.Add(db.Autor.Find(ex.Id_Autor));
                    }
                }
                libritos.Add(librito);
            }

            return View(libritos);
        }
        [HttpPost]
        public ActionResult Mostrar(Filtro filtro)
        {
            List<Libro> todos = db.Libro.ToList();
            List<EscritoPor> escp = db.EscritoPor.ToList();
            IQueryable<Libro> qryLibro = db.Libro;
            IQueryable<Autor> qryAutor = db.Autor;
            IQueryable<Genero> qryGenero = db.Genero;
            IQueryable<Editorial> qryEditorial= db.Editorial;

            List<Libro> aux = new List<Libro>();

            if (filtro.Titulo != null)
            {
                qryLibro = qryLibro.Where(lib => lib.Titulo.Contains(filtro.Titulo));
                qryAutor = qryAutor.Where(aut => aut.Nombre.Contains(filtro.Titulo));
                qryGenero = qryGenero.Where(gen => gen.Genero1.Contains(filtro.Titulo));
                qryEditorial = qryEditorial.Where(ed => ed.Editorial1.Contains(filtro.Titulo));

            foreach (Libro libro in qryLibro)
            {
                aux.Add(libro);
            }
            foreach (Autor aut in qryAutor)
            {
                    List<EscritoPor> escpor = db.EscritoPor.Where(es => es.Id_Autor == aut.Id_Autor).ToList();
                    foreach (EscritoPor escrito in escpor) {
                        aux.Add(db.Libro.Where(l => l.Id_Libro == escrito.Id_Libro).FirstOrDefault());
                    }
                 
            }
            foreach (Genero gen in qryGenero)
            {
                    aux.Add(db.Libro.Where(l => l.Id_Genero == gen.Id_Genero).FirstOrDefault());
                }
            foreach (Editorial ed in qryEditorial)
            {
                aux.Add(db.Libro.Where(l => l.Id_Editorial == ed.Id_Editorial).FirstOrDefault());
            }
            }

            List<Librito> libritos = new List<Librito>();
            List<EscritoPor> listadoex = db.EscritoPor.ToList();
            foreach (Libro l in aux)
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

    [HttpPost]
    public ActionResult MostrarOrdenado(string ordenarPor)
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
           
            switch (ordenarPor)
            {
                case "Titulo":
                    libritos = libritos.OrderBy(l => l.Titulo).ToList();
                    break;
                case "Autor":
                    libritos = libritos.OrderBy(l => l.Autores.First().Nombre).ToList(); 
                    break;
                case "Editorial":
                    libritos = libritos.OrderBy(l => l.Editorial).ToList();
                    break;
                case "Genero":
                    libritos = libritos.OrderBy(l => l.Genero).ToList();
                    break;
                default:
                    libritos = libritos.OrderBy(l => l.Titulo).ToList();
                    break;
            }
            return RedirectToAction("Mostrar",libritos);
        }

        public ActionResult MostrarUno(int Id)
        {
            this.ViewBag.TituloPagina = "Libro";
            Librito libro = new Librito();
            Libro l = db.Libro.Find(Id);
            libro.Id_Libro = l.Id_Libro;
            libro.Titulo = l.Titulo;
            libro.Descripcion = l.Descripcion;
            libro.Editorial = db.Editorial.Where(e => e.Id_Editorial == l.Id_Editorial).FirstOrDefault().Editorial1;
            libro.Genero = db.Genero.Where(g => g.Id_Genero == l.Id_Genero).FirstOrDefault().Genero1;
            libro.Imagen = l.Imagen;
            libro.ISBN = l.ISBN;
            List<EscritoPor> listadoex = db.EscritoPor.ToList();
            foreach (EscritoPor ex in listadoex)
            {
                if (l.Id_Libro == ex.Id_Libro)
                {
                    libro.Autores.Add(db.Autor.Find(ex.Id_Autor));
                }
            }

            return View(libro);
        }
        public ActionResult Crear()
        {
            this.ViewBag.TituloPagina = "Agregar Libro";
            List<Autor> Autores = new List<Autor>();
            return View("Crear", Autores);
        }

        [HttpPost]
        public ActionResult Crear(string ISBN, string Titulo, IEnumerable<string> Autores, string Genero,string Editorial, string Imagen, string Descripcion)
        {
            if (ISBN == null || Titulo == null || Genero == null || Editorial == null|| Autores == null)
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

                Genero nuevoGen = new Genero();
                nuevoGen.Genero1 = Genero;
                generoController.Crear(nuevoGen);
                nuevoLibro.Id_Genero = db.Genero.Where(g => g.Genero1 == Genero).FirstOrDefault().Id_Genero;
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
                Editorial newEditorial = new Editorial();
                newEditorial.Editorial1 = Editorial;
                editorialController.Crear(newEditorial);
                nuevoLibro.Id_Editorial = db.Editorial.Where(e => e.Editorial1 == Editorial).FirstOrDefault().Id_Editorial;
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
            //Autores es la lista que entra por parametro al metodo
            foreach (string autor in Autores)
            {
                int idAutor = -1;
                //autoresAux es la lista de los autores de la base
                foreach (Autor autorAux in autoresAux)
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
                    Autor autorNew = new Autor();
                    autorNew.Nombre = autor;
                        autorController.Crear(autorNew);
                        escritoPor.Id_Autor = db.Autor.Where(aut => aut.Nombre == autor).FirstOrDefault().Id_Autor;
                    }
                //busco id del libro asi lo asigno a escritoPor
                escritoPor.Id_Libro = db.Libro.Where(l => l.Titulo == Titulo).FirstOrDefault().Id_Libro;
                db.EscritoPor.Add(escritoPor);
                db.SaveChanges();
            }

            return RedirectToAction("Mostrar");
        }
      
        public ActionResult Eliminar(int Id)
        {
            List<EscritoPor> listadoex = db.EscritoPor.ToList();
            Libro aEliminar = db.Libro.Find(Id);
            foreach(EscritoPor e in listadoex)
            {
                if (e.Id_Libro == Id)
                {
                    db.EscritoPor.Remove(e);
                }
            }
            db.Libro.Remove(aEliminar);
            db.SaveChanges();

            return RedirectToAction("Mostrar");
        }
        public ActionResult Editar(int Id)
        {
            this.ViewBag.TituloPagina = "Agregar Libro";
            Librito libro = new Librito();
            Libro aux = db.Libro.Find(Id);
            libro.Descripcion = aux.Descripcion;
            libro.Editorial = db.Editorial.Find(aux.Id_Editorial).Editorial1;
            libro.Genero = db.Genero.Find(aux.Id_Genero).Genero1;
            libro.Id_Libro = Id;
            libro.Imagen = aux.Imagen;
            libro.ISBN = aux.ISBN;
            libro.Titulo = aux.Titulo;
            List<EscritoPor> escritopor = db.EscritoPor.ToList();
            foreach(EscritoPor esx in escritopor)
            {
                if(esx.Id_Libro == Id)
                {
                    libro.Autores.Add(db.Autor.Find(esx.Id_Autor));
                }
            }
            return View(libro);
        }

        [HttpPost]
        public ActionResult Editar(Librito libro)
        {
            int idEditorial = -1;
            int idGenero = -1;
            EscritoPor escritoPor = new EscritoPor();
            Libro nuevoLibro = new Libro();

            //busco el genero, si no esta lo creo y lo asigno, sino lo asigno directamente
            List<Genero> generos = db.Genero.ToList();
            foreach (Genero g in generos)
            {
                if (g.Genero1 == libro.Genero)
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

                Genero nuevoGen = new Genero();
                nuevoGen.Genero1 = libro.Genero;
                generoController.Crear(nuevoGen);
                nuevoLibro.Id_Genero = db.Genero.Where(g => g.Genero1 == libro.Genero).FirstOrDefault().Id_Genero;
            }
            //busco la editorial, si esta la asigno, sino la agrego y asigno
            List<Editorial> editoriales = db.Editorial.ToList();
            foreach (Editorial e in editoriales)
            {
                if (e.Editorial1 == libro.Editorial)
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
                Editorial newEditorial = new Editorial();
                newEditorial.Editorial1 = libro.Editorial;
                editorialController.Crear(newEditorial);
                nuevoLibro.Id_Editorial = db.Editorial.Where(e => e.Editorial1 == libro.Editorial).FirstOrDefault().Id_Editorial;
            }
            nuevoLibro.Descripcion = libro.Descripcion;
            nuevoLibro.ISBN = libro.ISBN;
            nuevoLibro.Titulo = libro.Titulo;
            nuevoLibro.Imagen = libro.Imagen;
            nuevoLibro.Id_Libro = libro.Id_Libro;

            this.db.Libro.Attach(nuevoLibro);
            this.db.Entry(nuevoLibro).State = System.Data.Entity.EntityState.Modified;
            this.db.SaveChanges();
            
            //borro los escritopor del id del libro
            List<EscritoPor> listaEsx = db.EscritoPor.ToList();
            foreach(EscritoPor ex in listaEsx)
            {
                if(ex.Id_Libro == libro.Id_Libro)
                {
                    EscritoPor aux = this.db.EscritoPor.FirstOrDefault(a => a.Id_EscritoPor == ex.Id_EscritoPor );
                    this.db.EscritoPor.Remove(aux);
                    this.db.SaveChanges();
                }
            }

            // me fijo si el autor existe o no y lo asigno a escritoPor
            List<Autor> autoresAux = db.Autor.ToList();
            
            //Autores es la lista que entra por parametro al metodo
            foreach (Autor autor in libro.Autores)
            {
                int idAutor = -1;
                //autoresAux es la lista de los autores de la base
                foreach (Autor autorAux in autoresAux)
                {
                    if (autor.Nombre == autorAux.Nombre)
                    {
                        idAutor = autorAux.Id_Autor;
                    }
                }
                if (idAutor != -1)
                {
                    escritoPor.Id_Autor = idAutor;
                }
                else
                {
                    Autor autorNew = new Autor();
                    autorNew.Nombre = autor.Nombre;
                    autorController.Crear(autorNew);
                    escritoPor.Id_Autor = db.Autor.Where(aut => aut.Nombre == autor.Nombre).FirstOrDefault().Id_Autor;
                }
                //busco id del libro asi lo asigno a escritoPor
                escritoPor.Id_Libro = db.Libro.Where(l => l.Titulo == libro.Titulo).FirstOrDefault().Id_Libro;
                db.EscritoPor.Add(escritoPor);
                db.SaveChanges();
            }

            return RedirectToAction("Mostrar");
        }
    }
}