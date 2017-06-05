using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Commands.Sync
{
    [Serializable]
    public class GetPointsCommand : BaseCommand, ICommand
    {
        public Int32 Points { get; set; }
        public Int64 PlayerId { get; set; }

        public void Process()
        {

            //ich muss gucken wie ich das in den server schiebe..
            //oder der server ist nur die laufzeit wärend die hier aufgerufen werden
            using (var db = new PetaPoco.Database("BrowserPhoenixDB"))
            {
                db.Execute("UPDATE public.player SET score = " + Points + " WHERE id=" + PlayerId + ";");
            }
        }
    }
}