using BrowserPhoenix.Shared;
using BrowserPhoenix.Shared.Commands;
using BrowserPhoenix.Shared.Commands.Sync;
using BrowserPhoenix.Shared.Domain;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BrowserPhoenix
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

            var context = HttpContext.Current;
            if (context == null || context.Request == null)
                return;

            var url = context.Request.Url.AbsolutePath;

            // We dont want to authorizate the user for images and some other controllers, in order
            // to increate the site performance
            if (IsStaticContent(url))
                return;

            if(Request.IsAuthenticated && !url.ToLower().Contains("logout"))
            {
                var userId = User.Identity.GetUserId();
                var player = Player.Authentificate(userId);

                if (player == null)
                    Response.Redirect("/account/logout");                    
            }
            
            

        }

        private static Boolean IsStaticContent(String url)
        {
            // We dont want to authorizate the user for images and some other controllers, in order
            // to increate the site performance
            if (url.EndsWith(".js", StringComparison.CurrentCultureIgnoreCase) ||
                url.EndsWith(".css", StringComparison.CurrentCultureIgnoreCase) ||
                url.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase) ||
                url.EndsWith(".jpeg", StringComparison.CurrentCultureIgnoreCase) ||
                url.EndsWith(".gif", StringComparison.CurrentCultureIgnoreCase) ||
                url.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase) ||
                url.EndsWith(".ico", StringComparison.CurrentCultureIgnoreCase))
                return true;

            if ((url.StartsWith("/shrink/js/", StringComparison.CurrentCultureIgnoreCase) ||
                url.StartsWith("/shrink/css/", StringComparison.CurrentCultureIgnoreCase) ||
                url.StartsWith("/image/", StringComparison.CurrentCultureIgnoreCase) ||
                url.StartsWith("/personportrait/view/", StringComparison.CurrentCultureIgnoreCase)) &&
                !url.StartsWith("/image/resizer", StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
    }
}
