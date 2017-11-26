using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Commands.Sync
{
    [Serializable]
    public class CreateTroopMovementCommand : BaseCommand, ICommand
    {
        public Int64 ColonyId { get; set; }

        public Int32 TargetX { get; set; }
        public Int32 TargetY { get; set; }

        public TroopCollection SelectedTroops { get; set; }
        
        //kann man vllt auch weglassen wenn eindeutig ist was passiert anhand vom ziel. dunno
        public TroopMovementType Type { get; set; }
        

        public void Process()
        {
            using (var db = DatabasePortal.Open())
            {
                //check if field is available


                //check if colony is also the start x/y
                //check if troops that got selected are still available
                //take the amound of troops minus the amount that was not selected
                //create new troops from the troop collectiom
                //create troop movement
                //create timer when they'l all arrive and set it on every of those new troops

                var colony = Colony.GetById(db, ColonyId);

                var tempTroops = SelectedTroops.GetTroops();

                var movingTroops = new List<Troop>();
                foreach (var troop in tempTroops)
                {
                    var movingTroop = colony.CreateMovingTroop(db, troop.Type, troop.Amount);
                    
                    movingTroops.Add(movingTroop);
                }


                TroopMovement.Create(db, CreateDate, colony.PlayerId, movingTroops, colony.XCord, colony.YCord, TargetX, TargetY, Type );
            }
        }
    }
}