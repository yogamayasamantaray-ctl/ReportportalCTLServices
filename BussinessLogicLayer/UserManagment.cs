using DataAccessLogicLayer;
using System;
using System.Data;

namespace BussinessLogicLayer
{
    public class UserManagment
    {
        public Boolean ExecuteQueryForInsertingDataIntoUers(string User_id, string User_Name)
        {
            string qryStatus = string.Empty;
            string existUsers = string.Empty;
            bool val = false;
            string strQry;


            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                string strqery = string.Empty;
                strqery = "select USER_NAME from USER_DETAILS where UPPER(USER_ID) = '" + User_id.ToUpper() + "'";
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                //string[] arrayOfTests = User_Name.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
                //User_Name = String.Join(", ", arrayOfTests);
                User_Name = User_Name.Replace(",", " ");
                User_Name = User_Name.Replace("'", " ");
                //string passwordDB = Dec.Decrypt(userTable.Rows[0][0].ToString());
                if (userTable.Rows.Count == 0)
                {
                    strQry = "insert INTO user_details(USER_ID,user_name,creation_date) VALUES('" + User_id + "','" + User_Name + "',sysdate)";
                    val = detAccess.ExecuteQueryForInsert(strQry);
                }
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "ExecuteQueryForInsertingDataIntoUers");
                throw ex;
            }
            return val;
        }
        public Boolean AssignUserRole(string user_id, string role_id, string assignedby, string eng_Boundary)
        {
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {

                val = detAccess.AssignUserRole(user_id, role_id, assignedby, eng_Boundary);

            }
            catch (Exception ex)
            {
                AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "UpdateCopperParcel");
                throw ex;
            }
            return val;
        }
        //public Boolean AssignUserRole(string user_ID, string Role_id, string Wirecenter_id)
        //{
        //    string qryStatus = string.Empty;
        //    string existUsers = string.Empty;
        //    bool val = false;
        //    string strQry;


        //    try
        //    {
        //        AccessData detAccess = new AccessData();
        //        strQry = "insert INTO USER_ROLE(USER_ID,ROLE_ID,WC_ID,CR_UPD_DATE) VALUES('" + user_ID + "','" + Role_id + "','" + Wirecenter_id + "',sysdate)";
        //        val = detAccess.ExecuteQueryForInsert(strQry);
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return val;
        //}
        public Boolean DeleteUserRole(string objectid,string assignedby)
        {
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {

                val = detAccess.DeleteUserRole(objectid, assignedby);

            }
            catch (Exception ex)
            {
                AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "UpdateCopperParcel");
                throw ex;
            }
            return val;
        }
        public Boolean UpdatingUserRole(string user_ID, string Role_id, string Wirecenter_id)
        {
            string qryStatus = string.Empty;
            string existUsers = string.Empty;
            bool val = false;
            string strQry;


            try
            {
                AccessData detAccess = new AccessData();
                strQry = "update USER_ROLE set ROLE_ID='" + Role_id + "' where USER_ID = '" + user_ID + "' and WC_ID = '" + Wirecenter_id + "'";
                val = detAccess.ExecuteQueryForInsert(strQry);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "UpdatingUserRole");
                throw ex;
            }
            return val;
        }
        public string GetForgetPassword(string User_name, string HInt_id, string Hint_Ans)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();
                string strqery = string.Empty;

                strqery = "select PWD from user_details where HINT_QTN_ID= '" + HInt_id + "' and HINT_ANS = '" + Hint_Ans + "' and user_name = '" + User_name + "'";

                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetForgetPassword");
                throw ex;
            }
            return jsonstring;
        }
        public string GetRoleList(string RoleID)
        {
            string jsonstring = string.Empty;
            try
            {
                //string jsonstring = string.Empty;
                try
                {
                    AccessData detAccess = new AccessData();
                    DataTable userTable = new DataTable();
                    //Utilities Util = new Utilities();

                    userTable = detAccess.GetRoleList(RoleID);
                    //jsonstring = Utilities.ConvertDataTableToJSNString(userTable);

                    jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
                }
                catch (Exception ex)
                {
                    AccessData detAccess = new AccessData();
                    detAccess.RecordLogs(ex.Message.ToString(), "GetBIW_Boundary_Check");
                    throw ex;
                }
                return jsonstring;
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetRoleList");
                throw ex;
            }
            return jsonstring;
        }
        public Boolean Login(string User_name, string password)
        {
            bool jsonstring = false;
            try
            {
                Utilities Dec = new Utilities();
                AccessData detAccess = new AccessData();
                password = Dec.Decrypt(password);
                DataTable userTable = new DataTable();
                string strqery = string.Empty;
                User_name = User_name.ToUpper();
                strqery = "select PWD from USER_DETAILS where UPPER(USER_NAME) = '" + User_name.ToUpper() + "'";
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                string passwordDB = Dec.Decrypt(userTable.Rows[0][0].ToString());
                if (passwordDB == password)
                {
                    jsonstring = true;
                }
                else
                {
                    jsonstring = false;
                }
                //jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "Login");
                throw ex;
            }
            return jsonstring;
        }
        public Boolean VerifyUser(string User_name)
        {
            bool jsonstring = false;
            try
            {
                Utilities Dec = new Utilities();
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                string strqery = string.Empty;
                User_name = User_name.ToUpper();
                strqery = "select USER_NAME from USER_DETAILS where UPPER(USER_NAME) = '" + User_name.ToUpper() + "'";
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                //string passwordDB = Dec.Decrypt(userTable.Rows[0][0].ToString());
                if (userTable.Rows.Count != 0)
                {
                    jsonstring = true;
                }


                //jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "VerifyUser");
                throw ex;
            }
            return jsonstring;
        }
        public string VerifyWireCenter(string ROle_id, string wirecenter_id)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                string strqery = string.Empty;
                //strqery = "select ur.USER_ID,ud.USER_NAME from qual_schema.USER_ROLE ur inner join qual_schema.USER_DETAILS ud on ur.USER_ID = ud.USER_ID where ur.ROLE_ID = '"+ ROle_id + "' and ur.WC_ID = '"+ wirecenter_id + "'";
                strqery = "select ur.USER_ID,ur.USER_ID as USER_NAME from qual_schema.USER_ROLE ur  where ur.ROLE_ID = '" + ROle_id + "' and ur.WC_ID = '" + wirecenter_id + "'";
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);


                //jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "VerifyWireCenter");
                throw ex;
            }
            return jsonstring;
        }
        public string GetHintQnsList()
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                string strqery = string.Empty;
                strqery = "Select * from QUAL_SCHEMA.LUT_HINT_QTN";
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetHintQnsList");
                throw ex;
            }
            return jsonstring;
        }
        public string GetUserList(string RoleID)
        {
            string jsonstring = string.Empty;
            try
            {
                //string jsonstring = string.Empty;
                try
                {
                    AccessData detAccess = new AccessData();
                    DataTable userTable = new DataTable();
                    //Utilities Util = new Utilities();

                    userTable = detAccess.GetUserList(RoleID);
                    //jsonstring = Utilities.ConvertDataTableToJSNString(userTable);

                    jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
                }
                catch (Exception ex)
                {
                    AccessData detAccess = new AccessData();
                    detAccess.RecordLogs(ex.Message.ToString(), "GetBIW_Boundary_Check");
                    throw ex;
                }
                return jsonstring;
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetUserList");
                throw ex;
            }
            return jsonstring;
        }

        public string GetWireCenterRoles(string WCID, string Role_ID, string Eng_Bound)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //string strqery = string.Empty;
                //strqery = "select  WC_ID as wc_name,( case when role_id=1 then user_id  end) Execute, ( case when role_id=2 then user_id end) as Review," +
                //          "( case when role_id = 3 then user_id end) publish from user_role where role_id in (1, 2, 3) and wc_id in (" + WCID + ")";
                //DataSet userSet = detAccess.ExecuteQuery(strqery);
                //userTable = userSet.Tables[0];
                userTable = detAccess.GetWireCenterRoles(WCID, Role_ID, Eng_Bound);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetWireCenterRoles");
                throw ex;
            }
            return jsonstring;
        }
        public string GetWireCenterForRole()
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                string strqery = string.Empty;
                strqery = "select distinct (sc.wc_id || '-' || sc.WIRE_CENTER_NAME) as WIRE_CENTER_NAME,sc.ID,sc.WC_ID from swgisloc.GIS_LOCAL_CL_WIRE_CENTER sc";
                DataSet userSet = detAccess.ExecuteQuerySWGISLOC(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetWireCenterForRole");
                throw ex;
            }
            return jsonstring;
        }

        public string GetWireCenterIdName()
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                string strqery = string.Empty;
                strqery = "select distinct (avr.WIRE_CENTER_CLLI || '-' || sc.WIRE_CENTER_NAME) as WIRE_CENTER_NAME,(sc.ID|| '-' || sc.WC_ID) as ID from swgisloc.GIS_LOCAL_CL_WIRE_CENTER sc,qual_schema.addr_validation_report avr where sc.WC_ID = avr.WIRE_CENTER_CLLI";
                DataSet userSet = detAccess.ExecuteQuerySWGISLOC(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetWireCenterIdName");
                throw ex;
            }
            return jsonstring;
        }

        public string GetDesignationWCID(string WirecenterID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                string strqery = string.Empty;
                strqery = "select distinct(DESIGNATION_NO) from addr_validation_report where WIRE_CENTER_CLLI= '" + WirecenterID + "'";
                //strqery = "select distinct(DESIGNATION_NO) from addr_validation_report where WIRE_CENTER_CLLI='MPLSMNBE'";
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetDesignationWCID");
                throw ex;
            }
            return jsonstring;
        }

        public string GetUserDetails(string User_ID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                string strqery = string.Empty;
                if (User_ID != "0")
                {
                    strqery = "select UR.USER_ID,ud.USER_NAME as USER_NAME,UR.ROLE_ID, lr.ROLE_DESC,UR.WC_ID,(clw.wc_id || '-' || clw.WIRE_CENTER_NAME) as WIRE_CENTER_NAME,TO_CHAR(UR.CR_UPD_DATE, 'MM-DD-YYYY HH:MI:SS AM') CR_UPD_DATE from qual_schema.USER_ROLE UR  inner join qual_schema.LUT_ROLES lr on UR.ROLE_ID = lr.ROLE_ID inner join qual_Schema.USER_DETAILS ud on ur.USER_ID = ud.USER_ID left join SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER clw on UR.WC_ID = clw.ID where UR.USER_ID = '" + User_ID + "'";
                }
                else
                {
                    strqery = "select UR.USER_ID,ud.USER_NAME as USER_NAME,UR.ROLE_ID, lr.ROLE_DESC,UR.WC_ID,(clw.wc_id || '-' || clw.WIRE_CENTER_NAME) as WIRE_CENTER_NAME,TO_CHAR(UR.CR_UPD_DATE, 'MM-DD-YYYY HH:MI:SS AM') CR_UPD_DATE from qual_schema.USER_ROLE UR  inner join qual_schema.LUT_ROLES lr on UR.ROLE_ID = lr.ROLE_ID inner join qual_Schema.USER_DETAILS ud on ur.USER_ID = ud.USER_ID left join SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER clw on UR.WC_ID = clw.ID";
                    //strqery = "select UR.USER_ID,ud.USER_NAME,UR.ROLE_ID, lr.ROLE_DESC,UR.WC_ID,clw.WIRE_CENTER_NAME,TO_CHAR(UR.CR_UPD_DATE, 'MM-DD-YYYY HH:MI:SS AM') CR_UPD_DATE from qual_schema.USER_ROLE UR inner join qual_schema.USER_DETAILS ud on UR.USER_ID = ud.USER_ID inner join qual_schema.LUT_ROLES lr on UR.ROLE_ID = lr.ROLE_ID inner join SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER clw on UR.WC_ID = clw.ID";
                }
                DataSet userSet = detAccess.ExecuteQuerySWGISLOC(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetUserDetails");
                throw ex;
            }
            return jsonstring;
        }
        public Boolean GetBIWFENGB(string ObjectID, string user_id)
        {
            DataTable userTable = new DataTable();
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {

                val = detAccess.GetBIWFENGB(ObjectID, user_id);


            }
            catch (Exception ex)
            {
                AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "UpdateCopperParcel");
                throw ex;
            }
            return val;
        }
        public string GetAssignedRoleDetails(string RoleID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();

                userTable = detAccess.GetAssignedRoleDetails(RoleID);
                //jsonstring = Utilities.ConvertDataTableToJSNString(userTable);

                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetBIW_Boundary_Check");
                throw ex;
            }
            return jsonstring;
        }
        public Boolean BIWFENGBDelete(string ObjectID)
        {
            DataTable userTable = new DataTable();
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {

                val = detAccess.BIWFENGBDelete(ObjectID);


            }
            catch (Exception ex)
            {
                AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "UpdateCopperParcel");
                throw ex;
            }
            return val;
        }
        public Boolean UpdateBIWFENGB(string ObjectID, string user_id)
        {
            DataTable userTable = new DataTable();
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {

                val = detAccess.UpdateBIWFENGB(ObjectID, user_id);

            }
            catch (Exception ex)
            {
                AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "UpdateCopperParcel");
                throw ex;
            }
            return val;
        }
        public string GetBIW_Boundary_Check(string OBJECT_ID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();

                userTable = detAccess.GetBIW_Boundary_Check(OBJECT_ID);
                //jsonstring = Utilities.ConvertDataTableToJSNString(userTable);

                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetBIW_Boundary_Check");
                throw ex;
            }
            return jsonstring;
        }
        public string Get_ADDR(string ADDR)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();
                ADDR = "%" + ADDR + "%";
                userTable = detAccess.Get_ADDR(ADDR);
                //jsonstring = Utilities.ConvertDataTableToJSNString(userTable);

                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "Get_ADDR_DETAILS");
                throw ex;
            }
            return jsonstring;
        }
        public string Get_Polygon_Search(string Col_name, string Col_Val)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();
                Col_Val = "%" + Col_Val + "%";
                if(Col_name == "FCBL_ID")
                {
                    Col_name = "GET_FIBER_POLY_DETAILS";
                }
                else if(Col_name == "OBJECTID")
                {
                    Col_name = "GET_FIBER_POLY_DETAILS_POLYID";
                }
                else
                {
                    Col_name = "GET_FIBER_POLY_DETAILS_NDS_JOB";
                }
                userTable = detAccess.Get_Polygon_Search(Col_name, Col_Val);
                //jsonstring = Utilities.ConvertDataTableToJSNString(userTable);

                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "Get_Polygon_Search");
                throw ex;
            }
            return jsonstring;
        }
        public string GetParllelPolyCheckDetails(string objectid,string LayerID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //string strqery = string.Empty;
                //strqery = "select  WC_ID as wc_name,( case when role_id=1 then user_id  end) Execute, ( case when role_id=2 then user_id end) as Review," +
                //          "( case when role_id = 3 then user_id end) publish from user_role where role_id in (1, 2, 3) and wc_id in (" + WCID + ")";
                //DataSet userSet = detAccess.ExecuteQuery(strqery);
                //userTable = userSet.Tables[0];
                userTable = detAccess.GetParllelPolyCheckDetails(objectid, LayerID);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetParllelPolyCheckDetails");
                throw ex;
            }
            return jsonstring;
        }
        public string GetLockedFiberPolygonDetails(string user_name, string RoleID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //string strqery = string.Empty;
                //strqery = "select  WC_ID as wc_name,( case when role_id=1 then user_id  end) Execute, ( case when role_id=2 then user_id end) as Review," +
                //          "( case when role_id = 3 then user_id end) publish from user_role where role_id in (1, 2, 3) and wc_id in (" + WCID + ")";
                //DataSet userSet = detAccess.ExecuteQuery(strqery);
                //userTable = userSet.Tables[0];
                userTable = detAccess.GetLockedFiberPolygonDetails(user_name, RoleID);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetParllelPolyCheckDetails");
                throw ex;
            }
            return jsonstring;
        }
        public string GetLockedNOBUILDPolygonDetails(string user_name, string RoleID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //string strqery = string.Empty;
                //strqery = "select  WC_ID as wc_name,( case when role_id=1 then user_id  end) Execute, ( case when role_id=2 then user_id end) as Review," +
                //          "( case when role_id = 3 then user_id end) publish from user_role where role_id in (1, 2, 3) and wc_id in (" + WCID + ")";
                //DataSet userSet = detAccess.ExecuteQuery(strqery);
                //userTable = userSet.Tables[0];
                userTable = detAccess.GetLockedNOBUILDPolygonDetails(user_name, RoleID);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetParllelPolyCheckDetails");
                throw ex;
            }
            return jsonstring;
        }
        public string GetLockedPolygonCount(string user_name, string RoleID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //string strqery = string.Empty;
                //strqery = "select  WC_ID as wc_name,( case when role_id=1 then user_id  end) Execute, ( case when role_id=2 then user_id end) as Review," +
                //          "( case when role_id = 3 then user_id end) publish from user_role where role_id in (1, 2, 3) and wc_id in (" + WCID + ")";
                //DataSet userSet = detAccess.ExecuteQuery(strqery);
                //userTable = userSet.Tables[0];
                userTable = detAccess.GetLockedPolygonCount(user_name, RoleID);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetParllelPolyCheckDetails");
                throw ex;
            }
            return jsonstring;
        }
        public string GetWorkTypeDetails()
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //string strqery = string.Empty;
                //strqery = "select  WC_ID as wc_name,( case when role_id=1 then user_id  end) Execute, ( case when role_id=2 then user_id end) as Review," +
                //          "( case when role_id = 3 then user_id end) publish from user_role where role_id in (1, 2, 3) and wc_id in (" + WCID + ")";
                //DataSet userSet = detAccess.ExecuteQuery(strqery);
                //userTable = userSet.Tables[0];
                userTable = detAccess.GetWorkTypeDetails();
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetParllelPolyCheckDetails");
                throw ex;
            }
            return jsonstring;
        }
        public string GetUserProfileDetails(string User_Name)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //string strqery = string.Empty;
                //strqery = "select  WC_ID as wc_name,( case when role_id=1 then user_id  end) Execute, ( case when role_id=2 then user_id end) as Review," +
                //          "( case when role_id = 3 then user_id end) publish from user_role where role_id in (1, 2, 3) and wc_id in (" + WCID + ")";
                //DataSet userSet = detAccess.ExecuteQuery(strqery);
                //userTable = userSet.Tables[0];
                userTable = detAccess.GetUserProfileDetails(User_Name);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetUserProfileDetails");
                throw ex;
            }
            return jsonstring;
        }
        public string GetUserProfileDFDetails(string User_Name)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //string strqery = string.Empty;
                //strqery = "select  WC_ID as wc_name,( case when role_id=1 then user_id  end) Execute, ( case when role_id=2 then user_id end) as Review," +
                //          "( case when role_id = 3 then user_id end) publish from user_role where role_id in (1, 2, 3) and wc_id in (" + WCID + ")";
                //DataSet userSet = detAccess.ExecuteQuery(strqery);
                //userTable = userSet.Tables[0];
                userTable = detAccess.GetUserProfileDFDetails(User_Name);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetUserProfileDFDetails");
                throw ex;
            }
            return jsonstring;
        }
        public string GetWorkTypeCheck(string Work_Type, string objectid, string workstatus)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //string strqery = string.Empty;
                //strqery = "select  WC_ID as wc_name,( case when role_id=1 then user_id  end) Execute, ( case when role_id=2 then user_id end) as Review," +
                //          "( case when role_id = 3 then user_id end) publish from user_role where role_id in (1, 2, 3) and wc_id in (" + WCID + ")";
                //DataSet userSet = detAccess.ExecuteQuery(strqery);
                //userTable = userSet.Tables[0];
                userTable = detAccess.GetWorkTypeCheck(Work_Type,  objectid, workstatus);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetWorkTypeCheck");
                throw ex;
            }
            return jsonstring;
        }
        public string GetWorkTypeSQMCheck(string Work_Type)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                userTable = detAccess.GetWorkTypeSQMCheck(Work_Type);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetWorkTypeSQMCheck");
                throw ex;
            }
            return jsonstring;
        }
        public Boolean UpdateParllelPolyDetails(string ObjectID, string user_id, string LayerID)
        {
            DataTable userTable = new DataTable();
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {

                val = detAccess.UpdateParllelPolyDetails(ObjectID, user_id, LayerID);

            }
            catch (Exception ex)
            {
                AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "UpdateParllelPolyDetails");
                throw ex;
            }
            return val;
        }
        public Boolean UpdateParllelPolyLockedBy(string ObjectID, string user_id,string LayerID)
        {
            DataTable userTable = new DataTable();
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {

                val = detAccess.UpdateParllelPolyLockedBy(ObjectID, user_id, LayerID);

            }
            catch (Exception ex)
            {
                AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "UpdateParllelPolyDetails");
                throw ex;
            }
            return val;
        }
        public Boolean UpdateParllelPolyLockedStatus(string ObjectID, string LayerID, string user_id)
        {
            DataTable userTable = new DataTable();
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {

                val = detAccess.UpdateParllelPolyLockedStatus(ObjectID, LayerID, user_id);

            }
            catch (Exception ex)
            {
                AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "UpdateParllelPolyDetails");
                throw ex;
            }
            return val;
        }
        public Boolean WorkType_Details_Update(string ObjectID, string User_Name, string WorkType, string Remarks)
        {
            DataTable userTable = new DataTable();
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {

                val = detAccess.WorkType_Details_Update(ObjectID, User_Name, WorkType, Remarks);

            }
            catch (Exception ex)
            {
                AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "WorkType_Details_Update");
                throw ex;
            }
            return val;
        }
        public Boolean UserProfile_Details_Update(string ObjectID, string username, string Layer_List, string Default_BM, string BookMark_Name)
        {
            DataTable userTable = new DataTable();
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {

                val = detAccess.UserProfile_Details_Update(ObjectID, username, Layer_List, Default_BM, BookMark_Name);

            }
            catch (Exception ex)
            {
                AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "UserProfile_Details_Update");
                throw ex;
            }
            return val;
        }
        public Boolean UserProfile_DEFAULT_Update(string ObjectID)
        {
            DataTable userTable = new DataTable();
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {

                val = detAccess.UserProfile_DEFAULT_Update(ObjectID);

            }
            catch (Exception ex)
            {
                AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "UserProfile_DEFAULT_Update");
                throw ex;
            }
            return val;
        }
        public Boolean WorkType_Details_Insert(string WorkType, string User_Name, string Remarks)
        {
            DataTable userTable = new DataTable();
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {

                val = detAccess.WorkType_Details_Insert(WorkType,  User_Name, Remarks);

            }
            catch (Exception ex)
            {
                AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "WorkType_Details_Insert");
                throw ex;
            }
            return val;
        }
        public Boolean User_Profile_Insert(string username, string Layer_List, string Default_BM,string BookMark_Name)
        {
            DataTable userTable = new DataTable();
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {

                val = detAccess.User_Profile_Insert(username, Layer_List, Default_BM, BookMark_Name);

            }
            catch (Exception ex)
            {
                AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "User_Profile_Insert");
                throw ex;
            }
            return val;
        }
        public Boolean WorkType_Details_Delete(string ObjectID)
        {
            DataTable userTable = new DataTable();
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {

                val = detAccess.WorkType_Details_Delete(ObjectID);

            }
            catch (Exception ex)
            {
                AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "WorkType_Details_Delete");
                throw ex;
            }
            return val;
        }
        public Boolean UserProfile_Details_Delete(string ObjectID)
        {
            DataTable userTable = new DataTable();
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {

                val = detAccess.UserProfile_Details_Delete(ObjectID);

            }
            catch (Exception ex)
            {
                AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "UserProfile_Details_Delete");
                throw ex;
            }
            return val;
        }
        public string GetLayerListUserProfile(string ObjectID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                userTable = detAccess.GetLayerListUserProfile(ObjectID);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetWorkTypeSQMCheck");
                throw ex;
            }
            return jsonstring;
        }

        public string GetNDSJobNo(string WirecenterID, string DesignationNo)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                string strqery = string.Empty;

                strqery = "select distinct (sc.NDS_JOB_NBR) as NDS_JOB_NO from QUAL_SCHEMA.addr_validation_report sc " +
                //"where  WIRE_CENTER_CLLI = '" + WirecenterID + "'";
                "where WIRE_CENTER_CLLI = '" + WirecenterID + "' and DESIGNATION_NO = '" + DesignationNo + "'";
                //strqery = "select distinct (sc.NDS_JOB_NBR) as NDS_JOB_NBR from QUAL_SCHEMA.addr_validation_report sc ";
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetWireCenterForRole");
                throw ex;
            }
            return jsonstring;
        }


        public string GetAddValidationDT(string WirecenterID, string DesignationNo, string NDS_Jno)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable1 = new DataTable();
                string strqery = string.Empty;
                //strqery = "select WC_ID, WC_NAME ,FCBL_ID ,NDS_JNO from QUAL_SCHEMA.SERVICE_QUALIFICATION_MODEL where NDS_JNO= '" + NDS_Jno + "'";
                
                strqery = "Select ADDRESS, CITY, STATE, ZIP_CODE, NDS_LATLONG_QUAL_STATUS, NDS_GL_LATLONG_QUAL_STATUS, NDS_CL_LATLONG_QUAL_STATUS " +
                "from addr_validation_report where WIRE_CENTER_CLLI = '" + WirecenterID.Split('-')[0] + "' and DESIGNATION_NO = '" + DesignationNo + "' and NDS_JOB_NBR = '" + NDS_Jno + "'";

                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable1 = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable1);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetAddValidationDT");
                throw ex;
            }
            return jsonstring;
        }
        
        public string GetAddValidationDTexceloutput(string WirecenterID, string DesignationNo, string NDS_Jno)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                string strqery = string.Empty;
                strqery = "Select * " +
                "from addr_validation_report where WIRE_CENTER_CLLI = '" + WirecenterID.Split('-')[0] + "' and DESIGNATION_NO = '" + DesignationNo + "' and NDS_JOB_NBR = '" + NDS_Jno + "'";
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetAddValidationDTexceloutput");
                throw ex;
            }
            return jsonstring;
        }

    }
}
