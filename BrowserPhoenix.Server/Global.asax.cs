using BrowserPhoenix.Shared.Commands;
using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BrowserPhoenix.Server
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            CommandWorker commandWorkerObject = new CommandWorker();
            Thread commandWorkerThread = new Thread(commandWorkerObject.DoWork);

          
            commandWorkerThread.Start();

            TimerWorker timerWorkerObject = new TimerWorker();
            Thread timerWorkerThread = new Thread(timerWorkerObject.DoWork);


            timerWorkerThread.Start();

        }
    }
}
