using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using JSONFirebirdWebServiceTest.Models;
using JSONFirebirdWebServiceTest.HTTPResponses;
using System.Data;
using JSONFirebirdWebServiceTest.Exceptions;
using JSONFirebirdWebServiceTest.JSONFormatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;

namespace JSONFirebirdWebServiceTest.Connect
{
    [RoutePrefix("Connect")]
    public class DBConnectionController : ApiController
    {
        DBConnection fbconndetails = new DBConnection();

        [Route("defn")]
        [HttpGet]
        // GET: Connect/
        public HttpResponseMessage defn()
        {
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri(string.Concat(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority),"/Definitions/Connections.aspx"));
            return response;
        }

        [Route("GetConnectionSettings")]
        [HttpGet]
        // GET: Connect/GetConnectionSettings
        public IEnumerable<DBConnection> GetConnectionSettings()
        {
            yield return fbconndetails;
        }

        [Route("TestDBConnection")]
        [HttpGet]
        // GET: Connect/TestDBConnection
        public IHttpActionResult TestDBConnection()
        {
            
            Connection fbconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);
            if (fbconnection.TestConnection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize))
            {
                string msg = string.Concat("OK! Connected to: ", fbconndetails.DBHost + " " + fbconndetails.DBPath);
                return new TextHeaderResponses(msg, Request);
            }
            else
            {
                return this.NotFound("NO GO :(");
            }
           
        }

        private static string FormatJson(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }

        
        [Route("PerformSelectOnSettings")]
        [HttpGet]
        // GET: Connect/TestDBConnection
        public IHttpActionResult PerformSelectOnSettings()
        {

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);
            DataTable result = new DataTable();

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "select * from settings";

            using (selectconnection.fbconnect)
            {
 
                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);

                    using (FbDataReader fbsqlreader = fbcmd.ExecuteReader())
                    {
                        try
                        {
                            result.Load(fbsqlreader);
                            status = "Success";
                            code = "200";
                            message = "All Good!";
                        }
                        catch (Exception e)
                        {
                            ExceptionsLogByFile logger = new ExceptionsLogByFile();
                            logger.LogException(e);
                            status = "Fail";
                            code = "500";
                            message = "Internal Server Error";
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    ExceptionsLogByFile logger = new ExceptionsLogByFile();
                    logger.LogException(ex);
                    status = "Fail";
                    code = "503";
                    message = "Internal Server Error - Cannot access database";
                }
                finally
                {
                    
                }
                selectconnection.fbconnect.Close();

                
                mbuilder.status = status;
                mbuilder.code = code;
                mbuilder.message = message;

                jbuilder.Meta = mbuilder;

                jbuilder.Data = result;
                
                return Json(jbuilder);
                
            }


        }

        // GET: api/DBConnection/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/DBConnection
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DBConnection/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DBConnection/5
        public void Delete(int id)
        {
        }
    }
}
