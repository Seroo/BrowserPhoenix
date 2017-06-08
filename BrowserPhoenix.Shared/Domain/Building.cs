using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Domain
{
    [Serializable]
    [TableName("building")]
    [PrimaryKey("id")]
    public class Building
    {
        [Column("id")]
        public Int64 Id { get; set; }

        [Column("type")]
        public BuildingType Type { get; set; }

        [Column("create_date")]
        public DateTime CreateDate { get; set; }

        [Column("level")]
        public Int32 Level { get; set; }

        [Column("x_cord")]
        public Int32 XCord { get; set; }
        [Column("y_cord")]
        public Int32 YCord { get; set; }

        [Column("colony_id")]
        public Int64 ColonyId { get; set; }

        [Ignore]
        [Column("colony_id")]
        public Colony Colony { get; set; }

        [Ignore]
        [Column("timer_id")]
        public Timer Timer { get; set; }

        public static String JoinColony()
        {
            return " LEFT JOIN colony ON building.colony_id = colony.id ";
        }

        public static String JoinTimer()
        {
            return " LEFT JOIN timer ON building.id = timer.ref_id and timer.ref_type = 20 ";
        }

        public static IEnumerable<Building> GetByColonyId(Database db, Int64 id)
        {
            var buildingList = db.Fetch<Building, Colony, Timer>(
                    Sql.Builder
                    .Append("SELECT * FROM Building")
                    .Append(Building.JoinColony())
                    .Append(Building.JoinTimer())
                    .Append("WHERE building.colony_id =@0 ", id)
                    );
            
            return buildingList;
        }

        public static Building GetById(Database db, Int64 id)
        {
            var buildingList = db.Fetch<Building, Colony>(
                    Sql.Builder
                     .Append("SELECT * FROM Building")
                    .Append(Building.JoinColony())
                    .Append("WHERE building.id =@0 ", id)
                    );

            var building = buildingList.First();

            return building;
        }

        public static Building Create(Database portal, DateTime createDate, Int64 colonyId, BuildingType type, Int32 xCord, Int32 yCord)
        {
            var result = new Building();
            
            result.ColonyId = colonyId;
            result.XCord = xCord;
            result.YCord = yCord;
            result.Type = type;
            
            result.CreateDate = createDate;

            result.Level = 0;

            portal.Save(result);

            return result;
        }
        
        public void LevelUp(Database portal, DateTime createDate)
        {

            //feld wo das level datum gespeichert wird füllen
            //checken wegen max lvl

            this.Level = this.Level + 1;

            portal.Save(this);
        }
        

        public Boolean IsResourceBuilding()
        {
            return Type == BuildingType.MushroomFarm 
                || Type == BuildingType.Sandpit 
                || Type == BuildingType.QueenLair
                || Type == BuildingType.Garden
                || Type == BuildingType.AphidBreed
                || Type == BuildingType.BeetleBreed;
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