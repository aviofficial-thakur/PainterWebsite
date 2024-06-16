using PainterWebsite.AdminFiles.CommonFiles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace PainterWebsite.Models.HomeModel
{
    public class HomeModel
    {
        public DataTable GetImageFileList()
        {

            try
            {
                using (DataTable dt = new DataTable())
                {
                    using (SqlConnection con = DLConnection.getConnectionInstance())
                    {
                        SqlCommand cmd = new SqlCommand("Usp_GetImageFile", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", 1);
                        SqlDataAdapter Ada = new SqlDataAdapter(cmd);
                        Ada.Fill(dt);
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                //ErrorLogging.Logger.LogError(ex.Message);
                return null;
            }
        }

        public DataTable GetAllImageFileList()
        {

            try
            {
                using (DataTable dt = new DataTable())
                {
                    using (SqlConnection con = DLConnection.getConnectionInstance())
                    {
                        SqlCommand cmd = new SqlCommand("Usp_GetImageFile", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        SqlDataAdapter Ada = new SqlDataAdapter(cmd);
                        Ada.Fill(dt);
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                //ErrorLogging.Logger.LogError(ex.Message);
                return null;
            }
        }

        public string AddClient(string ClientName, string ClientEmailId, string ClientMobileNo, string ClientMessage)
        {
            string Result = "";
            try
            {
                using (SqlConnection con = DLConnection.getConnectionInstance())
                {
                    using (SqlCommand cmd = new SqlCommand("Usp_ClientSP", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClientName", ClientName);
                        cmd.Parameters.AddWithValue("@ClientEmail", ClientEmailId);
                        cmd.Parameters.AddWithValue("@ClientMobileNo", ClientMobileNo);
                        cmd.Parameters.AddWithValue("@ClientMessage", ClientMessage);
                        cmd.Parameters.Add("@ResultMsg", SqlDbType.VarChar, 100);
                        cmd.Parameters["@ResultMsg"].Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        Result = cmd.Parameters["@ResultMsg"].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogging.Logger.LogError(ex.Message);
                Result = "Error";
            }
            return Result;
        }


    }
}