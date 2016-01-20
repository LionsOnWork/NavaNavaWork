using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;

namespace DLL
{
    public class DataLayer
    {
        /// <summary>
        /// This method is used to execute the query using command
        /// </summary>
        /// <param name="cmdClass"></param>
        /// <param name="objExp"></param>
        public int ExecuteNonQuery(SqlCommand cmdClass, out Exception objExp)
        {
            int intRowsAffected = 0;
            objExp = null;
            try
            {
                SqlConnection conClass = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                cmdClass.Connection = conClass;
                if (conClass.State == ConnectionState.Closed)
                    conClass.Open();
                intRowsAffected = cmdClass.ExecuteNonQuery();
                conClass.Close();
                conClass.Dispose();
                cmdClass.Dispose();
            }
            catch (Exception exp)
            {
                objExp = exp;
            }
            return (intRowsAffected);

        }

        /// <summary>
        /// This method Returns the object after executing the execute scalar method
        /// </summary>
        /// <param name="cmdClass"></param>
        /// <param name="objExp"></param>
        public int ExecuteScalar(SqlCommand cmdClass, out Object obj, out Exception objExp)
        {
            int intStatus = 0;
            obj = null;
            objExp = null;
            try
            {
                SqlConnection conClass = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                cmdClass.Connection = conClass;
                if (conClass.State == ConnectionState.Closed)
                    conClass.Open();
                if (cmdClass.ExecuteScalar() != DBNull.Value)
                    obj = cmdClass.ExecuteScalar();
                else
                    obj = null;
                conClass.Close();
                conClass.Dispose();
                cmdClass.Dispose();
                if (obj != null)
                    intStatus = 1;
            }
            catch (Exception exp)
            {
                objExp = exp;
            }
            return (intStatus);
        }

        /// <summary>
        /// This method returns the Datatable populated by the DataAdapter
        /// </summary>
        /// <param name="dadClass"></param>
        /// <param name="dtbl"></param>
        /// <param name="objExp"></param>
        public int ExecuteDataAdapter(SqlDataAdapter dad, out DataTable dtbl, out Exception objExp)
        {
            int intStatus = 0;
            objExp = null;
            dtbl = null;
            dtbl = new DataTable();
            try
            {
                SqlConnection conClass = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                dad.SelectCommand.Connection = conClass;
                dad.Fill(dtbl);
                conClass.Close();
                conClass.Dispose();
                dad.Dispose();
                if (dtbl.Rows.Count > 0)
                    intStatus = 1;
            }
            catch (Exception exp)
            {
                objExp = exp;
                intStatus = -1;
            }
            return (intStatus);
        }
    }
}
