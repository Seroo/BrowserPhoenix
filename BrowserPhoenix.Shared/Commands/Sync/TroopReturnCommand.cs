using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Commands.Sync
{
    [Serializable]
    public class TroopReturnCommand : BaseCommand, ICommand
    {
        public Int32 XCord { get; set; }
        public Int32 YCord { get; set; }
        public Int64[] TroopIds {get;set;}
        

        public void Process()
        {
            using (var db = DatabasePortal.Open())
            {
                var colony = Colony.GetByCoordinates(db, XCord, YCord);

                var troops = Troop.GetByIds(db, TroopIds);
                colony.ReturnMovingTroop(db, troops);

            }
        }
    }
}