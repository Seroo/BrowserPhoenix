using BrowserPhoenix.Shared;
using BrowserPhoenix.Shared.Commands;
using BrowserPhoenix.Shared.Commands.Sync;
using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrowserPhoenix.Client.Controllers
{
    public class TroopController : Controller
    {
        // GET: Troop
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Create(Int64 id, TroopType type)
        {
            using (var db = DatabasePortal.Open())
            {
                //check if db would connect twice if i dont give it to the class
                var building = Building.GetById(db, id);


                var command = new CreateTroopCommand();
                command.BuildingId = building.Id;
                command.CreateDate = DateTime.Now;
                command.Type = type;

                CommandPortal.Send(command);
            }

            return Content("");
        }
    }
}