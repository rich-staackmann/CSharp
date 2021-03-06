﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TimeTrackr
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Just playing with routing here
            routes.MapRoute(
                name: "category",
                url: "Categories/{category}",
                defaults: new { controller = "Task", action = "TasksByCategory", category = UrlParameter.Optional },
                constraints: new { action = "TasksByCategory"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}