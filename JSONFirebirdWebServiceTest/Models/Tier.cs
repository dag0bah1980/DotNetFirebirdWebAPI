using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSONFirebirdWebServiceTest.Models
{
    public class Tier
    {
        public int ID;
        public DateTime CREATED;
        public DateTime MODIFIED;
        public bool ISACTIVE;
        public bool ISDELETED;
        public string TIERCODE;
        public string TIERNAME;
        public string DESCRIPTION;
        public int ORDERVALUE;
    }
}