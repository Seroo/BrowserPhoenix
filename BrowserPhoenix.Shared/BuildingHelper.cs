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
            switch (type)
            {
                case BuildingType.Sandpit:
                    return TimeSpan.FromSeconds(30 * GetMultiplikator(level, 1.8f));

                case BuildingType.MushroomFarm:
                    return TimeSpan.FromSeconds(30 * GetMultiplikator(level, 1.8f));

                case BuildingType.BeetleBreed:
                    return TimeSpan.FromSeconds(30 * GetMultiplikator(level, 1.8f));

                case BuildingType.Garden:
                    return TimeSpan.FromSeconds(30 * GetMultiplikator(level, 1.8f));

                case BuildingType.AphidBreed:
                    return TimeSpan.FromSeconds(30 * GetMultiplikator(level, 1.8f));

                case BuildingType.BroodLair:
                    return TimeSpan.FromSeconds(30 * GetMultiplikator(level, 1.8f));

                case BuildingType.ResearchHill:
                    return TimeSpan.FromSeconds(90 * GetMultiplikator(level, 1.6f));

                case BuildingType.GraveYard:
                    return TimeSpan.FromSeconds(1200);

                case BuildingType.TroopPool:
                    return TimeSpan.FromSeconds(60 * GetMultiplikator(level, 1.5f));

                case BuildingType.Stock:
                    return TimeSpan.FromSeconds(40 * GetMultiplikator(level, 1.8f));

                case BuildingType.Warehouse:
                    return TimeSpan.FromSeconds(40 * GetMultiplikator(level, 1.8f));

                case BuildingType.QueenLair:
                    return TimeSpan.FromSeconds(120 * GetMultiplikator(level, 1.6f));

                case BuildingType.Tower:
                    return TimeSpan.FromSeconds(3600);

                default:
                    return TimeSpan.FromHours(72);
            }
      
        }

        public static float GetMultiplikator(Int32 level, float percentage)
        {
            if (level == 1)
                return 1f;
            var count = 0;
            var multiplikator = 1f;
            while (count++ < level)
            {
                multiplikator = multiplikator * percentage;
            }
            return multiplikator;
        }

        public static ResourceCollection GetBuildCost(BuildingType type, Int32 level)
        {
            var result = new ResourceCollection();


            switch (type)
            {
                case BuildingType.Sandpit:
                    result.Sand = (float)10 * (float)GetMultiplikator(level, 1.2f);
                    result.Leave = (float)15 * (float)GetMultiplikator(level, 1.2f);
                    result.Chitin = (float)5 * (float)GetMultiplikator(level, 1.2f);
                    result.Larvae = (float)2 * 1f;
                    break;

                case BuildingType.MushroomFarm:
                    result.Sand = (float)10 * (float)GetMultiplikator(level, 1.2f);
                    result.Leave = (float)10 * (float)GetMultiplikator(level, 1.2f);
                    result.Chitin = (float)3 * (float)GetMultiplikator(level, 1.2f);
                    result.Larvae = (float)3 * 1f;
                    break;

                case BuildingType.BeetleBreed:
                    result.Sand = (float)15 * (float)GetMultiplikator(level, 1.2f);
                    result.Leave = (float)15 * (float)GetMultiplikator(level, 1.2f);
                    result.Chitin = (float)8 * (float)GetMultiplikator(level, 1.2f);
                    result.Larvae = (float)3 * 1f;
                    break;

                case BuildingType.Garden:
                    result.Sand = (float)15 * (float)GetMultiplikator(level, 1.2f);
                    result.Leave = (float)10 * (float)GetMultiplikator(level, 1.2f);
                    result.Chitin = (float)5 * (float)GetMultiplikator(level, 1.2f);
                    result.Larvae = (float)2 * 1f;
                    break;

                case BuildingType.AphidBreed:
                    result.Sand = (float)10 * (float)GetMultiplikator(level, 1.3f);
                    result.Leave = (float)12 * (float)GetMultiplikator(level, 1.3f);
                    result.Chitin = (float)2 * (float)GetMultiplikator(level, 1.3f);
                    result.Larvae = (float)4 * (float)1f;
                    break;

                case BuildingType.BroodLair:
                    result.Sand = (float)100 * (float)GetMultiplikator(level, 1.25f);
                    result.Leave = (float)100 * (float)GetMultiplikator(level, 1.25f);
                    result.Chitin = (float)20 * (float)GetMultiplikator(level, 1.25f);
                    result.Larvae = (float)2 * (float)1f;
                    break;

                case BuildingType.ResearchHill:
                    result.Sand = (float)100 * (float)GetMultiplikator(level, 1.25f);
                    result.Leave = (float)100 * (float)GetMultiplikator(level, 1.25f);
                    result.Chitin = (float)50 * (float)GetMultiplikator(level, 1.25f);
                    result.Larvae = (float)5 * (float)GetMultiplikator(level, 1.25f);
                    break;

                case BuildingType.GraveYard:
                    result.Sand = (float)500 * (float)GetMultiplikator(level, 1.25f);
                    result.Leave = (float)500 * (float)GetMultiplikator(level, 1.25f);
                    result.Chitin = (float)50 * (float)GetMultiplikator(level, 1.25f);
                    result.Larvae = (float)5 * (float)GetMultiplikator(level, 1.25f);
                    break;

                case BuildingType.TroopPool:
                    result.Sand = (float)100 * (float)GetMultiplikator(level, 1.25f);
                    result.Leave = (float)100 * (float)GetMultiplikator(level, 1.25f);
                    result.Chitin = (float)20 * (float)GetMultiplikator(level, 1.25f);
                    result.Larvae = (float)2 * (float)1f;
                    break;

                case BuildingType.Stock:
                    result.Sand = (float)20 * (float)GetMultiplikator(level, 1.25f);
                    result.Leave = (float)20 * (float)GetMultiplikator(level, 1.25f);
                    result.Chitin = (float)5 * (float)GetMultiplikator(level, 1.25f);
                    result.Larvae = (float)1 * (float)1f;
                    break;

                case BuildingType.Warehouse:
                    result.Sand = (float)20 * (float)GetMultiplikator(level, 1.25f);
                    result.Leave = (float)20 * (float)GetMultiplikator(level, 1.25f);
                    result.Chitin = (float)5 * (float)GetMultiplikator(level, 1.25f);
                    result.Larvae = (float)1 * (float)1f;
                    break;

                case BuildingType.QueenLair:
                    result.Sand = (float)150 * (float)GetMultiplikator(level, 1.3f);
                    result.Leave = (float)150 * (float)GetMultiplikator(level, 1.3f);
                    result.Chitin = (float)50 * (float)GetMultiplikator(level, 1.3f);
                    result.Larvae = (float)5 * (float)1f;
                    break;

                case BuildingType.Tower:
                    result.Sand = (float)300;
                    result.Leave = (float)300;
                    result.Chitin = (float)100;
                    result.Larvae = (float)15;
                    break;
            }

            return result;

        }

        public static float GetProduction(Building building)
        {
            double multiplikator = 1;

            switch (building.Type)
            {
                case BuildingType.AphidBreed:
                    return 20 * (float)GetMultiplikator(building.Level, 1.5f);

                case BuildingType.Sandpit:
                    return 20 * (float)GetMultiplikator(building.Level, 1.5f);

                case BuildingType.Garden:
                    return 20 * (float)GetMultiplikator(building.Level, 1.5f);

                case BuildingType.MushroomFarm:
                    return 20 * (float)GetMultiplikator(building.Level, 1.5f);

                case BuildingType.BeetleBreed:
                    return 15 * (float)GetMultiplikator(building.Level, 1.5f);

                case BuildingType.QueenLair:
                    return 10 * (float)GetMultiplikator(building.Level, 1.5f);

                default:
                    return 0;
            }
            
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