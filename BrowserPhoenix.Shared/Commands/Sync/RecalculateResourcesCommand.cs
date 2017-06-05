using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Commands.Sync
{
    [Serializable]
    public class RecalculateResourcesCommand : BaseCommand, ICommand
    {
        public DateTime TimeOfHappening { get; set; }
        public Int64 ColonyId { get; set; }
        
        public RecalculateResourcesCommand(Int64 colonyId, DateTime timeOfHappening)
        {
            TimeOfHappening = timeOfHappening;
            ColonyId = colonyId;
        }

        public void Process()
        {
            using (var portal = DatabasePortal.Open())
            {
                //check if field is available

                var colony = Colony.GetById(portal, ColonyId);
                colony.UpdateResources(portal, this.TimeOfHappening);
            }
        }
    }
}