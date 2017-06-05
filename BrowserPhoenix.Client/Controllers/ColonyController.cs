
using BrowserPhoenix.Client.Models;
using BrowserPhoenix.Shared;
using BrowserPhoenix.Shared.Commands;
using BrowserPhoenix.Shared.Commands.Sync;
using BrowserPhoenix.Shared.Domain;
using PetaPoco;
using PetaPoco.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrowserPhoenix.Controllers
{
    public class ColonyController : Controller
    {
        // GET: Colony

        public ActionResult Index(Int64? id)
        {
            using (var db = DatabasePortal.Open())
            {
                Colony colony = null;

                if(id.HasValue)
                {
                    colony = Colony.GetById(db, id.Value);
                    Player.Current.Colony = colony;
                }
                else
                {
                    colony = Player.Current.Colony;
                }

                //check if db would connect twice if i dont give it to the class
                
                var result = new ColonyModel();
                result.Colony = colony;
                result.Buildings = Building.GetByColonyId(db, colony.Id);

                return View(result);
            }
            
        }

        [Authorize]
        public ActionResult Create(Int32 x, Int32 y)
        {
            var command = new CreateColonyCommand();
            command.XCord = x;
            command.YCord = y;
            command.PlayerId = Player.Current.Id;

            CommandPortal.Send(command);

            return Content("");
        }

        [Authorize]
        public ActionResult Build(Int64 id, BuildingType type, Int32 x, Int32 y)
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

        [Authorize]
        public ActionResult LevelUp(Int64 id)
        {

            if(Player.Current.Colony.CheckResourcesAvailable(id))
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

        //just a test
        [Authorize]
        public ActionResult RefreshResources(Int64 id)
        {
            var command = new RecalculateResourcesCommand(id, DateTime.Now);
            CommandPortal.Send(command);

            return Redirect("/colony/index/" + id);
        }
    }
}