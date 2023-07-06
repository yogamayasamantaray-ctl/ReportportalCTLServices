using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLogicLayer;
using System.Data;
using System.Threading;
using System.Configuration;

namespace BussinessLogicLayer
{
    public class UserDetails
    {

        public string GetDesignationList(string WirecenterID, string status, string User_id)
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
                // string sessWCId = "W1,W2";
                string desStatus = "";
                string SchemaTable = ConfigurationManager.AppSettings["CopperTable"];
                string SchemaTerTable = ConfigurationManager.AppSettings["Terminal"];
                string strqery = string.Empty;
                // string strqery = "select distinct designation from copper_line_of_count where FK_SHEATH_WITH_LOC_TERMINAL IN (Select ID from sheath_with_loc_terminal where fk_cl_wire_center = '" + WirecenterID + "')";
                //To be executed
                if (status == "To be Executed")
                {

                    strqery = " select distinct clc.designation from swgisloc." + SchemaTable + " clc, swgisloc." + SchemaTerTable + " st "
                                + " where  clc.FK_SHEATH_WITH_LOC_TERMINAL = st.ID and st.fk_cl_wire_center = '" + WirecenterID + "'  "
                                + " and clc.designation not in(select rd.designation_id from qual_schema.review_details rd "
                                + " where rd.WC_ID = '" + WirecenterID + "' and rd.STATUS_ID not in(207) "
                                + " and rd.job_id not in(select jd.job_id from qual_schema.job_details jd "
                                + " where jd.job_id=rd.job_id and jd.job_status in(103,104,106) ) and rd.DESIGNATION_ID in(select sq.FCBL_ID "
                                + " from qual_schema.service_qualification_model sq where rd.WC_ID='" + WirecenterID + "' and rd.DESIGNATION_ID=sq.FCBL_ID "
                                + " and UPPER(sq.createmode) not in ('MANUAL'))) and clc.designation IS NOT NULL";

                }
                //Executed
                else if (status == "Executed")
                {
                    strqery = "select distinct clc.designation from swgisloc." + SchemaTable + " clc, swgisloc." + SchemaTerTable + " st where " +
                            " clc.FK_SHEATH_WITH_LOC_TERMINAL = st.ID and st.fk_cl_wire_center = '" + WirecenterID + "' " +
                            "and clc.designation  in(select designation_id from qual_schema.review_details where WC_ID = '" + WirecenterID + "' and STATUS_ID in(203,204) ) and clc.designation IS NOT NULL";
                }
                else if (status == "Manual")
                {
                    strqery = " select distinct clc.designation from swgisloc." + SchemaTable + " clc, swgisloc." + SchemaTerTable + " st "
                                + " where  clc.FK_SHEATH_WITH_LOC_TERMINAL = st.ID and st.fk_cl_wire_center = '" + WirecenterID + "'  "
                                + " and clc.designation in(select rd.designation_id from qual_schema.review_details rd "
                                + " where rd.WC_ID = '" + WirecenterID + "' and rd.STATUS_ID not in(207) "
                                + " and rd.job_id not in(select jd.job_id from qual_schema.job_details jd "
                                + " where jd.job_id=rd.job_id and jd.job_status in(103,104,106) ) and rd.DESIGNATION_ID in(select sq.FCBL_ID "
                                + " from qual_schema.service_qualification_model sq where rd.WC_ID='" + WirecenterID + "' and rd.DESIGNATION_ID=sq.FCBL_ID "
                                + " and UPPER(sq.createmode)='MANUAL')) and clc.designation IS NOT NULL";

                }
                else
                {
                    strqery = "select distinct clc.designation from swgisloc." + SchemaTable + " clc, swgisloc." + SchemaTerTable + " st where " +
                           " clc.FK_SHEATH_WITH_LOC_TERMINAL = st.ID and st.fk_cl_wire_center = '" + WirecenterID + "' and clc.designation IS NOT NULL ";

                }
                DataSet userSet = detAccess.ExecuteQuerySWGISLOC(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetDesignationList");
                throw ex;

            }
            return jsonstring;
        }

