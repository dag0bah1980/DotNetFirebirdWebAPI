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
    public class ProjectTypesController : ApiController
    {
        [Route("Projecttypes")]
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

            string sqlcmd = "select * from projecttypes";

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

        [Route("Projecttypes/{requestedid}")]
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

            string sqlcmd = "select * from projecttypes where id = @requestedid";

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

        [Route("Projecttypes/CodeMatch/{requestedcode}")]
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

        [Route("Projecttypes")]
        [HttpPost]
        // POST: api/Tags
        public IHttpActionResult Post(ProjectType newProjectType)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "insert into projecttypes" +
                "(ISACTIVE,ISDELETED,PROJTYPECODE,PROJTYPENAME, PARENTPROJTYPEID,DESCRIPTION,ORDERVALUE)" +
                "VALUES " +
                "(@ISACTIVE,@ISDELETED,@PROJTYPECODE,@PROJTYPENAME,@PARENTPROJTYPEID,@DESCRIPTION,@ORDERVALUE)";


            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter isactiveParam = new FbParameter("@ISACTIVE", FbDbType.Boolean);
                    FbParameter isdeletedParam = new FbParameter("@ISDELETED", FbDbType.Boolean);
                    FbParameter projtypecodeParam = new FbParameter("@PROJTYPECODE", FbDbType.VarChar);
                    FbParameter projtypenameParam = new FbParameter("@PROJTYPENAME", FbDbType.VarChar);
                    FbParameter parentprojtypeidParam = new FbParameter("@PARENTPROJTYPEID", FbDbType.VarChar);
                    FbParameter descriptionParam = new FbParameter("@DESCRIPTION", FbDbType.VarChar);
                    FbParameter ordervalueParam = new FbParameter("@ORDERVALUE", FbDbType.Integer);


                    isactiveParam.Value = newProjectType.ISACTIVE;
                    isdeletedParam.Value = newProjectType.ISDELETED;
                    projtypecodeParam.Value = newProjectType.PROJTYPECODE;
                    projtypenameParam.Value = newProjectType.PROJTYPENAME;
                    parentprojtypeidParam.Value = newProjectType.PARENTPROJTYPEID;
                    descriptionParam.Value = newProjectType.DESCRIPTION;
                    ordervalueParam.Value = newProjectType.ORDERVALUE;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(isactiveParam);
                    fbcmd.Parameters.Add(isdeletedParam);
                    fbcmd.Parameters.Add(projtypecodeParam);
                    fbcmd.Parameters.Add(projtypenameParam);
                    fbcmd.Parameters.Add(parentprojtypeidParam);
                    fbcmd.Parameters.Add(descriptionParam);
                    fbcmd.Parameters.Add(ordervalueParam);

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

        [Route("Projecttypes/Update/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Update/5
        public IHttpActionResult Update(int updatedId, [FromBody]ProjectType updatedProjectType)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string updatedPriorityCode = updatedProjectType.PROJTYPECODE;

            string sqlcmd = "update ProjectType set " +
                "WHERE " +
                "ID = @ID and" +
                "PROJTYPECODE = @PROJTYPECODE";


            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter projtypecodeParam = new FbParameter("@PROJTYPECODE", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    projtypecodeParam.Value = updatedPriorityCode;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(projtypecodeParam);

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


        [Route("Projecttypes/Delete/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Delete/5
        public IHttpActionResult LogicalDelete(int updatedId, [FromBody]ProjectType updateProjectType)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "update priority set " +
                "ISDELETED=true " +
                " WHERE " +
                " ID = @ID and " +
                " PROJTYPECODE = @PROJTYPECODE";


            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter projtypecodeParam = new FbParameter("@PROJTYPECODE", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    projtypecodeParam.Value = updateProjectType.PROJTYPECODE;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(projtypecodeParam);

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

        [Route("Projecttypes/Deactivate/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Deactivate/5
        public IHttpActionResult Deactivate(int updatedId, [FromBody]ProjectType updateProjectType)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "update ProjectType set " +
                "ISACTIVE=false " +
                "WHERE " +
                " ID = @ID and " +
                " PROJTYPECODE = @PROJTYPECODE";

            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter projtypecodeParam = new FbParameter("@PROJTYPECODE", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    projtypecodeParam.Value = updateProjectType.PROJTYPECODE;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(projtypecodeParam);

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


        [Route("Projecttypes/UnDelete/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Delete/5
        public IHttpActionResult LogicalUndelete(int updatedId, [FromBody]ProjectType updateProjectType)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "update ProjectType set " +
                "ISDELETED=false " +
                " WHERE " +
                " ID = @ID and " +
                " PROJTYPECODE = @PROJTYPECODE";


            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter projtypecodeParam = new FbParameter("@PROJTYPECODE", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    projtypecodeParam.Value = updateProjectType.PROJTYPECODE;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(projtypecodeParam);

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

        [Route("Projecttypes/Activate/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Deactivate/5
        public IHttpActionResult Activate(int updatedId, [FromBody]ProjectType updateProjectType)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "update ProjectType set " +
                "ISACTIVE=true " +
                "WHERE " +
                " ID = @ID and " +
                " PROJTYPECODE = @PROJTYPECODE";

            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter projtypecodeParam = new FbParameter("@PROJTYPECODE", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    projtypecodeParam.Value = updateProjectType.PROJTYPECODE;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(projtypecodeParam);

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
