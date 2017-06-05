using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Commands
{
    [Serializable]
    public class GenericCommand<T> : NormalCommand
    {
        public T Content { get; set; }

        public override void Run()
        {

        }
    }
}