using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace WFPlusPlusBiggy
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //var settings = new FriendlyUrlSettings();
            //settings.AutoRedirectMode = RedirectMode.Permanent;
            //routes.EnableFriendlyUrls(settings);

            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Off; // RedirectMode.Permanent
            routes.EnableFriendlyUrls(settings);
        }
    }
}
