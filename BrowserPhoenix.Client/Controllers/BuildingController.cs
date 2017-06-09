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
    public class BuildingController : Controller
    {
        // GET: Building
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BroodLair(Int64 id)
        {
            using (var db = DatabasePortal.Open())
            {
                var troops = Troop.GetByBuildingId(db, id);

                return View(troops);
            }
                

            
        }

        [Authorize]
        public ActionResult LevelUp(Int64 id)
        {

            if (Player.Current.Colony.CheckResourcesAvailable(id))
            {
                var command = new LevelUpBuildingCommand();
                command.BuildingId = id;
                command.CreateDate = DateTime.Now;
                command.PlayerId = Player.Current.Id;

                CommandPortal.Send(command);


                return Content("");
            }
            else
            {
                return Content("Resources Missing");
            }

        }

        [Authorize]
        public ActionResult Create(Int64 id, BuildingType type, Int32 x, Int32 y)
        {
            using (var db = DatabasePortal.Open())
            {
                //check if db would connect twice if i dont give it to the class
                var colony = Colony.GetById(db, id);


                var command = new CreateBuildingCommand();
                command.ColonyId = colony.Id;
                command.CreateDate = DateTime.Now;
                command.Type = type;
                command.XCord = x;
                command.YCord = y;

                CommandPortal.Send(command);
            }

            return Content("");
        }
    }
}