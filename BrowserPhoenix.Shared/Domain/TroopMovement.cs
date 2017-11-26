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
    public class TroopMovement
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

        [Column("type")]
        public TroopMovementType Type { get; set; }

        [Column("player_id")]
        public Int64 PlayerId { get; set; }

        [Column("count")]
        public Int32 Count { get; set; }

        [Ignore]
        [Column("timer_id")]
        public Timer Timer { get; set; }

        public static String JoinTimer()
        {
            return " LEFT JOIN timer ON troop_movement.id = timer.ref_id and timer.ref_type = 40 ";
        }

        public static TroopMovement Create(Database db, DateTime createDate, Int64 playerId, IEnumerable<Troop> selectedTroops, Int32 startX, Int32 startY, Int32 targetX, Int32 targetY, TroopMovementType type )
        {
            var moveTime = TroopHelper.GetMoveTime(selectedTroops, startX, startY, targetX, targetY);


            //switch from collection to actual troop list
            
            var arrivalDate = createDate.Add(moveTime);

            

            var result = new TroopMovement();
            result.CreateDate = createDate;
            result.StartX = startX;
            result.StartY = startX;
            result.TargetX = targetX;
            result.TargetY = targetY;
            result.ArrivalDate = arrivalDate;
            result.Troops = selectedTroops.Select(x => x.Id).ToArray();
            result.Type = type;
            result.PlayerId = playerId;
            result.Count = selectedTroops.Sum(x => x.Amount);
            //save move
            db.Save(result);

            var timer = Timer.Create(db, playerId, TimerType.TroopMovement, RefType.TroopMovement, result.Id, result.ArrivalDate);
          
            foreach (var troop in selectedTroops)
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
                    .Append("WHERE troop.id in (@0) ", this.Troops)
                    );
            
            return troops;
        }

        public static TroopMovement GetById(Database db, Int64 id)
        {
            var movementList = db.Fetch<TroopMovement, Timer>(
                    Sql.Builder
                    .Append("SELECT * FROM troop_movement")
                    .Append(TroopMovement.JoinTimer())
                    .Append("WHERE troop_movement.id =@0 ", id)
                    );

            var movement = movementList.First();

            return movement;
        }


    }

}