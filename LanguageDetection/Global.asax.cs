using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using LanguageDetection;
using LanguageDetector.DAL.Repository;

namespace LanguageDetection
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
	        RouteTable.Routes.MapHttpRoute(
		        name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = System.Web.Http.RouteParameter.Optional } 
		        );
            
            Database.SetInitializer(new LanguageDetectorInitializer());

            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}
