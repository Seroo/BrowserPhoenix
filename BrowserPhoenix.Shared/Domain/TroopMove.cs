using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Domain
{
    [Serializable]
    [TableName("troop_movement")]
    [PrimaryKey("id")]
    public class TroopMove
    {
        [Column("id")]
        public Int64 Id { get; set; }

        [Column("troops")]
        public Int64[] Troops { get; set; }

        [Column("create_date")]
        public DateTime CreateDate { get; set; }

        [Column("arrival_date")]
        public DateTime ArrivalDate { get; set; }

        [Column("start_x")]
        public Int32 StartX { get; set; }
        
        [Column("start_y")]
        public Int32 StartY { get; set; }

        [Column("target_x")]
        public Int32 TargetX { get; set; }
      
        [Column("target_y")]
        public Int32 TargetY { get; set; }

        
        public static TroopMove Create(Database db, DateTime createDate, DateTime arrivalDate, Int64 playerId, IEnumerable<Troop> troops, Int32 startX, Int32 startY, Int32 targetX, Int32 targetY )
        {
            var result = new TroopMove();
            result.CreateDate = createDate;
            result.StartX = startX;
            result.StartY = startX;
            result.TargetX = targetX;
            result.TargetY = targetY;
            result.ArrivalDate = arrivalDate;
            result.Troops = troops.Select(x => x.Id).ToArray();

            //save move
            db.Save(result);

            var timer = Timer.Create(db, playerId, TimerType.TroopMovement, RefType.Troop, result.Id, result.ArrivalDate);

            foreach(var troop in troops)
            {
                troop.AddTimer(db, timer);
            }

            

            return result;
        }

        public IEnumerable<Troop> GetTroops(Database db)
        {
            var troops = db.Fetch<Troop, Timer>(
                    Sql.Builder
                    .Append("SELECT * FROM troop")
                    .Append(Troop.JoinTimer())
                    .Append("WHERE troop.id in (=@0) ", String.Join(",", this.Troops))
                    );
            
            return troops;
        }

     
    }

}