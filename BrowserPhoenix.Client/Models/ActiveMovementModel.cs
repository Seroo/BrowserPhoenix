using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BrowserPhoenix.Shared;
using BrowserPhoenix.Shared.Domain;
using PetaPoco;

namespace BrowserPhoenix.Client.Models
{
    public class ActiveMovementModel
    {

        public IEnumerable<TroopMovement> List { get; set; }

        public ActiveMovementModel()
        {
            using (var db = DatabasePortal.Open())
            {


                var movements = db.Fetch<TroopMovement>(
                      Sql.Builder
                      .Append("SELECT * FROM troop_movement")
                      .Append("WHERE troop_movement.player_id =@0 ", Player.Current.Id)
                      );
                //check if db would connect twice if i dont give it to the class


                foreach (var movement in movements)
                {
                    var troops = movement.GetTroops(db);
                }


                List = movements.OrderBy(x => x.ArrivalDate);
            }
        }

    }
}