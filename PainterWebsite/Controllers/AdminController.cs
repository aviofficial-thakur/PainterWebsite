using Newtonsoft.Json;
using PainterWebsite.Models.AdminModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PainterWebsite.Filter;

namespace PainterWebsite.Controllers
{
    public class AdminController : Controller
    {
        AdminModel obj = new AdminModel();
        // GET: Admin
        
        public ActionResult UserRole()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetUserData()
        {

            DataTable Dt = new AdminModel().GetUserList();

            return new JsonResult
            {
                Data = JsonConvert.SerializeObject(Dt),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        [HttpPost]
        public JsonResult SaveUser(int UserId, string EmailId, string Password, string FirstName, string LastName,int Status)
        {
            string result = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    result = obj.AddUser(UserId, EmailId, Password, FirstName, LastName,Status);
                }
                else
                {
                    // ModelState is not valid, return error message
                    result = "Invalid user data. Please check the input.";
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                // ErrorLogging.Logger.LogError(ex.Message);

                // Return error message
                result = "An error occurred while saving the user.";
            }
            return Json(result);
        }

        public JsonResult EditUserList(int UserId)
        {
            DataTable dt = new DataTable();
                dt = obj.EditUser(UserId);
         
            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);

        }
      

    }
}