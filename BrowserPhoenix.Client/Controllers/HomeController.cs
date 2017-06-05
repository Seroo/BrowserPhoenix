using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Messaging;
using BrowserPhoenix.Models;

using Microsoft.AspNet.Identity;
using BrowserPhoenix.Shared.Commands.Sync;
using BrowserPhoenix.Shared;
using BrowserPhoenix.Shared.Domain;

namespace BrowserPhoenix.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            ApplicationDbContext db = new ApplicationDbContext();
            ApplicationUser appUser = db.Users.FirstOrDefault(x => x.Id == userId);
           

            return View();
        }

        public ActionResult Reg(Int32 id)
        {
            var user = ApplicationUser.Create("user-" + id, "user@email" + id + ".de");
            user.CreatePlayer();

            return Content("");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test(String test)
        {
            MessageQueue queue = new MessageQueue(".\\Private$\\phoenix");
            
            queue.Formatter = new BinaryMessageFormatter();
       
            Message m = new Message();

            m.Formatter = queue.Formatter;
            
            return Content("");
        }

        public ActionResult Score()
        {
            using (var db = DatabasePortal.Open())
            {
                Timer.Create(db, 0, TimerType.CalculateHighscore, RefType.None, 0, DateTime.Now.AddMinutes(2));
            }
            return Content("");   
        }
    }
}