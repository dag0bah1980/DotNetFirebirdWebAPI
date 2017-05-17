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
    public class TagsController : ApiController
    {
        [Route("Tags")]
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

            string sqlcmd = "select * from tags";

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

        [Route("Tags/{requestedid}")]
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

            string sqlcmd = "select * from tags where id = @requestedid";

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

        [Route("Tags")]
        [HttpPost]
        // POST: api/Tags
        public IHttpActionResult Post(Tag newTag)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "insert into tags (ISACTIVE,ISDELETED,TAG,DESCRIPTION)" +
                "VALUES " +
                "(@ISACTIVE, @ISDELETED, @TAG, @DESCRIPTION)";
           

            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter isactiveParam = new FbParameter("@ISACTIVE", FbDbType.Boolean);
                    FbParameter isdeletedParam = new FbParameter("@ISDELETED", FbDbType.Boolean);
                    FbParameter tagParam = new FbParameter("@TAG", FbDbType.VarChar);
                    FbParameter descriptionParam = new FbParameter("@DESCRIPTION", FbDbType.VarChar);

                    isactiveParam.Value = newTag.ISACTIVE;
                    isdeletedParam.Value = newTag.ISDELETED;
                    tagParam.Value = newTag.TAG;
                    descriptionParam.Value = newTag.DESCRIPTION;


                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(isactiveParam);
                    fbcmd.Parameters.Add(isdeletedParam);
                    fbcmd.Parameters.Add(tagParam);
                    fbcmd.Parameters.Add(descriptionParam);


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

        [Route("Tags/Update/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Update/5
        public IHttpActionResult Update(int updatedId, [FromBody]Tag updatedTag)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "update tag set " +
                "MODIFIED=@MODIFIED " +
                "WHERE ID = @ID";


            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter modifiedParam = new FbParameter("@MODIFIED", FbDbType.TimeStamp);


                    idParam.Value = updatedId;
                    modifiedParam.Value = DateTime.Now.ToString();


                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(modifiedParam);



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


        [Route("Tags/Delete/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Delete/5
        public IHttpActionResult LogicalDelete(int updatedId, [FromBody]Tag updateTag)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "update tags set " +
                "ISDELETED=true " +
                " WHERE "  + 
                " ID = @ID and " +
                " TAG = @TAG";


            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter tagParam = new FbParameter("@TAG", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    tagParam.Value = updateTag.TAG;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(tagParam);

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

        [Route("Tags/Deactivate/{updatedId}")]
        [HttpPut]
        // PUT: api/Tags/Deactivate/5
        public IHttpActionResult Deactivate(int updatedId, [FromBody]Tag updateTag)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "update tags set " +
                "ISACTIVE=false " +
                "WHERE " +
                " ID = @ID and " +
                " TAG = @TAG";

            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter tagParam = new FbParameter("@TAG", FbDbType.VarChar);

                    idParam.Value = updatedId;
                    tagParam.Value = updateTag.TAG;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(tagParam);

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
