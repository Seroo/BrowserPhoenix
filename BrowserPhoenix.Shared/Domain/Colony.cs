using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Domain
{
    [Serializable]
    [TableName("colony")]
    [PrimaryKey("id")]
    public class Colony
    {
        [Column("id")]
        public Int64 Id { get; set; }
        [Column("name")]
        public String Name { get; set; }
        [Column("create_date")]
        public DateTime CreateDate { get; set; }
        [Column("x_cord")]
        public Int32 XCord { get; set; }
        [Column("y_cord")]
        public Int32 YCord { get; set; }

        [Column("player_id")]
        public Int64 PlayerId { get; set; }

        [Ignore]
        [Column("player_id")]
        public Player Player { get; set; }

        [Column("resource_id")]
        public Int64 ResourceId { get; set; }

        [Ignore]
        [Column("resource_id")]
        public ColonyResource Resources { get; set; }

        public static String JoinPlayer()
        {
            return " LEFT JOIN player ON colony.player_id = player.id ";
        }

        public static String JoinResources()
        {
            return " LEFT JOIN colony_resource ON colony.resource_id = colony_resource.id ";
        }

        public Boolean IsOwnedByCurrentPlayer()
        {
            return this.PlayerId == Player.Current.Id;
        }

        public static Colony GetById(Database db, Int64 id)
        {
            var colonyList = db.Fetch<Colony, Player, ColonyResource>(
                    Sql.Builder
                    .Append("SELECT * FROM Colony")

                    .Append(Colony.JoinPlayer())
                    .Append(Colony.JoinResources())
                    .Append("WHERE colony.id =@0 ", id)
                    );

            var colony = colonyList.First();

            return colony;
        }

        public static Colony GetPlayersFirst(Database db, Int64 playerId)
        {
            var colony = db.Fetch<Colony, Player, ColonyResource>(
                    Sql.Builder
                    .Append("SELECT * FROM Colony")
                    .Append(Colony.JoinPlayer())
                    .Append(Colony.JoinResources())
                    .Append("WHERE player.id =@0 ", playerId)
                    .Append(" order by colony.create_date asc")
                    .Append("limit 1")
                    )
                    .FirstOrDefault();

          
            return colony;
        }

        public static IEnumerable<Colony> GetByPlayerId(Database db, Int64 playerId)
        {
            return db.Fetch<Colony, Player, ColonyResource>(
                    Sql.Builder
                    .Append("SELECT * FROM Colony")
                    .Append(Colony.JoinPlayer())
                    .Append(Colony.JoinResources())
                    .Append("WHERE player.id =@0 ", playerId)
                    );
        }

        public static Colony GetByCoordinates(Database db, Int32 x, Int32 y)
        {
            var colonyList = db.Fetch<Colony>(
                    Sql.Builder
                    .Append("SELECT * FROM Colony")
                    .Append("WHERE Colony.x_cord =@0 ", x)
                    .Append("And Colony.y_cord =@0 ", y)
                    );

            return colonyList.FirstOrDefault();
        }


        public static IEnumerable<Tuple<Int64, String>> GetNameTupleList(Database db, Int64 playerId)
        {
            var result = db.Fetch<Colony>(
                    Sql.Builder
                    .Append("SELECT * FROM Colony")
                    .Append(Colony.JoinPlayer())
                    .Append("WHERE player.id =@0 ", playerId)
                    );

            return result.Select(x => new Tuple<Int64, String>(x.Id, x.Name));
        }

        public static Colony Create(Database portal, Int64 playerId, String coloynName, Int32 xCord, Int32 yCord)
        {
            var result = new Colony();
            
            result.PlayerId = playerId;
            result.XCord = xCord;
            result.YCord = yCord;
            result.CreateDate = DateTime.Now;
            result.Name = coloynName;
            

            var resources = ColonyResource.Create(portal);

            result.ResourceId = resources.Id;

            portal.Save(result);

            return result;
        }

        public void UpdateResources(Database db, DateTime timeOfHappening)
        {
            if(this.Resources != null)
            {
                this.Resources.RecalculateResources(db, timeOfHappening);
            }
            
        }
        
        public void UpdateResourceProduction(Database db, DateTime timeOfHappening)
        {
            var buildings = Building.GetByColonyId(db, this.Id);
            
            var resourceBuildings = buildings.Where(x => x.IsResourceBuilding == true).ToList();

            this.Resources.RecalculateResourceProduction(db, timeOfHappening, resourceBuildings);
        }

        public Boolean CheckResourcesAvailable(BuildingType type, Int32 level)
        {
            var costs = BuildingHelper.GetBuildCost(type, level);

            return this.Resources.CheckAvailable(costs);
        }

        public Boolean CheckResourcesAvailable(Int64 buildingId)
        {
            using (var db = DatabasePortal.Open())
            {
                var building = Building.GetById(db, buildingId);

                return CheckResourcesAvailable(building.Type, building.Level + 1);
            }
        }

        public void RemoveResources(Database db, ResourceCollection resources)
        {
            var colonyResources = ColonyResource.GetById(db, this.ResourceId);

            colonyResources.RemoveResources(db, resources);
        }

        public Troop CreateMovingTroop(Database db, TroopType type, Int32 amount)
        {
            var inactiveTroop = Troop.GetInactiveTroopByType(db, this.Id, type);

            inactiveTroop.Amount = inactiveTroop.Amount - amount;

            db.Save(inactiveTroop);

            return Troop.Create(db, CreateDate, 0, 0, type, amount, TroopStatus.Busy);

        }

        public void ReturnMovingTroop(Database db, IEnumerable<Troop> troops)
        {
            foreach (var troop in troops)
            {
                var inactiveTroop = Troop.GetInactiveTroopByType(db, this.Id, troop.Type);
                if (inactiveTroop == null)
                {
                    //WTF!= jemand hat gecheatet!!!
                    var testpoint = "";
                }

                inactiveTroop.Amount += troop.Amount;

                db.Save(inactiveTroop);
                db.Delete(troop);
            }
        }

        public void SetSurvivingTroops(Database db, IEnumerable<Troop> troops)
        {
            foreach(var troop in troops)
            {
                db.Save(troop);
            }
            //TODO
        }
    }

}