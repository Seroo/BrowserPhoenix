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
                

                var troops = Troop.GetByIds(db, TroopIds);

                //check was für ein gegner sich an den koordinaten überhaupt befindet
                //fight kram

                //und wieder back
                //(falls noch welche da sind)
                TroopMovement.Create(db, DateTime.Now, PlayerId, troops, FightX, FightY, ReturnX, ReturnY, TroopMovementType.Return);
         
            }
        }
    }
}