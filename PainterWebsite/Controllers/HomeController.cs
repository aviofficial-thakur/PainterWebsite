using Newtonsoft.Json;
using PainterWebsite.Models.GalleryModel;
using PainterWebsite.Models.HomeModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace PainterWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }


       
        public JsonResult GetImageData()
        {

            DataTable Dt = new HomeModel().GetImageFileList();

            return new JsonResult
            {
                Data = JsonConvert.SerializeObject(Dt),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult GetAllImageData()
        {

            DataTable Dt = new HomeModel().GetAllImageFileList();

            return new JsonResult
            {
                Data = JsonConvert.SerializeObject(Dt),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        [HttpPost]
        public JsonResult SaveClient(Contactlist objcontact)
        {
            HomeModel obj = new HomeModel();
            string result = string.Empty;
            string emailbody = "";
            try
            {
                if (ModelState.IsValid)
                {
                    result = obj.AddClient(objcontact.ClientName, objcontact.ClientEmailId, objcontact.ClientMobileNo, objcontact.ClientMessage);
                    emailbody = RenderHtml(objcontact.ClientName, objcontact.ClientEmailId, objcontact.ClientMobileNo, objcontact.ClientMessage);
                    SendEmail("singhroyal6767@gmail.com", "Inquiry Regarding Paint Color Work", emailbody);
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
        public void SendEmail(string to, string subject, string body)
        {
            // SMTP server settings
            string smtpServer = "smtp-relay.brevo.com";
            int smtpPort = 587;

            // Sender's email credentials
            string smtpUsername = "757b99002@smtp-brevo.com";
            string smtpPassword = "HWPUjANOfCpXLE3G";

            // Create a new SmtpClient object
            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);

            // Set the SMTP client credentials
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

            // Enable SSL encryption if required by the SMTP server
            smtpClient.EnableSsl = true;

            // Create a new MailMessage object
            MailMessage mailMessage = new MailMessage();

            // Set the sender, recipient, subject, and body
            mailMessage.From = new MailAddress(smtpUsername);
            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            // Send the email
            smtpClient.Send(mailMessage);
        }


        public static string RenderHtml(string name, string email, string mobile, string message)
        {
            string htmlTemplate = @"<html>
        <head>
        </head>
        <body>
        <p>Full Name :- {0}<br />
        Email Id :- {1}<br />
        Mobile No :- {2}<br />
        Message :- {3}</p>
        </body>
        </html>";

            return string.Format(htmlTemplate, name, email, mobile, message);
        }
        public class Contactlist
        {
            public string ClientName { get; set; }
            public string ClientEmailId { get; set; }
            public string ClientMobileNo { get; set; }
            public string ClientMessage { get; set; }
        }
    }
}