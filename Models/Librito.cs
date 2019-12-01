using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccentureAccademySchultenAlejandro.Models
{
    public class Librito
    {
        public int Id_Libro { get; set; }
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public List<Autor> Autores { get; set; }
        public string Editorial { get; set; }
        public string Genero { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }

        public Librito()
        {
            this.Autores = new List<Autor>();
        }
    }

}