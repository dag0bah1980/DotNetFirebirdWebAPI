using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JSONFirebirdWebServiceTest.Models
{
    public class Project
    {
        public int ID;
        public DateTime CREATED;
        public DateTime MODIFIED;
        public bool ISACTIVE;
        public bool ISDELETED;
        public string DESCRIPTION;
        public string PROJECTTYPE;
        public int TIER;
        public int PRIORITYID;
        public int CREATEDBY;
        public int ASSIGNEDTO;
        public bool RECURRING;
        public DateTime DUEDATE;
        public int PARENTID;
        public int STATUSID;
    }
}