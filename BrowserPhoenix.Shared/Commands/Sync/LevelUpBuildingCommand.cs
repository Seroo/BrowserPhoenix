using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Commands.Sync
{
    [Serializable]
    public class LevelUpBuildingCommand : BaseCommand, ICommand
    {
        public Int64 BuildingId { get; set; }
        
        public void Process()
        {
          
            using (var db = DatabasePortal.Open())
            {
                //check if db would connect twice if i dont give it to the class
                var building = Building.GetById(db, BuildingId);

                
                var buildTime = BuildingHelper.GetBuildTime(building.Type, building.Level + 1);
                
                var costs = BuildingHelper.GetBuildCost(building.Type, building.Level + 1);

                building.Colony.RemoveResources(db, costs);

                var endDate = CreateDate.Add(buildTime);

                Timer.Create(db, PlayerId, TimerType.LevelUpBuilding, RefType.Building, building.Id, endDate);
                
            }
         
        }
    }
}