using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Configuration;

namespace DataAccessLogicLayer
{
    public class AccessData
    {
        OracleConnection UNIQUALDB = new OracleConnection(ConfigurationManager.ConnectionStrings["UNIQUALDB"].ConnectionString);
        OracleConnection SWGISLOCDB = new OracleConnection(ConfigurationManager.ConnectionStrings["SWGISLOCDB"].ConnectionString);//connection
        OracleConnection UNIQUALDBDev = new OracleConnection(ConfigurationManager.ConnectionStrings["UNIQUALDBDev"].ConnectionString);
        //NpgsqlConnection UksdiAdmin = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Uksdiconnection"].ConnectionString);
        public string toggleConnection()
        {
            try
            {
                if (UNIQUALDB.State == ConnectionState.Closed)
                    UNIQUALDB.Open();
                else
                    UNIQUALDB.Close();
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return UNIQUALDB.State.ToString();
        }
        public string toggleConnectionDev()
        {
            try
            {
                if (UNIQUALDBDev.State == ConnectionState.Closed)
                    UNIQUALDBDev.Open();
                else
                    UNIQUALDBDev.Close();
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return UNIQUALDBDev.State.ToString();
        }
        public string toggleConnectionSWGISLOC()
        {
            try
            {
                if (SWGISLOCDB.State == ConnectionState.Closed)
                    SWGISLOCDB.Open();
                else
                    SWGISLOCDB.Close();
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return SWGISLOCDB.State.ToString();
        }
        public DataSet ExecuteQuery(string strQry)
        {
            string connStatus = toggleConnection();
            DataSet dsQry = new DataSet();
            try
            {
                if (connStatus.ToUpper().Equals("OPEN"))
                {
                    OracleDataAdapter adpConn = new OracleDataAdapter(strQry, UNIQUALDB);
                    adpConn.Fill(dsQry);


                }

            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine("Error reading from {0}. Message = {1}", e.Message);

            }
            finally
            {
                toggleConnection();
            }
            return dsQry;

        }

        public Boolean ExecuteQueryForInsert(string strQry)
        {
            Boolean val = false;
            try
            {
                string connStatus = toggleConnection();
                if (connStatus.ToUpper() == "OPEN")
                {
                    OracleCommand npgcmd = new OracleCommand(strQry, UNIQUALDB);
                    int i = npgcmd.ExecuteNonQuery();

                    if (i == 0)
                        val = false;
                    else
                    {
                        val = true;
                    }
                }
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                toggleConnection();
            }

            return val;
        }
        public DataSet ExecuteQuerySWGISLOC(string strQry)
        {
            string connStatus = toggleConnectionSWGISLOC();
            DataSet dsQry = new DataSet();
            try
            {
                if (connStatus.ToUpper().Equals("OPEN"))
                {
                    OracleDataAdapter adpConn = new OracleDataAdapter(strQry, SWGISLOCDB);
                    adpConn.Fill(dsQry);


                }

            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine("Error reading from {0}. Message = {1}", e.Message);

            }
            finally
            {
                toggleConnectionSWGISLOC();
            }
            return dsQry;

        }

        public Boolean ExecuteQueryForInsertSWGISLOC(string strQry)
        {
            Boolean val = false;
            try
            {
                string connStatus = toggleConnectionSWGISLOC();
                if (connStatus.ToUpper() == "OPEN")
                {
                    OracleCommand npgcmd = new OracleCommand(strQry, SWGISLOCDB);
                    int i = npgcmd.ExecuteNonQuery();

                    if (i == 0)
                        val = false;
                    else
                    {
                        val = true;
                    }
                }
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                toggleConnectionSWGISLOC();
            }

            return val;
        }
        public Boolean verifyDatatableRowsCount(string strQry)
        {
            Boolean val = false;
            string connStatus = toggleConnection();
            DataSet dsQry = new DataSet();
            if (connStatus.ToUpper() == "OPEN")
            {
                OracleDataAdapter adpConn = new OracleDataAdapter(strQry, UNIQUALDB);
                adpConn.Fill(dsQry);
            }
            toggleConnection();

            int i = dsQry.Tables[0].Rows.Count;

            if (i == 0)
                val = false;
            else if (i >= 1)
            {
                val = true;
            }
            return val;
        }
        //public string toggleConnectionUKSDI()
        //{
        //    try
        //    {
        //        if (UksdiAdmin.State == ConnectionState.Closed)
        //            UksdiAdmin.Open();
        //        else
        //            ErdasapolloUksdiAdmin.Close();
        //    }
        //    catch (System.IO.IOException e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    return UksdiAdmin.State.ToString();
        //}

        //public DataSet ExecuteQueryUKSDI(string strQry)
        //{
        //    string connStatus = toggleConnectionUKSDI();
        //    DataSet dsQry = new DataSet();
        //    try
        //    {
        //        if (connStatus.ToUpper().Equals("OPEN"))
        //        {
        //            NpgsqlDataAdapter adpConn = new NpgsqlDataAdapter(strQry, UksdiAdmin);
        //            adpConn.Fill(dsQry);


        //        }

        //    }
        //    catch (System.IO.IOException e)
        //    {
        //        Console.WriteLine("Error reading from {0}. Message = {1}", e.Message);

        //    }
        //    finally
        //    {
        //        toggleConnectionUKSDI();
        //    }
        //    return dsQry;

        //}

        //public Boolean ExecuteQueryForInsertUKSDI(string strQry)
        //{
        //    Boolean val = false;
        //    try
        //    {
        //        string connStatus = toggleConnectionUKSDI();
        //        if (connStatus.ToUpper() == "OPEN")
        //        {
        //            NpgsqlCommand npgcmd = new NpgsqlCommand(strQry, UksdiAdmin);
        //            int i = npgcmd.ExecuteNonQuery();

        //            if (i == 0)
        //                val = false;
        //            else
        //            {
        //                val = true;
        //            }
        //        }
        //    }
        //    catch (System.IO.IOException e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    finally
        //    {
        //        toggleConnectionUKSDI();
        //    }

        //    return val;
        //}
        public Boolean InsertReviewImageData(string User_id, string wirecenter, string designation, string qual_type_id, string polygonID, string status, string base64, string review, string CDateTime, string qual_status)
        {
            //use filestream object to read the image. 
            //read to the full length of image to a byte array. 
            bool val = false;
            //add this byte as an oracle parameter and insert it into database. 
            try
            {
                //proceed only when the image has a valid path 
                //a byte array to read the image 
                //byte[] blob = new byte[fls.Length];
                //fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));
                //fls.Close();
                ////open the database using odp.net and insert the data 
                //connstr = "User Id="userid";Password="password";Data Source="datasource";";
                //conn = new OracleConnection(connstr);
                //conn.Open();

                string connStatus = toggleConnection();
                if (connStatus.ToUpper() == "OPEN")
                {
                    OracleCommand cmnd;
                    string query = string.Empty;
                    //query = "insert into emp(id,name,photo) values(" + txtid.Text + "," + "'" + txtname.Text + "'," + " :BlobParameter )";
                    //query = "insert into review_details(user_id, wire_cent_id, designation_id, qual_type_id, polygon_id,REMARKS, status, review_date,FILE_IMAGE) VALUES('" + User_id + "', '" + wirecenter + "', '" + designation + "', '" + qual_type_id + "', '" + polygonID + "', '" + review + "', '" + status + "', sysdate," + " :BlobParameter )";
                    if (qual_status == "1" || qual_status == "2")
                    {
                        string[] NDSJobNumber = designation.Split('|');
                        string JobNum = designation;
                        string JobNum2 = string.Empty;
                        if (NDSJobNumber.Length > 1)
                        {
                            int ind = JobNum.LastIndexOf('|');
                            JobNum2 = JobNum.Substring(ind + 1);
                            JobNum = JobNum.Substring(0, ind);
                        }
                        if (status == "207")
                        {
                            if (NDSJobNumber.Length == 1)
                            {
                                query = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS'),FILE_IMAGE = :BlobParameter where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum + "'  and QUAL_STATUS_ID = '" + qual_status + "'";
                            }
                            else
                            {
                                query = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS'),FILE_IMAGE = :BlobParameter where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum + "'  and QUAL_STATUS_ID = '" + qual_status + "' and NDS_JNO = '" + JobNum2 + "'";
                            }
                        }
                        else
                        {
                            if (NDSJobNumber.Length == 1)
                            {
                                query = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS'),FILE_IMAGE = :BlobParameter where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum + "'  and QUAL_STATUS_ID = '" + qual_status + "'";
                            }
                            else
                            {
                                query = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS'),FILE_IMAGE = :BlobParameter where WC_ID= '" + wirecenter + "' and designation_id= '" + JobNum + "'  and QUAL_STATUS_ID = '" + qual_status + "' and NDS_JNO = '" + JobNum2 + "'";
                            }
                        }
                    }
                    //else if (qual_status == "2")
                    //{
                    //    if (status == "207")
                    //    {
                    //        string[] NDSJobNumber = designation.Split('-');
                    //        query = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS'),FILE_IMAGE = :BlobParameter where WC_ID= '" + wirecenter + "' and designation_id= '" + designation + "'";

                    //    }
                    //    else
                    //    {
                    //        string[] NDSJobNumber = designation.Split('-');
                    //        query = "update review_details set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS'),FILE_IMAGE = :BlobParameter where WC_ID= '" + wirecenter + "' and designation_id= '" + designation + "'  and QUAL_STATUS_ID = '" + qual_status + "' and NDS_JNO = '" + NDSJobNumber[1] + "'";

                    //    }
                    //}
                    else
                    {
                        query = "update REVIEW_COPPER set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS'),FILE_IMAGE = :BlobParameter where WC_ID= '" + wirecenter + "' and designation_id= '" + designation + "'";
                    }
                    //byte[] decodedByte = Base64.decode(yourBase64String, 0);
                    string[] strArray = base64.Split(',');
                    byte[] imageBytes = Convert.FromBase64String(strArray[1]);
                    //insert the byte as oracle parameter of type blob 
                    OracleParameter blobParameter = new OracleParameter();
                    blobParameter.OracleDbType = OracleDbType.Blob;
                    blobParameter.ParameterName = "BlobParameter";
                    blobParameter.Value = imageBytes;
                    cmnd = new OracleCommand(query, UNIQUALDB);
                    cmnd.Parameters.Add(blobParameter);

                    int i = cmnd.ExecuteNonQuery();

                    if (i == 0)
                        val = false;
                    else
                    {
                        val = true;
                    }
                    cmnd.Dispose();
                }
                toggleConnection();

            }
            catch (Exception ex)
            {

            }
            return val;
        }
        public Boolean InsertReviewImageDataNoB(string User_id, string wirecenter, string review, string polygonID, string status, string CDateTime, string base64)
        {
            //use filestream object to read the image. 
            //read to the full length of image to a byte array. 
            bool val = false;
            //add this byte as an oracle parameter and insert it into database. 
            try
            {
                //proceed only when the image has a valid path 
                //a byte array to read the image 
                //byte[] blob = new byte[fls.Length];
                //fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));
                //fls.Close();
                ////open the database using odp.net and insert the data 
                //connstr = "User Id="userid";Password="password";Data Source="datasource";";
                //conn = new OracleConnection(connstr);
                //conn.Open();

                string connStatus = toggleConnection();
                if (connStatus.ToUpper() == "OPEN")
                {
                    OracleCommand cmnd;
                    string query = string.Empty;
                    //query = "insert into emp(id,name,photo) values(" + txtid.Text + "," + "'" + txtname.Text + "'," + " :BlobParameter )";
                    //query = "insert into review_details(user_id, wire_cent_id, designation_id, qual_type_id, polygon_id,REMARKS, status, review_date,FILE_IMAGE) VALUES('" + User_id + "', '" + wirecenter + "', '" + designation + "', '" + qual_type_id + "', '" + polygonID + "', '" + review + "', '" + status + "', sysdate," + " :BlobParameter )";

                    if (status == "207")
                    {
                        query = "update REVIEW_NOBUILD set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS'),FILE_IMAGE = :BlobParameter where WC_ID= '" + wirecenter + "'";
                    }
                    else
                    {
                        query = "update REVIEW_NOBUILD set REMARKS = '" + review + "',status_id = '" + status + "',review_date = TO_DATE('" + CDateTime + "', 'MM/DD/YYYY HH24:MI:SS'),FILE_IMAGE = :BlobParameter where WC_ID= '" + wirecenter + "' and POLYGON_ID= '" + polygonID + "'";
                    }
                    //byte[] decodedByte = Base64.decode(yourBase64String, 0);
                    string[] strArray = base64.Split(',');
                    byte[] imageBytes = Convert.FromBase64String(strArray[1]);
                    //insert the byte as oracle parameter of type blob 
                    OracleParameter blobParameter = new OracleParameter();
                    blobParameter.OracleDbType = OracleDbType.Blob;
                    blobParameter.ParameterName = "BlobParameter";
                    blobParameter.Value = imageBytes;
                    cmnd = new OracleCommand(query, UNIQUALDB);
                    cmnd.Parameters.Add(blobParameter);

                    int i = cmnd.ExecuteNonQuery();

                    if (i == 0)
                        val = false;
                    else
                    {
                        val = true;
                    }
                    cmnd.Dispose();
                }
                toggleConnection();

            }
            catch (Exception ex)
            {

            }
            return val;
        }

        public Boolean InsertReviewCopperImageData(string polygonID, string wirecenter, string base64)
        {
            bool val = false;
            try
            {
                string connStatus = toggleConnection();
                if (connStatus.ToUpper() == "OPEN")
                {
                    string[] desarray = polygonID.Split('-');
                    OracleCommand cmnd;
                    string query = string.Empty;
                    query = "update REVIEW_COPPER set FILE_IMAGE = :BlobParameter where WC_ID= '" + wirecenter + "' and APN= '" + desarray[0] + "' and FIPS = '" + desarray[1] + "'";
                    //byte[] decodedByte = Base64.decode(yourBase64String, 0);
                    string[] strArray = base64.Split(',');
                    byte[] imageBytes = Convert.FromBase64String(strArray[1]);
                    //insert the byte as oracle parameter of type blob 
                    OracleParameter blobParameter = new OracleParameter();
                    blobParameter.OracleDbType = OracleDbType.Blob;
                    blobParameter.ParameterName = "BlobParameter";
                    blobParameter.Value = imageBytes;
                    cmnd = new OracleCommand(query, UNIQUALDB);
                    cmnd.Parameters.Add(blobParameter);

                    int i = cmnd.ExecuteNonQuery();

                    if (i == 0)
                        val = false;
                    else
                    {
                        val = true;
                    }
                    cmnd.Dispose();
                }
                toggleConnection();

            }
            catch (Exception ex)
            {

            }
            return val;
        }
        public DataTable getdataForStreets(string WIreCenteID, string LFACSCLLI, string Job_ID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_STREET_NAME", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("wc_ids", OracleDbType.Varchar2, WIreCenteID, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("Lfacwc_ids", OracleDbType.Varchar2, LFACSCLLI, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("v_job_id", OracleDbType.Varchar2, Job_ID, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public Boolean GetBIWFENGB(string Objectid, string user_id)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_BIWF_INSERT_BIW", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_objectid", OracleDbType.Varchar2, Objectid, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_userid", OracleDbType.Varchar2, user_id, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                //OracleDataReader objReader = _oracleCommand.ExecuteReader();
                //if (objReader.HasRows)
                //{
                //    dt.Load(objReader);
                //}
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public DataTable GetUserList(string RoleID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("Get_User_DropList", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("Role_id", OracleDbType.Varchar2, RoleID, ParameterDirection.Input);

                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("User_Detalis", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public DataTable GetWireCenterList(string ToolName)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("Get_WireCenter_DropList", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("ToolName", OracleDbType.Varchar2, ToolName, ParameterDirection.Input);

                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("User_Detalis", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public DataTable GetRoleList(string RoleID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("Get_role_DropDown", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("Role_id", OracleDbType.Varchar2, RoleID, ParameterDirection.Input);

                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("User_Detalis", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public DataTable GetAssignedRoleDetails(string RoleID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("Get_role_details", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("Role_id", OracleDbType.Varchar2, RoleID, ParameterDirection.Input);

                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("User_Detalis", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public Boolean BIWFENGBDelete(string Objectid)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("BIWF_DELETE_BIW", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_objectid", OracleDbType.Varchar2, Objectid, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                //OracleDataReader objReader = _oracleCommand.ExecuteReader();
                //if (objReader.HasRows)
                //{
                //    dt.Load(objReader);
                //}
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public Boolean UpdateBIWFENGB(string Objectid, string user_id)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("BIWF_UPDATE_BIWODS", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_objectid", OracleDbType.Varchar2, Objectid, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_userid", OracleDbType.Varchar2, user_id, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                //OracleDataReader objReader = _oracleCommand.ExecuteReader();
                //if (objReader.HasRows)
                //{
                //    dt.Load(objReader);
                //}
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public Boolean UpdateParllelPolyDetails(string Objectid, string user_id, string LayerID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("UPDATE_PARLLEL_POLYGON", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_objectid", OracleDbType.Varchar2, Objectid, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_LayName", OracleDbType.Varchar2, LayerID, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_userid", OracleDbType.Varchar2, user_id, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                //OracleDataReader objReader = _oracleCommand.ExecuteReader();
                //if (objReader.HasRows)
                //{
                //    dt.Load(objReader);
                //}
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public Boolean UpdateParllelPolyLockedBy(string Objectid, string user_id, string LayerID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("UPDATE_PARLLEL_POLYGON_LOCKED_BY", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_objectid", OracleDbType.Varchar2, Objectid, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_LayName", OracleDbType.Varchar2, LayerID, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_userid", OracleDbType.Varchar2, user_id, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                //OracleDataReader objReader = _oracleCommand.ExecuteReader();
                //if (objReader.HasRows)
                //{
                //    dt.Load(objReader);
                //}
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public Boolean UpdateParllelPolyLockedStatus(string ObjectID, string LayerID, string user_id)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("UPDATE_PARLLEL_LOCKEDSTATUS", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_objectid", OracleDbType.Varchar2, ObjectID, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_LayName", OracleDbType.Varchar2, LayerID, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_userid", OracleDbType.Varchar2, user_id, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                //OracleDataReader objReader = _oracleCommand.ExecuteReader();
                //if (objReader.HasRows)
                //{
                //    dt.Load(objReader);
                //}
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public Boolean WorkType_Details_Update(string ObjectID, string User_Name, string WorkType, string Remarks)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_WORKTYPE_UPDATE", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_objectid", OracleDbType.Varchar2, ObjectID, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_username", OracleDbType.Varchar2, User_Name, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_worktype", OracleDbType.Varchar2, WorkType, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_remarks", OracleDbType.Varchar2, Remarks, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                //OracleDataReader objReader = _oracleCommand.ExecuteReader();
                //if (objReader.HasRows)
                //{
                //    dt.Load(objReader);
                //}
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public Boolean UserProfile_Details_Update(string ObjectID, string username, string Layer_List, string Default_BM, string BookMark_Name)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("USER_PROFILE_UPDATE", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_objectid", OracleDbType.Varchar2, ObjectID, ParameterDirection.Input); 
                _oracleCommand.Parameters.Add("p_username", OracleDbType.Varchar2, username, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_layerlist", OracleDbType.Varchar2, Layer_List, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_default", OracleDbType.Varchar2, Default_BM, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_Bookmark_Name", OracleDbType.Varchar2, BookMark_Name, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                //OracleDataReader objReader = _oracleCommand.ExecuteReader();
                //if (objReader.HasRows)
                //{
                //    dt.Load(objReader);
                //}
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public Boolean UserProfile_DEFAULT_Update(string ObjectID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("USER_PROFILE_DF_RESET_UPDATE", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_objectid", OracleDbType.Varchar2, ObjectID, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                //OracleDataReader objReader = _oracleCommand.ExecuteReader();
                //if (objReader.HasRows)
                //{
                //    dt.Load(objReader);
                //}
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public Boolean WorkType_Details_Delete(string ObjectID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_WORKTYPE_DELETE", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_objectid", OracleDbType.Varchar2, ObjectID, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public Boolean UserProfile_Details_Delete(string ObjectID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("USER_PROFILE_DELETE", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_objectid", OracleDbType.Varchar2, ObjectID, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public Boolean WorkType_Details_Insert(string WorkType,string User_Name, string Remarks)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_WORKTYPE_Insert", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_worktype", OracleDbType.Varchar2, WorkType, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_username", OracleDbType.Varchar2, User_Name, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_remarks", OracleDbType.Varchar2, Remarks, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                //OracleDataReader objReader = _oracleCommand.ExecuteReader();
                //if (objReader.HasRows)
                //{
                //    dt.Load(objReader);
                //}
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public Boolean User_Profile_Insert(string username, string Layer_List, string Default_BM,string BookMark_Name)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("USER_PROFILE_Insert", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_username", OracleDbType.Varchar2, username, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_layerlist", OracleDbType.Varchar2, Layer_List, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_default", OracleDbType.Varchar2, Default_BM, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_Bookmark_Name", OracleDbType.Varchar2, BookMark_Name, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public Boolean BIWF_Insert_ODS_INERVICE(string desgination, string Nds_job, string statuIN, string CLLI)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("BIWF_Insert_ODS_INERVICE", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_designation", OracleDbType.Varchar2, desgination, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_nds_des", OracleDbType.Varchar2, Nds_job, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_status", OracleDbType.Varchar2, statuIN, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_CLLI", OracleDbType.Varchar2, CLLI, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                //OracleDataReader objReader = _oracleCommand.ExecuteReader();
                //if (objReader.HasRows)
                //{
                //    dt.Load(objReader);
                //}
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public Boolean BIWF_Insert_ODS_INCONSTRUCTION(string desgination, string Nds_job, string statuIN, string CLLI)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("BIWF_Insert_ODS_INCONSTRUCTION", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_designation", OracleDbType.Varchar2, desgination, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_nds_des", OracleDbType.Varchar2, Nds_job, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_status", OracleDbType.Varchar2, statuIN, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_CLLI", OracleDbType.Varchar2, CLLI, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                //OracleDataReader objReader = _oracleCommand.ExecuteReader();
                //if (objReader.HasRows)
                //{
                //    dt.Load(objReader);
                //}
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public Boolean AssignUserRole(string user_id, string role_id, string assignedby, string eng_Boundary)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("ASSIGN_ROLE_TO_USER", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("user_id", OracleDbType.Varchar2, user_id, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("Role_id", OracleDbType.Varchar2, role_id, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("Assigned_By", OracleDbType.Varchar2, assignedby, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("Eng_Boun_val", OracleDbType.Varchar2, eng_Boundary, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("p_userid", OracleDbType.Varchar2, eng_Boundary, ParameterDirection.Input);
                //  _oracleCommand.Parameters.Add("p_objectid", OracleDbType.Varchar2, Objectid, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                //OracleDataReader objReader = _oracleCommand.ExecuteReader();
                //if (objReader.HasRows)
                //{
                //    dt.Load(objReader);
                //}
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public Boolean DeleteUserRole(string objectid, string assignedby)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            Boolean val = false;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("Delete_ROLE_TO_USER", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("P_object_ID", OracleDbType.Varchar2, objectid, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("AssignedBy", OracleDbType.Varchar2, assignedby, ParameterDirection.Input);
                _oracleCommand.ExecuteNonQuery();
                val = true;
                return val;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public DataTable GetBIW_Boundary_Check(string object_id)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_BIW_BOUNDARY_CHECK", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_objectid", OracleDbType.Varchar2, object_id, ParameterDirection.Input);

                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("BIW_Boundary", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public DataTable Get_ADDR(string ADDR)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_ADDR_DETAILS", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_addr", OracleDbType.Varchar2, ADDR, ParameterDirection.Input);

                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("ADDR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public DataTable Get_Polygon_Search(string Col_name, string Col_Val)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand(Col_name, UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_Col_name", OracleDbType.Varchar2, Col_name, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_Col_Val", OracleDbType.Varchar2, Col_Val, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("ADDR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public DataTable getdataForZip(string WC_CLLI)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_ZIP_CODE", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("WC_CCLI_V", OracleDbType.Varchar2, WC_CLLI, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }

        public DataTable getdataForStreetforZip(string WC_CLLI, string ZIP)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_STREET_NAME_BY_ZIP", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("wc_ids_V", OracleDbType.Varchar2, WC_CLLI, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("v_ZIP", OracleDbType.Varchar2, ZIP, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("WC_CCLI_V", OracleDbType.Varchar2, WC_CLLI, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }
        }
        public DataTable getdataForAddressforstreet(string WC_CLLI, string ZIP, string StreetName)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_Address_street", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("wc_ids_V", OracleDbType.Varchar2, WC_CLLI, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("v_ZIP", OracleDbType.Varchar2, ZIP, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("v_StreetName", OracleDbType.Varchar2, StreetName, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("WC_CCLI_V", OracleDbType.Varchar2, WC_CLLI, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }
        }
        public DataTable getdataForZipAPNFIPS(string WC_CLLI, string ZIPS)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_ZIP_APNFIPS_CODE", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("WC_CCLI_V", OracleDbType.Varchar2, WC_CLLI, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("ZIP_V", OracleDbType.Varchar2, ZIPS, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public DataTable getdataForCopperManual(string WIreCenteID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("Get_CopperManualPolygon", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("WC_CLLI", OracleDbType.Varchar2, WIreCenteID, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("CopperPolygon", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public DataTable getdataWireCenterID(string WIreCenteID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_WireCenterID", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("WC_CLLI", OracleDbType.Varchar2, WIreCenteID, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("WireCenterID", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }

        public DataTable getdataCopperPolylist(string WIreCenteID, string User_id, string Job_ID, string toolname, string street_name)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("Get_CopperPolygon", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("WC_CLLI", OracleDbType.Varchar2, WIreCenteID, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("ToolName", OracleDbType.Varchar2, toolname, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("V_Street", OracleDbType.Varchar2, street_name, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("v_job_id", OracleDbType.Varchar2, Job_ID, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("CopperPolygon", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public DataTable getdataAPNFIPS(string WIreCenteID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_APNFIPS_COPPER", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("wc_ids", OracleDbType.Varchar2, WIreCenteID, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("CopperPolygon", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public DataTable getdataCopperPolySearch(string WIreCenteID, string street_name)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_STREET_NAME_SEARCH", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("wc_ids", OracleDbType.Varchar2, WIreCenteID, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("V_Street", OracleDbType.Varchar2, street_name, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("CopperPolygon", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public DataTable GetServiceCopperPolygonDetails(string WIreCenteID, string Job_ID, string QualStatusID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_POLYGON_PARCE_INFO", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("v_job_id", OracleDbType.Varchar2, Job_ID, ParameterDirection.Input);

                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("rec_curse", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public DataTable GetServiceCopperPolygonArchiveDetails(string APN, string FIPS, string Job_ID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                string[] desarray = APN.Split('-');
                _oracleCommand = new OracleCommand("GET_PARCE_POLYGON_WORKFLOW_INFO", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("v_APN", OracleDbType.Varchar2, desarray[0], ParameterDirection.Input);
                _oracleCommand.Parameters.Add("V_FIPS", OracleDbType.Varchar2, desarray[1], ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("rec_curse", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public DataTable GetServiceCopperPolygonExceptions_OLD(string WIreCenteID, string Job_ID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_JOB_EXCEPTION_FOR_COPPER", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("V_JOBID", OracleDbType.Varchar2, Job_ID, ParameterDirection.Input);

                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("rec_curse", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public DataTable GetServiceCopperPolygonFailures(string WIreCenteID, string Job_ID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("GET_JOB_FAILURE_INFO_FOR_COPPER", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("V_JOB_ID", OracleDbType.Varchar2, Job_ID, ParameterDirection.Input);

                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("rec_curse", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public Boolean UpdateCopperPolyStatus(string PolygonID, string Remarks, string statusID, string review_date, string WCID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                string[] desarray = PolygonID.Split('-');
                toggleConnection();
                _oracleCommand = new OracleCommand("UpdateReviewCopper", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_Remarks", OracleDbType.Varchar2, Remarks, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("P_APN", OracleDbType.Varchar2, desarray[0], ParameterDirection.Input);
                _oracleCommand.Parameters.Add("P_FIPS", OracleDbType.Varchar2, desarray[1], ParameterDirection.Input);
                _oracleCommand.Parameters.Add("P_Status_ID", OracleDbType.Varchar2, statusID, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("reiview_date", OracleDbType.Varchar2, review_date, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("wirecenterID", OracleDbType.Varchar2, WCID, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("return_value", OracleDbType.Long).Direction = ParameterDirection.ReturnValue;
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("STS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                try
                {
                    _oracleCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
                //var abc = _oracleCommand.Parameters["return_value"].Value;
                //OracleDataReader objReader = _oracleCommand.ExecuteReader();
                //if (objReader.HasRows)
                //{
                //    dt.Load(objReader);
                //}
                //return val.ToString();
            }
            catch (Exception ex)
            {
                return false;
                //throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public DataTable GetWireCenterRoles(string WCID, string Role_ID, string Eng_Bound)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("Get_Role_Check", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("P_CUID", OracleDbType.Varchar2, WCID, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("Role_id", OracleDbType.Varchar2, Role_ID, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("ENG_BOUNDARY", OracleDbType.Varchar2, Eng_Bound, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("User_Detalis", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }

        public DataTable GetParllelPolyCheckDetails(string objectid, string LayerID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("GET_PARLLEL_POLYGON_LOCKED_BY", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_objectid", OracleDbType.Varchar2, objectid, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_LayName", OracleDbType.Varchar2, LayerID, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("ADDR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public DataTable GetLockedFiberPolygonDetails(string user_name, string RoleID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("GET_FIBLOCKED_POLYGON_DETAILS", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_UserName", OracleDbType.Varchar2, user_name, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_roleid", OracleDbType.Varchar2, RoleID, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("ADDR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public DataTable GetLockedNOBUILDPolygonDetails(string user_name, string RoleID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("GET_NOBUILDLOCKED_POLYGON_DETAILS", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_UserName", OracleDbType.Varchar2, user_name, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_roleid", OracleDbType.Varchar2, RoleID, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("ADDR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public DataTable GetLockedPolygonCount(string user_name, string RoleID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("GET_LOCKED_POLYGON_COUNT", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_UserName", OracleDbType.Varchar2, user_name, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_roleid", OracleDbType.Varchar2, RoleID, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("ADDR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public DataTable GetWorkTypeDetails()
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("GET_WORK_TYPE_DETAILS", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("ADDR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public DataTable GetUserProfileDetails(string UserName)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("GET_USER_PROFILE_DETAILS", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_username", OracleDbType.Varchar2, UserName, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("ADDR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public DataTable GetUserProfileDFDetails(string UserName)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("GET_USER_PROFILE_DEFAULT_DETAILS", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_username", OracleDbType.Varchar2, UserName, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("ADDR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public DataTable GetWorkTypeCheck(string Work_type, string objectid,string workstatus)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("GET_WORK_TYPE_Check", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_WorkType", OracleDbType.Varchar2, Work_type, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_objectid", OracleDbType.Varchar2, objectid, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("p_workstatus", OracleDbType.Varchar2, workstatus, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("ADDR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public DataTable GetWorkTypeSQMCheck(string Work_type)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("GET_WORK_TYPE_SQM_Check", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_WorkType", OracleDbType.Varchar2, Work_type, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("ADDR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public DataTable GetLayerListUserProfile(string ObjectID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("GET_LAYER_LIST_USERPROFILE", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("p_objectid", OracleDbType.Varchar2, ObjectID, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("ADDR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public DataTable GetLoginAccessDetails(string UserName)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnectionDev();
                _oracleCommand = new OracleCommand("Get_Login_Details", UNIQUALDBDev);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("P_User_name", OracleDbType.Varchar2, UserName, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("User_Detalis", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnectionDev();
            }

        }
        public DataSet FillDataSet(string storedProcedureName, Dictionary<string, object> parameters = null)
        {
            DataSet dataset = new DataSet();
            string connStatus = toggleConnection();
            if (connStatus.ToUpper() == "OPEN")
            {
                //OracleConnection connection = new OracleConnection(GetConnectionString());
                OracleCommand command = new OracleCommand(storedProcedureName, UNIQUALDB);
                command.CommandTimeout = 20000;

                //Checking whether any parameters are there for stored procedure
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> pair in parameters)
                    {
                        command.Parameters.Add(pair.Key, pair.Value);
                    }
                }
                command.CommandType = CommandType.StoredProcedure;
                OracleDataAdapter dataAdapter = new OracleDataAdapter();
                dataAdapter.SelectCommand = command;
                //Fillint the Datatable.
                dataAdapter.Fill(dataset);
            }
            toggleConnection();
            return dataset;
        }
        public string RecordLogs(string Logtxt, string MethodName)
        {
            string LogFilePath = ConfigurationManager.AppSettings["LogFilePath"];
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@LogFilePath, true))
            {
                file.Write("\r\nLog Entry : ");
                file.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                file.WriteLine("Method:" + MethodName);
                file.WriteLine($"  :{Logtxt}");
                file.WriteLine("-------------------------------");

            }

            return "Done";
        }
        #region Vinay Changes
        public DataTable GetDTJobReportDetails(string varUserRole, string varUserId)
        {
            DataTable dt = new DataTable("dtJobReportDetails");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("JOB_REPORT.GET_JOB_REPORT_DETAILS", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("VARROLEID", OracleDbType.Varchar2, varUserRole, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("VARUSERID", OracleDbType.Varchar2, varUserId, ParameterDirection.Input);

                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("cur_out", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                    dt.Load(objReader);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }
        }

        public DataTable GetServiceFiberPolygonExceptions(string Job_ID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("JOB_REPORT.GET_FIBER_POLYGON_DETAILS", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("VARJOBID", OracleDbType.Varchar2, Job_ID, ParameterDirection.Input);

                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("CUR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }

        public DataTable GetServiceCopperPolygonExceptions(string Job_ID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("JOB_REPORT.GET_COPPER_POLYGON_DETAILS", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("VARJOBID", OracleDbType.Varchar2, Job_ID, ParameterDirection.Input);

                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("CUR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }

        public DataTable GetServicePolygonFailures(string Job_ID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("JOB_REPORT.GET_POLYGON_FAILURE_DETAILS", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("VARJOBID", OracleDbType.Varchar2, Job_ID, ParameterDirection.Input);

                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("CUR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }


        public DataTable GetJobReportImage(string strPlygonId, string strQual)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("JOB_REPORT.GET_JOB_REPORT_IMG", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("VARPOLYGONID", OracleDbType.Varchar2, strPlygonId, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("VARTYPE", OracleDbType.Varchar2, strQual, ParameterDirection.Input);
                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("CUR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                toggleConnection();
            }

        }
        public DataTable GetServiceNoBuildPolygonExceptions(string Job_ID)
        {
            DataTable dt = new DataTable("dtQual");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("JOB_REPORT.GET_NO_BUILD_POLYGON_DETAILS", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("VARJOBID", OracleDbType.Varchar2, Job_ID, ParameterDirection.Input);



                //_oracleCommand.Parameters.Add("fltLON", OracleDbType.BinaryFloat, lon, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("CUR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {



                throw;
            }
            finally
            {
                toggleConnection();
            }



        }
        public DataTable GetodinAddresses(string strTriggerId)
        {
            DataTable dt = new DataTable("dtOdinaddresses");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("IDENTIFY.GET_ODIN_ADDRESSES", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("VARTRIGGERID", OracleDbType.Varchar2, strTriggerId, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("CUR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {





                throw;
            }
            finally
            {
                toggleConnection();
            }





        }



        public DataTable GetodinJson(string strTriggerId)
        {
            DataTable dt = new DataTable("dtOdinaddresses");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("IDENTIFY.GET_ODIN_JSON", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("VARTRIGGERID", OracleDbType.Varchar2, strTriggerId, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("CUR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {





                throw;
            }
            finally
            {
                toggleConnection();
            }





        }




        public DataTable GetCopperAddresses(string strAPN, string strFIPS)
        {
            DataTable dt = new DataTable("dtCopperaddresses");
            OracleCommand _oracleCommand = null;
            try
            {
                toggleConnection();
                _oracleCommand = new OracleCommand("IDENTIFY.GET_COPPER_ADDRESS", UNIQUALDB);
                _oracleCommand.BindByName = true;
                _oracleCommand.CommandType = CommandType.StoredProcedure;
                _oracleCommand.Parameters.Add("VARAPN", OracleDbType.Varchar2, strAPN, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("VARFIPS", OracleDbType.Varchar2, strFIPS, ParameterDirection.Input);
                _oracleCommand.Parameters.Add("CUR_OUT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader objReader = _oracleCommand.ExecuteReader();
                if (objReader.HasRows)
                {
                    dt.Load(objReader);
                }
                return dt;
            }
            catch (Exception ex)
            {





                throw;
            }
            finally
            {
                toggleConnection();
            }
        }
        #endregion

    }
}
