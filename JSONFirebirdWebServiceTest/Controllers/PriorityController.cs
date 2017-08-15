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
    public class PriorityController : ApiController
    {
        [Route("Priorities")]
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

            string sqlcmd = "select * from priority";

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

        [Route("Priorities/{requestedid}")]
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

            string sqlcmd = "select * from priority where id = @requestedid";

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

        [Route("Priorities/CodeMatch/{requestedcode}")]
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

        [Route("Priorities")]
        [HttpPost]
        // POST: api/Tags
        public IHttpActionResult Post(Priority newPriority)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "insert into priority" +
                "(ISACTIVE,ISDELETED,PRIORITYCODE,PRIORITYNAME,DESCRIPTION,ORDERVALUE)" +
                "VALUES " +
                "(@ISACTIVE,@ISDELETED,@PRIORITYCODE,@PRIORITYNAME,@DESCRIPTION,@ORDERVALUE)";


            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter isactiveParam = new FbParameter("@ISACTIVE", FbDbType.Boolean);
                    FbParameter isdeletedParam = new FbParameter("@ISDELETED", FbDbType.Boolean);
                    FbParameter prioritycodeParam = new FbParameter("@PRIORITYCODE", FbDbType.VarChar);
                    FbParameter prioritynameParam = new FbParameter("@PRIORITYNAME", FbDbType.VarChar);
                    FbParameter descriptionParam = new FbParameter("@DESCRIPTION", FbDbType.VarChar);
                    FbParameter ordervalueParam = new FbParameter("@ORDERVALUE", FbDbType.Integer);


                    isactiveParam.Value = newPriority.ISACTIVE;
                    isdeletedParam.Value = newPriority.ISDELETED;
                    prioritycodeParam.Value = newPriority.PRIORITYCODE;
                    prioritynameParam.Value = newPriority.PRIORITYNAME;
                    descriptionParam.Value = newPriority.DESCRIPTION;
                    ordervalueParam.Value = newPriority.ORDERVALUE;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(isactiveParam);
                    fbcmd.Parameters.Add(isdeletedParam);
                    fbcmd.Parameters.Add(prioritycodeParam);
                    fbcmd.Parameters.Add(prioritynameParam);
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

        [Route("Priorities/Update/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Update/5
        public IHttpActionResult Update(int updatedId, [FromBody]Priority updatedPriority)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string updatedPriorityCode = updatedPriority.PRIORITYCODE;

            string sqlcmd = "update priority set " +
                "WHERE " +
                "ID = @ID and" +
                "Prioritycode = @PRIORITYCODE";


            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter prioritycodeParam = new FbParameter("@PRIORITYCODE", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    prioritycodeParam.Value = updatedPriorityCode;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(prioritycodeParam);

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


        [Route("Priorities/Delete/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Delete/5
        public IHttpActionResult LogicalDelete(int updatedId, [FromBody]Priority updatePriority)
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
                " PRIORITYCODE = @PRIORITYCODE";


            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter prioritycodeParam = new FbParameter("@PRIORITYCODE", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    prioritycodeParam.Value = updatePriority.PRIORITYCODE;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(prioritycodeParam);

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

        [Route("Priorities/Deactivate/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Deactivate/5
        public IHttpActionResult Deactivate(int updatedId, [FromBody]Priority updatePriority)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "update priority set " +
                "ISACTIVE=false " +
                "WHERE " +
                " ID = @ID and " +
                " PRIORITYCODE = @PRIORITYCODE";

            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter prioritycodeParam = new FbParameter("@PRIORITYCODE", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    prioritycodeParam.Value = updatePriority.PRIORITYCODE;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(prioritycodeParam);

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


        [Route("Priorities/UnDelete/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Delete/5
        public IHttpActionResult LogicalUndelete(int updatedId, [FromBody]Priority updatePriority)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "update priority set " +
                "ISDELETED=false " +
                " WHERE " +
                " ID = @ID and " +
                " PRIORITYCODE = @PRIORITYCODE";


            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter prioritycodeParam = new FbParameter("@PRIORITYCODE", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    prioritycodeParam.Value = updatePriority.PRIORITYCODE;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(prioritycodeParam);

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

        [Route("Priorities/Activate/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Deactivate/5
        public IHttpActionResult Activate(int updatedId, [FromBody]Priority updatePriority)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "update priority set " +
                "ISACTIVE=true " +
                "WHERE " +
                " ID = @ID and " +
                " PRIORITYCODE = @PRIORITYCODE";

            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter prioritycodeParam = new FbParameter("@PRIORITYCODE", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    prioritycodeParam.Value = updatePriority.PRIORITYCODE;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(prioritycodeParam);

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

