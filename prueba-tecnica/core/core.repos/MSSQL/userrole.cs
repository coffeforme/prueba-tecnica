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
    
    public partial class userrole
    {
        public System.Guid uid { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public System.Guid id_user { get; set; }
        public System.Guid id_role { get; set; }
    
        public virtual role role { get; set; }
        public virtual user user { get; set; }
    }
}
