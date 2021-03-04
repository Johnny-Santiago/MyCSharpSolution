using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace Delivery
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteTable.Routes.Add("sodomanifest-details", new Route("sodomanifest/{edit_mode}/{Id}.aspx", new PageRouteHandler("~/SODOManifest.aspx")));
            //RouteTable.Routes.Add("target-details", new Route("target/{edit_mode}/{Id}.aspx", new PageRouteHandler("~/Priorities/Target_Details.aspx")));
            //RouteTable.Routes.Add("issue-details", new Route("issue/{edit_mode}/{Id}.aspx", new PageRouteHandler("~/Priorities/Issue_Details.aspx")));
            //RouteTable.Routes.Add("chart-details", new Route("chart/{edit_mode}/{Id}.aspx", new PageRouteHandler("~/Priorities/Chart_Details.aspx")));
            //RouteTable.Routes.Add("morningmeetingimage-details", new Route("morningmeetingimage/{edit_mode}/{Id}.aspx", new PageRouteHandler("~/Priorities/MorningMeetingImage_Details.aspx")));
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
