using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrowserPhoenix.Shared;
using BrowserPhoenix.Shared.Domain;
using PetaPoco;

namespace BrowserPhoenix.Client.Models
{
    public class MovementController : Controller
    {
        // GET: Movement
        public ActionResult Index()
        {
            using (var db = DatabasePortal.Open())
            {


                var movements = db.Fetch<TroopMovement>(
                      Sql.Builder
                      .Append("SELECT * FROM troop_movement")
                      .Append("WHERE troop_movement.player_id =@0 ", Player.Current.Id)
                      );
                //check if db would connect twice if i dont give it to the class
                

                foreach(var movement in movements)
                {
                    var troops = movement.GetTroops(db);
                }


                return View(movements.OrderBy(x => x.ArrivalDate));
            }


            return View();
        }
    }
}