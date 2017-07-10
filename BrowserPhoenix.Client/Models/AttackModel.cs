using BrowserPhoenix.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Client.Models
{
    public class AttackModel
    {
        public Int32 StartX { get; set; }
        public Int32 StartY { get; set; }

        public Int32 TargetX { get; set; }
        public Int32 TargetY { get; set; }

        public IEnumerable<Tuple<Int64, String>> PlayerColonies { get; set; }
        public TroopCollection AvailableTroops { get; set; }
        public TroopCollection SelectedTroops { get; set; }

        public String TargetName { get; set; }
        public Int64? TargetId { get; set; }

    }
    
}