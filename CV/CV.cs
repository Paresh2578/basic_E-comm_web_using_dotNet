using bulkyApp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace bulkyApp.CV
{
    public class IsLogIn : ActionFilterAttribute, IAuthorizationFilter
    {
        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (string.IsNullOrEmpty(SessionAccess.getUserID(filterContext.HttpContext)))
            {
                filterContext.Result = new RedirectResult("~/Authentication/Authentication/LogIn");
            }
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            filterContext.HttpContext.Response.Headers["Expires"] = "-1";
            filterContext.HttpContext.Response.Headers["Pragma"] = "no-cache";
            base.OnResultExecuting(filterContext);
        }
    }

    public class IsAdmin : ActionFilterAttribute, IAuthorizationFilter
    {
        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (SessionAccess.getRole(filterContext.HttpContext) == "Admin")
            {
                filterContext.Result = new RedirectResult("~/Customer/Home/Index");
            }
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            filterContext.HttpContext.Response.Headers["Expires"] = "-1";
            filterContext.HttpContext.Response.Headers["Pragma"] = "no-cache";
            base.OnResultExecuting(filterContext);
        }
    }
}
