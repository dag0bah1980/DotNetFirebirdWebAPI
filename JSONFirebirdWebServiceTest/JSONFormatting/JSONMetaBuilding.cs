using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Newtonsoft.Json;

namespace JSONFirebirdWebServiceTest.JSONFormatting
{
    public class JSONMetaBuilding
    {
        
        public class JSONPackage
        {
            public metadetails Meta { get; set; }
        }

        public class metadetails
        {
            public string status { get; set; }
            public string code { get; set; }
            public string message { get; set; }
            public string jwttoken { get; set; }
        }



    }
 

}