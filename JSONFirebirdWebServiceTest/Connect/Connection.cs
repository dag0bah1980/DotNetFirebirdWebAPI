using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FirebirdSql.Data.FirebirdClient;
using System.IO;
using JSONFirebirdWebServiceTest.Exceptions;


namespace JSONFirebirdWebServiceTest.Connect
{
    public class Connection
    {        
        public FbConnection fbconnect;
        public bool testconnect;

        public void CloseConnection(Connection c)
        {
            try
            {
                c.fbconnect.Close();

            }
            catch (Exception ex)
            {
                ExceptionsLogByFile logger = new ExceptionsLogByFile();
                logger.LogException(ex);

            }
        }
      
        public Connection(string host, string data, int port, string user, string password, int connectionlifetime, bool pooling, int minpoolsize, int maxpoolsize)
        {
            try
            {
                FbConnectionStringBuilder fbconnstr = new FbConnectionStringBuilder();
                fbconnstr.DataSource = host;
                fbconnstr.Database = data;
                fbconnstr.Port = port;
                fbconnstr.UserID = user;
                fbconnstr.Password = password;
                fbconnstr.ConnectionLifeTime = connectionlifetime;
                fbconnstr.Pooling = pooling;
                fbconnstr.MinPoolSize = minpoolsize;
                fbconnstr.MaxPoolSize = maxpoolsize;

                fbconnect = new FbConnection(fbconnstr.ToString());
            }
            catch (Exception ex)
            {
                ExceptionsLogByFile logger = new ExceptionsLogByFile();
                logger.LogException(ex);
            }
        }

        public bool TestConnection(string host, string data, int port, string user, string password, int connectionlifetime, bool pooling, int minpoolsize, int maxpoolsize)
        {
            bool result = false;
            FbConnectionStringBuilder fbconnstr = new FbConnectionStringBuilder();
            fbconnstr.DataSource = host;
            fbconnstr.Database = data;
            fbconnstr.Port = port;
            fbconnstr.UserID = user;
            fbconnstr.Password = password;
            fbconnstr.ConnectionLifeTime = connectionlifetime;
            fbconnstr.Pooling = pooling;
            fbconnstr.MinPoolSize = minpoolsize;
            fbconnstr.MaxPoolSize = maxpoolsize;

            fbconnect = new FbConnection(fbconnstr.ToString());


            using (fbconnect)
            {
                try
                {
                    fbconnect.Open();
                    result = true;
                }
                catch (Exception ex)
                {
                    ExceptionsLogByFile logger = new ExceptionsLogByFile();
                    logger.LogException(ex);
                }
                finally
                {
                    fbconnect.Close();
                }
                return result;                
            }
        }

    }

}