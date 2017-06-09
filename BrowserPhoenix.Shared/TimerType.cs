using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared
{
    [Serializable]
    public enum TimerType
    {
        CreateColony = 10,
        CreateBuilding = 20,
        LevelUpBuilding = 30,
        CalculateHighscore = 40,
        CreateTroop = 50
    }
}