using BigBlueButtonAPI.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DotNet46Sample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static HttpClient HttpClient = new HttpClient();
        public static BigBlueButtonAPISettings BigBlueButtonAPISettings;
        protected void Application_Start()
        {
            BigBlueButtonAPISettings = new BigBlueButtonAPISettings
            {
                ServerAPIUrl = ConfigurationManager.AppSettings["BigBlueButtonAPISettings:ServerAPIUrl"],
                SharedSecret = ConfigurationManager.AppSettings["BigBlueButtonAPISettings:SharedSecret"]
            };

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
