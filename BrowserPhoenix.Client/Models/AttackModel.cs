using BrowserPhoenix.Shared;
using BrowserPhoenix.Shared.Domain;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Client.Models
{
    public class AttackModel
    {
        public Int32 StartX { get; set; }
        public Int32 StartY { get; set; }

        public Int32 TargetX { get; set; }
        public Int32 TargetY { get; set; }

        public IEnumerable<Tuple<Int64, String>> PlayerColonies { get; set; }
        public TroopCollection AvailableTroops { get; set; }
        public TroopCollection SelectedTroops { get; set; }

        public String TargetName { get; set; }
        public Int64? TargetId { get; set; }


        public AttackModel(Database db, Int32? x, Int32? y)
        { 
            this.StartX = Player.Current.Colony.XCord;
            this.StartY = Player.Current.Colony.YCord;
            this.PlayerColonies = Colony.GetNameTupleList(db, Player.Current.Id);

            if (x.HasValue && x.HasValue)
            {
                this.TargetX = x.Value;
                this.TargetY = y.Value;

                //todo
                //check the target coordinates for colonies and or npc stuff
                this.TargetName = "uff ae town";
                this.TargetId = 14;
            }

            this.AvailableTroops = Troop.GetInactiveTroopsByColony(db, Player.Current.Colony.Id);
        }
    }
    
}