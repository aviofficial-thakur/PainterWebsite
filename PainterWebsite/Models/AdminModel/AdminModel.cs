
using PainterWebsite.AdminFiles.CommonFiles;
using PainterWebsite.Logic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace PainterWebsite.Models.AdminModel
{
    public class AdminModel
    {
        public DataTable GetUserList()
        {

            try
            {
                using (DataTable dt = new DataTable())
                {
                    using (SqlConnection con = DLConnection.getConnectionInstance())
                    {
                        SqlCommand cmd = new SqlCommand("Usp_AddUser", con);
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

        public string AddUser(int UserId, string EmailId, string Password, string FirstName, string LastName,int Status)
        {
            string Result = "";
            try
            {
                using (SqlConnection con = DLConnection.getConnectionInstance())
                {
                    using (SqlCommand cmd = new SqlCommand("Usp_AddUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userid", UserId);
                        cmd.Parameters.AddWithValue("@emailid", EmailId);
                        cmd.Parameters.AddWithValue("@password", Password);
                        cmd.Parameters.AddWithValue("@firstname", FirstName);
                        cmd.Parameters.AddWithValue("@lastname", LastName);
                        cmd.Parameters.AddWithValue("@isactive", Status);
                        cmd.Parameters.AddWithValue("@Flag", UserId == 0 ? "1" : "4");
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

        public DataTable EditUser(int UserId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = DLConnection.getConnectionInstance())
                {
                    using (SqlCommand cmd = new SqlCommand("Usp_AddUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@flag", 3);
                        cmd.Parameters.AddWithValue("@userid", UserId);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return dt;
        }
    }
}