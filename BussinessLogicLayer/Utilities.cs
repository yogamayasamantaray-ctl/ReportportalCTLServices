using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security.Cryptography;
using System.Web.Services;
using MebiusLib;

namespace BussinessLogicLayer
{
    public class Utilities
    {
        public static string ConvertJSN(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }


        public static string ConvertJSN2(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "parea")
                    {

                        row.Add(col.ColumnName, Math.Round(Convert.ToDouble(dr[col]), 2));
                    }
                    else
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        public static string ConvertJSNString(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "parea" || col.ColumnName == "SNo" || col.ColumnName == "area")
                    {

                        row.Add(col.ColumnName, Math.Round(Convert.ToDouble(dr[col]), 2));
                    }
                    else
                    {
                        row.Add(col.ColumnName, Convert.ToString(dr[col]));
                    }
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
        /// <summary>
        /// Convert Data Table to List type
        /// </summary>
        /// <typeparam name="T"> class which needs to perform in ex : clsUser</typeparam>
        /// <param name="dt"> Data Table which needs to perform in</param>
        /// <returns> returns data in the format of list specified class</returns>

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        #region Testing


        //public static List<string> testing(DataTable dt)
        //{
        //    List<string> data = new List<string> { };
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        string objItem = GetObj(row);
        //        data.Add(objItem);
        //    }
        //    return data;
        //}

        //private static string GetObj(DataRow dr)
        //{           
        //    string lstAry = string.Empty;


        //    foreach (DataColumn column in dr.Table.Columns)
        //    {
        //        var objectToSerialize = new layoutBoundryFields();
        //        objectToSerialize.items = new List<layoutBoundryFields> 
        //                  {
        //                     new layoutBoundryFields {  taluk_name = dr.Table.Rows[0][column.ColumnName].ToString(), index = "index1" },
        //                     new layoutBoundryFields { name = "test2", index = "index2" }
        //                  };

        //        lstAry = lstAry + column.ColumnName + ":" + dr.Table.Rows[0][column.ColumnName].ToString()+", ";
        //    }
        //    return lstAry;
        //}

        #endregion

        public static string ConvertDataTable(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = 2147483647;
            // List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();

            Dictionary<string, string> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, string>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
        public static string ConvertDataTableToJSNString(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = 2147483647;
            // List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();
            List<string> lstColumnName = new List<string>();

            string JSONString = "{\"ColuomnName\":[NA],\"Data\":[NA]}";

            Dictionary<string, string> row;
            int i = 0;

            try
            {

                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, string>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (i == 0)
                        {
                            lstColumnName.Add(col.ColumnName);
                        }
                        TypeCode yourTypeCode = Type.GetTypeCode(col.DataType);
                        if (dr[col].ToString() == "" || dr[col] == null)
                        {
                            row.Add(col.ColumnName, "");
                        }
                        else
                        {
                            switch (yourTypeCode)
                            {
                                case TypeCode.Byte:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.SByte:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Int16:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.UInt16:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Int32:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.UInt32:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Int64:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.UInt64:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Single:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Double:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Decimal:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Boolean:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.DateTime:
                                    DateTime date = Convert.ToDateTime(dr[col.ColumnName]);
                                    row.Add(col.ColumnName, Convert.ToString(date.ToShortDateString().ToString()));
                                    break;
                                case TypeCode.String:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Empty:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                default:    // TypeCode.DBNull, TypeCode.Char and TypeCode.Object
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                            }
                        }
                        //row.Add(col.ColuomnName, Convert.ToString(dr[col]));
                    }
                    rows.Add(row);
                    i++;
                }
                JSONString = "{\"ColuomnName\":[" + JsonConvert.SerializeObject(lstColumnName) + "],\"Data\":[" + serializer.Serialize(rows) + "]}";
            }
            catch (Exception)
            {
                JSONString = "{\"ColuomnName\":[NA],\"Data\":[NA]}";
            }
            return JSONString;
        }
        public static string TransposeDataTableToJSNString(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = 2147483647;

            List<string> lstLabels = new List<string>();
            List<string> lstData = new List<string>();
            string JSONString = "{\"Labels\":[NA],\"Data\":[NA]}";

            foreach (DataRow dr in dt.Rows)
            {
                lstLabels.Add(Convert.ToString(dr[0]));
                lstData.Add(Convert.ToString(dr[1]));
            }

            JSONString = "{\"Labels\":[" + JsonConvert.SerializeObject(lstLabels) + "],\"Data\":[" + JsonConvert.SerializeObject(lstData) + "]}";

            return JSONString; ;
        }

