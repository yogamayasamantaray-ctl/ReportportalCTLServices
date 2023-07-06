using DataAccessLogicLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BussinessLogicLayer
{
    public class LoginServices
    {
        public string GetLoginAccessDetails(string UserName)
        {
            string qryStatus = string.Empty;
            string existUsers = string.Empty;
            string jsonstring = string.Empty;
            string strQry;
            List<string> existUsers1 = new List<string>();


            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();
                string userName = HttpContext.Current.Request.LogonUserIdentity.Name;
                String[] breakApart = userName.Split('\\');
                ////string strqery = "select a.user_id,a.user_name,b.role_id,r.role_desc,b.WC_ID from QUAL_SCHEMA.user_details a inner join QUAL_SCHEMA.user_role b on a.user_id=b.user_id inner join QUAL_SCHEMA.lut_roles r on b.Role_id = r.Role_id where UPPER(a.user_name) = '" + breakApart[1].ToUpper() + "'";
                //string strqery = "select b.user_id,b.user_id as user_name,b.role_id,r.role_desc,b.WC_ID from QUAL_SCHEMA.user_role b inner join QUAL_SCHEMA.lut_roles r on b.Role_id = r.Role_id where UPPER(b.user_id) = '" + breakApart[1].ToUpper() + "'";
                //DataSet userSet = detAccess.ExecuteQuerySWGISLOC(strqery);
                //userTable = userSet.Tables[0];
                //jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
                userTable = detAccess.GetLoginAccessDetails(breakApart[1].ToUpper());
                loguser(userName, "login");
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetLoginAccessDetails");
                throw ex;
            }
            return jsonstring;
        }
        public string GetHacthonData(string Objectid)
        {
            
            string jsonstring = string.Empty;


            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                
                //string strqery = "select a.user_id,a.user_name,b.role_id,r.role_desc,b.WC_ID from QUAL_SCHEMA.user_details a inner join QUAL_SCHEMA.user_role b on a.user_id=b.user_id inner join QUAL_SCHEMA.lut_roles r on b.Role_id = r.Role_id where UPPER(a.user_name) = '" + breakApart[1].ToUpper() + "'";
                string strqery = "select QUAL_JSON from qual_schema.CL_SUBADDRESS_POINT where objectid = '" + Objectid + "'";
                DataSet userSet = detAccess.ExecuteQuerySWGISLOC(strqery);
                userTable = userSet.Tables[0];
                if (userTable.Rows[0][0].ToString() != "")
                {
                    Byte[] bytes = (byte[])userTable.Rows[0][0];
                    string base64String = Encoding.UTF8.GetString(bytes);
                    jsonstring = base64String;
                }
               // jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetHacthonData");
                throw ex;
            }
            return jsonstring;
        }

        public void loguser(string username, string remarks)
        {

            string strQry;

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string path = HttpContext.Current.Server.MapPath("log.xml"); // Server.MapPath("log.xml");
            if (File.Exists(path))
            {
                ds.ReadXml(path);
                dt = ds.Tables[0];
            }
            else
            {
                dt.Columns.Add("CUID");
                dt.Columns.Add("time");
                dt.Columns.Add("remarks");
                dt.TableName = "Userlog";
                ds.Tables.Add(dt);
            }
            DataRow dr = ds.Tables[0].NewRow();
            dr["CUID"] = username;
            dr["remarks"] = remarks;
            dr["time"] = DateTime.Now.ToString("dd'-'MMM'-'yyyy hh:mm:ss tt");
            ds.Tables[0].Rows.Add(dr);
            AccessData detAccess = new AccessData();
            strQry = "INSERT INTO DASHBOARDLOGS(CUID, REMARKS, TIME) VALUES( '" + dr["CUID"] + "', '" + dr["remarks"] + "','" + dr["time"] + "')";
           var val = detAccess.ExecuteQueryForInsert(strQry);
            ds.WriteXml(path);
        }
    }
}
