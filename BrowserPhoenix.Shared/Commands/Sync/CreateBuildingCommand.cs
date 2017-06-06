using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Commands.Sync
{
    [Serializable]
    public class CreateBuildingCommand : BaseCommand, ICommand
    {
        public Int32 XCord { get; set; }
        public Int32 YCord { get; set; }
        public Int64 ColonyId { get; set; }
        public BuildingType Type { get; set; }

        public void Process()
        {
            using (var db = DatabasePortal.Open())
            {
                //check if field is available

                var building = Building.Create(db, this.CreateDate, ColonyId, Type, XCord, YCord);

                building = Building.GetById(db, building.Id);


                var buildTime = BuildingHelper.GetBuildTime(building.Type, building.Level + 1);

                var costs = BuildingHelper.GetBuildCost(building.Type, building.Level + 1);

                building.Colony.RemoveResources(db, costs);

                var endDate = CreateDate.Add(buildTime);

                Timer.Create(db, PlayerId, TimerType.LevelUpBuilding, RefType.Building, building.Id, endDate);
            }
        }
    }
}