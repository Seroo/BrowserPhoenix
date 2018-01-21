using BrowserPhoenix.Client.Models;
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
                var troops = Troop.GetInactiveTroopCollectionByColony(db, Player.Current.Colony.Id);// Troop.Get GetByColonyId(db, Player.Current.Colony.Id, true);
                                            
                var result = new BroodLairModel();
                result.Troops = troops;
                result.Building = Building.GetById(db, id);
             
                return View(result);
            }
                

            
        }

        [Authorize]
        public ActionResult LevelUp(Int64 id)
        {

            if (Player.Current.Colony.CheckResourcesAvailable(id))
            {
                var command = new LevelUpBuildingTimer();
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
        public ActionResult Create(BuildingType type, Int32 x, Int32 y)
        {
            using (var db = DatabasePortal.Open())
            {
                if (Player.Current.Colony.CheckResourcesAvailable(type, 1))
                {
                    var command = new CreateBuildingTimer();
                    command.ColonyId = Player.Current.Colony.Id;
                    command.CreateDate = DateTime.Now;
                    command.Type = type;
                    command.XCord = x;
                    command.YCord = y;

                    CommandPortal.Send(command);
                }
                else
                {
                    return Content("Resources Missing");
                }

                
            }

            return Content("");
        }
    }
}