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
        public DateTime TimeOfHappening { get; set; }

        public void Process()
        {
          
            using (var db = DatabasePortal.Open())
            {
                var building = Building.GetById(db, BuildingId);
                var colony = Colony.GetById(db, building.ColonyId);

                if (building.IsResourceBuilding)
                {
                    colony.UpdateResources(db, TimeOfHappening);
                }

                building.LevelUp(db, TimeOfHappening);
                if (building.IsResourceBuilding)
                {
                    colony.UpdateResourceProduction(db, TimeOfHappening);
                }
            }
         
        }
    }
}