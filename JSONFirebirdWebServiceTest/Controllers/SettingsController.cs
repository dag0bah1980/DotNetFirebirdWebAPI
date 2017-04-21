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

namespace JSONFirebirdWebServiceTest.Controllers
{
    [RoutePrefix("api")]
    public class SettingsController : ApiController
    {

        [Route("Settings")]
        [HttpGet]
        // GET: api/Settings
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

        [Route("Settings/{requestedid}")]
        [HttpGet]
        // GET: api/Settings
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

            string sqlcmd = "select * from settings where id = @requestedid";

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


        [Route("Settings")]
        [HttpPost]
        // POST: api/Settings
        public IHttpActionResult Post(Setting newSetting)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "insert into settings (CREATED,MODIFIED,KEYNAME,FIELDTYPE,STRINGVALUE,BOOLVALUE)" +
                //"INTVALUE,FLOATVALUE,DATEVALUE,TIMESTAMPVALUE,TIMEVALUE,CATEGORYNAME,USERID," +
                //"DEVICENAME,BLOBTXTVALUE,BLOBBINVALUE)" +
                "VALUES " +
                "(@CREATED, @MODIFIED, @KEYNAME, @FIELDTYPE, @STRINGVALUE, @BOOLVALUE)";
            //"@INTVALUE,@FLOATVALUE,@DATEVALUE,@TIMESTAMPVALUE,@TIMEVALUE,@CATEGORYNAME,@USERID," +
            //"@DEVICENAME,@BLOBTXTVALUE,@BLOBBINVALUE)";

            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter createdParam = new FbParameter("@CREATED", FbDbType.TimeStamp);
                    FbParameter modifiedParam = new FbParameter("@MODIFIED", FbDbType.TimeStamp);
                    FbParameter keynameParam = new FbParameter("@KEYNAME", FbDbType.VarChar);
                    FbParameter fieldtypeParam = new FbParameter("@FIELDTYPE", FbDbType.VarChar);
                    FbParameter stringvalueParam = new FbParameter("@STRINGVALUE", FbDbType.VarChar);
                    FbParameter boolvalueParam = new FbParameter("@BOOLVALUE", FbDbType.Boolean);

                    createdParam.Value = DateTime.Now.ToString();
                    modifiedParam.Value = DateTime.Now.ToString();
                    keynameParam.Value = newSetting.KEYNAME;
                    fieldtypeParam.Value = newSetting.FIELDTYPE;
                    stringvalueParam.Value = newSetting.STRINGVALUE;
                    boolvalueParam.Value = newSetting.BOOLVALUE;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(createdParam);
                    fbcmd.Parameters.Add(modifiedParam);
                    fbcmd.Parameters.Add(keynameParam);
                    fbcmd.Parameters.Add(fieldtypeParam);
                    fbcmd.Parameters.Add(stringvalueParam);
                    fbcmd.Parameters.Add(boolvalueParam);


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

                }

                mbuilder.status = status;
                mbuilder.code = code;
                mbuilder.message = message;

                jbuilder.Meta = mbuilder;

                return Json(jbuilder);
            }
        }

        [Route("Settings/{updatedId}")]
        [HttpPut]
        // PUT: api/Settings/5
        public IHttpActionResult Update(int updatedId, [FromBody]Setting updatedSetting)
        {
            DBConnection fbconndetails = new DBConnection();

            Connection selectconnection = new Connection(fbconndetails.DBHost, string.Concat(fbconndetails.DBPath, fbconndetails.DBFile), Convert.ToInt32(fbconndetails.DBPort), fbconndetails.DBUser, fbconndetails.DBPassword, fbconndetails.DBConnectionLifeTime, fbconndetails.DBPooling, fbconndetails.DBMinPoolSize, fbconndetails.DBMaxPoolSize);

            JSONObject jbuilder = new JSONObject();
            JSONObject.MetaDetails mbuilder = new JSONObject.MetaDetails();

            string status = "";
            string code = "";
            string message = "";

            string sqlcmd = "update settings set " +
                "MODIFIED=@MODIFIED, " +
                "KEYNAME=@KEYNAME, " +
                "FIELDTYPE=@FIELDTYPE, " +
                "STRINGVALUE=@STRINGVALUE, " +
                "BOOLVALUE=@BOOLVALUE " +
                "WHERE ID = @ID";
           

            using (selectconnection.fbconnect)
            {

                try
                {
                    selectconnection.fbconnect.Open();
                    FbTransaction fbtrans = selectconnection.fbconnect.BeginTransaction();
                    FbParameter idParam = new FbParameter("@ID", FbDbType.BigInt);
                    FbParameter modifiedParam = new FbParameter("@MODIFIED", FbDbType.TimeStamp);
                    FbParameter keynameParam = new FbParameter("@KEYNAME", FbDbType.VarChar);
                    FbParameter fieldtypeParam = new FbParameter("@FIELDTYPE", FbDbType.VarChar);
                    FbParameter stringvalueParam = new FbParameter("@STRINGVALUE", FbDbType.VarChar);
                    FbParameter boolvalueParam = new FbParameter("@BOOLVALUE", FbDbType.Boolean);

                    idParam.Value = updatedId;
                    modifiedParam.Value = DateTime.Now.ToString();
                    keynameParam.Value = updatedSetting.KEYNAME;
                    fieldtypeParam.Value = updatedSetting.FIELDTYPE;
                    stringvalueParam.Value = updatedSetting.STRINGVALUE;
                    boolvalueParam.Value = updatedSetting.BOOLVALUE;

                    FbCommand fbcmd = new FbCommand(sqlcmd, selectconnection.fbconnect, fbtrans);
                    fbcmd.Parameters.Add(idParam);
                    fbcmd.Parameters.Add(modifiedParam);
                    fbcmd.Parameters.Add(keynameParam);
                    fbcmd.Parameters.Add(fieldtypeParam);
                    fbcmd.Parameters.Add(stringvalueParam);
                    fbcmd.Parameters.Add(boolvalueParam);


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

                }

                mbuilder.status = status;
                mbuilder.code = code;
                mbuilder.message = message;

                jbuilder.Meta = mbuilder;

                return Json(jbuilder);
            }
        }

        // DELETE: api/Settings/5
        public void Delete(int id)
        {
        }
    }
}
