using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Commands
{
    public abstract class CommandProcessor
    {
        public abstract void ProcessCommand(object type);
    }

    public class CommandProcessor<T> : CommandProcessor where T : ICommand
    {
        public override void ProcessCommand(object o)
        {
            if (!(o is T))
            {
                throw new NotImplementedException();
            }
            ProcessMessage((T)o);
        }

        public void ProcessMessage(T type)
        {
            type.Process();
        }
    }
}