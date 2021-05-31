using System;

namespace core.entities.auth
{
    public class userentity
    {
        public System.Guid uid { get; set; }
        public string username { get; set; }
        public byte[] password { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string fullname { get; set; }
        public string cel { get; set; }
        public string address { get; set; }
        public byte[] salt { get; set; }
        public bool requirechangepass { get; set; }
        public string confirmationcode { get; set; }
        public bool sayhi { get; set; }
        public bool emailconfirmed { get; set; }
        public bool state { get; set; }
        public System.DateTime date { get; set; }

        public string pass { get; set; }
        public Guid id_rol { get; set; }
        public string role { get; set; }
    }
}