        public static string ConvertDataTableToJSNStringasColumns(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = 2147483647;
            // List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();
            List<string> lstColumnName = new List<string>();

            string JSONString = "{\"ColuomnName\":[NA],\"Data\":[NA]}";

            Dictionary<string, string> row;
            int i = 0;

            try
            {

                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, string>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (i == 0)
                        {
                            lstColumnName.Add(col.ColumnName);
                        }
                        TypeCode yourTypeCode = Type.GetTypeCode(col.DataType);
                        if (dr[col].ToString() == "" || dr[col] == null)
                        {
                            row.Add(col.ColumnName, "");
                        }
                        else
                        {
                            switch (yourTypeCode)
                            {
                                case TypeCode.Byte:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.SByte:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Int16:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.UInt16:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Int32:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.UInt32:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Int64:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.UInt64:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Single:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Double:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Decimal:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Boolean:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.DateTime:
                                    DateTime date = Convert.ToDateTime(dr[col.ColumnName]);
                                    row.Add(col.ColumnName, Convert.ToString(date.ToShortDateString().ToString()));
                                    break;
                                case TypeCode.String:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Empty:
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                                default:    // TypeCode.DBNull, TypeCode.Char and TypeCode.Object
                                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                                    break;
                            }
                        }
                        //row.Add(col.ColuomnName, Convert.ToString(dr[col]));
                    }
                    rows.Add(row);
                    i++;
                }
                JSONString = "{\"ColuomnName\":[" + JsonConvert.SerializeObject(lstColumnName) + "],\"Data\":[" + serializer.Serialize(rows) + "]}";
            }
            catch (Exception)
            {
                JSONString = "{\"ColuomnName\":[NA],\"Data\":[NA]}";
            }
            return JSONString;
        }

        /// <summary>
        /// conevert each row object as per req
        /// </summary>
        /// <typeparam name="T"> class which needs to perform in ex: clsUser</typeparam>
        /// <param name="dr"> datarow which needs to perform n</param>
        /// <returns>class object in the user specified way</returns>

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name.ToLower() == column.ColumnName.ToLower())
                        if (!string.IsNullOrWhiteSpace((dr[column.ColumnName]).ToString()))
                        {
                            if (column.ColumnName == "reg_date")
                            {
                                DateTime date = Convert.ToDateTime(dr[column.ColumnName]);

                                pro.SetValue(obj, date.ToShortDateString().ToString(), null);

                            }
                            else if (column.DataType.Name == "DateTime")
                            {
                                DateTime date = Convert.ToDateTime(dr[column.ColumnName]);

                                pro.SetValue(obj, date.ToShortDateString().ToString(), null);
                            }
                            else if (column.ColumnName == "shape_area")
                            {
                                double shape_area = Convert.ToDouble(dr[column.ColumnName]);

                                pro.SetValue(obj, shape_area, null);
                            }
                            else if (column.ColumnName.ToLower() == "key")
                            {
                                if (dr[column.ColumnName] != null)
                                {
                                    string key = Convert.ToString(dr[column.ColumnName]);
                                    pro.SetValue(obj, key, null);
                                }
                            }
                            else
                            {
                                pro.SetValue(obj, dr[column.ColumnName], null);
                            }



                        }
                        else
                        {
                            //pro.SetValue(obj, "NA", null);
                        }
                    else
                        continue;
                }
            }
            return obj;
        }
        //public static string ThumbnailList(DataTable dt)
        //{
        //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //    serializer.MaxJsonLength = 2147483647;
        //    // List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //    //List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();
        //    List<object> rows = new List<object>();

        //    List<string> lstColumnName = new List<string>();


        //    string JSONString = "{\"ColumnName\":[NA],\"Data\":[NA]}";

        //    //  Dictionary<string, string> row;
        //    int i = 0;

        //    try
        //    {


        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            //   row = new Dictionary<string>();
        //            List<string> row1 = new List<string>();

        //            foreach (DataColumn col in dt.Columns)
        //            {
        //                if (i == 0)
        //                {
        //                    lstColumnName.Add(col.ColumnName);
        //                }

        //                if (dr[col].ToString() == "" || dr[col] == null)
        //                {
        //                    //  row1.Add("");

        //                }
        //                else
        //                {
        //                    byte[] binDate = (byte[])dr[col];
        //                    string ThumbNailBase64 = ResizeBase64ImageString(binDate, 500, 500);

        //                    row1.Add(ThumbNailBase64);

        //                }
        //                //row.Add(col.ColuomnName, Convert.ToString(dr[col]));
        //            }
        //            rows.Add(row1);
        //            i++;
        //        }
        //        JSONString = "{\"ColumnName\":" + JsonConvert.SerializeObject(lstColumnName) + ",\"Data\":" + JsonConvert.SerializeObject(rows) + "}";

        //    }
        //    catch (Exception)
        //    {
        //        JSONString = "{\"ColumnName\":[NA],\"Data\":[NA]}";
        //    }



        //    return JSONString;



        //}
        //public static string ResizeBase64ImageString(byte[] imageBytes, int desiredWidth, int desiredHeight)
        //{
        //    // Base64String = Base64String.Replace("data:image/png;base64,", "");

        //    // Convert Base64 String to byte[]
        //    // byte[] imageBytes = Convert.FromBase64String(Base64String);

        //    using (MemoryStream ms = new MemoryStream(imageBytes))
        //    {
        //        // Convert byte[] to Image
        //        ms.Write(imageBytes, 0, imageBytes.Length);
        //        Image image = Image.FromStream(ms, true);

        //        var imag = ScaleImage(image, desiredWidth, desiredHeight);

        //        byte[] imageBytes1 = converterDemo(imag);

        //        //Then Convert byte[] to Base64 String
        //        string base64String = Convert.ToBase64String(imageBytes1);
        //        return base64String;

        //    }
        //}

        //public static byte[] converterDemo(Image x)
        //{
        //    ImageConverter _imageConverter = new ImageConverter();
        //    byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
        //    return xByte;
        //}


        //public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        //{
        //    var ratioX = (double)maxWidth / image.Width;
        //    var ratioY = (double)maxHeight / image.Height;
        //    var ratio = Math.Min(ratioX, ratioY);

        //    var newWidth = (int)(image.Width * ratio);
        //    var newHeight = (int)(image.Height * ratio);

        //    var newImage = new Bitmap(newWidth, newHeight);

        //    using (var graphics = Graphics.FromImage(newImage))
        //        graphics.DrawImage(image, 0, 0, newWidth, newHeight);

        //    return newImage;
        //}






        //public static void logEntry(string user_name, string log_event_type, string data)
        //{
        //    Boolean qryStatus = false;
        //    try
        //    {
        //        byte[] time = BitConverter.GetBytes(DateTime.Now.ToBinary());
        //        byte[] key = Guid.NewGuid().ToByteArray();
        //        string log_entry_id = Convert.ToBase64String(time.Concat(key).ToArray());
        //        DateTime date_of_entry = DateTime.Now;
        //        string strQry = "INSERT INTO public.log_entry(log_entry_id,user_name,log_event_type,date_of_entry,data)VALUES ('" + log_entry_id + "','" + user_name + "','" + log_event_type + "','" + date_of_entry + "','" + data + "')";
        //        AccessData AccessDataObj = new AccessData();
        //        qryStatus = AccessDataObj.ExecuteQueryForInsertingData(strQry);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //} 

        public static string DataList(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = 2147483647;
            // List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            //List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();
            List<object> rows = new List<object>();

            List<string> lstColumnName = new List<string>();


            string JSONString = "{\"ColumnName\":[NA],\"Data\":[NA]}";

            //  Dictionary<string, string> row;
            int i = 0;

            try
            {


                foreach (DataRow dr in dt.Rows)
                {
                    //   row = new Dictionary<string>();
                    List<string> row1 = new List<string>();

                    foreach (DataColumn col in dt.Columns)
                    {
                        if (i == 0)
                        {
                            lstColumnName.Add(col.ColumnName);
                        }
                        TypeCode yourTypeCode = Type.GetTypeCode(col.DataType);
                        if (dr[col].ToString() == "" || dr[col] == null)
                        {
                            row1.Add("");

                        }
                        else
                        {
                            switch (yourTypeCode)
                            {
                                //case TypeCode.Object:
                                //    byte[] binDate = (byte[])dr[col];
                                //    //     string temp_inBase64 = Convert.ToBase64String(binDate);
                                //    //    row1.Add(temp_inBase64);

                                //    //string ThumbNailBase64 = ResizeBase64ImageString(binDate, 500, 500);

                                //    //row1.Add(ThumbNailBase64);

                                //    //break;
                                case TypeCode.Byte:

                                    row1.Add(Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.SByte:
                                    row1.Add(Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Int16:
                                    row1.Add(Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.UInt16:
                                    row1.Add(Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Int32:
                                    row1.Add(Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.UInt32:
                                    row1.Add(Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Int64:
                                    row1.Add(Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.UInt64:
                                    row1.Add(Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Single:
                                    row1.Add(Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Double:
                                    row1.Add(Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Decimal:
                                    row1.Add(Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Boolean:
                                    row1.Add(Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.DateTime:
                                    DateTime date = Convert.ToDateTime(dr[col.ColumnName]);
                                    row1.Add(date.ToString("MM/dd/yyyy hh:mm tt"));
                                    break;
                                case TypeCode.String:
                                    row1.Add(Convert.ToString(dr[col]));
                                    break;
                                case TypeCode.Empty:
                                    row1.Add(Convert.ToString(dr[col]));
                                    break;
                                default:    // TypeCode.DBNull, TypeCode.Char and TypeCode.Object
                                    row1.Add(Convert.ToString(dr[col]));
                                    break;
                            }

                        }
                        //row.Add(col.ColuomnName, Convert.ToString(dr[col]));
                    }
                    rows.Add(row1);
                    i++;
                }
                JSONString = "{\"ColumnName\":" + JsonConvert.SerializeObject(lstColumnName) + ",\"Data\":" + serializer.Serialize(rows) + "}";
            }
            catch (Exception)
            {
                JSONString = "{\"ColumnName\":[NA],\"Data\":[NA]}";
            }



            return JSONString;
        }

        //public static string ImageDataList(DataTable dt)
        //{
        //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //    serializer.MaxJsonLength = 2147483647;
        //    // List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //    //List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();
        //    List<object> rows = new List<object>();

        //    List<string> lstColumnName = new List<string>();


        //    string JSONString = "";
        //    //"{\"ColumnName\":[NA],\"Data\":[NA]}";

        //    //  Dictionary<string, string> row;
        //    int i = 0;

        //    try
        //    {


        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            //   row = new Dictionary<string>();
        //            List<string> row1 = new List<string>();

        //            foreach (DataColumn col in dt.Columns)
        //            {
        //                if (i == 0)
        //                {
        //                    lstColumnName.Add(col.ColumnName);
        //                }
        //                TypeCode yourTypeCode = Type.GetTypeCode(col.DataType);
        //                if (dr[col].ToString() == "" || dr[col] == null)
        //                {
        //                    row1.Add("");

        //                }
        //                else
        //                {
        //                    switch (yourTypeCode)
        //                    {
        //                        case TypeCode.Object:
        //                            byte[] binDate = (byte[])dr[col];
        //                            //     string temp_inBase64 = Convert.ToBase64String(binDate);
        //                            //    row1.Add(temp_inBase64);
        //                            string ThumbNailBase64 = "";
        //                            if (binDate.Length > 50)
        //                            {
        //                                ThumbNailBase64 = ResizeBase64ImageString(binDate, 500, 500);
        //                            }


        //                            row1.Add(ThumbNailBase64);

        //                            break;
        //                        case TypeCode.Byte:

        //                            row1.Add(Convert.ToString(dr[col]));
        //                            break;
        //                        case TypeCode.SByte:
        //                            row1.Add(Convert.ToString(dr[col]));
        //                            break;
        //                        case TypeCode.Int16:
        //                            row1.Add(Convert.ToString(dr[col]));
        //                            break;
        //                        case TypeCode.UInt16:
        //                            row1.Add(Convert.ToString(dr[col]));
        //                            break;
        //                        case TypeCode.Int32:
        //                            row1.Add(Convert.ToString(dr[col]));
        //                            break;
        //                        case TypeCode.UInt32:
        //                            row1.Add(Convert.ToString(dr[col]));
        //                            break;
        //                        case TypeCode.Int64:
        //                            row1.Add(Convert.ToString(dr[col]));
        //                            break;
        //                        case TypeCode.UInt64:
        //                            row1.Add(Convert.ToString(dr[col]));
        //                            break;
        //                        case TypeCode.Single:
        //                            row1.Add(Convert.ToString(dr[col]));
        //                            break;
        //                        case TypeCode.Double:
        //                            row1.Add(Convert.ToString(dr[col]));
        //                            break;
        //                        case TypeCode.Decimal:
        //                            row1.Add(Convert.ToString(dr[col]));
        //                            break;
        //                        case TypeCode.Boolean:
        //                            row1.Add(Convert.ToString(dr[col]));
        //                            break;
        //                        case TypeCode.DateTime:
        //                            DateTime date = Convert.ToDateTime(dr[col.ColumnName]);
        //                            //row1.Add(Convert.ToString(date.ToShortDateString().ToString()));
        //                            row1.Add(Convert.ToString(date.ToString()));
        //                            break;
        //                        case TypeCode.String:
        //                            row1.Add(Convert.ToString(dr[col]));
        //                            break;
        //                        case TypeCode.Empty:
        //                            row1.Add(Convert.ToString(dr[col]));
        //                            break;
        //                        default:    // TypeCode.DBNull, TypeCode.Char and TypeCode.Object
        //                            row1.Add(Convert.ToString(dr[col]));
        //                            break;
        //                    }

        //                }
        //                //row.Add(col.ColuomnName, Convert.ToString(dr[col]));
        //            }
        //            rows.Add(row1);
        //            i++;
        //        }
        //        JSONString = "{\"ColumnName\":" + JsonConvert.SerializeObject(lstColumnName) + ",\"Data\":" + serializer.Serialize(rows) + "}";
        //    }
        //    catch (Exception)
        //    {
        //        JSONString = "{\"ColumnName\":[NA],\"Data\":[NA]}";
        //    }



        //    return JSONString;
        //}

        //public static string ImageList(DataTable dt)
        //{
        //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //    serializer.MaxJsonLength = 2147483647;
        //    // List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //    //List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();
        //    List<object> rows = new List<object>();

        //    List<string> lstColumnName = new List<string>();


        //    string JSONString = "{\"ColumnName\":[NA],\"Data\":[NA]}";

        //    //  Dictionary<string, string> row;
        //    int i = 0;

        //    try
        //    {


        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            //   row = new Dictionary<string>();
        //            List<string> row1 = new List<string>();

        //            foreach (DataColumn col in dt.Columns)
        //            {
        //                if (i == 0)
        //                {
        //                    lstColumnName.Add(col.ColumnName);
        //                }
        //                TypeCode yourTypeCode = Type.GetTypeCode(col.DataType);
        //                if (dr[col].ToString() == "" || dr[col] == null)
        //                {
        //                    row1.Add("");

        //                }
        //                else
        //                {

        //                    byte[] binDate = (byte[])dr[col];
        //                    string temp_inBase64 = Convert.ToBase64String(binDate);
        //                    row1.Add(temp_inBase64);

        //                }
        //                //row.Add(col.ColuomnName, Convert.ToString(dr[col]));
        //            }
        //            rows.Add(row1);
        //            i++;
        //        }
        //        JSONString = "{\"ColumnName\":" + JsonConvert.SerializeObject(lstColumnName) + ",\"Data\":" + serializer.Serialize(rows) + "}";
        //    }
        //    catch (Exception)
        //    {
        //        JSONString = "{\"ColumnName\":[NA],\"Data\":[NA]}";
        //    }



        //    return JSONString;
        //}

        public string Decrypt(string Password)
        {
            string bpassword = "password";
            SJCLDecryptor sd = new SJCLDecryptor(Password, bpassword);
            string jsonstring = sd.Plaintext;
            return jsonstring;
        }
    }


}
