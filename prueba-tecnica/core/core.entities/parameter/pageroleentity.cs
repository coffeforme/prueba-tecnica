using System;

namespace core.entities.parameter
{
    public class pageroleentity
    {
        public int id { get; set; }
        public System.Guid id_role { get; set; }
        public int id_page { get; set; }
        public string title { get; set; }
        public string icon { get; set; }
        public string route { get; set; }
        public string actions { get; set; }
    }
}
