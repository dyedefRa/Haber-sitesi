using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace haberPortali
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Haber",
                url: "{controller}/{action}/{baslik}/{id}",
                defaults: new { controller = "Haber", action = "Goster", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Gorus",
               url: "{controller}/{action}/{baslik}/{id}",
               defaults: new { controller = "Gorus", action = "GorusGoster", id = UrlParameter.Optional }
           );
        }
    }
}
