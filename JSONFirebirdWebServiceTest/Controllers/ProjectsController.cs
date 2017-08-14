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
using JSONFirebirdWebServiceTest.Definitions;

namespace JSONFirebirdWebServiceTest.Controllers
{
    [RoutePrefix("api")]
    public class ProjectsController : ApiController
    {
        [Route("Projects")]
        [HttpGet]
        // GET: 
        public IHttpActionResult Get()
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);
            DataTable result = new DataTable();

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "select * from projects";

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

        [Route("Projects/{requestedid}")]
        [HttpGet]
        // GET: api/Users/5
        public IHttpActionResult Get(int requestedid)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);
            DataTable result = new DataTable();

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "select * from projects where id = @requestedid";

            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@requestedid", FbDbType.BigInt);
                    idParam.Value = requestedid;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);


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

        [Route("Projects/CodeMatch/{requestedcode}")]
        [HttpGet]
        // GET: api/Users/5
        public IHttpActionResult Get(string requestedcode)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);
            DataTable result = new DataTable();

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "select * from codecheck_tag(@requestedcode)";

            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter codeParam = new FbParameter("@requestedcode", FbDbType.VarChar);
                    codeParam.Value = requestedcode;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(codeParam);


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

        [Route("Projects")]
        [HttpPost]
        // POST: api/Tags
        public IHttpActionResult Post(Project newProject)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "insert into projects (ISACTIVE,ISDELETED,DESCRIPTION, PROJECTTYPE, TIER, PRIORITYID, CREATEDBY, ASSIGNEDTO, RECURRING, DUEDATE, PARENTID)" +
                "VALUES " +
                "(@ISACTIVE,@ISDELETED,@DESCRIPTION, @PROJECTTYPE, @TIER, @PRIORITYID, @CREATEDBY, @ASSIGNEDTO, @RECURRING, @DUEDATE, @PARENTID)";


            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter isactiveParam = new FbParameter("@ISACTIVE", FbDbType.Boolean);
                    FbParameter isdeletedParam = new FbParameter("@ISDELETED", FbDbType.Boolean);
                    FbParameter descriptionParam = new FbParameter("@DESCRIPTION", FbDbType.VarChar);
                    FbParameter projecttypeParam = new FbParameter("@PROJECTTYPE", FbDbType.VarChar);
                    FbParameter tierParam = new FbParameter("@TIER", FbDbType.BigInt);
                    FbParameter priorityidParam = new FbParameter("@PRIORITYID", FbDbType.BigInt);
                    FbParameter createdbyParam = new FbParameter("@CREATEDBY", FbDbType.BigInt);
                    FbParameter assignedtoParam = new FbParameter("@ASSIGNEDTO", FbDbType.BigInt);
                    FbParameter recurringParam = new FbParameter("@RECURRING", FbDbType.Boolean);
                    FbParameter duedateParam = new FbParameter("@DUEDATE", FbDbType.TimeStamp);
                    FbParameter parentidParam = new FbParameter("@PARENTID", FbDbType.BigInt);
                    
                    isactiveParam.Value = newProject.ISACTIVE;
                    isdeletedParam.Value = newProject.ISDELETED;
                    descriptionParam.Value = newProject.DESCRIPTION;
                    projecttypeParam.Value = newProject.PROJECTTYPE;
                    tierParam.Value = newProject.TIER;
                    priorityidParam.Value = newProject.PRIORITYID;
                    createdbyParam.Value = newProject.CREATEDBY;
                    assignedtoParam.Value = newProject.ASSIGNEDTO;
                    recurringParam.Value = newProject.RECURRING;
                    duedateParam.Value = newProject.DUEDATE;
                    parentidParam.Value = newProject.PARENTID;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(isactiveParam);
                    fbcmd.Parameters.Add(isdeletedParam);
                    fbcmd.Parameters.Add(descriptionParam);
                    fbcmd.Parameters.Add(projecttypeParam);
                    fbcmd.Parameters.Add(tierParam);
                    fbcmd.Parameters.Add(priorityidParam);
                    fbcmd.Parameters.Add(createdbyParam);
                    fbcmd.Parameters.Add(assignedtoParam);
                    fbcmd.Parameters.Add(recurringParam);
                    fbcmd.Parameters.Add(duedateParam);
                    fbcmd.Parameters.Add(parentidParam);

                    fbcmd.ExecuteNonQuery();
                    fbtrans.Commit();
                    status = "Success";
                    code = "200";
                    message = "All Good!";
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.StartsWith("violation of PRIMARY or UNIQUE KEY constraint \"UNQ"))
                    {
                        status = "Fail";
                        code = "503";
                        message = "DB Error - Duplicate code";
                    }
                    else
                    {
                        ExceptionsLogByFile logger = new ExceptionsLogByFile();
                        logger.LogException(ex);
                        status = "Fail";
                        code = "503";
                        message = "Internal Server Error - Cannot access database";
                    }
                }
                finally
                {
                    selectconnection.fbconnect.Close();
                }

                mbuilder.status = status;
                mbuilder.code = code;
                mbuilder.message = message;

                jbuilder.Meta = mbuilder;

                return Json(jbuilder);
            }
        }

        [Route("Projects/Update/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Update/5
        public IHttpActionResult Update(int updatedId, [FromBody]Project updatedProject)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string updatedBodyProjectDescription = updatedProject.DESCRIPTION;

            string sqlcmd = "update tag set " +
                "WHERE " +
                "ID = @ID and" +
                "Description = @DESCRIPTION";


            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter descriptionParam = new FbParameter("@DESCRIPTION", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    descriptionParam.Value = updatedBodyProjectDescription;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(descriptionParam);

                    fbcmd.ExecuteNonQuery();
                    fbtrans.Commit();
                    status = "Success";
                    code = "200";
                    message = "All Good!";
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
                    selectconnection.fbconnect.Close();
                }

                mbuilder.status = status;
                mbuilder.code = code;
                mbuilder.message = message;

                jbuilder.Meta = mbuilder;

                return Json(jbuilder);
            }
        }


        [Route("Projects/Delete/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Delete/5
        public IHttpActionResult LogicalDelete(int updatedId, [FromBody]Project updateProject)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "update projects set " +
                "ISDELETED=true " +
                " WHERE " +
                " ID = @ID and " +
                " DESCRIPTION = @DESCRIPTION";


            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter descriptionParam = new FbParameter("@DESCRIPTION", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    descriptionParam.Value = updateProject.DESCRIPTION;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(descriptionParam);

                    fbcmd.ExecuteNonQuery();
                    fbtrans.Commit();
                    status = "Success";
                    code = "200";
                    message = "All Good!";
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
                    selectconnection.fbconnect.Close();
                }

                mbuilder.status = status;
                mbuilder.code = code;
                mbuilder.message = message;

                jbuilder.Meta = mbuilder;

                return Json(jbuilder);
            }
        }

        [Route("Projects/Deactivate/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Deactivate/5
        public IHttpActionResult Deactivate(int updatedId, [FromBody]Project updateProject)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "update projects set " +
                "ISACTIVE=false " +
                "WHERE " +
                " ID = @ID and " +
                " DESCRIPTION = @DESCRIPTION";

            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter descriptionParam = new FbParameter("@DESCRIPTION", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    descriptionParam.Value = updateProject.DESCRIPTION;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(descriptionParam);

                    fbcmd.ExecuteNonQuery();
                    fbtrans.Commit();
                    status = "Success";
                    code = "200";
                    message = "All Good!";
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
                    selectconnection.fbconnect.Close();
                }

                mbuilder.status = status;
                mbuilder.code = code;
                mbuilder.message = message;

                jbuilder.Meta = mbuilder;

                return Json(jbuilder);
            }
        }


        [Route("Projects/UnDelete/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Delete/5
        public IHttpActionResult LogicalUndelete(int updatedId, [FromBody]Project updateProject)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "update projects set " +
                "ISDELETED=false " +
                " WHERE " +
                " ID = @ID and " +
                " DESCRIPTION = @DESCRIPTION";


            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter descriptionParam = new FbParameter("@DESCRIPTION", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    descriptionParam.Value = updateProject.DESCRIPTION;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(descriptionParam);

                    fbcmd.ExecuteNonQuery();
                    fbtrans.Commit();
                    status = "Success";
                    code = "200";
                    message = "All Good!";
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
                    selectconnection.fbconnect.Close();
                }

                mbuilder.status = status;
                mbuilder.code = code;
                mbuilder.message = message;

                jbuilder.Meta = mbuilder;

                return Json(jbuilder);
            }
        }

        [Route("Projects/Activate/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Deactivate/5
        public IHttpActionResult Activate(int updatedId, [FromBody]Tag updateTag)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "update projects set " +
                "ISACTIVE=true " +
                "WHERE " +
                " ID = @ID and " +
                " DESCRIPTION = @DESCRIPTION";

            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter descriptionParam = new FbParameter("@DESCRIPTION", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    descriptionParam.Value = updateTag.TAG;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(descriptionParam);

                    fbcmd.ExecuteNonQuery();
                    fbtrans.Commit();
                    status = "Success";
                    code = "200";
                    message = "All Good!";
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
                    selectconnection.fbconnect.Close();
                }

                mbuilder.status = status;
                mbuilder.code = code;
                mbuilder.message = message;

                jbuilder.Meta = mbuilder;

                return Json(jbuilder);
            }
        }
        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }

    }

}

