using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JSONFirebirdWebServiceTest.Models;
using JSONFirebirdWebServiceTest.Exceptions;
using JSONFirebirdWebServiceTest.JSONFormatting;
using System.Data;
using JSONFirebirdWebServiceTest.Connect;
using System.Configuration;
using JSONFirebirdWebServiceTest.JWT;
using FirebirdSql.Data.FirebirdClient;

namespace JSONFirebirdWebServiceTest.Controllers
{
    [RoutePrefix("auth")]
    public class FooController : ApiController
    {
        // GET: api/Foo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Foo/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("foo")]
        // POST: auth/Foo
        public IHttpActionResult Post(Foo submittedFoo)
        {
            var re = Request;
            var headers = re.Headers;

            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);
            DataTable result = new DataTable();

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";
            string token = "";

            JWTTest jwtobject = new JWTTest();

            string sqlcmd = "select * from FOO";

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
                            message = "headers: " + headers;
                            fbtrans.Commit();
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
                mbuilder.jwttoken = token;
                mbuilder.appversion = ConfigurationManager.AppSettings["APIVersion"];

                jbuilder.Meta = mbuilder;

                jbuilder.Data = result;

                return Json(jbuilder);
            }
        }

        // PUT: api/Foo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Foo/5
        public void Delete(int id)
        {
        }
    }
}
