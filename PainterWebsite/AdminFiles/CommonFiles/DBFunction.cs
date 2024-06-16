

namespace PainterWebsite.AdminFiles.CommonFiles
{
    #region Namespace

    using System;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Text;
    //using HRMS.Multitenant;

    #endregion
    /// <summary>
    /// A single class to create sql connection for whole application
    /// Created and modified by Fareed on 30/03/2017
    /// </summary>
    public static class DLConnection
    {
        /// <summary>
        /// this method use to get connection string
        /// </summary>
        /// <returns></returns>
        public static string GetConnection()
        {
            return getConnectionString();
        }

        /// <summary>
        /// this method use to get connection instance
        /// </summary>
        /// <returns></returns>
        public static SqlConnection getConnectionInstance()
        {
            SqlConnection _Con = null; ;
            try
            {
                string ConnectionString = string.Empty;
                ConnectionString = getConnectionString();
                _Con = new SqlConnection(ConnectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while establishing the database connection: " + ex.Message);
            }

            return _Con;

        }

        /// <summary>
        /// this method use to get connection string on the basis of appliation archtecture
        /// </summary>
        /// <returns></returns>
        public static string getConnectionString()
        {
            string ConnectionString = string.Empty;
            try
            {
                //bool IsMultitenantArchtecture = Convert.ToBoolean(ConfigurationManager.AppSettings["MultitenantArchtecture"]);
                //if (IsMultitenantArchtecture)
                //{
                //    using (ConnectionRepository objConnectionRepository = new ConnectionRepository())
                //    {
                //        ConnectionString = objConnectionRepository.GetClientConnectionString();
                //    }
                //}
                //else
                //{
                ConnectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while establishing the database connection: " + ex.Message);
            }
            return ConnectionString;
        }

    }
}