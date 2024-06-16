using Newtonsoft.Json;
using PainterWebsite.Models.GalleryModel;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net;

namespace PainterWebsite.Controllers
{
    
    public class GalleryController : Controller
    {
        GalleryModel gobj = new GalleryModel();
        // GET: Gallery
        public ActionResult Gallery()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetGalleryData()
        {

            DataTable Dt = new GalleryModel().GetGalleryList();

            return new JsonResult
            {
                Data = JsonConvert.SerializeObject(Dt),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        [HttpPost]
        public ActionResult GallerySave(GalleryModel galleryobj)
        {
            string Message = string.Empty;
            try
            {
                if (Request.Files.Count > 0)
                {

                    HttpFileCollectionBase files = Request.Files;

                    HttpPostedFileBase file = files[0];
                    string fname;

                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    string path = Server.MapPath("~/Uploads/GalleryData/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var fileExtension = Path.GetExtension(file.FileName);
                    string FileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileExtension;
                    fname = FileName;
                    string Filepath = "~/Uploads/GalleryData/" + fname;
                    // Get the complete folder path and store the file inside it.  
                    string Folderpath = Path.Combine(Server.MapPath("~/Uploads/GalleryData/"), fname);
                    file.SaveAs(Folderpath);
                    galleryobj.FilePath = Filepath;
                }
                var Result = gobj.AddGallery(galleryobj.Fileid,galleryobj.FileName,galleryobj.FilePath,galleryobj.Status);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception ex)
            {
                //result = "An error occurred while saving the user.";
            }
            return null;
        }

        public JsonResult EditgalleryList(int FileId)
        {
            DataTable dt = new DataTable();
            dt = gobj.EditGaller(FileId);

            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult DownloadAttachment(string FilePath)
        {
            try
            {
                
               

                string physicalPath = Server.MapPath(FilePath);

                physicalPath = physicalPath.Replace("\\Gallery\\", "\\");

                // Check if the file exists
                if (!System.IO.File.Exists(physicalPath))
                {
                    return Json(new { error = "File not found." });
                }

                // Return the file as binary data
                byte[] fileData = System.IO.File.ReadAllBytes(physicalPath);

                // Determine the content type based on file extension (example for JPEG files)
                string contentType = "image/jpeg"; // Adjust as needed

                return Json(fileData, contentType);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // For now, returning the exception message as JSON response
                return Json(new { error = ex.Message });
            }
        }










    }
}