using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared
{
    public class TroopCollection
    {
      

        public Int32 Queen { get; set; }
        public Int32 Worker { get; set; }
        public Int32 Warrior { get; set; }
        public Int32 HeavyWarrior { get; set; }
        public Int32 Scout { get; set; }
        public Int32 Marksman { get; set; }
        public Int32 Hunter { get; set; }
        public Int32 FireAnt { get; set; }
        public Int32 Reaper { get; set; }
        public Int32 TowerGuard { get; set; }
        public Int32 Guard { get; set; }
        public Int32 BullAnt { get; set; }
        public Int32 Nemesis { get; set; }
        public Int32 SwarmGuard { get; set; }

        public float GetTroop(TroopType type)
        {
            switch(type)
            {
                case TroopType.Queen:
                    return Queen;

                case TroopType.Worker:
                    return Worker;

                case TroopType.Warrior:
                    return Warrior;

                case TroopType.HeavyWarrior:
                    return HeavyWarrior;

                case TroopType.Scout:
                    return Scout;

                case TroopType.Marksman:
                    return Marksman;

                case TroopType.Hunter:
                    return Hunter;

                case TroopType.FireAnt:
                    return FireAnt;

                case TroopType.Reaper:
                    return Reaper;

                case TroopType.TowerGuard:
                    return TowerGuard;

                case TroopType.Guard:
                    return Guard;

                case TroopType.BullAnt:
                    return BullAnt;

                case TroopType.Nemesis:
                    return BullAnt;

                case TroopType.SwarmGuard:
                    return BullAnt;

                default:
                    return 1000;
            }
        }
    }
}