using PainterWebsite.AdminFiles.CommonFiles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace PainterWebsite.Models.GalleryModel
{
    public class GalleryModel
    {
        public int Fileid { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int Status { get; set; }

        public string AddGallery( int FileId, string FileName, string FilePath,int Status)
        {
            string Result = "";
            try
            {
                using (SqlConnection con = DLConnection.getConnectionInstance())
                {
                    using (SqlCommand cmd = new SqlCommand("Usp_AddGallery", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@fileid", FileId);
                        cmd.Parameters.AddWithValue("@filename", FileName);
                        cmd.Parameters.AddWithValue("@filepath", FilePath);
                        cmd.Parameters.AddWithValue("@isactive", Status);
                        cmd.Parameters.AddWithValue("@Flag", FileId == 0 ? "1" : "4");
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

        public DataTable GetGalleryList()
        {

            try
            {
                using (DataTable dt = new DataTable())
                {
                    using (SqlConnection con = DLConnection.getConnectionInstance())
                    {
                        SqlCommand cmd = new SqlCommand("Usp_AddGallery", con);
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
        public DataTable EditGaller(int FileId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = DLConnection.getConnectionInstance())
                {
                    using (SqlCommand cmd = new SqlCommand("Usp_AddGallery", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@flag", 3);
                        cmd.Parameters.AddWithValue("@fileid", FileId);
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