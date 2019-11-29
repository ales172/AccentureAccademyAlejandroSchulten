using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccentureAccademySchultenAlejandro.Models
{
    public class Librito
    {
        public string Titulo { get; set; }
        public List<Autor> Autores { get; set; }
        public Editorial Editorial { get; set; }
    
    public Librito()
        {
            this.Autores = new List<Autor>();
        }
    }

}