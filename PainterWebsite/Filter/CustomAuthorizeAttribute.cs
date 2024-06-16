using System.Web.Mvc;

namespace PainterWebsite.Filter
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Check if the user is authenticated but the session has expired
            if (filterContext.HttpContext.User.Identity.IsAuthenticated && filterContext.HttpContext.Session["UserDataList"] == null)
            {
                // Redirect to the login page
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
            else
            {
                // User is not authenticated, show the default unauthorized message
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}