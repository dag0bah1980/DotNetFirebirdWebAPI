using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace JSONFirebirdWebServiceTest.Models
{
    public class DBConnection
    {
        public string DBFile { get; set; }
        public string DBPath { get; set; }
        public string DBUser { get; set; }
        public string DBPassword { get; set; }
        public string DBHost { get; set; }
        public int DBPort { get; set; }
        public int DBConnectionLifeTime { get; set; }
        public bool DBPooling { get; set; }
        public int DBMinPoolSize { get; set; }
        public int DBMaxPoolSize { get; set; }
        public int DBPacketSize { get; set; }
    
        public DBConnection()
        {
            DBFile = ConfigurationManager.AppSettings["DBFile"];
            DBPath = ConfigurationManager.AppSettings["DBPath"];
            DBUser = ConfigurationManager.AppSettings["DBUser"];
            DBPassword = ConfigurationManager.AppSettings["DBPassword"];
            DBHost = ConfigurationManager.AppSettings["DBHost"];
            DBPort = Convert.ToInt32(ConfigurationManager.AppSettings["DBPort"]);
            DBConnectionLifeTime = Convert.ToInt32(ConfigurationManager.AppSettings["DBConnectionLifeTime"]);
            DBPooling = Convert.ToBoolean(ConfigurationManager.AppSettings["DBPooling"]);
            DBMinPoolSize = Convert.ToInt32(ConfigurationManager.AppSettings["DBMinPoolSize"]);
            DBMaxPoolSize = Convert.ToInt32(ConfigurationManager.AppSettings["DBMaxPoolSize"]);
            DBPacketSize = Convert.ToInt32(ConfigurationManager.AppSettings["DBPacketSize"]);
        }    
    }
}