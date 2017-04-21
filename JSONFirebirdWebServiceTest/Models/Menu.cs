using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSONFirebirdWebServiceTest.Models
{
    public class Menu
    {
        public int ID { get; set; }
        public string CODE { get; set; }
        public bool ISACTIVE { get; set; }
        public bool ISDELETED { get; set; }
        public DateTime CREATED { get; set; }
        public DateTime MODIFIED { get; set; }
        public string DESCRIPT { get; set; }
        public string SHORTDESCRIPT { get; set; }
        public int PARENTID { get; set; }
        public int ORD { get; set; }
        public string LINK { get; set; }
        public bool HIDDEN { get; set; }

    }
}