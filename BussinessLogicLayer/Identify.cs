using DataAccessLogicLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer
{
    public class Identify
    {
        public string getOdinAddreses(string strTriggerId)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                userTable = detAccess.GetodinAddresses(strTriggerId);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetServicePolygonFailures");
                throw ex;
            }
            return jsonstring;
        }

        public string getOdinJson(string strTriggerId)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                userTable = detAccess.GetodinJson(strTriggerId);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetServicePolygonFailures");
                throw ex;
            }
            return jsonstring;
        }

        public string getCopperAddresses(string strAPN,string strFIPS)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                userTable = detAccess.GetCopperAddresses(strAPN,strFIPS);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetServicePolygonFailures");
                throw ex;
            }
            return jsonstring;
        }
    }
}
