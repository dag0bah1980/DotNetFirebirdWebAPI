using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSONFirebirdWebServiceTest.Models
{
    public class User
    {
        public int ID { get; set; }
        public DateTime CREATED { get; set; }
        public DateTime MODIFIED { get; set; }
        public bool ISACTIVE { get; set; }
        public bool ISDELETED { get; set; }
        public string USERNAME { get; set; }
        public string FNAME { get; set; }
        public string LNAME { get; set; }
        public string PWORD { get; set; }
    }

   
}