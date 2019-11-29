
namespace AccentureAccademySchultenAlejandro.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Autor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Autor()
        {
            this.EscritoPor = new HashSet<EscritoPor>();
        }
    
        public int Id_Autor { get; set; }
        public string Nombre { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EscritoPor> EscritoPor { get; set; }
    }
}