        public string GetWireCenterList(string User_id, string ToolName, string sessWCId)
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
                //string sessWCId = "'1572328488445168705','1572328488445168924'";
                //  string strqery = "select b.wire_cent_id,a.wc_id from swgisloc.cl_wire_center a " +
                //     "  inner join QUAL_SCHEMA.user_role b on a.id=b.wire_cent_id where b.User_id = '" + User_id + "' ";
                userTable = detAccess.GetWireCenterList(ToolName);
                //jsonstring = Utilities.ConvertDataTableToJSNString(userTable);

                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetWireCenterList");
                throw ex;
            }
            return jsonstring;
        }
        public string GetPolygonDataList(string WirecenterID, string User_id, string tool_name, string Job_ID, string qual_Type, string qual_Status)
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
                DataTable userTable2 = new DataTable();
                //Utilities Util = new Utilities();
                string strqery2 = string.Empty;
                if (tool_name == "Review")
                {
                    if (qual_Status == "1")
                    {
                        if (Job_ID == null)
                        {
                            strqery2 = "select distinct rd.DESIGNATION_ID,rd.NDS_JNO as NDS_JNO from qual_schema.review_details rd where rd.STATUS_ID in(203,204,209) and rd.WC_ID = '" + WirecenterID + "' and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "'";
                        }
                        else
                        {
                            strqery2 = "select distinct rd.DESIGNATION_ID,rd.NDS_JNO as NDS_JNO from qual_schema.review_details rd where rd.STATUS_ID in(203,204,209) and rd.JOB_ID = '" + Job_ID + "' and rd.WC_ID = '" + WirecenterID + "'  and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106)  and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "'";
                        }
                    }
                    else if (qual_Status == "2")
                    {
                        if (Job_ID == null)
                        {
                            strqery2 = "select rd.DESIGNATION_ID,rd.NDS_JNO as NDS_JNO from qual_schema.review_details rd where rd.STATUS_ID in(203,204,209) and rd.WC_ID = '" + WirecenterID + "' and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "' and rd.NDS_JNO is not null order by DESIGNATION_ID";
                        }
                        else
                        {
                            strqery2 = "select rd.DESIGNATION_ID , rd.NDS_JNO as NDS_JNO from qual_schema.review_details rd where rd.STATUS_ID in(203,204,209) and rd.JOB_ID = '" + Job_ID + "' and rd.WC_ID = '" + WirecenterID + "'  and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "' and rd.NDS_JNO is not null order by DESIGNATION_ID";
                        }
                    }
                    DataSet userSet2 = detAccess.ExecuteQuery(strqery2);
                    userTable2 = userSet2.Tables[0];
                    jsonstring = Utilities.ConvertDataTableToJSNString(userTable2);
                }
                else
                {
                    if (qual_Status == "1")
                    {
                        if (Job_ID == null)
                        {
                            //strqery2 = "select distinct rd.DESIGNATION_ID from qual_schema.review_details rd where rd.STATUS_ID ='206' and rd.WC_ID = '" + WirecenterID + "' and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=206) and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "'";

                            //strqery2 = " select  distinct rd.DESIGNATION_ID from qual_schema.review_details rd where rd.WC_ID = '" + WirecenterID + "' "
                            //+ "and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) "
                            //+ "and rd.QUAL_TYPE_ID = '" + qual_Type + "' group by  rd.DESIGNATION_ID,rd.job_id having count(rd.DESIGNATION_ID)=(select count(rd1.DESIGNATION_ID) from qual_schema.review_details rd1 "
                            //+ "where rd1.WC_ID = '" + WirecenterID + "' and rd.designation_id=rd1.designation_id "
                            //+ "and rd.job_id=rd1.job_id and rd1.job_id not in(select jd1.job_id from qual_schema.job_details jd1 where jd1.job_id=rd1.job_id and jd1.job_status=106) "
                            //+ "and rd1.QUAL_TYPE_ID = '" + qual_Type + "' and rd1.STATUS_ID ='206')";

                            strqery2 = " select  distinct rd.DESIGNATION_ID, rd.NDS_JNO as NDS_JNO from qual_schema.review_details rd where rd.WC_ID = '" + WirecenterID + "' "
                           + "and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) "
                           + "and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.STATUS_ID ='206'";
                        }
                        else
                        {
                            //strqery2 = " select  distinct rd.DESIGNATION_ID from qual_schema.review_details rd where rd.WC_ID = '" + WirecenterID + "' and rd.JOB_ID = '" + Job_ID + "' "
                            //+ "and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) "
                            //+ "and rd.QUAL_TYPE_ID = '" + qual_Type + "' group by  rd.DESIGNATION_ID,rd.job_id having count(rd.DESIGNATION_ID)=(select count(rd1.DESIGNATION_ID) from qual_schema.review_details rd1 "
                            //+ "where rd1.WC_ID = '" + WirecenterID + "' and rd.JOB_ID = '" + Job_ID + "' and rd.designation_id=rd1.designation_id "
                            //+ "and rd.job_id=rd1.job_id and rd1.job_id not in(select jd1.job_id from qual_schema.job_details jd1 where jd1.job_id=rd1.job_id and jd1.job_status=106) "
                            //+ "and rd1.QUAL_TYPE_ID = '" + qual_Type + "' and rd1.STATUS_ID ='206')";

                            strqery2 = " select  distinct rd.DESIGNATION_ID,rd.NDS_JNO as NDS_JNO from qual_schema.review_details rd where rd.WC_ID = '" + WirecenterID + "' and rd.JOB_ID = '" + Job_ID + "' "
                            + "and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) "
                            + "and rd.QUAL_TYPE_ID = '" + qual_Type + "'  and rd.STATUS_ID ='206'";
                        }
                        DataSet userSet2 = detAccess.ExecuteQuery(strqery2);
                        userTable2 = userSet2.Tables[0];
                        jsonstring = Utilities.ConvertDataTableToJSNString(userTable2);
                    }
                    else if (qual_Status == "2")
                    {
                        if (Job_ID == null)
                        {
                            //strqery2 = "select distinct rd.DESIGNATION_ID from qual_schema.review_details rd where rd.STATUS_ID ='206' and rd.WC_ID = '" + WirecenterID + "' and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=206) and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "'";

                            //strqery2 = " select  distinct rd.DESIGNATION_ID from qual_schema.review_details rd where rd.WC_ID = '" + WirecenterID + "' "
                            //+ "and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) "
                            //+ "and rd.QUAL_TYPE_ID = '" + qual_Type + "' group by  rd.DESIGNATION_ID,rd.job_id having count(rd.DESIGNATION_ID)=(select count(rd1.DESIGNATION_ID) from qual_schema.review_details rd1 "
                            //+ "where rd1.WC_ID = '" + WirecenterID + "' and rd.designation_id=rd1.designation_id "
                            //+ "and rd.job_id=rd1.job_id and rd1.job_id not in(select jd1.jord.NDS_JNO as DESIGNATION_ID_id from qual_schema.job_details jd1 where jd1.job_id=rd1.job_id and jd1.job_status=106) "
                            //+ "and rd1.QUAL_TYPE_ID = '" + qual_Type + "' and rd1.STATUS_ID ='206')";
                            strqery2 = " select  distinct rd.DESIGNATION_ID,rd.NDS_JNO as NDS_JNO from qual_schema.review_details rd where rd.WC_ID = '" + WirecenterID + "' "
                           + "and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) "
                           + "and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.STATUS_ID ='206'";
                        }
                        else
                        {
                            //strqery2 = " select  distinct rd.DESIGNATION_ID from qual_schema.review_details rd where rd.WC_ID = '" + WirecenterID + "' and rd.JOB_ID = '" + Job_ID + "' "
                            //+ "and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) "
                            //+ "and rd.QUAL_TYPE_ID = '" + qual_Type + "' group by  rd.DESIGNATION_ID,rd.job_id having count(rd.DESIGNATION_ID)=(select count(rd1.DESIGNATION_ID) from qual_schema.review_details rd1 "
                            //+ "where rd1.WC_ID = '" + WirecenterID + "' and rd.JOB_ID = '" + Job_ID + "' and rd.designation_id=rd1.designation_id "
                            //+ "and rd.job_id=rd1.job_id and rd1.job_id not in(select jd1.job_id from qual_schema.job_details jd1 where jd1.job_id=rd1.job_id and jd1.job_status=106) "
                            //+ "and rd1.QUAL_TYPE_ID = '" + qual_Type + "' and rd1.STATUS_ID ='206')";

                            strqery2 = " select  distinct rd.DESIGNATION_ID,rd.NDS_JNO as NDS_JNO from qual_schema.review_details rd where rd.WC_ID = '" + WirecenterID + "' and rd.JOB_ID = '" + Job_ID + "' "
                            + "and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) "
                            + "and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.STATUS_ID ='206'";
                        }
                        DataSet NDSCheck = detAccess.ExecuteQuery(strqery2);
                        DataTable NDSDataTable = new DataTable();
                        NDSDataTable = NDSCheck.Tables[0];
                        if (NDSDataTable.Rows.Count != 0)
                        {
                            string queryValues2 = string.Empty;
                            for (int i = 0; i < NDSDataTable.Rows.Count; i++)
                            {
                                if (i == 0)
                                {
                                    queryValues2 = "'" + NDSDataTable.Rows[i][0] + "'";
                                }
                                else
                                {
                                    queryValues2 = queryValues2 + "," + "'" + NDSDataTable.Rows[i][0] + "'";
                                }

                            }
                            //jsonstring = Utilities.ConvertDataTableToJSNString(userTable2);

                            string strquery = "select rd.DESIGNATION_ID , rd.NDS_JNO as NDS_JNO from qual_schema.review_details rd where rd.DESIGNATION_ID in (" + queryValues2 + ") and rd.QUAL_STATUS_ID = '" + qual_Status + "' and rd.WC_ID = '" + WirecenterID + "' and rd.QUAL_TYPE_ID = '" + qual_Type + "' and STATUS_ID ='206'";
                            DataSet userSet2 = detAccess.ExecuteQuery(strquery);
                            userTable2 = userSet2.Tables[0];
                            jsonstring = Utilities.ConvertDataTableToJSNString(userTable2);
                        }
                    }

                }
            }



            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetPolygonDataList");
                throw ex;
            }
            return jsonstring;
        }

        //public string GetPolygonDataList(string WirecenterID, string User_id, string tool_name, string Job_ID, string qual_Type, string qual_Status)
        //{
        //    string qryStatus = string.Empty;
        //    string existUsers = string.Empty;
        //    string jsonstring = string.Empty;
        //    string strQry;
        //    List<string> existUsers1 = new List<string>();


        //    try
        //    {
        //        AccessData detAccess = new AccessData();
        //        DataTable userTable = new DataTable();
        //        DataTable userTable2 = new DataTable();
        //        //Utilities Util = new Utilities();
        //        string strqery2 = string.Empty;
        //        if (tool_name == "Review")
        //        {
        //            if (qual_Status == "1")
        //            {
        //                if (Job_ID == null)
        //                {
        //                    strqery2 = "select distinct rd.DESIGNATION_ID from qual_schema.review_details rd where rd.STATUS_ID in(203,204,209) and rd.WC_ID = '" + WirecenterID + "' and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "'";
        //                }
        //                else
        //                {
        //                    strqery2 = "select distinct rd.DESIGNATION_ID from qual_schema.review_details rd where rd.STATUS_ID in(203,204,209) and rd.JOB_ID = '" + Job_ID + "' and rd.WC_ID = '" + WirecenterID + "'  and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106)  and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "'";
        //                }
        //            }
        //            else if (qual_Status == "2")
        //            {
        //                if (Job_ID == null)
        //                {
        //                    strqery2 = "select rd.DESIGNATION_ID || '|' || rd.NDS_JNO as DESIGNATION_ID from qual_schema.review_details rd where rd.STATUS_ID in(203,204,209) and rd.WC_ID = '" + WirecenterID + "' and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "' and rd.NDS_JNO is not null order by DESIGNATION_ID";
        //                }
        //                else
        //                {
        //                    strqery2 = "select rd.DESIGNATION_ID || '|' || rd.NDS_JNO as DESIGNATION_ID from qual_schema.review_details rd where rd.STATUS_ID in(203,204,209) and rd.JOB_ID = '" + Job_ID + "' and rd.WC_ID = '" + WirecenterID + "'  and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "' and rd.NDS_JNO is not null order by DESIGNATION_ID";
        //                }
        //            }
        //            DataSet userSet2 = detAccess.ExecuteQuery(strqery2);
        //            userTable2 = userSet2.Tables[0];
        //            jsonstring = Utilities.ConvertDataTableToJSNString(userTable2);
        //        }
        //        else
        //        {
        //            if (qual_Status == "1")
        //            {
        //                if (Job_ID == null)
        //                {
        //                    //strqery2 = "select distinct rd.DESIGNATION_ID from qual_schema.review_details rd where rd.STATUS_ID ='206' and rd.WC_ID = '" + WirecenterID + "' and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=206) and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "'";and rd.JOB_ID = '3008' "
        //                    strqery2 = "select distinct rd.DESIGNATION_ID from qual_schema.review_details rd where rd.STATUS_ID ='206' and rd.WC_ID = '" + WirecenterID + "' and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=206) and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "'";
        //                }
        //                else
        //                {
        //                    strqery2 = "select distinct rd.DESIGNATION_ID from qual_schema.review_details rd where rd.STATUS_ID ='206' and rd.JOB_ID = '" + Job_ID + "' and rd.WC_ID = '" + WirecenterID + "' and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=206) and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "'";

        //                }
        //                DataSet CheckDesg = detAccess.ExecuteQuery(strqery2);
        //                DataTable CheckDataTable = new DataTable();
        //                CheckDataTable = CheckDesg.Tables[0];
        //                if (CheckDataTable.Rows.Count != 0)
        //                {
        //                    string queryValues = string.Empty;
        //                    for (int i = 0; i < CheckDataTable.Rows.Count; i++)
        //                    {
        //                        if (i == 0)
        //                        {
        //                            queryValues = "'" + CheckDataTable.Rows[i][0] + "'";
        //                        }
        //                        else
        //                        {
        //                            queryValues = queryValues + "," + "'" + CheckDataTable.Rows[i][0] + "'";
        //                        }

        //                    }
        //                    strqery2 = "select distinct rd.DESIGNATION_ID from qual_schema.review_details rd where rd.WC_ID = '" + WirecenterID + "' "
        //                                + " and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) "
        //                                + " and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "' and rd.designation_id in(" + queryValues + ")   group by  rd.DESIGNATION_ID "
        //                                + " having count(rd.DESIGNATION_ID)=(select count(rd1.DESIGNATION_ID) from qual_schema.review_details rd1 where rd1.WC_ID = '" + WirecenterID + "' "
        //                                + " and rd1.job_id not in(select jd1.job_id from qual_schema.job_details jd1 where jd1.job_id=rd1.job_id and jd1.job_status=106) "
        //                                + " and rd1.QUAL_TYPE_ID = '" + qual_Type + "' and rd1.QUAL_STATUS_ID = '" + qual_Status + "' and rd1.designation_id in(" + queryValues + ") and rd1.STATUS_ID ='206')";
        //                    DataSet userSet2 = detAccess.ExecuteQuery(strqery2);
        //                    userTable2 = userSet2.Tables[0];
        //                    jsonstring = Utilities.ConvertDataTableToJSNString(userTable2);
        //                }
        //            }
        //            else if (qual_Status == "2")
        //            {

        //                if (Job_ID == null)
        //                {
        //                    strqery2 = "select rd.DESIGNATION_ID from qual_schema.review_details rd where rd.STATUS_ID ='206' and rd.WC_ID = '" + WirecenterID + "' and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=206) and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "' and rd.NDS_JNO is not null order by DESIGNATION_ID";

        //                }
        //                else
        //                {
        //                    strqery2 = "select rd.DESIGNATION_ID from qual_schema.review_details rd where rd.STATUS_ID ='206' and rd.JOB_ID = '" + Job_ID + "' and rd.WC_ID = '" + WirecenterID + "' and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=206) and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "' and rd.NDS_JNO is not null order by DESIGNATION_ID";
        //                }
        //                DataSet CheckDesg = detAccess.ExecuteQuery(strqery2);
        //                DataTable CheckDataTable = new DataTable();
        //                CheckDataTable = CheckDesg.Tables[0];
        //                if (CheckDataTable.Rows.Count != 0)
        //                {
        //                    string queryValues = string.Empty;
        //                    for (int i = 0; i < CheckDataTable.Rows.Count; i++)
        //                    {
        //                        if (i == 0)
        //                        {
        //                            queryValues = "'" + CheckDataTable.Rows[i][0] + "'";
        //                        }
        //                        else
        //                        {
        //                            queryValues = queryValues + "," + "'" + CheckDataTable.Rows[0][i] + "'";
        //                        }

        //                    }
        //                    strqery2 = "select distinct rd.DESIGNATION_ID from qual_schema.review_details rd where rd.WC_ID = '" + WirecenterID + "' "
        //                                + " and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) "
        //                                + " and rd.QUAL_TYPE_ID = '" + qual_Type + "' and rd.QUAL_STATUS_ID = '" + qual_Status + "' and rd.designation_id in(" + queryValues + ")   and rd.NDS_JNO is not null group by  rd.DESIGNATION_ID "
        //                                + " having count(rd.DESIGNATION_ID)=(select count(rd1.DESIGNATION_ID) from qual_schema.review_details rd1 where rd1.WC_ID = '" + WirecenterID + "' "
        //                                + " and rd1.job_id not in(select jd1.job_id from qual_schema.job_details jd1 where jd1.job_id=rd1.job_id and jd1.job_status=106) "
        //                                + " and rd1.QUAL_TYPE_ID = '" + qual_Type + "' and rd1.QUAL_STATUS_ID = '" + qual_Status + "' and rd1.designation_id in(" + queryValues + ") and rd1.STATUS_ID ='206' and rd1.NDS_JNO is not null)";
        //                    DataSet NDSCheck = detAccess.ExecuteQuery(strqery2);
        //                    DataTable NDSDataTable = new DataTable();
        //                    NDSDataTable = NDSCheck.Tables[0];
        //                    if (NDSDataTable.Rows.Count != 0)
        //                    {
        //                        string queryValues2 = string.Empty;
        //                        for (int i = 0; i < NDSDataTable.Rows.Count; i++)
        //                        {
        //                            if (i == 0)
        //                            {
        //                                queryValues2 = "'" + NDSDataTable.Rows[i][0] + "'";
        //                            }
        //                            else
        //                            {
        //                                queryValues2 = queryValues2 + "," + "'" + NDSDataTable.Rows[0][i] + "'";
        //                            }

        //                        }
        //                        //jsonstring = Utilities.ConvertDataTableToJSNString(userTable2);

        //                        string strquery = "select rd.DESIGNATION_ID || '|' || rd.NDS_JNO as DESIGNATION_ID from qual_schema.review_details rd where rd.DESIGNATION_ID in (" + queryValues2 + ") and rd.QUAL_STATUS_ID = '" + qual_Status + "' and rd.WC_ID = '" + WirecenterID + "' and rd.QUAL_TYPE_ID = '" + qual_Type + "'";
        //                        DataSet userSet2 = detAccess.ExecuteQuery(strquery);
        //                        userTable2 = userSet2.Tables[0];
        //                        jsonstring = Utilities.ConvertDataTableToJSNString(userTable2);
        //                    }
        //                }

        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return jsonstring;
        //}
        public string GetNoBuildPolygonDataList(string WirecenterID, string User_id, string ToolName)
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
                DataTable userTable2 = new DataTable();
                //Utilities Util = new Utilities();5
                string strqery2 = string.Empty;
                if (ToolName == "Review")
                {
                    strqery2 = "select distinct rd.POLYGON_ID from qual_schema.REVIEW_NOBUILD rd where rd.STATUS_ID in(203,204,209) and rd.WC_ID = '" + WirecenterID + "' and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id=rd.job_id and jd.job_status=106) order by POLYGON_ID";
                }
                else
                {
                    strqery2 = "select distinct rd.POLYGON_ID from qual_schema.REVIEW_NOBUILD rd where rd.STATUS_ID = '206' and rd.WC_ID = '" + WirecenterID + "' and rd.job_id not in(select jd.job_id from qual_schema.job_details jd where jd.job_id = rd.job_id and jd.job_status = 206)";
                }
                DataSet userSet2 = detAccess.ExecuteQuery(strqery2);
                userTable2 = userSet2.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable2);

            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetNoBuildPolygonDataList");
                throw ex;
            }
            return jsonstring;
        }
        public string GetJobID(string UserID, string wirecenterID, string Qual_type_Id)
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
                string strqery = string.Empty;

                strqery = "select jd.JOB_ID from qual_schema.JOB_DETAILS jd where jd.WC_ID = '" + wirecenterID + "' and QUAL_TYPE_ID = '" + Qual_type_Id + "'";

                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetJobID");
                throw ex;
            }
            return jsonstring;
        }
        public string GetStatusList(string Tool_name)
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
                string strqery = string.Empty;
                if (Tool_name == "Review")
                {
                    strqery = "select status_id,status_desc from qual_schema.lut_status where Status_id in (206,207)";
                }
                else
                {
                    strqery = "select status_id,status_desc from qual_schema.lut_status where Status_id in (209,208)";
                }
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetStatusList");
                throw ex;
            }
            return jsonstring;
        }
        public string GetQualificationTypeList()
        {
            string jsonstring = string.Empty;
            string strQry;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();

                string strqery = "select Qual_type_ID,Qual_Type_Desc from LUT_QUAL_TYPES order by Qual_type_ID";
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetQualificationTypeList");
                throw ex;
            }
            return jsonstring;
        }


        public string GetQualificationStatusList()
        {
            string jsonstring = string.Empty;
            string strQry;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();
                string strqery = "select QUAL_STATUS_ID,QUAL_STATUS_DESC from LUT_QUAL_STATUS where QUAL_STATUS_ID != '3'";
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetQualificationStatusList");
                throw ex;
            }
            return jsonstring;
        }
        public Boolean ExecuteQueryForInsertingDataIntoReviewDetails(string User_id, string wirecenter, string designation, string qual_type_id, string polygonID, string status, string ToolName, string base64, string review, string CDateTime, string qual_status, string CreateMode)
        {
            string qryStatus = string.Empty;
            string existUsers = string.Empty;
            bool val = false;
            string strQry;


            try
            {
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();
                AccessData detAccess = new AccessData();
                //string strqery = "select USER_ROLE_ID from User_Role where WIRE_CENT_ID = '" + wirecenter + "' and USER_ID = '1'";
                //DataSet userSet = detAccess.ExecuteQuery(strqery);
                //userTable = userSet.Tables[0];
                //string User_roleId = userTable.Rows[0][0].ToString();
                if (ToolName == "Execute")
                {
                    if (qual_type_id == "2")
                    {
                        string job_Des = "SQM to be Executed";
                        strQry = "INSERT INTO job_details(JOB_CREATED_DATE, job_status, WC_ID,JOB_DESIGNATION,QUAL_TYPE_ID,SOURCE,QUAL_STATUS,CREATE_MODE,CUID) VALUES( TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS'), '101', '" + wirecenter + "','" + designation + "','" + qual_type_id + "','1','0','" + CreateMode + "','" + User_id + "')";
                        val = detAccess.ExecuteQueryForInsert(strQry);
                    }
                    else
                    {
                        string job_Des = "SQM to be Executed";
                        strQry = "INSERT INTO job_details(JOB_CREATED_DATE, job_status, WC_ID,JOB_DESIGNATION,QUAL_TYPE_ID,SOURCE,QUAL_STATUS,CREATE_MODE,CUID) VALUES( TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS'), '101', '" + wirecenter + "','" + designation + "','" + qual_type_id + "','1','0','" + CreateMode + "','" + User_id + "')";
                        val = detAccess.ExecuteQueryForInsert(strQry);
                        if (val)
                        {
                            string strquery2 = "select JOB_ID from qual_schema.JOB_DETAILS where JOB_DESIGNATION = '" + designation + "' and WC_ID = '" + wirecenter + "' and JOB_CREATED_DATE = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS') and job_status = '101' and QUAL_TYPE_ID = '" + qual_type_id + "'";
                            //string strquery2 = "SELECT MAX(JOB_ID)FROM qual_schema.JOB_DETAILS";
                            DataSet MaxIDSet = detAccess.ExecuteQuery(strquery2);
                            DataTable Maxtable = MaxIDSet.Tables[0];
                            string maxid = Maxtable.Rows[0][0].ToString();
                            string[] desarray = designation.Split(',');
                            for (int i = 0; i < desarray.Length; i++)
                            {

                                strQry = "insert into review_details(user_id,WC_ID,designation_id,qual_type_id,status_id,review_date,REMARKS,JOB_ID,QUAL_STATUS_ID) VALUES('" + User_id + "','" + wirecenter + "','" + desarray[i] + "','" + qual_type_id + "','" + status + "',TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS'),'" + review + "','" + maxid + "','1')";
                                val = detAccess.ExecuteQueryForInsert(strQry);
                            }
                            //if (val)
                            //Thread.Sleep(10000);
                            //Task.Delay(10000).ContinueWith(t => DownloadDocsMainPageAsync());

                            //  await DownloadDocsMainPageAsync();
                        }
                    }
                }
                else if (ToolName == "Review")
                {

                    /* bool val2 = false;
                     strQry = "Update Job_Details set JOB_CREATED_DATE = sysdate, job_status = '2' where wire_center_id = '" + wirecenter + "'";
                     val2 = detAccess.ExecuteQueryForInsert(strQry);
                     if (val2)
                     {*/
                    string[] desarray = designation.Split(',');

                    for (int i = 0; i < desarray.Length; i++)
                    {


                        if (qual_status == "1" || qual_status == "2")
                        {
                            string[] NDSJobNumber = desarray[i].Split('|');
                            string JobNum = desarray[i];
                            string JobNum2 = string.Empty;
                            if (NDSJobNumber.Length > 1)
                            {
                                int ind = JobNum.LastIndexOf('|');
                                JobNum2 = JobNum.Substring(ind + 1);
                                JobNum = JobNum.Substring(0, ind);
                            }
                            if (base64 != "")
                            {
                                val = detAccess.InsertReviewImageData(User_id, wirecenter, desarray[i], qual_type_id, polygonID, status, base64, review, CDateTime, qual_status);
                            }
                            else
                            {
                                if (status == "207")
                                {
                                    if (NDSJobNumber.Length == 1)
                                    {

                                        strQry = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS') where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum + "'";
                                        val = detAccess.ExecuteQueryForInsert(strQry);
                                    }
                                    else
                                    {
                                        strQry = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS') where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum + "' and QUAL_STATUS_ID = '" + qual_status + "' and NDS_JNO = '" + JobNum2 + "'";
                                        val = detAccess.ExecuteQueryForInsert(strQry);
                                    }
                                }
                                else
                                {
                                    if (NDSJobNumber.Length == 1)
                                    {

                                        strQry = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS') where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum + "' and QUAL_STATUS_ID = '" + qual_status + "'";
                                        val = detAccess.ExecuteQueryForInsert(strQry);
                                    }
                                    else
                                    {
                                        strQry = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS') where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum + "' and QUAL_STATUS_ID = '" + qual_status + "' and NDS_JNO = '" + JobNum2 + "'";
                                        val = detAccess.ExecuteQueryForInsert(strQry);
                                    }
                                }
                            }
                        }
                        //else if (qual_status == "2")
                        //{
                            //string[] NDSJobNumber = desarray[i].Split('|');
                            //string JobNum = desarray[i];
                            //int ind = JobNum.LastIndexOf('|');
                            //string JobNum2 = JobNum.Substring(0, ind);
                            //JobNum = JobNum.Substring(ind + 1);
                            //if (base64 != "")
                            //{
                            //    val = detAccess.InsertReviewImageData(User_id, wirecenter, JobNum2, qual_type_id, polygonID, status, base64, review, CDateTime, qual_status);
                            //}
                            //else
                            //{
                            //    if (status == "207")
                            //    {
                            //        strQry = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS') where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum2 + "'";
                            //        val = detAccess.ExecuteQueryForInsert(strQry);
                            //    }
                            //    else
                            //    {
                            //        strQry = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS') where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum2 + "' and QUAL_STATUS_ID = '" + qual_status + "' and NDS_JNO = '" + JobNum + "'";
                            //        val = detAccess.ExecuteQueryForInsert(strQry);
                            //    }
                            //}
                       // }
                        else
                        {
                            string[] NDSJobNumber = designation.Split('|');
                            string JobNum = desarray[i];
                            int ind = JobNum.LastIndexOf('|');
                            string JobNum2 = JobNum.Substring(0, ind);
                            JobNum = JobNum.Substring(ind + 1);
                            if (base64 != "")
                            {
                                val = detAccess.InsertReviewImageData(User_id, wirecenter, JobNum2, qual_type_id, polygonID, status, base64, review, CDateTime, qual_status);
                            }
                            else
                            {

                                strQry = "update REVIEW_COPPER set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS') where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum2 + "'";
                                val = detAccess.ExecuteQueryForInsert(strQry);



                            }
                        }
                    }
                    //val = detAccess.ExecuteQueryForInsert(strQry);
                    //}
                }
                else if (ToolName == "NoBuild")
                {
                    //string job_Des = "SQM to be Executed";
                    strQry = "INSERT INTO job_details(JOB_CREATED_DATE, job_status, WC_ID,JOB_DESIGNATION,QUAL_TYPE_ID,SOURCE,QUAL_STATUS,CREATE_MODE,CUID) VALUES( TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS'), '101', '" + wirecenter + "','" + designation + "','" + qual_type_id + "','1','3','" + CreateMode + "','" + User_id + "')";
                    val = detAccess.ExecuteQueryForInsert(strQry);
                }
                else
                {
                    //bool val2 = false;
                    //strQry = "Update Job_Details set JOB_CREATED_DATE = sysdate, job_status = '" + status + "' where wire_center_id = '" + wirecenter + "'";
                    //val2 = detAccess.ExecuteQueryForInsert(strQry);
                    //if (val2)
                    //{
                    //if (qual_status == "1")
                    //{
                    string[] desarray = designation.Split(',');
                    for (int i = 0; i < desarray.Length; i++)
                    {
                        string[] NDSJobNumber = desarray[i].Split('|');
                        string JobNum = desarray[i];
                        string JobNum2 = string.Empty;
                        if (NDSJobNumber.Length > 1)
                        {
                            int ind = JobNum.LastIndexOf('|');
                            JobNum2 = JobNum.Substring(ind + 1);
                            JobNum = JobNum.Substring(0, ind);
                        }
                        if (status == "209")
                        {
                            if (NDSJobNumber.Length == 1)
                            {
                                strQry = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = sysdate where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum + "'";
                                val = detAccess.ExecuteQueryForInsert(strQry);
                            }
                            else
                            {
                                strQry = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = sysdate where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum + "' and QUAL_STATUS_ID = '" + qual_status + "' and NDS_JNO = '" + JobNum2 + "'";
                                val = detAccess.ExecuteQueryForInsert(strQry);
                            }
                        }
                        else
                        {
                            if (NDSJobNumber.Length == 1)
                            {
                                strQry = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = sysdate where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum + "' and QUAL_STATUS_ID = '" + qual_status + "'";
                                val = detAccess.ExecuteQueryForInsert(strQry);
                            }
                            else
                            {
                                strQry = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = sysdate where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum + "' and QUAL_STATUS_ID = '" + qual_status + "' and NDS_JNO = '" + JobNum2 + "'";
                                val = detAccess.ExecuteQueryForInsert(strQry);
                            }
                            if (val == true)
                            {
                                string strquerySt = "SELECT QUAL_STATUS_DESC FROM LUT_QUAL_STATUS WHERE QUAL_STATUS_ID = '" + qual_status + "'";
                                //string strquery2 = "SELECT MAX(JOB_ID)FROM qual_schema.JOB_DETAILS";
                                DataSet MaxIDSetSt = detAccess.ExecuteQuery(strquerySt);
                                DataTable MaxtableSt = MaxIDSetSt.Tables[0];
                                string QualStatus = MaxtableSt.Rows[0][0].ToString();
                                if (NDSJobNumber.Length == 1)
                                {
                                    string strqueryDel = "Select objectid from qual_schema.sqm_publish where DESIGNATION = '" + JobNum + "' and STATUS = '" + QualStatus + "'";
                                    //string strquery2 = "SELECT MAX(JOB_ID)FROM qual_schema.JOB_DETAILS";
                                    DataSet MaxIDSetDel = detAccess.ExecuteQuery(strqueryDel);
                                    DataTable MaxtableDel = MaxIDSetDel.Tables[0];
                                    if (MaxtableDel.Rows.Count != 0)
                                    {
                                        string DesignationCheck = MaxtableDel.Rows[0][0].ToString();
                                        strQry = "Delete from qual_schema.sqm_publish where DESIGNATION = '" + JobNum + "' and STATUS = '" + QualStatus + "' and OBJECTID = '" + DesignationCheck + "'";
                                        val = detAccess.ExecuteQueryForInsert(strQry);

                                    }
                                }
                                else
                                {
                                    string strqueryDel = "Select objectid from qual_schema.sqm_publish where DESIGNATION = '" + JobNum + "' and NDS_JOB_NBR = '" + JobNum2 + "' and STATUS = '" + QualStatus + "'";
                                    //string strquery2 = "SELECT MAX(JOB_ID)FROM qual_schema.JOB_DETAILS";
                                    DataSet MaxIDSetDel = detAccess.ExecuteQuery(strqueryDel);
                                    DataTable MaxtableDel = MaxIDSetDel.Tables[0];
                                    if (MaxtableDel.Rows.Count != 0)
                                    {
                                        string DesignationCheck = MaxtableDel.Rows[0][0].ToString();
                                        strQry = "Delete from qual_schema.sqm_publish where DESIGNATION = '" + JobNum + "' and NDS_JOB_NBR = '" + JobNum2 + "' and STATUS = '" + QualStatus + "' and OBJECTID = '" + DesignationCheck + "'";
                                        val = detAccess.ExecuteQueryForInsert(strQry);

                                    }
                                }
                                string strquery2 = "SELECT QUAL_TYPE_DESC FROM LUT_QUAL_TYPES WHERE QUAL_TYPE_ID = '" + qual_type_id + "'";
                                //string strquery2 = "SELECT MAX(JOB_ID)FROM qual_schema.JOB_DETAILS";
                                DataSet MaxIDSet = detAccess.ExecuteQuery(strquery2);
                                DataTable Maxtable = MaxIDSet.Tables[0];
                                string QualTyp = Maxtable.Rows[0][0].ToString();
                                if (NDSJobNumber.Length == 1)
                                {
                                    strQry = "insert into sqm_publish (OBJECTID,SERVICE_TYPE,STATUS,AVAILABILTY_DATE,BANDWIDTH,SERVING_WIRE_CENTER_CLLI,SERVING_WIRE_CENTER_NAME,DESIGNATION,NDS_JOB_NBR,CREATEMODE,SHAPE) "
                                        + "select objectid,'" + QualTyp + "','" + QualStatus + "',AVAIL_DT, BAND,(select WC_ID from SWGISLOC.gis_local_cl_wire_center where id = '" + wirecenter + "'),"
                                        + "WC_NAME,FCBL_ID,NDS_JNO,'Automatic',SHAPE from service_qualification_model where FCBL_ID = '" + JobNum + "' and QU_TY_STAT = '" + qual_type_id + "'";
                                    val = detAccess.ExecuteQueryForInsert(strQry);
                                }
                                else
                                {
                                    strQry = "insert into sqm_publish (OBJECTID,SERVICE_TYPE,STATUS,AVAILABILTY_DATE,BANDWIDTH,SERVING_WIRE_CENTER_CLLI,SERVING_WIRE_CENTER_NAME,DESIGNATION,NDS_JOB_NBR,CREATEMODE,SHAPE) "
                                        + "select objectid,'" + QualTyp + "','" + QualStatus + "',AVAIL_DT, BAND,(select WC_ID from SWGISLOC.gis_local_cl_wire_center where id = '" + wirecenter + "'),"
                                        + "WC_NAME,FCBL_ID,NDS_JNO,'Automatic',SHAPE from service_qualification_model where FCBL_ID = '" + JobNum + "' and QU_TY_STAT = '" + qual_status + "' and NDS_JNO = '" + JobNum2 + "'";
                                    val = detAccess.ExecuteQueryForInsert(strQry);
                                }
                                if (val == true)
                                {
                                    string strquery3 = "select WC_ID from SWGISLOC.gis_local_cl_wire_center where id = '" + wirecenter + "'";
                                    //string strquery2 = "SELECT MAX(JOB_ID)FROM qual_schema.JOB_DETAILS";
                                    DataSet CLLIDS = detAccess.ExecuteQuery(strquery3);
                                    DataTable CLLIDB = CLLIDS.Tables[0];
                                    string CLLI = CLLIDB.Rows[0][0].ToString();
                                    string qual_st;
                                    if (qual_status == "1")
                                    {
                                        qual_st = "IN SERVICE";
                                    }
                                    else
                                    {
                                        qual_st = "IN CONSTRUCTION";
                                    }
                                    if (NDSJobNumber.Length == 1)
                                    {
                                        val = detAccess.BIWF_Insert_ODS_INERVICE(JobNum, "", qual_st, CLLI);
                                    }
                                    else
                                    {
                                        val = detAccess.BIWF_Insert_ODS_INCONSTRUCTION(JobNum, JobNum2, qual_st, CLLI);
                                    }
                                }

                            }
                        }
                    }
                    //}
                    //else if (qual_status == "2")
                    //{
                    //    string[] desarray = designation.Split(',');
                    //    for (int i = 0; i < desarray.Length; i++)
                    //    {
                    //        string[] NDSJobNumber = desarray[i].Split('|');
                    //        string JobNum = desarray[i];
                    //        int ind = JobNum.LastIndexOf('|');
                    //        string JobNum2 = JobNum.Substring(0, ind);
                    //        JobNum = JobNum.Substring(ind + 1);
                    //        if (status == "209")
                    //        {
                    //            strQry = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = sysdate where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum2 + "'";
                    //            val = detAccess.ExecuteQueryForInsert(strQry);
                    //        }
                    //        else
                    //        {
                    //            strQry = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = sysdate where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum2 + "' and QUAL_STATUS_ID = '" + qual_status + "' and NDS_JNO = '" + JobNum + "'";
                    //            val = detAccess.ExecuteQueryForInsert(strQry);


                    //            if (val == true)
                    //            {
                    //                string strqueryDel = "Select objectid from qual_schema.sqm_publish where DESIGNATION = '" + JobNum2 + "' and NDS_JOB_NBR = '" + JobNum + "' and STATUS = 'IN CONSTRUCTION'";
                    //                //string strquery2 = "SELECT MAX(JOB_ID)FROM qual_schema.JOB_DETAILS";
                    //                DataSet MaxIDSetDel = detAccess.ExecuteQuery(strqueryDel);
                    //                DataTable MaxtableDel = MaxIDSetDel.Tables[0];
                    //                if(MaxtableDel.Rows.Count != 0)
                    //                {
                    //                    string DesignationCheck = MaxtableDel.Rows[0][0].ToString();
                    //                    strQry = "Delete from qual_schema.sqm_publish where DESIGNATION = '" + JobNum2 + "' and NDS_JOB_NBR = '" + JobNum + "' and STATUS = 'IN CONSTRUCTION' and OBJECTID = '" + DesignationCheck + "'";
                    //                    val = detAccess.ExecuteQueryForInsert(strQry);

                    //                }                                    
                    //                string strquery2 = "SELECT QUAL_TYPE_DESC FROM LUT_QUAL_TYPES WHERE QUAL_TYPE_ID = '" + qual_type_id + "'";
                    //                //string strquery2 = "SELECT MAX(JOB_ID)FROM qual_schema.JOB_DETAILS";
                    //                DataSet MaxIDSet = detAccess.ExecuteQuery(strquery2);
                    //                DataTable Maxtable = MaxIDSet.Tables[0];
                    //                string QualTyp = Maxtable.Rows[0][0].ToString();
                    //                //strQry = "insert into sqm_publish (OBJECTID,SERVICE_TYPE,STATUS,AVAILABILTY_DATE,BANDWIDTH,SERVING_WIRE_CENT ER_CLLI,SERVING_WIRE_CENTER_NAME, "
                    //                //      + " DESIGNATION,NDS_JOB_NBR,CREATEMODE,SHAPE) "
                    //                //      + "select objectid,'" + QualTyp + "','IN CONSTRUCTION',AVAIL_DT, BAND,(select WC_ID from SWGISLOC.gis_local_cl_wire_center where id = '" + wirecenter + "'), "
                    //                //      + "WC_NAME,FCBL_ID,NDS_JNO,'Automatic',SHAPE from service_qualification_model where FCBL_ID = '" + JobNum2 + "' and NDS_JNO = '" + JobNum + "'";
                    //                strQry = "insert into sqm_publish (OBJECTID,SERVICE_TYPE,STATUS,AVAILABILTY_DATE,BANDWIDTH,SERVING_WIRE_CENTER_CLLI,SERVING_WIRE_CENTER_NAME,DESIGNATION,NDS_JOB_NBR,CREATEMODE,SHAPE) "
                    //                        + "select objectid,'" + QualTyp + "','IN CONSTRUCTION',AVAIL_DT, BAND,(select WC_ID from SWGISLOC.gis_local_cl_wire_center where id = '" + wirecenter + "'),"
                    //                        + "WC_NAME,FCBL_ID,NDS_JNO,'Automatic',SHAPE from service_qualification_model where FCBL_ID = '" + JobNum2 + "' and NDS_JNO = '" + JobNum + "' and QU_TY_STAT = '2'";
                    //                val = detAccess.ExecuteQueryForInsert(strQry);
                    //                if (val == true)
                    //                {
                    //                    string strquery3 = "select WC_ID from SWGISLOC.gis_local_cl_wire_center where id = '" + wirecenter + "'";
                    //                    //string strquery2 = "SELECT MAX(JOB_ID)FROM qual_schema.JOB_DETAILS";
                    //                    DataSet CLLIDS = detAccess.ExecuteQuery(strquery3);
                    //                    DataTable CLLIDB = CLLIDS.Tables[0];
                    //                    string CLLI = CLLIDB.Rows[0][0].ToString();
                    //                    val = detAccess.BIWF_Insert_ODS_INCONSTRUCTION(JobNum2, JobNum, "IN CONSTRUCTION");
                    //                }


                    //            }
                    //        }
                    //    }
                    //}

                    //}
                }

            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "ExecuteQueryForInsertingDataIntoReviewDetails");
                throw ex;
            }
            return val;
        }
        public Boolean ExecuteQueryForUpdateDataIntoNoBuild(string User_id, string wirecenter, string polygonID, string status, string review, string CDateTime, string base64, string ToolName)
        {
            string qryStatus = string.Empty;
            string existUsers = string.Empty;
            bool val = false;
            string strQry;


            try
            {
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();
                AccessData detAccess = new AccessData();
                string[] desarray = polygonID.Split(',');
                if (ToolName == "Review")
                {
                    for (int i = 0; i < desarray.Length; i++)
                    {
                        if (base64 != "")
                        {
                            val = detAccess.InsertReviewImageDataNoB(User_id, wirecenter, review, desarray[i], status, CDateTime, base64);
                        }
                        else
                        {
                            if (status == "207")
                            {
                                strQry = "update REVIEW_NOBUILD set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS') where WC_ID= '" + wirecenter + "'";
                                //strQry = "update REVIEW_NOBUILD set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS') where WC_ID= '" + wirecenter + "' and POLYGON_ID= '" + desarray[i] + "'";
                                val = detAccess.ExecuteQueryForInsert(strQry);
                            }
                            else
                            {
                                strQry = "update REVIEW_NOBUILD set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS') where WC_ID= '" + wirecenter + "' and POLYGON_ID= '" + desarray[i] + "'";
                                //strQry = "update REVIEW_NOBUILD set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS') where WC_ID= '" + wirecenter + "' and POLYGON_ID= '" + desarray[i] + "'";
                                val = detAccess.ExecuteQueryForInsert(strQry);
                            }
                        }
                    }
                }
                else if (ToolName == "Publish")
                {
                    for (int i = 0; i < desarray.Length; i++)
                    {
                        if (status == "209")
                        {
                            strQry = "update REVIEW_NOBUILD set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS') where WC_ID= '" + wirecenter + "'";
                            //strQry = "update REVIEW_NOBUILD set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS') where WC_ID= '" + wirecenter + "' and POLYGON_ID= '" + desarray[i] + "'";
                            val = detAccess.ExecuteQueryForInsert(strQry);
                        }
                        else
                        {
                            strQry = "update REVIEW_NOBUILD set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS') where WC_ID= '" + wirecenter + "' and POLYGON_ID= '" + desarray[i] + "'";
                            //strQry = "update REVIEW_NOBUILD set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS') where WC_ID= '" + wirecenter + "' and POLYGON_ID= '" + desarray[i] + "'";
                            val = detAccess.ExecuteQueryForInsert(strQry);
                        }
                    }
                }
            }


            //val = detAccess.ExecuteQueryForInsert(strQry);

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "ExecuteQueryForUpdateDataIntoNoBuild");
                throw ex;
            }
            return val;
        }
    }
}
