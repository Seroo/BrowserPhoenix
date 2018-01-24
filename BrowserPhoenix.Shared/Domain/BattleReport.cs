using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace BrowserPhoenix.Shared.Domain
{
    [Serializable]
    [TableName("battle_report")]
    [PrimaryKey("id")]
    public class BattleReport
    {
        [Column("id")]
        public Int64 Id { get; set; }

        [Column("create_date")]
        public DateTime CreateDate { get; set; }

        [Column("content")]
        public String Content { get; set; }
        
        [Column("defender_id")]
        public Int64 DefenderId { get; set; }

        [Ignore]
        [Column("defender_id")]
        public Player Defender { get; set; }

        [Column("attacker_id")]
        public Int64 AttackerId { get; set; }

        [Ignore]
        [Column("attacker_id")]
        public Player Attacker { get; set; }

        [Column("colony_id")]
        public Int64 ColonyId { get; set; }

        [Ignore]
        [Column("colony_id")]
        public Colony Colony { get; set; }


        public static String JoinColony()
        {
            return " LEFT JOIN colony ON building.colony_id = colony.id ";
        }

        public static String JoinDefender()
        {
            return " LEFT JOIN player ON battle_report.defender_id = player.id ";
        }

        public static String JoinAttacker()
        {
            return " LEFT JOIN player ON battle_report.attacker_id = player.id ";
        }

      
        public static BattleReport Create(Database portal, Int64 attackerId, Int64 defenderId, Int64 colonyId, DateTime createDate)
        {
            var result = new BattleReport();
            
            result.ColonyId = colonyId;
            result.AttackerId = attackerId;
            result.DefenderId = defenderId;
            result.CreateDate = createDate;
            result.Content = "";


            var attacker = Player.GetById(portal, attackerId);
            var defender = Player.GetById(portal, defenderId);

            var colony = BrowserPhoenix.Shared.Domain.Colony.GetById(portal, colonyId);

            var sb = new StringBuilder();
            sb.AppendLine("Battle Report");
            sb.AppendLine(String.Format("Attacker({0}) VS Defender({1}) at {2} on the {3}", attacker.Username, defender.Username, colony.Name, createDate.ToString()));
            sb.AppendLine("");

            result.Content = sb.ToString();
            portal.Save(result);

            return result;
        }
        
        public BattleReport AddRound(Int32 round, TroopType attackerType, Int32 attackerStartAmount, Int32 attackerEndAmount, TroopType defenderType, Int64 defenderStartAmount, Int32 defenderEndAmount)
        {
            var defenderLosses = defenderStartAmount - defenderEndAmount;
            var attackerLosses = attackerStartAmount - attackerEndAmount;

            var sb = new StringBuilder();
            sb.AppendLine("Round " + round);
            sb.AppendLine(String.Format("Defender has lost {0} of his {1} {2} | Attacker has lost {3} of his {4} {5}", defenderLosses, defenderStartAmount, defenderType, attackerLosses, attackerStartAmount, attackerType));


            this.Content = this.Content + sb.ToString();

            return this;
        }
    }

}