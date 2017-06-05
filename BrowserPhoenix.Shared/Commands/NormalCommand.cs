using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Messaging;

namespace BrowserPhoenix.Shared.Commands
{
    [Serializable]
    public abstract class NormalCommand : INormalCommand
    {
        public CommandType Type { get; set; }

        public abstract void Run();
      
    }
}