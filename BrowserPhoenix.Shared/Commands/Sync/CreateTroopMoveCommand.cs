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

        public Int32 StartX { get; set; }
        public Int32 StartY { get; set; }

        public Int32 TargetX { get; set; }
        public Int32 TargetY { get; set; }

        public TroopCollection SelectedTroops { get; set; }
        
        

        public void Process()
        {
            using (var db = DatabasePortal.Open())
            {
                //check if field is available

                var colony = Colony.GetById(db, ColonyId);

                //check if colony is also the start x/y
                //check if troops that got selected are still available
                //take the amound of troops minus the amount that was not selected
                //create new troops from the troop collectiom
                //create troop movement
                //create timer when they'l all arrive and set it on every of those new troops



                var availableTroops = Troop.GetInactiveTroopsByColony(db, colony.Id);

                if(!availableTroops.HasMoreThan(SelectedTroops))
                {
                    //really bad shit happened and there are less troops than there should be
                    var a = "b";
                }
              
                
                var moveTime = TroopHelper.GetMoveTime(SelectedTroops, StartX, StartY, TargetX, TargetY);

                var tempTroops = SelectedTroops.GetTroops();

                var movingTroops = new List<Troop>();
                foreach (var troop in tempTroops)
                {
                    colony.RemoveTroop(db, troop.Type, troop.Amount);
                    var movingTroop = Troop.Create(db, CreateDate, 0, colony.Id, troop.Type, troop.Amount, true);
                    movingTroops.Add(movingTroop);
                }
                var endDate = CreateDate.Add(moveTime);


                TroopMove.Create(db, CreateDate, endDate, colony.PlayerId, movingTroops, StartX, StartY, TargetX, TargetY);
         
            }
        }
    }
}