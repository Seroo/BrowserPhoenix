using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared
{
    [Serializable]
    public enum BuildingType
    {
        [Description("Queen's Lair")]
        QueenLair = 10,
        [Description("Brood Lair")]
        BroodLair = 20,
        [Description("Stock")]
        Stock = 30,
        Warehouse = 40,
        Sandpit = 50,
        [Description("Mushroom Farm")]
        MushroomFarm = 60,
        Garden = 70,
        [Description("Aphids Breed")]
        AphidsBreed = 80,
     //   Rampart = 90,
   //     Trench = 100,
        [Description("Research Hill")]
        ResearchHill = 110,
        GraveYard = 120,
        [Description("Beetles Breed")]
        BeetleBreed = 130,
        [Description("Troop Pool")]
        TroopPool = 140,
        Tower = 150

    }
}