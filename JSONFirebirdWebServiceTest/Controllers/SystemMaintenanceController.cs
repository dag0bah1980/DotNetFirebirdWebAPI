using JSONFirebirdWebServiceTest.Exceptions;
using JSONFirebirdWebServiceTest.JSONFormatting;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JSONFirebirdWebServiceTest.Controllers
{
    [RoutePrefix("api")]
    public class SystemMaintenanceController : ApiController
    {        

        [Route("SystemMaintenance/GetHardDriveInfo")]
        [HttpGet]
        // GET: api/SystemMaintenance
        public IHttpActionResult GetHardDriveSize()
        {
            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";         

            mbuilder.status = status;
            mbuilder.code = code;
            mbuilder.message = message;

            jbuilder.Meta = mbuilder;

            DataTable hddsize = new DataTable();

            hddsize.Clear();
            hddsize.Columns.Add("Drive");
            hddsize.Columns.Add("Size");
            hddsize.Columns.Add("AvailableSpace");
            DataRow _gethddsize = hddsize.NewRow();

            DriveInfo di = new DriveInfo(@"D:\");

            try
            {
                if (di.IsReady)
                {
                    _gethddsize["Drive"] = di.Name;
                    _gethddsize["Size"] = di.TotalSize;
                    _gethddsize["AvailableSpace"] = di.AvailableFreeSpace;
                }
                hddsize.Rows.Add(_gethddsize);
                mbuilder.status = "Success";
                mbuilder.code = "200";
                mbuilder.message = "All Good!";
            }
            catch (Exception e)
            {
                ExceptionsLogByFile logger = new ExceptionsLogByFile();
                logger.LogException(e);
                status = "Fail";
                code = "500";
                message = "Internal Server Error";
            }

            jbuilder.Data = hddsize;

            return Json(jbuilder);
        }

        // GET: api/SystemMaintenance/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SystemMaintenance
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SystemMaintenance/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SystemMaintenance/5
        public void Delete(int id)
        {
        }

    }
}
