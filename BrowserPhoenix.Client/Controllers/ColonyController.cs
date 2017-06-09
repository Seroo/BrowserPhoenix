
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

        [Authorize]
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