using BrowserPhoenix.Shared.Commands;
using BrowserPhoenix.Shared.Commands.Sync;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Domain
{
    [Serializable]
    [TableName("timer")]
    [PrimaryKey("id")]
    public class Timer
    {
        [Column("id")]
        public Int64 Id { get; set; }
    
        [Column("create_date")]
        public DateTime CreateDate { get; set; }
    
        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("player_id")]
        public Int64 PlayerId { get; set; }

        [Column("type")]
        public TimerType Type { get; set; }

        [Column("ref_type")]
        public RefType RefType { get; set; }

        [Column("ref_id")]
        public Int64 RefId { get; set; }
        
        
        //hier könnte man dann noch felder hinzufügen von möglichen refs
        // z.b. colony oder building
        
        public static Timer Create(Database portal, Int64 playerId, TimerType type, RefType refType, Int64 refId, DateTime endDate)
        {
            var result = new Timer();
            result.Type = type;
            result.RefType = refType;
            result.RefId = refId;
            result.CreateDate = DateTime.Now;
            result.EndDate = endDate;
            result.PlayerId = playerId;

            portal.Save(result);

            return result;
        }

        public static void Check()
        {
            using (var db = DatabasePortal.Open())
            {
                var timer = db.Fetch<Timer>(
                        Sql.Builder
                        .Append("SELECT * FROM Timer")
                        .Append("WHERE timer.end_date < @0 ", DateTime.Now)
                        .Append(" order by timer.create_date asc")
                        .Append("limit 1")
                        )
                        .FirstOrDefault();

                if(timer != null)
                {
                    TimerProcessor.Process(db, timer);
                    timer.Delete(db);
                }
            }
        }

        public void Delete(Database db)
        {
            db.Execute(Sql.Builder.Append("Delete From Timer Where id = @0", this.Id));
        }
        
        public TimeSpan TimeTillEnd()
        {
            return this.EndDate - DateTime.Now;
        }
    }

}