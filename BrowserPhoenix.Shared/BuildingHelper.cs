using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared
{
    public static class BuildingHelper
    {
        public static TimeSpan GetBuildTime(BuildingType type, Int32 level)
        {
            double multiplikator = 1;

            switch(type)
            {
                case BuildingType.MushroomFarm:
                    multiplikator = 1.2;
                    break;

                case BuildingType.Sandpit:
                    multiplikator = 1.2;
                    break;

                case BuildingType.Garden:
                    multiplikator = 1.2;
                    break;

                case BuildingType.Stock:
                    multiplikator = 1.8;
                    break;

                case BuildingType.Warehouse:
                    multiplikator = 1.8;
                    break;

                case BuildingType.QueenLair:
                    multiplikator = 5;
                    break;

                case BuildingType.Trench:
                    multiplikator = 2.4;
                    break;

                case BuildingType.Rampart:
                    multiplikator = 2.4;
                    break;

                default:
                    multiplikator = 1;
                    break;
            }

            var seconds = 0;
            switch(level)
            {
                case 1:
                    seconds = 60;
                    break;

                case 2:
                    seconds = 150;
                    break;

                case 3:
                    seconds = 350;
                    break;

                case 4:
                    seconds = 1000;
                    break;

                case 5:
                    seconds = 2200;
                    break;

                case 6:
                    seconds = 5000;
                    break;

            }

            return TimeSpan.FromSeconds(seconds * multiplikator);
        }

        public static ResourceCollection GetBuildCost(BuildingType type, Int32 level)
        {
            var result = new ResourceCollection();

            double multiplikator = 1;
            if (level < 5)
            {
                multiplikator = 1;
            }
            else if(level < 10)
            {
                multiplikator = 1.5;
            }
            else if(level < 15)
            {
                multiplikator = 2.5;
            }
            else if(level < 20)
            {
                multiplikator = 4;
            }
            else
            {
                multiplikator = 8;
            }

            switch (type)
            {
                case BuildingType.MushroomFarm:
                    result.Food = (float)15 * (float)multiplikator;
                    result.Sand = (float)10 * (float)multiplikator;
                    result.Leave = (float)10 * (float)multiplikator;
                    break;

                case BuildingType.Sandpit:
                    result.Sand = (float)15 * (float)multiplikator;
                    result.Leave = (float)5 * (float)multiplikator;
                    break;

                case BuildingType.Garden:
                    result.Sand = (float)5 * (float)multiplikator;
                    result.Leave = (float)15 * (float)multiplikator;
                    break;

                case BuildingType.Stock:
                    
                    break;

                case BuildingType.Warehouse:
                    
                    break;

                case BuildingType.QueenLair:
                   
                    break;

                case BuildingType.Trench:
                    
                    break;

                case BuildingType.Rampart:
                   
                    break;

            }

            return result;

        }

        public static float GetProduction(Building building)
        {
            double multiplikator = 1;

            switch (building.Type)
            {
                case BuildingType.MushroomFarm:
                    multiplikator = 1;
                    break;

                case BuildingType.Sandpit:
                    multiplikator = 1;
                    break;

                case BuildingType.Garden:
                    multiplikator = 1;
                    break;

                case BuildingType.BroodLair:
                    multiplikator = 0.1;
                    break;
                    
                default:
                    multiplikator = 1;
                    break;
            }

            var production = building.Level * 10;
            switch (building.Level)
            {
                case 1:
                    production = 10;
                    break;

                case 2:
                    production = 20;
                    break;

                case 3:
                    production = 30;
                    break;

                case 4:
                    production = 40;
                    break;

                case 5:
                    production = 50;
                    break;

                case 6:
                    production = 60;
                    break;

            }

            return (float)production * (float)multiplikator;
        }

        public static Int32 GetMaxLevel(BuildingType type)
        {
            switch(type)
            {
                case BuildingType.MushroomFarm:
                    return 100;

                default:
                    return 100;
            }
        }

    }
}