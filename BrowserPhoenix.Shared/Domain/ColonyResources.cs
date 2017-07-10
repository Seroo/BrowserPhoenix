using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Domain
{
    [Serializable]
    [TableName("colony_resource")]
    [PrimaryKey("id")]
    public class ColonyResource
    {
        [Column("id")]
        public Int64 Id { get; set; }
    
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    
        [Column("sand")]
        public float Sand { get; set; }

        [Column("sand_production")]
        public float SandProduction { get; set; }

        [Column("sugar")]
        public float Sugar { get; set; }

        [Column("sugar_production")]
        public float SugarProduction { get; set; }

        [Column("chitin")]
        public float Chitin { get; set; }

        [Column("chitin_production")]
        public float ChitinProduction { get; set; }

        [Column("leave")]
        public float Leave { get; set; }

        [Column("leave_production")]
        public float LeaveProduction { get; set; }

        [Column("larvae")]
        public float Larvae { get; set; }

        [Column("larvae_production")]
        public float LarvaeProduction { get; set; }

        [Column("food")]
        public float Food { get; set; }

        [Column("food_production")]
        public float FoodProduction { get; set; }


        public static ColonyResource Create(Database portal)
        {
            var result = new ColonyResource();
            result.LastUpdate = DateTime.Now;
            portal.Save(result);

            return result;
        }

        public ColonyResource AddStartingResources(Database db)
        {
            this.Larvae = 5;
            this.Leave = 50;
            this.Sand = 50;
            this.Sugar = 20;
            this.Chitin = 0;

            db.Save(this);
            return this;
        }

        public static ColonyResource GetById(Database db, Int64 id)
        {
            var resourcesList = db.Fetch<ColonyResource>(
                    Sql.Builder
                    .Append("SELECT * FROM colony_resource ")
                    .Append("WHERE colony_resource.id =@0 ", id)
                    );

            return resourcesList.First();
        }

        public void RecalculateResources(Database portal, DateTime timeOfHappening)
        {
            var passedTime = timeOfHappening - LastUpdate;

            this.Sand += (float)passedTime.TotalHours * SandProduction;
            this.Sugar += (float)passedTime.TotalHours * SugarProduction;
            this.Food += (float)passedTime.TotalHours * FoodProduction;
            this.Chitin += (float)passedTime.TotalHours * ChitinProduction;
            this.Leave += (float)passedTime.TotalHours * LeaveProduction;
            this.Larvae += (float)passedTime.TotalHours * LarvaeProduction;

            this.LastUpdate = timeOfHappening;

            portal.Save(this);
        }

        public void RecalculateResourceProduction(Database portal, DateTime timeOfHappening, IEnumerable<Building> buildings)
        {
            var startDate = this.LastUpdate;
            var endDate = timeOfHappening;

            this.FoodProduction = 0;
            this.ChitinProduction = 0;
            this.LarvaeProduction = 0;
            this.LeaveProduction = 0;
            this.SugarProduction = 0;
            this.SandProduction = 0;

            foreach (var building in buildings)
            {
                switch (building.Type)
                {
                    case BuildingType.MushroomFarm:
                        FoodProduction += BuildingHelper.GetProduction(building); 
                        break;

                    case BuildingType.Sandpit:
                        SandProduction += BuildingHelper.GetProduction(building);
                        break;

                    case BuildingType.BroodLair:
                        LarvaeProduction += BuildingHelper.GetProduction(building);
                        break;

                    case BuildingType.Garden:
                        LeaveProduction += BuildingHelper.GetProduction(building);
                        break;

                    case BuildingType.AphidBreed:
                        SugarProduction += BuildingHelper.GetProduction(building);
                        break;

                    case BuildingType.QueenLair:
                        LarvaeProduction += BuildingHelper.GetProduction(building);
                        break;
                }
            }

            this.LastUpdate = timeOfHappening;
            portal.Save(this);
        }

        public Boolean CheckAvailable(ResourceCollection resources)
        {
            var result = true;

            if (this.Sand < resources.Sand)
                result = false;

            if (this.Sugar < resources.Sugar)
                result = false;

            if (this.Leave < resources.Leave)
                result = false;

            if (this.Food < resources.Food)
                result = false;

            if (this.Chitin < resources.Chitin)
                result = false;

            if (this.Larvae < resources.Larvae)
                result = false;


            return result;
        }

        public void RemoveResources(Database db, ResourceCollection resources)
        {
            this.Sand -= resources.Sand;
            this.Sugar -= resources.Sugar;
            this.Larvae -= resources.Larvae;
            this.Leave -= resources.Leave;
            this.Chitin -= resources.Chitin;
            this.Food -= resources.Food;

            db.Save(this);
        }

        public float GetProductionPerTenSeconds(ResourceType type)
        {
            switch(type)
            {
                case ResourceType.Chitin:
                    if (ChitinProduction == 0)
                        return 0;

                    return ChitinProduction / 360;
                case ResourceType.Food:
                    if (FoodProduction == 0)
                        return 0;

                    return FoodProduction / 360;
                case ResourceType.Larvae:
                    if (LarvaeProduction == 0)
                        return 0;

                    return LarvaeProduction / 360;
                case ResourceType.Leave:
                    if (LeaveProduction == 0)
                        return 0;

                    return LeaveProduction / 360;
                case ResourceType.Sand:
                    if (SandProduction == 0)
                        return 0;

                    return SandProduction / 360;
                case ResourceType.Sugar:
                    if (SugarProduction == 0)
                        return 0;

                    return SugarProduction / 360;
                default:
                    return 0;
            }
        }
    }

}