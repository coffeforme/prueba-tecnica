using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.entities.auth
{
    public class userroleentity
    {
        public System.Guid uid { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public System.Guid id_user { get; set; }
        public System.Guid id_role { get; set; }
    }
}
