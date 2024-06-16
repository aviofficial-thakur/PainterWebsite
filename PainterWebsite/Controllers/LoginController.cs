using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BCrypt.Net;
using PainterWebsite.Filter;

public class LoginController : Controller
{
    private string connectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;

    // GET: Login

    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public ActionResult CheckLogin(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return Json(new { success = false, message = "Please provide both email and password." });
        }

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT UserID, PasswordHash, FirstName, LastName FROM Users WHERE Email = @Email AND IsActive = 1";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        int userId = reader.GetInt32(0);
                        string storedPasswordHash = reader.GetString(1);
                        string firstName = reader.GetString(2);
                        string lastName = reader.IsDBNull(3) ? "" : reader.GetString(3);

                        // Verify password
                        if (password == storedPasswordHash)
                        {
                            // Authentication successful
                            // Set authentication cookie
                            HttpCookie authCookie = new HttpCookie("AuthCookie");
                            authCookie["UserID"] = userId.ToString();
                            authCookie.Expires = DateTime.Now.AddMinutes(20); // Adjust expiration time as needed
                            Response.Cookies.Add(authCookie);

                            // Set custom authentication
                            //CustomAuthentication.SetAuthCookie(userId.ToString());

                            // Create a list of hashtables to store user data
                            List<Hashtable> userDataList = new List<Hashtable>();

                            // Create a hashtable to store user data for a specific user
                            Hashtable userData = new Hashtable();
                            userData["LoggedInUserID"] = userId;
                            userData["FullName"] = firstName + " " + lastName;

                            // Add the hashtable to the list
                            userDataList.Add(userData);

                            // Store the list in session with a specific key
                            Session["UserDataList"] = userDataList;

                            return Json(new { success = true }); // Return success response
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            return Json(new { success = false, message = "An error occurred while processing your request." });
        }

        // If control reaches here, login failed
        return Json(new { success = false, message = "Invalid email or password." });
    }

    public ActionResult Logout()
    {
        // Clear authentication cookie
        Response.Cookies["AuthCookie"].Expires = DateTime.Now.AddDays(-1);

        // Abandon session
        Session.Abandon();

        // Redirect to login page after logout
        return RedirectToAction("Login");
    }
}
