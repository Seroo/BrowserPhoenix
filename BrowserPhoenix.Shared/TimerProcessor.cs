using BrowserPhoenix.Shared.Domain;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared
{
    public static class TimerProcessor
    {
        public static void Process(Database db, Timer timer)
        {
            switch (timer.RefType)
            {
                case RefType.Building:
                    {
                        var building = Building.GetById(db, timer.RefId);
                        var colony = Colony.GetById(db, building.ColonyId);

                        switch (timer.Type)
                        {
                            case TimerType.CreateBuilding:
                                if (building.IsResourceBuilding())
                                {
                                    colony.UpdateResources(db, timer.EndDate);
                                }

                                building.LevelUp(db, timer.EndDate);
                                if (building.IsResourceBuilding())
                                {
                                    colony.UpdateResourceProduction(db, timer.EndDate);
                                }
                                break;

                            case TimerType.LevelUpBuilding:
                                if (building.IsResourceBuilding())
                                {
                                    colony.UpdateResources(db,timer.EndDate);
                                }

                                building.LevelUp(db, timer.EndDate);
                                if (building.IsResourceBuilding())
                                {
                                    colony.UpdateResourceProduction(db, timer.EndDate);
                                }
                                break;

                            

                        }
                    }
                    break;

                case RefType.Colony:
                    break;

                case RefType.Troop:

                    var troop = Troop.GetById(db, timer.RefId);

                    switch(timer.Type)
                    {
                        case TimerType.CreateTroop:

                            troop.AddUnit(db, timer.EndDate);
                            break;
                    }
                    
                    break;

                
                case RefType.None:

                    switch(timer.Type)
                    {
                        case TimerType.CalculateHighscore:


                            foreach (var player in Player.GetAll(db))
                            {
                                player.RecalculateScore(db);
                            }

                            Timer.Create(db, 0, TimerType.CalculateHighscore, RefType.None, 0, timer.EndDate.AddMinutes(1));

                            break;
                    }

                    


                    break;
            }

        }
    }
}