using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Commands.Sync
{
    [Serializable]
    public class CreateTroopCommand : BaseCommand, ICommand
    {
        public Int64 BuildingId { get; set; }
        public Int64 ColonyId { get; set; }
        public TroopType Type { get; set; }

        public void Process()
        {
            using (var db = DatabasePortal.Open())
            {
                //check if field is available

                var building = Building.GetById(db, BuildingId);

                var troop = Troop.GetInactiveTroopByType(db, ColonyId, Type);

                if(troop == null)
                {
                    troop = Troop.Create(db, this.CreateDate, BuildingId, building.ColonyId, Type);
                }
                                
                var buildTime = TroopHelper.GetBuildTime(troop.Type, building.Level);

                var costs = TroopHelper.GetBuildCost(troop.Type, building.Level);

                building.Colony.RemoveResources(db, costs);

                var endDate = CreateDate.Add(buildTime);


                //the timer gets created but has no information which troops should be build..
                //need to look into a way to pass this information
                Timer.Create(db, PlayerId, TimerType.CreateTroop, RefType.Building, BuildingId, endDate, troop.Type.ToString());
            }
        }
    }
}