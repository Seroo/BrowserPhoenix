﻿using PetaPoco;
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

        [Column("is_busy")]
        public Boolean IsBusy { get; set; }

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

        public static TroopCollection GetInactiveTroopsByColony(Database db, Int64 colonyId)
        {
            var troops = db.Fetch<Troop, Colony, Timer>(
                    Sql.Builder
                    .Append("SELECT * FROM troop")
                    .Append(Troop.JoinColony())
                    .Append(Troop.JoinTimer())
                    .Append("WHERE troop.colony_id =@0 ", colonyId)
                    .Append("AND troop.is_busy =@0 ", false)
                    );

            return TroopCollection.Create(troops);
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
                    .Append(" AND troop.is_busy =@0 ", false)
                    );

            if(troops.Any())
            {
                return troops.First();
            }
            else
            {
                return null;
            }
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
        
        public static Troop Create(Database portal, DateTime createDate, Int64 buildingId, Int64 colonyId, TroopType type, Int32 amount, Boolean isBusy = false)
        {
            var result = new Troop();
            
            result.ColonyId = colonyId;
            result.BuildingId = buildingId;
            result.Type = type;
            
            result.CreateDate = createDate;
            result.IsBusy = isBusy;
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