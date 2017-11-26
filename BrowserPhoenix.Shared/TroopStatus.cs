using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared
{
    [Serializable]
    public enum TroopStatus
    {
        Idle = 0,
        Busy = 10,       
        Employed = 20
    }
}