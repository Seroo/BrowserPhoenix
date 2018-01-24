using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Domain
{
    [Serializable]
    [TableName("troop")]
    [PrimaryKey("id")]
    public class Troop
    {
        [Column("id")]
        public Int64 Id { get; set; }

        [Column("type")]
        public TroopType Type { get; set; }

        [Column("create_date")]
        public DateTime CreateDate { get; set; }

        [Column("amount")]
        public Int32 Amount { get; set; }

        [Column("building_id")]
        public Int64 BuildingId { get; set; }

        [Column("colony_id")]
        public Int64 ColonyId { get; set; }

        [Ignore]
        [Column("colony_id")]
        public Colony Colony { get; set; }

        [Ignore]
        [Column("timer_id")]
        public Timer Timer { get; set; }

        [Column("status")]
        public TroopStatus Status { get; set; }

        [Ignore]
        public TroopDefenseValues Defense
        {
            get
            {
                return TroopHelper.GetDefense(this);
            }
        }
        [Ignore]
        public TroopAttackValues Attack
        {
            get
            {
                return TroopHelper.GetAttack(this);
            }
        }

        public static String JoinColony()
        {
            return " LEFT JOIN colony ON troop.colony_id = colony.id ";
        }

        public static String JoinTimer()
        {
            return " LEFT JOIN timer ON troop.id = timer.ref_id and timer.ref_type = 30";
        }

        public static IEnumerable<Troop> GetByColonyId(Database db, Int64 id)
        {
            var troops = db.Fetch<Troop, Colony, Timer>(
                    Sql.Builder
                    .Append("SELECT * FROM troop")
                    .Append(Troop.JoinColony())
                    .Append(Troop.JoinTimer())
                    .Append("WHERE troop.colony_id =@0 ", id)
                    );
            
            return troops;
        }

        public static TroopCollection GetInactiveTroopCollectionByColony(Database db, Int64 colonyId)
        {
            var troops = db.Fetch<Troop, Colony, Timer>(
                    Sql.Builder
                    .Append("SELECT * FROM troop")
                    .Append(Troop.JoinColony())
                    .Append(Troop.JoinTimer())
                    .Append("WHERE troop.colony_id =@0 ", colonyId)
                    .Append("AND troop.status=@0 ", TroopStatus.Idle)
                    );

            return TroopCollection.Create(troops);
        }


        public static IEnumerable<Troop> GetInactiveTroopsByColony(Database db, Int64 colonyId)
        {
            var troops = db.Fetch<Troop, Colony, Timer>(
                    Sql.Builder
                    .Append("SELECT * FROM troop")
                    .Append(Troop.JoinColony())
                    .Append(Troop.JoinTimer())
                    .Append("WHERE troop.colony_id =@0 ", colonyId)
                    .Append("AND troop.status=@0 ", TroopStatus.Idle)
                    );

            return troops;
        }

        public static Troop GetInactiveTroopByType(Database db, Int64 colonyId, TroopType type)
        {
            var troops = db.Fetch<Troop, Colony, Timer>(
                    Sql.Builder
                    .Append("SELECT * FROM troop")
                    .Append(Troop.JoinColony())
                    .Append(Troop.JoinTimer())
                    .Append("WHERE troop.colony_id =@0 ", colonyId)
                    .Append(" AND troop.type=@0 ", type)
                    .Append(" AND troop.status =@0 ", TroopStatus.Idle)
                    );
            return troops.FirstOrDefault();
            
        }
        
        public static Troop GetById(Database db, Int64 id)
        {
            var troops = db.Fetch<Troop, Colony>(
                    Sql.Builder
                     .Append("SELECT * FROM troop")
                    .Append(Troop.JoinColony())
                    .Append("WHERE troop.id =@0 ", id)
                    );

            var troop = troops.First();

            return troop;
        }

        public static IEnumerable<Troop> GetByIds(Database db, IEnumerable<Int64> troopIds)
        {
            var troops = db.Fetch<Troop, Colony>(
                    Sql.Builder
                    .Append("SELECT * FROM troop")
                    .Append(Troop.JoinColony())
                    .Append("WHERE troop.id in (@0) ", troopIds)
                    );

            return troops;
        }

        public static Troop Create(Database portal, DateTime createDate, Int64 buildingId, Int64 colonyId, TroopType type, Int32 amount, TroopStatus status = TroopStatus.Idle)
        {
            var result = new Troop();
            
            result.ColonyId = colonyId;
            result.BuildingId = buildingId;
            result.Type = type;
            
            result.CreateDate = createDate;
            result.Status = status;
            result.Amount = amount;

            portal.Save(result);

            return result;
        }
        
        public void AddUnit(Database portal, DateTime createDate)
        {
            this.Amount = this.Amount + 1;

            portal.Save(this);
        }

        public void AddTimer(Database portal, Timer timer)
        {
            this.Timer = timer;

            portal.Save(this);
        }


        public Boolean HasTimer()
        {
            return this.Timer.Id != 0;
        }
        
        public Int32 CalculateSurvivorAgainst(Troop attacker)
        {
            var pierceDefValue = this.Defense.Pierce * this.Amount;
            var bluntDefValue = this.Defense.Blunt * this.Amount;
            var acidDefValue = this.Defense.Acid * this.Amount;
            var slashDefValue = this.Defense.Slash * this.Amount;

            var pierceAttackValue = attacker.Attack.Pierce * attacker.Amount;
            var bluntAttackValue = attacker.Attack.Blunt * attacker.Amount;
            var acidAttackValue = attacker.Attack.Acid * attacker.Amount;
            var slashAttackValue = attacker.Attack.Slash * attacker.Amount;


            var pierceSurvivingDefense = pierceDefValue - pierceAttackValue;
            var bluntSurivingDefense = bluntDefValue - bluntAttackValue;
            var acidSurvivingDefense = acidDefValue - acidAttackValue;
            var slashSurvivingDefense = slashDefValue - slashAttackValue;

            var aliveAnts = this.Amount;
            if (pierceSurvivingDefense < pierceDefValue)
            {
                //beispiel
                //10 attacker mit je 2 schaden
                //15 verteidiger mit je 2 def
                //pierfDefValue = 30
                //pierceSurvivingDefense = 10 (attacker hat 20 schaden)
                //X = x - ( 30 - ( 10 / 2))
                //x = x - (ALLE am lebenden - ( überlebende punkte / def = überlebende einheiten))

                //aliveAnts = aliveAnts + (pierceDefValue - (pierceSurvivingDefense / this.Defense.Pierce));
                aliveAnts = aliveAnts - ((pierceDefValue - pierceSurvivingDefense) / this.Defense.Pierce);
            }

            if (bluntSurivingDefense < bluntDefValue)
            {
                aliveAnts = aliveAnts - ((bluntDefValue - bluntSurivingDefense) / this.Defense.Blunt);
            }

            if (acidSurvivingDefense < acidDefValue)
            {
                aliveAnts = aliveAnts - ((acidDefValue - acidSurvivingDefense) / this.Defense.Acid);
            }

            if (slashSurvivingDefense < slashDefValue)
            {
                aliveAnts = aliveAnts - ((slashDefValue - slashSurvivingDefense) / this.Defense.Slash);
            }

            if (aliveAnts > 0)
                return aliveAnts;
            else
                return 0;
        }

        public Int32 CalculateSurvivorad(Troop defender)
        {
            var pierceDefValue = defender.Defense.Pierce * defender.Amount;
            var bluntDefValue = defender.Defense.Blunt * defender.Amount;
            var acidDefValue = defender.Defense.Acid * defender.Amount;
            var slashDefValue = defender.Defense.Slash * defender.Amount;

            var pierceAttackValue = this.Attack.Pierce * this.Amount;
            var bluntAttackValue = this.Attack.Blunt * this.Amount;
            var acidAttackValue = this.Attack.Acid * this.Amount;
            var slashAttackValue = this.Attack.Slash * this.Amount;


            var pierceSurvivingDefense = pierceDefValue - pierceAttackValue;
            var bluntSurivingDefense = bluntDefValue - bluntAttackValue;
            var acidSurvivingDefense = acidDefValue - acidAttackValue;
            var slashSurvivingDefense = slashDefValue - slashAttackValue;

            //check pierce losses
            var aliveAnts = defender.Amount;
            if (pierceSurvivingDefense < pierceDefValue)
            {
                //beispiel
                //10 verteidiger mit je 2 schaden
                //15 verteidiger mit je 2 def
                //pierfDefValue = 30
                //pierceSurvivingDefense = 10 (attacker hat 20 schaden)
                //X = x - ( 30 - ( 10 / 2))
                //x = x - (ALLE am lebenden - ( überlebende punkte / def = überlebende einheiten))

                //aliveAnts = aliveAnts + (pierceDefValue - (pierceSurvivingDefense / this.Defense.Pierce));
                aliveAnts = aliveAnts - ((pierceDefValue - pierceSurvivingDefense) / defender.Defense.Pierce);
            }

            if (bluntSurivingDefense < bluntDefValue)
            {
                aliveAnts = aliveAnts - ((bluntDefValue - bluntSurivingDefense) / defender.Defense.Blunt);
            }

            if (acidSurvivingDefense < acidDefValue)
            {
                aliveAnts = aliveAnts - ((acidDefValue - acidSurvivingDefense) / defender.Defense.Acid);
            }

            if (slashSurvivingDefense < slashDefValue)
            {
                aliveAnts = aliveAnts - ((slashDefValue - slashSurvivingDefense) / defender.Defense.Slash);
            }

            if (aliveAnts > 0)
                return aliveAnts;
            else
                return 0;
        }


        /*
        public ColonyResource RecalculateResources(Database db, ColonyResource resources, DateTime startDate, DateTime endDate)
        {

            var production = BuildingHelper.GetProduction(this);

            var passedTime = endDate - startDate;

            var generatedResources = (float)passedTime.TotalHours* production;

            
            switch(this.Type)
            {
                case BuildingType.MushroomFarm:
                    resources.Food += generatedResources;
                    break;

                case BuildingType.Sandpit:
                    resources.Sand += generatedResources;
                    break;

                case BuildingType.BroodLair:
                    resources.Larvae += generatedResources;
                    break;

                case BuildingType.Garden:
                    resources.Leave += generatedResources;
                    break;

                case BuildingType.AphidsBreed:
                    resources.Sugar += generatedResources;
                    break;
            }

            return resources;
        }*/
    }

}