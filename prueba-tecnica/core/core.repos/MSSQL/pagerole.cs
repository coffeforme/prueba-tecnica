//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace core.repos.MSSQL
{
    using System;
    using System.Collections.Generic;
    
    public partial class pagerole
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pagerole()
        {
            this.pageroleaction = new HashSet<pageroleaction>();
        }
    
        public int id { get; set; }
        public System.Guid id_role { get; set; }
        public int id_page { get; set; }
        public Nullable<System.DateTime> date { get; set; }
    
        public virtual page page { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pageroleaction> pageroleaction { get; set; }
    }
}
