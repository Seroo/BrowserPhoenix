using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Messaging;
using BrowserPhoenix.Shared.Commands;
using BrowserPhoenix.Shared;
using BrowserPhoenix.Shared.Domain;

namespace BrowserPhoenix.Server.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult ServerTest()
        {
            //CommandPortal.ProcessNext();
            
            /*

            test.Run();
            if(test.Type == CommandType.Attack)
            {
                var bla = (PlayerRefreshCommand<Complex>)test;

                bla.Run();
            }
            
    */
            return Content("as");
        }
    }
}
