using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSONFirebirdWebServiceTest.Models
{
    public class ProjectType
    {
        public int ID;
        public DateTime CREATED;
        public DateTime MODIFIED;
        public bool ISACTIVE;
        public bool ISDELETED;
        public string PROJTYPECODE;
        public string PROJTYPENAME;
        public int PARENTPROJTYPEID;
        public string DESCRIPTION;
        public int ORDERVALUE;
    }
}