using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Commands.Sync
{
    [Serializable]
    public class CalculateFightCommand : BaseCommand, ICommand
    {
        public Int32 ReturnX { get; set; }
        public Int32 ReturnY { get; set; }
        public Int32 FightX { get; set; }
        public Int32 FightY { get; set; }
        public Int64[] TroopIds {get;set;}
     

        public void Process()
        {
            using (var db = DatabasePortal.Open())
            {
                try
                {
                    
                    var attackerTroops = Troop.GetByIds(db, TroopIds);
                    var attackerId = PlayerId;


                    //checken was an x y ist?
                    //oder direkt festmachen das dieser command ein atack colony command ist?


                    var colony = Colony.GetByCoordinates(db, FightX, FightY);
                    var defenderId = colony.PlayerId;


                    var defenderTroops = Troop.GetInactiveTroopsByColony(db, colony.Id);

                    //jetzt die frage wegen der reihenfolge in der die truppen angreifen und wo das abgespeichert werden soll


                    var battleReport = BattleReport.Create(db, attackerId, defenderId, colony.Id, CreateDate);

                    
                    var fightResult = TroopHelper.Fight(defenderTroops, attackerTroops, battleReport);

                    //datenbank seite mit eintragen wer jetzt wieviele truppen noch hat
                    colony.SetSurvivingTroops(db, fightResult.Item1);

                    //check was für ein gegner sich an den koordinaten überhaupt befindet
                    //fight kram
                    var returningTroops = fightResult.Item2;


                    db.Save(fightResult.Item3);
                    //und wieder back
                    //(falls noch welche da sind)



                    if(returningTroops.Count(x => x.Amount > 0) > 0)
                    {
                        TroopMovement.Create(db, CreateDate, PlayerId, returningTroops, FightX, FightY, ReturnX, ReturnY, TroopMovementType.Return);
                    }
                    else
                    {
                        foreach(var troop in returningTroops)
                        {
                            db.Delete(troop);
                        }
                    }

                    
                }
                catch(Exception ex)
                {
                    var test = "asd";
                }

         
            }
        }
    }
}