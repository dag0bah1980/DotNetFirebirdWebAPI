using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JSONFirebirdWebServiceTest.JSONFormatting
{
    public class JSONObject
    {
        public MetaDetails Meta;
        public DataTable Data;

        public JSONObject()
        {
            MetaDetails Meta = new MetaDetails();
            Meta.code = "";
            Meta.status = "";
            Meta.message = "";
            Meta.jwttoken = "";
            Meta.appversion = "";

        }

        public class MetaDetails
        {
            public string status { get; set; }
            public string code { get; set; }
            public string message { get; set; }
            public string jwttoken { get; set; }
            public string appversion { get; set; }
        }

        public void SetMetaDetails (string st, string co, string me)
        {
            Meta.status = st;
            Meta.code = co;
            Meta.message = me;
        }

        public void SetMetaDetailsToken (string st, string co, string me, string to, string appver)
        {
            Meta.status = st;
            Meta.code = co;
            Meta.message = me;
            Meta.jwttoken = to;
            Meta.appversion = appver;
        }
    }
}