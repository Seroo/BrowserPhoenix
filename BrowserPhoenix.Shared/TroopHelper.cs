using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared
{
    public static class TroopHelper
    {
        public static ResourceCollection GetBuildCost(TroopType type, Int32 buildingLevel)
        {
            var result = new ResourceCollection();

            //hack 
            buildingLevel = 1;

            switch (type)
            {
                case TroopType.Worker:
                    result.Larvae = (float)1 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Food = (float)5 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Sugar = (float)5 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Chitin = (float)0 * (float)GetMultiplikator(buildingLevel, 1f);
                   
                    break;

                case TroopType.BullAnt:
                    result.Larvae = (float)5 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Food = (float)40 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Sugar = (float)90 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Chitin = (float)40 * (float)GetMultiplikator(buildingLevel, 1f);

                    break;

                case TroopType.FireAnt:
                    result.Larvae = (float)3 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Food = (float)10 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Sugar = (float)40 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Chitin = (float)15 * (float)GetMultiplikator(buildingLevel, 1f);

                    break;

                case TroopType.Hunter:
                    result.Larvae = (float)2 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Food = (float)20 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Sugar = (float)40 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Chitin = (float)10 * (float)GetMultiplikator(buildingLevel, 1f);

                    break;

                case TroopType.Warrior:
                    result.Larvae = (float)1 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Food = (float)15 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Sugar = (float)10 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Chitin = (float)5 * (float)GetMultiplikator(buildingLevel, 1f);

                    break;

                case TroopType.Nemesis:
                    result.Larvae = (float)5 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Food = (float)40 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Sugar = (float)90 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Chitin = (float)40 * (float)GetMultiplikator(buildingLevel, 1f);

                    break;

                case TroopType.Reaper:
                    result.Larvae = (float)4 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Food = (float)50 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Sugar = (float)40 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Chitin = (float)10 * (float)GetMultiplikator(buildingLevel, 1f);

                    break;

                case TroopType.Marksman:
                    result.Larvae = (float)2 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Food = (float)10 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Sugar = (float)50 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Chitin = (float)10 * (float)GetMultiplikator(buildingLevel, 1f);

                    break;

                case TroopType.SwarmGuard:
                    result.Larvae = (float)8 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Food = (float)35 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Sugar = (float)35 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Chitin = (float)150 * (float)GetMultiplikator(buildingLevel, 1f);

                    break;

                case TroopType.HeavyWarrior:
                    result.Larvae = (float)1 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Food = (float)10 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Sugar = (float)15 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Chitin = (float)5 * (float)GetMultiplikator(buildingLevel, 1f);

                    break;

                case TroopType.TowerGuard:
                    result.Larvae = (float)2 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Food = (float)25 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Sugar = (float)10 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Chitin = (float)15 * (float)GetMultiplikator(buildingLevel, 1f);

                    break;

                case TroopType.Guard:
                    result.Larvae = (float)5 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Food = (float)50 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Sugar = (float)30 * (float)GetMultiplikator(buildingLevel, 1f);
                    result.Chitin = (float)60 * (float)GetMultiplikator(buildingLevel, 1f);

                    break;
                    
            }

            return result;

        }

        public static TimeSpan GetBuildTime(TroopType type, Int32 level)
        {
            switch (type)
            {
                case TroopType.Worker:
                    return TimeSpan.FromMinutes(5 * GetMultiplikator(level, 1f));

                case TroopType.BullAnt:
                    return TimeSpan.FromMinutes(40 * GetMultiplikator(level, 1f));

                case TroopType.FireAnt:
                    return TimeSpan.FromMinutes(25 * GetMultiplikator(level, 1f));

                case TroopType.Hunter:
                    return TimeSpan.FromMinutes(15 * GetMultiplikator(level, 1f));
                    
                case TroopType.Warrior:
                    return TimeSpan.FromMinutes(15 * GetMultiplikator(level, 1f));

                case TroopType.Nemesis:
                    return TimeSpan.FromMinutes(40 * GetMultiplikator(level, 1f));

                case TroopType.Reaper:
                    return TimeSpan.FromMinutes(20 * GetMultiplikator(level, 1f));

                case TroopType.Marksman:
                    return TimeSpan.FromMinutes(15 * GetMultiplikator(level, 1f));

                case TroopType.SwarmGuard:
                    return TimeSpan.FromMinutes(40 * GetMultiplikator(level, 1f));

                case TroopType.HeavyWarrior:
                    return TimeSpan.FromMinutes(15 * GetMultiplikator(level, 1f));

                case TroopType.TowerGuard:
                    return TimeSpan.FromMinutes(8 * GetMultiplikator(level, 1f));

                case TroopType.Guard:
                    return TimeSpan.FromMinutes(40 * GetMultiplikator(level, 1f));
                 
                default:
                    return TimeSpan.FromHours(72);
            }

        }

        public static TimeSpan GetMoveTime(IEnumerable<Troop> troops, Int32 startX, Int32 startY, Int32 targetX, Int32 targetY)
        {
            //placeholder
            return new TimeSpan(0, 8, 30);
        }

        public static float GetMultiplikator(Int32 level, float percentage)
        {
            if (level == 1)
                return 1f;
            var count = 0;
            var multiplikator = 1f;
            while (count++ < level)
            {
                multiplikator = percentage;//multiplikator * percentage;
            }
            return multiplikator;
        }
    }
}