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

                if (building.HasTimer())
                    return Content("Already something in build");

                var command = new CreateTroopCommand();
                command.BuildingId = building.Id;
                command.ColonyId = building.ColonyId;
                command.CreateDate = DateTime.Now;
                command.Type = type;

                CommandPortal.Send(command);
            }

            return Content("");
        }

        [HttpGet]
        public ActionResult Attack(Int32? xCord, Int32? yCord)
        {
            using (var db = DatabasePortal.Open())
            {
                var result = new AttackModel(db, xCord, yCord);
               

                return View(result);
            }
        }

        [HttpPost]
        public ActionResult Attack(Int32 xCord, Int32 yCord, TroopCollection troops)
        {
            using (var db = DatabasePortal.Open())
            {
                var result = new AttackModel(db, xCord, yCord);

                if (!result.AvailableTroops.HasMoreThan(troops))
                {
                    return Content("jo fehler jo nich genug truppen");// View(result);
                    //error message
                }

            
                var command = new CreateTroopMovementCommand()
                {
                    PlayerId = Player.Current.Id,
                    SelectedTroops = troops,
                    CreateDate = DateTime.Now,
                    TargetX = xCord,
                    TargetY = yCord,
                    Type = TroopMovementType.Attack,
                    ColonyId = Player.Current.Colony.Id
                };

                CommandPortal.Send(command);


                return View(result);
            }
        }
    }
}