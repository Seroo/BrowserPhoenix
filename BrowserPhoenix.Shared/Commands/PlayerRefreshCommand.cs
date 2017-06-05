using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared.Commands
{
    [Serializable]
    public class PlayerRefreshCommand<Complex> : GenericCommand<Complex>
    {
        public new Complex Content { get; set; }

        public override void Run()
        {
            var miau = "new world";
        }
    }
}