using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrackKnowledgeWeb.Helpers
{
    public class AFAuthorization : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!SessionManager.HasSession("api_key"))
            {
                filterContext.Result = new RedirectResult("~/Home/Error");
                return;
            }
        }
    }
}