using BrowserPhoenix.Shared.Commands;
using BrowserPhoenix.Shared.Commands.Sync;
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
                                var command = new LevelUpBuildingCommand();
                                command.BuildingId = timer.RefId;
                                command.TimeOfHappening = timer.EndDate;

                                CommandPortal.Send(command);
                                break;

                            case TimerType.LevelUpBuilding:
                                var command2 = new LevelUpBuildingCommand();
                                command2.BuildingId = timer.RefId;
                                command2.TimeOfHappening = timer.EndDate;

                                CommandPortal.Send(command2);

                                break;

                            case TimerType.CreateTroop:

                                var troopType = timer.AdditionalDataCreateTroop();
                                var inactiveTroop = Troop.GetInactiveTroopByType(db, building.ColonyId, troopType);
                                inactiveTroop.AddUnit(db, timer.EndDate);
                                break;

                        }
                    }
                    break;

                case RefType.Colony:
                    break;

                case RefType.Troop:

                   

                    switch(timer.Type)
                    {
                        
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