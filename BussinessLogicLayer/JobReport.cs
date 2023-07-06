using DataAccessLogicLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer
{
    public class JobReport
    {
        public string GetJobReportDetailsOld(string userRoleId, string UserID)
        {
            string jsonstring = string.Empty;
            string strQry;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();
                string strqery = string.Empty;
                if (userRoleId == "4" || userRoleId == "")
                {
                    //strqery = "select distinct jd.JOB_ID,ud.USER_ID as user_name,TO_CHAR(jd.JOB_CREATED_DATE, 'MM-DD-YYYY HH:MI:SS AM') JOB_CREATED_DATE,ls.STATUS_DESC,jd.WC_ID,(clw.wc_id || '-' || clw.WIRE_CENTER_NAME) as WIRE_CENTER_NAME,jd.JOB_DESIGNATION,TO_CHAR(jd.JOB_END_DATETIME, 'MM-DD-YYYY HH:MI:SS AM') JOB_END_DATETIME,TO_CHAR(jd.JOB_START_DATETIME, 'MM-DD-YYYY HH:MI:SS AM') JOB_START_DATETIME,jd.SOURCE,jd.DAEMON_VERSION,jd.SQM_VERSION,(select count(je.job_id) from JOB_EXCEPTIONS je where je.job_id=jd.job_id) job_execption,(select count(jf.job_id) from JOB_FAILURES jf where jf.job_id=jd.job_id) job_failure,(select count(jl.job_id) from JOB_STATISTICS_LOGS jl where jl.job_id=jd.job_id) job_stats  from JOB_DETAILS jd inner join LUT_STATUS ls on jd.JOB_STATUS = ls.STATUS_ID inner join SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER clw on jd.WC_ID = clw.ID inner join qual_schema.REVIEW_DETAILS rd on rd.JOB_ID = jd.JOB_ID inner join qual_schema.USER_ROLE ud on ud.user_id = rd.USER_ID";
                    strqery = "select distinct jd.JOB_ID,ud.USER_ID as user_name,TO_CHAR(jd.JOB_CREATED_DATE, 'MM-DD-YYYY HH:MI:SS AM') JOB_CREATED_DATE_VAL, jd.JOB_CREATED_DATE, " +
                            " ls.STATUS_DESC,jd.WC_ID,(clw.wc_id || '-' || clw.WIRE_CENTER_NAME) as WIRE_CENTER_NAME,jd.JOB_DESIGNATION, " +
                            " TO_CHAR(jd.JOB_END_DATETIME, 'MM-DD-YYYY HH:MI:SS AM') JOB_END_DATETIME,TO_CHAR(jd.JOB_START_DATETIME, 'MM-DD-YYYY HH:MI:SS AM') " +
                            " JOB_START_DATETIME,jd.SOURCE,jd.DAEMON_VERSION,jd.SQM_VERSION,lt.QUAL_TYPE_DESC,jd.QUAL_TYPE_ID,ls.QUAL_STATUS_DESC,jd.QUAL_STATUS, je.job_execption, jf.job_failure, jl.job_stats " +
                            " from JOB_DETAILS jd inner join LUT_STATUS ls on jd.JOB_STATUS = ls.STATUS_ID " +
                            " inner join SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER clw on jd.WC_ID = clw.ID inner join qual_schema.USER_ROLE ud on ud.WC_ID = jd.WC_ID " +
                            " left join (select job_id, count(job_id) as job_execption from qual_schema.JOB_EXCEPTIONS Group by job_id)  je  on je.job_id=jd.job_id " +
                            " left join (select job_id, count(job_id) as job_failure from qual_schema.JOB_FAILURES Group by job_id)  jf  on jf.job_id=jd.job_id " +
                            " left join (select job_id, count(job_id) as job_stats from qual_schema.JOB_STATISTICS_LOGS Group by job_id)  jl  on jl.job_id=jd.job_id " +
                            " left join qual_schema.LUT_QUAL_TYPES lt on jd.QUAL_TYPE_ID = lt.QUAL_TYPE_ID left join qual_schema.LUT_QUAL_STATUS ls on jd.QUAL_STATUS = ls.QUAL_STATUS_ID where ud.ROLE_ID = '1' order by jd.JOB_CREATED_DATE desc";
                }
                else
                {
                    //strqery = "select distinct jd.JOB_ID,ud.USER_ID as user_name,TO_CHAR(jd.JOB_CREATED_DATE, 'MM-DD-YYYY HH:MI:SS AM') JOB_CREATED_DATE,ls.STATUS_DESC,jd.WC_ID,(clw.wc_id || '-' || clw.WIRE_CENTER_NAME) as WIRE_CENTER_NAME,jd.JOB_DESIGNATION,TO_CHAR(jd.JOB_END_DATETIME, 'MM-DD-YYYY HH:MI:SS AM') JOB_END_DATETIME,TO_CHAR(jd.JOB_START_DATETIME, 'MM-DD-YYYY HH:MI:SS AM') JOB_START_DATETIME,jd.SOURCE,jd.DAEMON_VERSION,jd.SQM_VERSION,(select count(je.job_id) from JOB_EXCEPTIONS je where je.job_id=jd.job_id) job_execption,(select count(jf.job_id) from JOB_FAILURES jf where jf.job_id=jd.job_id) job_failure,(select count(jl.job_id) from JOB_STATISTICS_LOGS jl where jl.job_id=jd.job_id) job_stats  from JOB_DETAILS jd inner join LUT_STATUS ls on jd.JOB_STATUS = ls.STATUS_ID inner join qual_schema.USER_ROLE rd on jd.WC_ID = rd.WC_ID inner join SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER clw on jd.WC_ID = clw.ID inner join qual_schema.REVIEW_DETAILS rd on rd.JOB_ID = jd.JOB_ID inner join qual_schema.USER_ROLE ud on ud.user_id = rd.USER_ID where rd.USER_ID = '" + UserID + "'";
                    strqery = "select distinct jd.JOB_ID,ud.USER_ID as user_name,TO_CHAR(jd.JOB_CREATED_DATE, 'MM-DD-YYYY HH:MI:SS AM') JOB_CREATED_DATE_VAL, jd.JOB_CREATED_DATE, " +
                                " ls.STATUS_DESC,jd.WC_ID,(clw.wc_id || '-' || clw.WIRE_CENTER_NAME) as WIRE_CENTER_NAME,jd.JOB_DESIGNATION, " +
                                " TO_CHAR(jd.JOB_END_DATETIME, 'MM-DD-YYYY HH:MI:SS AM') JOB_END_DATETIME,TO_CHAR(jd.JOB_START_DATETIME, 'MM-DD-YYYY HH:MI:SS AM') " +
                                " JOB_START_DATETIME,jd.SOURCE,jd.DAEMON_VERSION,jd.SQM_VERSION,lt.QUAL_TYPE_DESC,jd.QUAL_TYPE_ID,ls.QUAL_STATUS_DESC,jd.QUAL_STATUS,je.job_execption, jf.job_failure, jl.job_stats " +
                                " from JOB_DETAILS jd inner join LUT_STATUS ls on jd.JOB_STATUS = ls.STATUS_ID inner join qual_schema.USER_ROLE rd on jd.WC_ID = rd.WC_ID " +
                                " inner join SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER clw on jd.WC_ID = clw.ID inner join qual_schema.USER_ROLE ud on ud.WC_ID = jd.WC_ID " +
                                " left join (select job_id, count(job_id) as job_execption from qual_schema.JOB_EXCEPTIONS Group by job_id)  je  on je.job_id=jd.job_id " +
                            " left join (select job_id, count(job_id) as job_failure from qual_schema.JOB_FAILURES Group by job_id)  jf  on jf.job_id=jd.job_id " +
                            " left join (select job_id, count(job_id) as job_stats from qual_schema.JOB_STATISTICS_LOGS Group by job_id)  jl  on jl.job_id=jd.job_id " +
                                " left join qual_schema.LUT_QUAL_TYPES lt on jd.QUAL_TYPE_ID = lt.QUAL_TYPE_ID left join qual_schema.LUT_QUAL_STATUS ls on jd.QUAL_STATUS = ls.QUAL_STATUS_ID where ud.USER_ID = '" + UserID + "' and ud.ROLE_ID = '1' order by jd.JOB_CREATED_DATE desc";
                }
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                //for (int i = 0; i < userTable.Rows.Count; i++)
                //{

                //}
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetJobReportDetails");
                throw ex;
            }
            return jsonstring;
        }

        public string GetJobReportDetails(string userRoleId, string UserID)
        {
            string strJson = string.Empty;
            try
            {
                DataTable dt = new DataTable();
                AccessData dataAccess = new AccessData();
                dt = dataAccess.GetDTJobReportDetails(userRoleId, UserID);
                strJson = Utilities.ConvertDataTableToJSNString(dt);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetJobReportDetails");
                throw ex;
            }
            return strJson;
        }

        public string GetJobReportArchiveDetails(string JobID)
        {
            string jsonstring = string.Empty;
            string strQry;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();

                string strqery = "select distinct jd.JOB_ID,TO_CHAR(jd.JOB_UPDATED_DATE, 'MM-DD-YYYY HH:MI:SS AM') JOB_UPDATED_DATE,ls.STATUS_DESC, " +
                                " JB.WC_ID,(cl.wc_id || '-' || cl.WIRE_CENTER_NAME) as WIRE_CENTER_NAME from JOB_DETAILS_WORKFLOW jd inner join LUT_STATUS ls on jd.JOB_STATUS = ls.STATUS_ID " +
                                " inner join qual_schema.JOB_DETAILS JB on JB.JOB_ID = jd.JOB_ID inner join SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER cl on JB.WC_ID = cl.ID  where jd.JOB_ID = '" + JobID + "' order by JOB_UPDATED_DATE desc";
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetJobReportArchiveDetails");
                throw ex;
            }
            return jsonstring;
        }
        public string GetJobReportImage(string JobID, string Designation_ID, string Table_name, string ColumnName)
        {
            string jsonstring = string.Empty;
            string strQry;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();
                //string strqery = "select FILE_IMAGE from  qual_schema.REVIEW_DETAILS where JOB_ID = '2887' and DESIGNATION_ID ='LD11221W26'";
                string strqery = "select FILE_IMAGE from  qual_schema." + Table_name + " where JOB_ID = '" + JobID + "' and " + ColumnName + " ='" + Designation_ID + "'";
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                if (userTable.Rows[0][0].ToString() != "")
                {
                    Byte[] bytes = (byte[])userTable.Rows[0][0];
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    jsonstring = "data:image/png;base64," + base64String;
                }
                else
                {
                    jsonstring = "Image Not Found";
                }
                //jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
                //jsonstring = "{\"ColuomnName\":['FILE_IMAGE'],\"Data\":[NA]}";
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetJobReportImage");
                throw ex;
            }
            return jsonstring;
        }
        public string BinaryToText(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
        public string GetServicePolygonDetails(string wire_center_id, string Job_ID, string QualStatusID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();
                string strqery = string.Empty;

                //string strqery = "select s.Polygon_id,q.qual_type_desc,s.Status,s.Wire_center_id,s.Designation from QUAL_SCHEMA.service_polygons s inner join QUAL_SCHEMA.lut_qual_types q on s.Qual_type = q.qual_type_id where s.Wire_center_id = '" + wire_center_id + "' ";
                //string strqery = "select s.OBJECTID,s.Type,s.status,s.AVAIL_DT,s.BAND,s.WC_ID,s.WC_NAME,s.FCBL_ID,s.FLOC,s.NDS_JNO from QUAL_SCHEMA.service_polygon_output s  where s.WC_ID = '" + wire_center_id + "' and s.NDS_JNO='" + Job_ID +"'";
                //string strqery = "select distinct rd.job_id,rd.USER_ID,qt.qual_type_desc,rd.wc_id,(wc.wc_id || '-' || wc.WIRE_CENTER_NAME) as WIRE_CENTER_NAME ,rd.DESIGNATION_ID,st.status_desc,TO_CHAR(rd.review_date, 'MM-DD-YYYY HH:MI:SS AM') review_date,rd.remarks " +
                //                " from qual_schema.review_details rd,qual_schema.lut_qual_types qt, SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER wc, qual_schema.lut_status st " +
                //                " where rd.qual_type_id = qt.qual_type_id and rd.status_id = st.status_id and rd.wc_id = wc.id and rd.job_id = '" + Job_ID + "' order by review_date desc";
                if (QualStatusID == "1")
                {
                    strqery = "select distinct rd.job_id,rd.USER_ID,qt.qual_type_desc,rd.wc_id,(wc.wc_id || '-' || wc.WIRE_CENTER_NAME) as WIRE_CENTER_NAME ,rd.DESIGNATION_ID,rd.NDS_JNO,rd.QUAL_STATUS_ID,ls.QUAL_STATUS_DESC,st.status_desc,TO_CHAR(rd.review_date, 'MM-DD-YYYY HH:MI:SS AM') review_date,rd.remarks " +
                                    " from qual_schema.review_details rd,qual_schema.lut_qual_types qt, SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER wc, qual_schema.lut_status st,qual_schema.LUT_QUAL_STATUS ls " +
                                    " where rd.qual_type_id = qt.qual_type_id and rd.status_id = st.status_id and rd.wc_id = wc.id and rd.QUAL_STATUS_ID = ls.QUAL_STATUS_ID and rd.job_id = '" + Job_ID + "' order by review_date desc";
                }
                else if (QualStatusID == "3")
                {
                    strqery = " select RB.JOB_ID,RB.WC_ID,(clw.wc_id || '-' || clw.WIRE_CENTER_NAME) as WIRE_CENTER_NAME,RB.POLYGON_ID,RB.REMARKS,ls.STATUS_DESC,TO_CHAR(RB.REVIEW_DATE, 'MM-DD-YYYY HH:MI:SS AM') REVIEW_DATE from REVIEW_NOBUILD RB, "
                            + " SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER clw, LUT_STATUS ls "
                            + " where RB.WC_ID = clw.ID and RB.STATUS_ID = ls.STATUS_ID  and RB.JOB_ID = '" + Job_ID + "' order by REVIEW_DATE ";
                }
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetServicePolygonDetails");
                throw ex;
            }
            return jsonstring;
        }
        public string GetServiceArcPolygonDetails(string Desg_id, string Job_ID, string qual_status)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();
                string strqery = string.Empty;

                //string strqery = "select s.Polygon_id,q.qual_type_desc,s.Status,s.Wire_center_id,s.Designation from QUAL_SCHEMA.service_polygons s inner join QUAL_SCHEMA.lut_qual_types q on s.Qual_type = q.qual_type_id where s.Wire_center_id = '" + wire_center_id + "' ";
                //string strqery = "select s.OBJECTID,s.Type,s.status,s.AVAIL_DT,s.BAND,s.WC_ID,s.WC_NAME,s.FCBL_ID,s.FLOC,s.NDS_JNO from QUAL_SCHEMA.service_polygon_output s  where s.WC_ID = '" + wire_center_id + "' and s.NDS_JNO='" + Job_ID +"'";
                if (qual_status == "1")
                {
                    strqery = "select distinct rd.job_id,rd.USER_ID,qt.qual_type_desc,rd.wc_id,(wc.wc_id || '-' || wc.WIRE_CENTER_NAME) as WIRE_CENTER_NAME ,rd.DESIGNATION_ID,rd.NDS_JNO,rd.QUAL_STATUS_ID,ls.QUAL_STATUS_DESC,st.status_desc,TO_CHAR(rd.review_date, 'MM-DD-YYYY HH:MI:SS AM') review_date,rd.remarks " +
                                " from qual_schema.REVIEW_DETAILS_WORKFLOW rd,qual_schema.lut_qual_types qt, SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER wc, qual_schema.lut_status st,qual_schema.LUT_QUAL_STATUS ls " +
                                " where rd.qual_type_id = qt.qual_type_id and rd.status_id = st.status_id and rd.wc_id = wc.id and rd.QUAL_STATUS_ID = ls.QUAL_STATUS_ID and rd.job_id = '" + Job_ID + "' and rd.DESIGNATION_ID ='" + Desg_id + "' order by review_date desc";

                }
                else if (qual_status == "2")
                {
                    string[] NDSJobNumber = Desg_id.Split('-');
                    strqery = "select distinct rd.job_id,rd.USER_ID,qt.qual_type_desc,rd.wc_id,(wc.wc_id || '-' || wc.WIRE_CENTER_NAME) as WIRE_CENTER_NAME ,rd.DESIGNATION_ID,rd.NDS_JNO,rd.QUAL_STATUS_ID,ls.QUAL_STATUS_DESC,st.status_desc,TO_CHAR(rd.review_date, 'MM-DD-YYYY HH:MI:SS AM') review_date,rd.remarks " +
                                " from qual_schema.REVIEW_DETAILS_WORKFLOW rd,qual_schema.lut_qual_types qt, SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER wc, qual_schema.lut_status st,qual_schema.LUT_QUAL_STATUS ls " +
                                " where rd.qual_type_id = qt.qual_type_id and rd.status_id = st.status_id and rd.wc_id = wc.id and rd.QUAL_STATUS_ID = ls.QUAL_STATUS_ID and rd.job_id = '" + Job_ID + "' and rd.DESIGNATION_ID ='" + NDSJobNumber[0] + "' and NDS_JNO = '" + NDSJobNumber[1] + "'  order by review_date desc";
                }
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetServiceArcPolygonDetails");
                throw ex;
            }
            return jsonstring;
        }
        public string GetServiceArcNoBPolygonDetails(string Desg_id, string Job_ID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();
                string strqery = string.Empty;

                //string strqery = "select s.Polygon_id,q.qual_type_desc,s.Status,s.Wire_center_id,s.Designation from QUAL_SCHEMA.service_polygons s inner join QUAL_SCHEMA.lut_qual_types q on s.Qual_type = q.qual_type_id where s.Wire_center_id = '" + wire_center_id + "' ";
                //string strqery = "select s.OBJECTID,s.Type,s.status,s.AVAIL_DT,s.BAND,s.WC_ID,s.WC_NAME,s.FCBL_ID,s.FLOC,s.NDS_JNO from QUAL_SCHEMA.service_polygon_output s  where s.WC_ID = '" + wire_center_id + "' and s.NDS_JNO='" + Job_ID +"'";

                strqery = " select distinct RB.JOB_ID,RB.WC_ID,(clw.wc_id || '-' || clw.WIRE_CENTER_NAME) as WIRE_CENTER_NAME,RB.POLYGON_ID,RB.REMARKS,ls.STATUS_DESC,TO_CHAR(RB.REVIEW_DATE, 'MM-DD-YYYY HH:MI:SS AM') REVIEW_DATE from REVIEW_NOBUILD_WORKFLOW RB, "
                        + " SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER clw, LUT_STATUS ls "
                        + " where RB.WC_ID = clw.ID and RB.STATUS_ID = ls.STATUS_ID  and RB.JOB_ID = '" + Job_ID + "' and RB.POLYGON_ID = '" + Desg_id + "'  order by REVIEW_DATE ";

                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetServiceArcNoBPolygonDetails");
                throw ex;
            }
            return jsonstring;
        }

        public string GetServiceChangeLog(string Job_ID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();

                //string strqery = "select s.Polygon_id,q.qual_type_desc,s.Status,s.Wire_center_id,s.Designation from QUAL_SCHEMA.service_polygons s inner join QUAL_SCHEMA.lut_qual_types q on s.Qual_type = q.qual_type_id where s.Wire_center_id = '" + wire_center_id + "' ";
                //string strqery = "select s.OBJECTID,s.Type,s.status,s.AVAIL_DT,s.BAND,s.WC_ID,s.WC_NAME,s.FCBL_ID,s.FLOC,s.NDS_JNO from QUAL_SCHEMA.service_polygon_output s  where s.WC_ID = '" + wire_center_id + "' and s.NDS_JNO='" + Job_ID +"'";
                string strqery = " select distinct CG.WC_ID,CG.TYPE,CL.OBJECT_NAME,CG.STATUS,CG.STATUS_DATETIME,CG.LOCATION,CG.DESIGNATION_ID, "
                                + " CG.PROCESSED_JOB_ID,CG.REMARKS from CHANGE_REQUEST_GROUPS CG , CHANGE_REQUEST_LOG CL "
                                + " where CG.CHANGE_GROUP_ID = CL.CHANGE_GROUP_ID and CG.PROCESSED_JOB_ID = '" + Job_ID + "'";
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetServiceChangeLog");
                throw ex;
            }
            return jsonstring;
        }

        public string GetServicePolygonLogs(string wire_center_id, string JobID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();

                // string strqery = "select * from qual_schema.job_logs where WIRE_CENTER_ID =  '" + wire_center_id + "' and JOB_ID = '" + JobID + "' ";
                string strqery = "select distinct js.job_id,TO_CHAR(js.log_date_time, 'MM-DD-YYYY HH:MI:SS AM') log_date_time,(wc.wc_id || '-' || wc.WIRE_CENTER_NAME) as WIRE_CENTER_NAME ,st.status_desc, " +
                                " js.NO_OF_DESIGNATIONS_RECEIVED,js.NO_OF_DESIGNATIONS_PROCESSED,js.NO_OF_PROJECTS_RECEIVED,js.NO_OF_PROJECTS_PROCESSED,js.NO_OF_FAILED as NO_OF_DESIGNATION_FAILED,js.NO_OF_SUCCESS as NO_OF_POLYGONS_SUCCESS,js.NO_OF_EXCEPTION as NO_OF_POLYGONS_EXCEPTION " +
                                " from qual_schema.job_details jd, SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER wc, qual_schema.lut_status st, " +
                                " qual_schema.job_statistics_logs js where  jd.job_status = st.status_id and jd.WC_ID = wc.id " +
                                " and jd.job_id = js.job_id and jd.job_id = '" + JobID + "'";
                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetServicePolygonLogs");
                throw ex;
            }
            return jsonstring;
        }

        public string GetServicePolygonExceptionsold(string wire_center_id, string JobID, string qual_typeID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();
                if (qual_typeID != "3")
                {
                    // string strqery = "select * from qual_schema.JOB_EXCEPTIONS_OLD where WIRE_CENTER_ID =  '" + wire_center_id + "' and JOB_ID = '" + JobID + "' ";
                    string strqery = "select distinct rd.job_id,rd.wc_id,(wc.wc_id || '-' || wc.WIRE_CENTER_NAME) as WIRE_CENTER_NAME , " +
                                    " je.polygon_id,je.designation_id,je.NDS_JNO,ls.QUAL_STATUS_DESC,st.status_desc,je.exception_type,lj.EXCEPTION_MESSAGE as EXE_TYPE,je.exception_message,TO_CHAR(je.exception_datetime, 'MM-DD-YYYY HH:MI:SS AM') exception_datetime " +
                                    " from qual_schema.review_details rd, qual_schema.lut_qual_types qt,qual_schema.LUT_JOB_EXCEPTIONS lj, SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER wc, qual_schema.lut_status st, qual_schema.job_exceptions je,qual_schema.LUT_QUAL_STATUS ls " +
                                    " where rd.qual_type_id = qt.qual_type_id and rd.status_ID = st.status_id and je.exception_type= lj.EXCEPTION_TYPE and rd.WC_ID = wc.id and rd.DESIGNATION_ID = je.DESIGNATION_ID " +
                                    " and rd.job_id = je.job_id and je.QUAL_STATUS = ls.QUAL_STATUS_ID and rd.STATUS_ID = '204' and rd.job_id = '" + JobID + "' order by exception_datetime desc";
                    DataSet userSet = detAccess.ExecuteQuery(strqery);
                    userTable = userSet.Tables[0];
                }
                else
                {
                    //userTable = detAccess.GetServiceCopperPolygonExceptions(wire_center_id, JobID);
                    //jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
                }
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetServicePolygonExceptions");
                throw ex;
            }
            return jsonstring;
        }

        // vinay
        public string GetServicePolygonExceptions(string JobID, string qual_typeID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();
                if (qual_typeID == "1")
                {
                    userTable = detAccess.GetServiceFiberPolygonExceptions(JobID);
                    if (userTable.Rows.Count > 0)
                    {
                        var test = userTable.AsEnumerable()
                        .GroupBy(x => new
                        {
                            POLYGON_ID = x.Field<Decimal?>("POLYGON_ID"),
                            DESIGNATION_ID = x.Field<string>("DESIGNATION_ID"),
                            NDS_JNO = x.Field<string>("NDS_JNO"),
                            QUAL_STATUS_ID = x.Field<Int16>("QUAL_STATUS_ID"),
                            QUAL_STATUS_DESC = x.Field<string>("QUAL_STATUS_DESC"),
                            STATUS_DESC = x.Field<string>("STATUS_DESC"),
                        })
                        .Select(g => new
                        {
                            POLYGON_ID = g.Key.POLYGON_ID,
                            DESIGNATION_ID = g.Key.DESIGNATION_ID,
                            NDS_JNO = g.Key.NDS_JNO,
                            QUAL_STATUS_ID = g.Key.QUAL_STATUS_ID,
                            QUAL_STATUS_DESC = g.Key.QUAL_STATUS_DESC,
                            STATUS_DESC = g.Key.STATUS_DESC,
                            child = g.Select(r => new
                            {
                                EXCEPTION_MESSAGE = r["EXCEPTION_MESSAGE"].ToString(),
                                EXCEPTION_DATETIME = r["EXCEPTION_DATETIME"].ToString(),
                                REMARKS = r["REMARKS"].ToString()
                            }).ToArray()
                        });
                        jsonstring = JsonConvert.SerializeObject(test);
                    }
                }
                else if (qual_typeID == "2")
                {
                    userTable = detAccess.GetServiceCopperPolygonExceptions(JobID);
                    jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
                }
                else
                {
                    userTable = detAccess.GetServiceNoBuildPolygonExceptions(JobID);
                    jsonstring = JsonConvert.SerializeObject(userTable);
                }
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetServicePolygonExceptions");
                throw ex;
            }
            return jsonstring;
        }




        public string GetServicePolygonFailuresold(string wire_center_id, string JobID, string qual_typeID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();
                if (qual_typeID != "3")
                {
                    // string strqery = "select * from qual_schema.JOB_EXCEPTIONS_OLD where WIRE_CENTER_ID =  '" + wire_center_id + "' and JOB_ID = '" + JobID + "' ";
                    string strqery = "select distinct rd.job_id,rd.wc_id,(wc.wc_id || '-' || wc.WIRE_CENTER_NAME) as WIRE_CENTER_NAME , " +
                                    " je.DESIGNATION_ID,je.NDS_JNO,ls.QUAL_STATUS_DESC,st.status_desc,je.FAILURE_TYPE,lj.FAILURE_MESSAGE as Fail_TYPE, " +
                                    " je.FAILURE_MESSAGE,TO_CHAR(je.FAILURE_DATETIME, 'MM-DD-YYYY HH:MI:SS AM') Failure_datetime " +
                                    " from qual_schema.review_details rd, qual_schema.lut_qual_types qt,qual_schema.LUT_JOB_FAILURES lj, " +
                                    " SWGISLOC.GIS_LOCAL_CL_WIRE_CENTER wc, qual_schema.lut_status st, qual_schema.JOB_FAILURES je,qual_schema.LUT_QUAL_STATUS ls " +
                                    " where rd.qual_type_id = qt.qual_type_id and rd.status_ID = st.status_id and je.FAILURE_TYPE= lj.FAILURE_TYPE " +
                                    " and rd.WC_ID = wc.id and rd.job_id = je.job_id and je.QUAL_STATUS = ls.QUAL_STATUS_ID and rd.STATUS_ID = '205' and rd.DESIGNATION_ID = je.DESIGNATION_ID and rd.job_id = '" + JobID + "' order by Failure_datetime desc";

                    DataSet userSet = detAccess.ExecuteQuery(strqery);

                    userTable = userSet.Tables[0];
                }
                else
                {
                    userTable = detAccess.GetServiceCopperPolygonFailures(wire_center_id, JobID);
                }
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

        //vinay

        public string GetServicePolygonFailures(string JobID, string qual_typeID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                userTable = detAccess.GetServicePolygonFailures(JobID);
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

        public string GetJobReportImage(string strPolygonId, string strQualtype)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                userTable = detAccess.GetJobReportImage(strPolygonId, strQualtype);
                if (userTable.Rows[0]["FILE_IMAGE"].ToString() != "")
                {
                    Byte[] bytes = (byte[])userTable.Rows[0]["FILE_IMAGE"];
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    jsonstring = "data:image/png;base64," + base64String;
                }
                else
                {
                    jsonstring = "-99";
                }
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetServicePolygonFailures");
                throw ex;
            }
            return jsonstring;
        }


        public Boolean UpdateJobdetails(string wire_center_id, string JobID)
        {
            DataTable userTable = new DataTable();
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {
                string jodstatus = string.Empty;
                string strquery2 = "SELECT JOB_STATUS FROM qual_schema.JOB_DETAILS Where JOB_ID = '" + JobID + "'";
                DataSet MaxIDSet = detAccess.ExecuteQuery(strquery2);
                DataTable Maxtable = MaxIDSet.Tables[0];
                string JodStat = Maxtable.Rows[0][0].ToString();
                if (JodStat == "105")
                {
                    jodstatus = "104";
                }
                else
                {
                    jodstatus = "103";
                }
                strQry = "update qual_schema.job_details set JOB_STATUS = '" + jodstatus + "' where WC_ID = '" + wire_center_id + "' and JOB_ID = '" + JobID + "' ";
                val = detAccess.ExecuteQueryForInsert(strQry);
            }
            catch (Exception ex)
            {
                //AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "UpdateJobdetails");
                throw ex;
            }
            return val;
        }
        public Boolean UpdateCopperParcel(string apn, string fips, string SERVING_WIRE_CENTER_CLLI, string SERVING_WIRE_CENTER_NAME, string min_X, string min_y, string max_X, string max_y, string remarks, string objectid)
        {
            DataTable userTable = new DataTable();
            bool val = false;
            string strQry = string.Empty;
            AccessData detAccess = new AccessData();

            try
            {
                string jodstatus = string.Empty;
                strQry = "update custom_parcel_data_layer set APN= '" + apn + "',fips = '" + fips + "',SERVING_WIRE_CENTER_CLLI = '" + SERVING_WIRE_CENTER_CLLI + "',SERVING_WIRE_CENTER_NAME = '" + SERVING_WIRE_CENTER_NAME + "',min_x = '" + min_X + "', min_y = '" + min_y + "',max_x = '" + max_X + "',max_y = '" + max_y + "',remarks = '" + remarks + "',create_date_time = sysdate where objectid = '" + objectid + "'";
                val = detAccess.ExecuteQueryForInsert(strQry);
            }
            catch (Exception ex)
            {
                AccessData detAccess1 = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "UpdateCopperParcel");
                throw ex;
            }
            return val;
        }
        public string GetAddressDetails(string desig_id)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                string strqery = "select address,subaddress,designation,ndsno from swgisloc.odin_test where designation ='" + desig_id + "'";
                // string strqery = "select address,subaddress,designation,ndsno from swgisloc.odin_test where rownum=1";

                DataSet userSet = detAccess.ExecuteQuery(strqery);
                userTable = userSet.Tables[0];

                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetAddressDetails");
                throw ex;
            }
            return jsonstring;
        }
    }
}
