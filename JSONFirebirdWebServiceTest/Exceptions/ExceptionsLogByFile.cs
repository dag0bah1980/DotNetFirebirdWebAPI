using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Configuration;
using System.Collections;
using System.Collections.Specialized;

namespace JSONFirebirdWebServiceTest.Exceptions
{
    public class ExceptionsLogByFile
    {
        public static string ConnectionExceptionsFile="App_Data\\ErrorLog";

        public static string AppendTimeToLog (string FileName)
        {
            return string.Concat(FileName, DateTime.Now.ToString("yyyyMMdd"), ".txt");
        }

        public static string AppendErrorLogPathAndName (string Filename)
        {
            return string.Concat(HttpRuntime.AppDomainAppPath, ConnectionExceptionsFile, Filename);
        }

        public void LogException(Exception exc)
        {
            string ExceptionsFile ="";

            ExceptionsFile = AppendErrorLogPathAndName(ExceptionsFile);
            ExceptionsFile = AppendTimeToLog(ExceptionsFile);


            using (StreamWriter sw = File.AppendText(ExceptionsFile))
            {
                sw.WriteLine("********** {0} **********", DateTime.Now);
                string requestedResource = "Requested Resource: ";
                requestedResource = string.Concat(requestedResource, HttpContext.Current.Request.Url.AbsoluteUri);

                sw.WriteLine(requestedResource, DateTime.Now);

                string clientDetails = "Client Browser Details: ";

                var userAgent = HttpContext.Current.Request.UserAgent;
                var userBrowser = new HttpBrowserCapabilities { Capabilities = new Hashtable { { string.Empty, userAgent } } };
                var factory = new BrowserCapabilitiesFactory();
                factory.ConfigureBrowserCapabilities(new NameValueCollection(), userBrowser);

                //Set User browser Properties
                var BrowserBrand = userBrowser.Browser;
                var BrowserVersion = userBrowser.Version;

                clientDetails = string.Concat(clientDetails, " ", BrowserBrand, " ", BrowserVersion);
                
                sw.WriteLine(clientDetails, DateTime.Now);

                //See if it's a mobile device
                string mobileBoolean = "Mobile Device: ";
                var MobileBoolean = userBrowser.IsMobileDevice;

                mobileBoolean = string.Concat(mobileBoolean, MobileBoolean.ToString());
                sw.WriteLine(mobileBoolean, DateTime.Now);

                //Get IP's
                string clientAddress = "Client IP: ";
                //::1 is loopback IPv6 (localhost). Try from different computer to confirm
                //Other things that we may want to add: HttpContext.Current.Request.UserHostName which is DNS and HttpContext.Current.Request.UrlReferrer which is clients previous request that linked to the current url
                clientAddress = string.Concat(clientAddress, HttpContext.Current.Request.UserHostAddress);
                sw.WriteLine(clientAddress, DateTime.Now);

                //Get Request Type
                string requestType = "Request Type: ";
                requestType = string.Concat(requestType, HttpContext.Current.Request.RequestType);
                sw.WriteLine(requestType, DateTime.Now);



                
                if (exc.InnerException != null)
                {
                    sw.Write("Inner Exception Type: ");
                    sw.WriteLine(exc.InnerException.GetType().ToString());
                    sw.Write("Inner Exception: ");
                    sw.WriteLine(exc.InnerException.Message);
                    sw.Write("Inner Source: ");
                    sw.WriteLine(exc.InnerException.Source);
                    if (exc.InnerException.StackTrace != null)
                    {
                        sw.WriteLine("Inner Stack Trace: ");
                        sw.WriteLine(exc.InnerException.StackTrace);
                    }
                }
                sw.Write("Exception Type: ");
                sw.WriteLine(exc.GetType().ToString());
                sw.WriteLine("Exception: " + exc.Message);
                sw.WriteLine("Stack Trace: ");
                if (exc.StackTrace != null)
                {
                    sw.WriteLine(exc.StackTrace);
                    sw.WriteLine();
                }
                sw.Close();
            }
        }

    }
}