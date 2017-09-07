using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;

namespace JSONFirebirdWebServiceTest.SQLTools
{
    public class UpdateSetParser
    {
        public string ObjectToStringSetParameters(Object obj)
        {
            //var json = new JavaScriptSerializer().Serialize(obj);            
            //return json.ToString();

            string ListOfSetStatements = "";
            Type objType = obj.GetType();
            objType.GetProperties();
            foreach (PropertyInfo propertyInfo in objType.GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    object objValue = propertyInfo.GetValue(obj, null);
                    string currPropName = propertyInfo.Name;
                    string currPropType = propertyInfo.GetType().ToString();
                    string currPropValue = propertyInfo.GetValue(obj, null).ToString();

                    //ListOfSetStatements = ListOfSetStatements + "UList:" + propertyInfo.ToString() + " " + objValue;                  
                    ListOfSetStatements = ListOfSetStatements + currPropName + " = " + currPropValue + " , ";
                }
            }

            return ListOfSetStatements;
        }
    }
}