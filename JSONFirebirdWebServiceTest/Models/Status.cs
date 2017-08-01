using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSONFirebirdWebServiceTest.Models
{
    public class Status
    {
        public int ID;
        public DateTime CREATED;
        public DateTime MODIFIED;
        public bool ISACTIVE;
        public bool ISDELETED;
        public string STATUSCODE;
        public string STATUSNAME;
        public string DESCRIPTION;
        public int ORDERVALUE;
    }
}