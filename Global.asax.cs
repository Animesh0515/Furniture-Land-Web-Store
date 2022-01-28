using Furniture_Land_Web_store.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Furniture_Land_Web_store
{
    public class MvcApplication : System.Web.HttpApplication
    {
       
        protected void Application_Start()
        {
            DBconnectionModel.Server = ConfigurationManager.AppSettings["Server"];
            DBconnectionModel.Database = ConfigurationManager.AppSettings["Database"];
            DBconnectionModel.UserID = ConfigurationManager.AppSettings["UserID"];
            DBconnectionModel.Password = ConfigurationManager.AppSettings["Password"];
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
