using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Commands
{
    [Serializable]
    public class BaseCommand
    {

        //hier muss ich rausfinden wie ich einen constructor schreiben kann der auch für die übergeordneten klassen gillt
        //damit ich dann beim erstellen vom basecommand das Createdate und den current player festlege
        public DateTime CreateDate { get; set; }
        public Int64 PlayerId { get; set; }
    }
}