using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSONFirebirdWebServiceTest.Models
{
    public class Setting
    {
        public int ID;
        public DateTime CREATED;
        public DateTime MODIFIED;
        public string KEYNAME;
        public string FIELDTYPE;
        public string STRINGVALUE;
        public bool BOOLVALUE;
        public int INTVALUE;
        public float FLOATVALUE;
        public DateTime DATEVALUE;
        public DateTime TIMESTAMPVALUE;
        public DateTime TIMEVALUE;
        public string CATEGORYNAME;
        public int USERID;
        public string DEVICENAME;
        public string BLOBTXTVALUE;
        public Byte[] BLOBBINVALUE; 

    }
}