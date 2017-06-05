using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Commands.Sync
{
    [Serializable]
    public class CreateColonyCommand : BaseCommand, ICommand
    {
        public Int32 XCord { get; set; }
        public Int32 YCord { get; set; }
        public Int64 PlayerId { get; set; }

        public void Process()
        {
            using (var portal = DatabasePortal.Open())
            {
                //check if field is available

                Colony.Create(portal, PlayerId, XCord, YCord);
            }
        }
    }
}