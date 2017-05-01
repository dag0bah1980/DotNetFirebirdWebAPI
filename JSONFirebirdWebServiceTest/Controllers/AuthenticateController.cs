using JSONFirebirdWebServiceTest.Connect;
using JSONFirebirdWebServiceTest.Exceptions;
using JSONFirebirdWebServiceTest.JSONFormatting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirebirdSql.Data.FirebirdClient;
using JSONFirebirdWebServiceTest.Models;
using JSONFirebirdWebServiceTest.JWT;
using System.Configuration;
using System.Web;

namespace JSONFirebirdWebServiceTest.Controllers
{
    [RoutePrefix("auth")]
    public class AuthenticateController : ApiController
    {

        [Route("login/{username}/{password}")]
        [HttpGet]
        // GET: api/Authenticate
        public IHttpActionResult Login(string username, string password)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);
            DataTable result = new DataTable();

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "select * from Login(@username, @password)";

            

            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();

                    FbParameter usernameParam = new FbParameter("@username", FbDbType.VarChar);
                    usernameParam.Value = username;

                    FbParameter passwordParam = new FbParameter("@password", FbDbType.VarChar);
                    passwordParam.Value = password;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(usernameParam);
                    fbcmd.Parameters.Add(passwordParam);

                    

                    using (FbDataReader fbsqlreader = fbcmd.ExecuteReader())
                    {
                        try
                        {
                            result.Load(fbsqlreader);                            
                            status = "Success";
                            code = "200";
                            message = result.ToString();

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

                jbuilder.Meta = mbuilder;

                jbuilder.Data = result;

                return Json(jbuilder);
            }
        }

        [Route("login")]
        [HttpPost]
        public IHttpActionResult Post(Cred submittedCred)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);
            DataTable result = new DataTable();

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";
            string token = "";
            string appversion = "";

            JWTTest jwtobject = new JWTTest();

            string sqlcmd = "select * from login(@USERNAME,@PASSWORD)";

            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter usernameParam = new FbParameter("@USERNAME", FbDbType.VarChar);
                    usernameParam.Value = submittedCred.USERNAME;

                    FbParameter passwordParam = new FbParameter("@PASSWORD", FbDbType.VarChar);
                    passwordParam.Value = submittedCred.PASSWORD;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(usernameParam);
                    fbcmd.Parameters.Add(passwordParam);

                    using (FbDataReader fbsqlreader = fbcmd.ExecuteReader())
                    {
                        try
                        {
                            result.Load(fbsqlreader);
                            status = "Success";
                            code = "200";
                            message = "All Good!";
                            if (result.Rows[0]["SUCCESS"].Equals(true))
                            {
                                
                                //add user to users table in DB to count number of active users.
                                //I should break this out to another section of code...
                                string addusersql = "insert into CURRENTUSERS (USERID, IPADDRESS, BROWSER, SESSIONKEY)" + 
                                    "VALUES" + 
                                    "(@USERID, @IPADDRESS, @BROWSER, @SESSIONKEY)";

                                //Connection insertconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);
                                //insertconnection.fbconnect.Open();
                                
                                FbParameter useridParam = new FbParameter("@USERID", FbDbType.BigInt);
                                FbParameter ipaddressParam = new FbParameter("@IPADDRESS", FbDbType.VarChar);
                                FbParameter browserParam = new FbParameter("@BROWSER", FbDbType.VarChar);
                                FbParameter sessionkeyParam = new FbParameter("@SESSIONKEY", FbDbType.VarChar);


                                //From : http://stackoverflow.com/questions/17306038/how-would-you-detect-the-current-browser-in-an-api-controller
                                HttpRequestMessage currentRequest = this.Request;                                
                                System.Net.Http.Headers.HttpHeaderValueCollection<System.Net.Http.Headers.ProductInfoHeaderValue> userAgentHeader = currentRequest.Headers.UserAgent;


                                //For IP: http://stackoverflow.com/questions/1907195/how-to-get-ip-address                             
                                String ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                                //For list of server variables: https://msdn.microsoft.com/en-us/library/ms524602.aspx
                                String browser = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];


                                if (string.IsNullOrEmpty(ip))
                                {
                                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                                }
                                else
                                { // Using X-Forwarded-For last address
                                    ip = ip.Split(',')
                                           .Last()
                                           .Trim();
                                }

                                var ipaddress = ip;

                                string sessionkey = Guid.NewGuid().ToString();

                                useridParam.Value = result.Rows[0]["RESULTCODE"];
                                ipaddressParam.Value = ipaddress;
                                browserParam.Value = browser;
                                sessionkeyParam.Value =  sessionkey;

                                FbCommand fbinsertcmd = new FbCommand(addusersql, selectconnection.fbconnect, fbtrans);
                                fbinsertcmd.Parameters.Add(useridParam);
                                fbinsertcmd.Parameters.Add(ipaddressParam);
                                fbinsertcmd.Parameters.Add(browserParam);
                                fbinsertcmd.Parameters.Add(sessionkeyParam);

                                fbinsertcmd.ExecuteNonQuery();

                                token = jwtobject.GenerateToken(submittedCred.USERNAME);
                                //message = message + "Result is: " + result.Rows[0]["SUCCESS"];
                                message = sessionkey;
                            }
                            
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

        [Route("logout")]
        [HttpPost]
        public IHttpActionResult Post(LogoutCred submittedLogoutCred)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);
            DataTable result = new DataTable();

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";
            string token = "";
            string appversion = "";

            JWTTest jwtobject = new JWTTest();

            string sqlcmd = "select * from logout(@USERNAME,@SESSIONKEY)";

            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter usernameParam = new FbParameter("@USERNAME", FbDbType.VarChar);
                    usernameParam.Value = submittedLogoutCred.USERNAME;

                    FbParameter sessionkeyParam = new FbParameter("@SESSIONKEY", FbDbType.VarChar);
                    sessionkeyParam.Value = submittedLogoutCred.SESSIONKEY;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(usernameParam);
                    fbcmd.Parameters.Add(sessionkeyParam);


                    using (FbDataReader fbsqlreader = fbcmd.ExecuteReader())
                    {
                        try
                        {
                            result.Load(fbsqlreader);
                            if (result.Rows[0]["SUCCESS"].Equals(true))
                            {
                                status = "Success";
                                code = "200";
                                message = "All Good!";
                            }
                            else
                            {
                                status = "Odd Status: No matching username / sessionkey";
                                code = "200";
                                message = "Odd Status: No matching username / sessionkey";
                            }
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



        // GET: api/Authenticate/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Authenticate
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT: api/Authenticate/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Authenticate/5
        public void Delete(int id)
        {
        }
    }
}
