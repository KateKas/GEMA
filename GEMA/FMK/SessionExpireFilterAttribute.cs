using System.Web;
using System.Web.Mvc;

namespace GEMA.FMK
{
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            //if (HttpContext.Current.Session["IdUsuario"] == null)
            //    ctx.Response.Redirect("~/Acesso/Login");

            // check if session is supported
            if (ctx.Session != null)
            {

                // check if a new session id was generated
                if (ctx.Session.IsNewSession)
                {

                    // If it says it is a new session, but an existing cookie exists, then it must
                    // have timed out
                    string sessionCookie = ctx.Request.Headers["Cookie"];
                    if ((null != sessionCookie) && (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        ctx.Response.Redirect("~/Acesso/Login");
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}