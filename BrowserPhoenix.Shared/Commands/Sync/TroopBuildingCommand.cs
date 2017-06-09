﻿using BrowserPhoenix.Shared.Domain;
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
        public TroopType Type { get; set; }

        public void Process()
        {
            using (var db = DatabasePortal.Open())
            {
                //check if field is available

                var building = Building.GetById(db, BuildingId);

                var troop = Troop.Create(db, this.CreateDate, BuildingId, building.ColonyId, Type);

                troop = Troop.GetById(db, troop.Id);

               
                var buildTime = TroopHelper.GetBuildTime(troop.Type, building.Level);

                var costs = TroopHelper.GetBuildCost(troop.Type, building.Level);

                building.Colony.RemoveResources(db, costs);

                var endDate = CreateDate.Add(buildTime);

                Timer.Create(db, PlayerId, TimerType.CreateTroop, RefType.Troop, troop.Id, endDate);
            }
        }
    }
}