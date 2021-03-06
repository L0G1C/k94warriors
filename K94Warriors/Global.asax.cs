﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using K94Warriors.Filters;
using K94Warriors.Email;

namespace K94Warriors
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RegisterFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        private static void RegisterFilters(GlobalFilterCollection filters)
        {
            // Get from and to addresses from config
            var from = ConfigurationManager.AppSettings["ErrorFromEmailAddress"];
            var to = ConfigurationManager.AppSettings["ErrorToEmailAddress"];

            // Add custom error handler that will email problems out
            filters.Add(new EmailErrorAttribute(new SmtpMailer(), from, to)
            {
                // All exception types, let view specify master
                View = "Error",
            });

            FilterConfig.RegisterGlobalFilters(filters);
        }
    }
}