using DataAccessLogicLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer
{
    public class CopperPolygon
    {
        public string GetstreetDrp(string WCID, string Job_id)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                DataTable dt2 = detAccess.getdataWireCenterID(WCID);
                string wcidClli = dt2.Rows[0][0].ToString();
                string LfacsCLLI = string.Empty;
                if (dt2.Rows[0][2].ToString() == "Qwest") {
                    LfacsCLLI = dt2.Rows[0][0].ToString();
                }
                else
                {
                    LfacsCLLI = dt2.Rows[0][1].ToString();
                }
               
                userTable = detAccess.getdataForStreets(wcidClli, LfacsCLLI, Job_id);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetstreetDrp");
                throw ex;
            }
            return jsonstring;
        } 
        public string GetZipDrp(string WC_CLLI)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                DataTable dt2 = detAccess.getdataWireCenterID(WC_CLLI);
                string wcidClli = dt2.Rows[0][0].ToString();
                userTable = detAccess.getdataForZip(wcidClli);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetZipDrp");
                throw ex;
            }
            return jsonstring;
        }
        public string GetStrreetWitZipDrp(string WC_CLLI, string ZIP_Code)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                DataTable dt2 = detAccess.getdataWireCenterID(WC_CLLI);
                string wcidClli = dt2.Rows[0][0].ToString();
                userTable = detAccess.getdataForStreetforZip(wcidClli, ZIP_Code);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetStrreetWitZipDrp");
                throw ex;
            }
            return jsonstring;
        }
        public string GetAddressWithstreet(string WC_CLLI, string ZIP_Code, string Street_name)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                DataTable dt2 = detAccess.getdataWireCenterID(WC_CLLI);
                string wcidClli = dt2.Rows[0][0].ToString();
                userTable = detAccess.getdataForAddressforstreet(wcidClli, ZIP_Code, Street_name);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetAddressWithstreet");
                throw ex;
            }
            return jsonstring;
        }
        public string GetZipAPNFIPSDrp(string WC_CLLI, string ZIPS)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                DataTable dt2 = detAccess.getdataWireCenterID(WC_CLLI);
                string wcidClli = dt2.Rows[0][0].ToString();
                //string strqery = "select distinct (cl.APN || '-' || cl.FIPS_CODE) as ApnFIPS from swgisloc.CL_PARCELS CL INNER JOIN " +
                //                 " qual_schema.COPPER_SQM CO ON CO.APN = CL.APN and CO.FIPS = CL.FIPS_CODE " +
                //                 " where CO.WC_CLLI = '" + wcidClli + "' and ZIP in (" + ZIPS + ")";

                string strqery = "SELECT /*+ parallel(2) */ distinct c_c.STR " +
                                 " FROM swgisloc.CL_PARCELS c_a, swgisloc.cl_subaddress_point c_c, QUAL_SCHEMA.copper_sqm cs " +
                                 " WHERE c_c.ZIP = " + ZIPS + " AND c_a.APN = cs.APN and c_a.FIPS_CODE = cs.FIPS and " +
                                 " cs.wc_clli = '" + wcidClli + "' and SDO_INSIDE(c_c.shape, c_a.shape) = 'TRUE'";

                DataSet userSet = detAccess.ExecuteQuery(strqery);

                userTable = userSet.Tables[0];
                //userTable = detAccess.getdataForZipAPNFIPS(wcidClli, ZIPS);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetZipAPNFIPSDrp");
                throw ex;
            }
            return jsonstring;
        }

        public string GetADDRESSDrp(string WC_CLLI, string ZIPS,string streetName)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                DataTable dt2 = detAccess.getdataWireCenterID(WC_CLLI);
                string wcidClli = dt2.Rows[0][0].ToString();
                //string strqery = "select distinct (cl.APN || '-' || cl.FIPS_CODE) as ApnFIPS from swgisloc.CL_PARCELS CL INNER JOIN " +
                //                 " qual_schema.COPPER_SQM CO ON CO.APN = CL.APN and CO.FIPS = CL.FIPS_CODE " +
                //                 " where CO.WC_CLLI = '" + wcidClli + "' and ZIP in (" + ZIPS + ")";

                string strqery = "select  distinct (cs.UNIT_DES || ' | ' || cs.UNIT_NUM) as ADDR from swgisloc.cl_subaddress_point cs, swgisloc.CL_PARCELS cp, " +
                                 " QUAL_SCHEMA.copper_sqm cc where cs.ZIP = cp.ZIP AND cp.APN = cc.APN and cp.FIPS_CODE = cc.FIPS and cc.wc_clli = '" + wcidClli + "' and " +
                                 " cs.STR in(" + streetName + ") and cp.ZIP = " + ZIPS + " and cs.unit_des != ' '";

                DataSet userSet = detAccess.ExecuteQuery(strqery);

                userTable = userSet.Tables[0];
                //userTable = detAccess.getdataForZipAPNFIPS(wcidClli, ZIPS);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetADDRESSDrp");
                throw ex;
            }
            return jsonstring;
        }
        public string GetAPNFIPSDrp(string WC_CLLI, string ZIPS, string streetName, string units_num, string unit_Des)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                DataTable dt2 = detAccess.getdataWireCenterID(WC_CLLI);
                string wcidClli = dt2.Rows[0][0].ToString();
                //string strqery = "select distinct (cl.APN || '-' || cl.FIPS_CODE) as ApnFIPS from swgisloc.CL_PARCELS CL INNER JOIN " +
                //                 " qual_schema.COPPER_SQM CO ON CO.APN = CL.APN and CO.FIPS = CL.FIPS_CODE " +
                //                 " where CO.WC_CLLI = '" + wcidClli + "' and ZIP in (" + ZIPS + ")";
                string strquery;
                if (units_num != null && unit_Des != null) {
                    strquery = "SELECT /*+ parallel(2) */ distinct (cs.APN || '-' || cs.FIPS) as APNFIPS FROM  swgisloc.cl_subaddress_point c_c, QUAL_SCHEMA.copper_sqm cs " +
                               " WHERE c_c.ZIP = " + ZIPS + "   and cs.wc_clli = '" + wcidClli + "' and c_c.STR in (" + streetName + ") and UNIT_NUM in (" + units_num + ") and UNIT_DES in (" + unit_Des + ")   " +
                               " and SDO_INSIDE(c_c.shape, cs.shape) = 'TRUE'";
                }
                else
                {
                    strquery = "SELECT /*+ parallel(2) */ distinct (cs.APN || '-' || cs.FIPS) as APNFIPS FROM  swgisloc.cl_subaddress_point c_c, QUAL_SCHEMA.copper_sqm cs " +
                               " WHERE c_c.ZIP = '80503'  and cs.wc_clli = '" + wcidClli + "' and c_c.STR in (" + streetName + ") " +
                               " and SDO_INSIDE(c_c.shape, cs.shape) = 'TRUE'";
                }
                

                DataSet userSet = detAccess.ExecuteQuery(strquery);

                userTable = userSet.Tables[0];
                //userTable = detAccess.getdataForZipAPNFIPS(wcidClli, ZIPS);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetAPNFIPSDrp");
                throw ex;
            }
            return jsonstring;
        }
        public string GetPolygonDataList(string WirecenterID,string toolname, string User_id, string Job_ID, string street_name)
        {
            string jsonstring = string.Empty;
            List<string> existUsers1 = new List<string>();


            try
            {
                
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //DataTable dt2 = detAccess.getdataWireCenterID(WirecenterID);
                //string wcidClli = dt2.Rows[0][0].ToString();
                //detAccess.UpdateCopperPolyStatus("2", "Test", "206");
                userTable = detAccess.getdataCopperPolylist(WirecenterID, User_id, Job_ID, toolname, street_name);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);


            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetPolygonDataList");
                throw ex;
            }
            return jsonstring;
        }
        public string GetPolygonDataSearch(string WirecenterID, string street_name)
        {
            string jsonstring = string.Empty;
            List<string> existUsers1 = new List<string>();


            try
            {

                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //detAccess.UpdateCopperPolyStatus("2", "Test", "206");
                DataTable dt2 = detAccess.getdataWireCenterID(WirecenterID);
                string wcidClli = dt2.Rows[0][0].ToString();
                userTable = detAccess.getdataCopperPolySearch(wcidClli, street_name);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetPolygonDataSearch");
                throw ex;
            }
            return jsonstring;
        }
        public string GetAPNFIPSDataSearch(string WirecenterID)
        {
            string jsonstring = string.Empty;
            List<string> existUsers1 = new List<string>();


            try
            {

                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //detAccess.UpdateCopperPolyStatus("2", "Test", "206");
                DataTable dt2 = detAccess.getdataWireCenterID(WirecenterID);
                string wcidClli = dt2.Rows[0][0].ToString();
                userTable = detAccess.getdataAPNFIPS(wcidClli);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetAPNFIPSDataSearch");
                throw ex;
            }
            return jsonstring;
        }
        public Boolean UpdatingReviewCopper(string PolygonId, string Remarks, string Wirecenter_id, string status_id, string review_date, string toolname,string base64)
        {
            bool val = false;
            try
            {

                string[] desarray = PolygonId.Split(',');
                AccessData detAccess = new AccessData();
                
                for (int i = 0; i < desarray.Length; i++)
                {
                    ////strQry = "update USER_ROLE set ROLE_ID='" + Role_id + "' where USER_ID = '" + user_ID + "' and WC_ID = '" + Wirecenter_id + "'";
                    val = detAccess.UpdateCopperPolyStatus(desarray[i], Remarks, status_id, review_date, Wirecenter_id);
                    if (toolname == "Review" && base64 != "" && val == true)
                    {
                        val = detAccess.InsertReviewCopperImageData(desarray[i], Wirecenter_id, base64);
                    }
                }
            }

            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "UpdatingReviewCopper");
                throw ex;
            }
            return val;
        }
        public string GetServiceCopperPolygonDetails(string wire_center_id, string Job_ID, string QualStatusID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //detAccess.UpdateCopperPolyStatus("2", "Test", "206");
                userTable = detAccess.GetServiceCopperPolygonDetails(wire_center_id, Job_ID, QualStatusID);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);

            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetServiceCopperPolygonDetails");
                throw ex;
            }
            return jsonstring;
        }
        public string GetServiceCopperPolygonArchiveDetails(string APN, string FIPS, string Job_ID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //detAccess.UpdateCopperPolyStatus("2", "Test", "206");
                userTable = detAccess.GetServiceCopperPolygonArchiveDetails(APN, FIPS, Job_ID);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);

            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetServiceCopperPolygonArchiveDetails");
                throw ex;
            }
            return jsonstring;
        }
        public string GetCopperManulDrp(string WCID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                DataTable dt2 = detAccess.getdataWireCenterID(WCID);
                string wcidClli = dt2.Rows[0][0].ToString();
                userTable = detAccess.getdataForCopperManual(wcidClli);
                jsonstring = Utilities.ConvertDataTableToJSNString(userTable);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetCopperManulDrp");
                throw ex;
            }
            return jsonstring;
        }
        public string GetWCCLLI(string WCID)
        {
            string jsonstring = string.Empty;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable dt2 = detAccess.getdataWireCenterID(WCID);
                jsonstring = Utilities.ConvertDataTableToJSNString(dt2);
            }
            catch (Exception ex)
            {
                AccessData detAccess = new AccessData();
                detAccess.RecordLogs(ex.Message.ToString(), "GetWCCLLI");
                throw ex;
            }
            return jsonstring;
        }
        public string GetJobReportCopperImage(string JobID, string Designation_ID, string Table_name, string ColumnName)
        {
            string jsonstring = string.Empty;
            string strQry;
            try
            {
                AccessData detAccess = new AccessData();
                DataTable userTable = new DataTable();
                //Utilities Util = new Utilities();
                string[] desarray = Designation_ID.Split('-');
                //string strqery = "select FILE_IMAGE from  qual_schema.REVIEW_DETAILS where JOB_ID = '2887' and DESIGNATION_ID ='LD11221W26'";
                string strqery = "select FILE_IMAGE from  qual_schema." + Table_name + " where JOB_ID = '" + JobID + "' and APN ='" + desarray[0] + "' and FIPS = '" + desarray[1] + "'";
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
                detAccess.RecordLogs(ex.Message.ToString(), "GetJobReportCopperImage");
                throw ex;
            }
            return jsonstring;
        }
    }
}
