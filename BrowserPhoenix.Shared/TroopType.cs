using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared
{
    [Serializable]
    public enum TroopType
    {
        [Description("Queen")]
        Queen = 10,
        [Description("Worker")]
        Worker = 20,
        [Description("Warrior")]
        Warrior = 30,
        [Description("Heavy Warrior")]
        HeavyWarrior = 40,
        [Description("Scout")]
        Scout = 50,
        [Description("Marksman")]
        Marksman = 60,
        [Description("Hunter")]
        Hunter = 70,
        [Description("Fire Ant")]
        FireAnt = 80,
        [Description("Reaper")]
        Reaper = 90,
        [Description("Tower Guard")]
        TowerGuard = 100,
        [Description("Guard")]
        Guard = 110,
        [Description("Bull Ant")]
        BullAnt = 120,
        [Description("Nemesis")]
        Nemesis = 130,
        [Description("Swarm Guard")]
        SwarmGuard = 140

    }
}