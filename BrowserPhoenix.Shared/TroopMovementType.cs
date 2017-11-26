using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared
{
    [Serializable]
    public enum TroopMovementType
    {
        None = 0,
        Attack = 10,       
        Assist = 20,
        Return = 30
    }
}