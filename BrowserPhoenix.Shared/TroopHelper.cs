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

        public static TroopAttackValues GetAttack(Troop troop)
        {
            var result = new TroopAttackValues();

        
            switch (troop.Type)
            {
                case TroopType.Worker:
                    result.Pierce = 2;
                    result.Blunt = 2;
                    result.Acid = 2;
                    result.Slash = 2;

                    break;

                case TroopType.BullAnt:
                    result.Pierce = 0;
                    result.Blunt = 50;
                    result.Acid = 0;
                    result.Slash = 0;

                    break;

                case TroopType.FireAnt:
                    result.Pierce = 0;
                    result.Blunt = 0;
                    result.Acid = 20;
                    result.Slash = 0;

                    break;

                case TroopType.Hunter:
                    result.Pierce = 20;
                    result.Blunt = 0;
                    result.Acid = 0;
                    result.Slash = 0;

                    break;

                case TroopType.Warrior:
                    result.Pierce =0 ;
                    result.Blunt = 10;
                    result.Acid = 0;
                    result.Slash = 0;

                    break;

                case TroopType.Nemesis:
                    result.Pierce = 0;
                    result.Blunt = 50;
                    result.Acid = 0;
                    result.Slash = 0;

                    break;

                case TroopType.Reaper:
                    result.Pierce = 0;
                    result.Blunt = 0;
                    result.Acid = 0;
                    result.Slash = 30;

                    break;

                case TroopType.Marksman:
                    result.Pierce = 4;
                    result.Blunt = 0;
                    result.Acid = 0;
                    result.Slash = 0;

                    break;

                case TroopType.SwarmGuard:
                    result.Pierce = 0;
                    result.Blunt = 0;
                    result.Acid = 8;
                    result.Slash = 0;

                    break;

                case TroopType.HeavyWarrior:
                    result.Pierce = 0;
                    result.Blunt = 2;
                    result.Acid = 0;
                    result.Slash = 0;

                    break;

                case TroopType.TowerGuard:
                    result.Pierce = 0;
                    result.Blunt = 0;
                    result.Acid = 0;
                    result.Slash = 4;
                    break;

                case TroopType.Guard:
                    result.Pierce = 0;
                    result.Blunt = 3;
                    result.Acid = 0;
                    result.Slash = 0;

                    break;

            }

            return result;
        }

        public static TroopDefenseValues GetDefense(Troop troop)
        {
            var result = new TroopDefenseValues();


            switch (troop.Type)
            {
                case TroopType.Worker:
                    result.Pierce = 2;
                    result.Blunt = 2;
                    result.Acid = 2;
                    result.Slash = 2;

                    break;

                case TroopType.BullAnt:
                    result.Pierce = 2;
                    result.Blunt = 4;
                    result.Acid = 2;
                    result.Slash = 2;

                    break;

                case TroopType.FireAnt:
                    result.Pierce = 2;
                    result.Blunt = 2;
                    result.Acid = 20;
                    result.Slash = 3;

                    break;

                case TroopType.Hunter:
                    result.Pierce = 3;
                    result.Blunt = 2;
                    result.Acid = 5;
                    result.Slash = 2;

                    break;

                case TroopType.Warrior:
                    result.Pierce = 3;
                    result.Blunt = 3;
                    result.Acid = 3;
                    result.Slash = 3;

                    break;

                case TroopType.Nemesis:
                    result.Pierce = 2;
                    result.Blunt = 4;
                    result.Acid = 2;
                    result.Slash = 2;

                    break;

                case TroopType.Reaper:
                    result.Pierce = 3;
                    result.Blunt = 3;
                    result.Acid = 3;
                    result.Slash = 3;

                    break;

                case TroopType.Marksman:
                    result.Pierce = 20;
                    result.Blunt = 2;
                    result.Acid = 2;
                    result.Slash = 2;

                    break;

                case TroopType.SwarmGuard:
                    result.Pierce = 2;
                    result.Blunt = 2;
                    result.Acid = 75;
                    result.Slash = 2;

                    break;

                case TroopType.HeavyWarrior:
                    result.Pierce = 2;
                    result.Blunt = 10;
                    result.Acid = 2;
                    result.Slash = 2;

                    break;

                case TroopType.TowerGuard:
                    result.Pierce = 3;
                    result.Blunt = 3;
                    result.Acid = 3;
                    result.Slash = 15;
                    break;

                case TroopType.Guard:
                    result.Pierce = 10;
                    result.Blunt = 40;
                    result.Acid = 10;
                    result.Slash = 10;

                    break;

            }

            return result;
        }

        public static TimeSpan GetMoveTime(IEnumerable<Troop> troops, Int32 startX, Int32 startY, Int32 targetX, Int32 targetY)
        {
            //placeholder
            var distance = Math.Sqrt((Math.Pow(startX - startY, 2) + Math.Pow(targetX - targetY, 2)));

            var distanceInTime = 10 * distance;

            return new TimeSpan(0, 0, Int32.Parse(distanceInTime.ToString()));

         //   return new TimeSpan(0, 1, 15);
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

        //sehr sicher noch änderungen bezüglich colony usw
        //wall z.b.
        public static Tuple<IEnumerable<Troop>, IEnumerable<Troop>, BattleReport> Fight(IEnumerable<Troop> defender, IEnumerable<Troop> attacker, BattleReport report)
        {
            var attackingsTroops = attacker.ToArray();
            var defendingTroops = defender.ToArray();
            
            var currentAttackerGroup = 0;
            
            foreach (var defendingAnt in defendingTroops)
            {
                var maxCount = 10;
                var count = 0;
                var survivingDefenderAmount = defendingAnt.Amount;
                while (survivingDefenderAmount > 0 && count < maxCount)
                {
                    count++;
                    if (currentAttackerGroup >= attackingsTroops.Length)
                        continue;

                    var attackingAnt = attackingsTroops[currentAttackerGroup];

                    var attackerStartAmount = attackingAnt.Amount;
                    var defenderStartAmount = defendingAnt.Amount;
                    

                    var survivingAttackAmount = attackingAnt.CalculateSurvivorAgainst(defendingAnt);
                    var defendingSurvivorAmountTemp = defendingAnt.CalculateSurvivorAgainst(attackingAnt);
                    if (survivingAttackAmount == 0)
                        currentAttackerGroup++;

                    attackingAnt.Amount = survivingAttackAmount;
                    defendingAnt.Amount = defendingSurvivorAmountTemp;
                    survivingDefenderAmount = defendingSurvivorAmountTemp;


                    report = report.AddRound(count, attackingAnt.Type, attackerStartAmount, survivingAttackAmount, defendingAnt.Type, defenderStartAmount, defendingSurvivorAmountTemp);
                }

            }

            return new Tuple<IEnumerable<Troop>, IEnumerable<Troop>, BattleReport>(defendingTroops, attackingsTroops, report);
        }
    }
}