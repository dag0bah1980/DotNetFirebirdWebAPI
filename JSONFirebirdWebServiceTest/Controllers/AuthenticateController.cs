﻿using JSONFirebirdWebServiceTest.Connect;
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
                            token = jwtobject.GenerateToken(submittedCred.USERNAME);
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
        public void Post([FromBody]string value)
        {
        }

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
