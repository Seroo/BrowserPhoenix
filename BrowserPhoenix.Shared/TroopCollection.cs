using BrowserPhoenix.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowserPhoenix.Shared
{
    [Serializable]
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

        public IEnumerable<Troop> GetTroops()
        {
            var result = new List<Troop>();

            if (this.Queen != 0)
                result.Add(new Troop() { Type = TroopType.Queen, Amount = this.Queen });

            if (this.Worker != 0)
                result.Add(new Troop() { Type = TroopType.Worker, Amount = this.Worker });

            if (this.Warrior != 0)
                result.Add(new Troop() { Type = TroopType.Warrior, Amount = this.Warrior });

            if (this.HeavyWarrior != 0)
                result.Add(new Troop() { Type = TroopType.HeavyWarrior, Amount = this.HeavyWarrior });

            if (this.Scout != 0)
                result.Add(new Troop() { Type = TroopType.Scout, Amount = this.Scout });

            if (this.Marksman != 0)
                result.Add(new Troop() { Type = TroopType.Marksman, Amount = this.Marksman });

            if (this.Hunter != 0)
                result.Add(new Troop() { Type = TroopType.Hunter, Amount = this.Hunter });

            if (this.FireAnt != 0)
                result.Add(new Troop() { Type = TroopType.FireAnt, Amount = this.FireAnt });

            if (this.Reaper != 0)
                result.Add(new Troop() { Type = TroopType.Reaper, Amount = this.Reaper });

            if (this.TowerGuard != 0)
                result.Add(new Troop() { Type = TroopType.TowerGuard, Amount = this.TowerGuard });

            if (this.BullAnt != 0)
                result.Add(new Troop() { Type = TroopType.BullAnt, Amount = this.BullAnt });

            if (this.Nemesis != 0)
                result.Add(new Troop() { Type = TroopType.Nemesis, Amount = this.Nemesis });

            if (this.SwarmGuard != 0)
                result.Add(new Troop() { Type = TroopType.SwarmGuard, Amount = this.SwarmGuard });


            return result;
        }

        public static TroopCollection Create(IEnumerable<Troop> troops)
        {
            var result = new TroopCollection();

            foreach(var troop in troops)
            {
                var type = troop.Type;
                switch (type)
                {
                    case TroopType.Queen:
                        result.Queen = troop.Amount;
                        break;

                    case TroopType.Worker:
                        result.Worker = troop.Amount;
                        break;

                    case TroopType.Warrior:
                        result.Warrior = troop.Amount;
                        break;

                    case TroopType.HeavyWarrior:
                        result.HeavyWarrior = troop.Amount;
                        break;

                    case TroopType.Scout:
                        result.Scout = troop.Amount;
                        break;

                    case TroopType.Marksman:
                        result.Marksman = troop.Amount;
                        break;

                    case TroopType.Hunter:
                        result.Hunter = troop.Amount;
                        break;

                    case TroopType.FireAnt:
                        result.FireAnt = troop.Amount;
                        break;

                    case TroopType.Reaper:
                        result.Reaper = troop.Amount;
                        break;

                    case TroopType.TowerGuard:
                        result.TowerGuard = troop.Amount;
                        break;

                    case TroopType.Guard:
                        result.Guard = troop.Amount;
                        break;

                    case TroopType.BullAnt:
                        result.BullAnt = troop.Amount;
                        break;

                    case TroopType.Nemesis:
                        result.Nemesis = troop.Amount;
                        break;

                    case TroopType.SwarmGuard:
                        result.SwarmGuard = troop.Amount;
                        break;

                    default:

                        break;
                }
               
            }
            return result;
        }

        public Boolean HasMoreThan(TroopCollection collection)
        {
            var result = true;

            if (this.Queen < collection.Queen)
                result = false;

            if (this.Worker < collection.Worker)
                result = false;

            if (this.Warrior < collection.Warrior)
                result = false;

            if (this.HeavyWarrior < collection.HeavyWarrior)
                result = false;

            if (this.Scout < collection.Scout)
                result = false;

            if (this.Marksman < collection.Marksman)
                result = false;

            if (this.Hunter < collection.Hunter)
                result = false;

            if (this.FireAnt < collection.FireAnt)
                result = false;

            if (this.Reaper < collection.Reaper)
                result = false;

            if (this.TowerGuard < collection.TowerGuard)
                result = false;

            if (this.BullAnt < collection.BullAnt)
                result = false;

            if (this.Nemesis < collection.Nemesis)
                result = false;

            if (this.SwarmGuard < collection.SwarmGuard)
                result = false;

            return result;            
        }

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
                    return Nemesis;

                case TroopType.SwarmGuard:
                    return SwarmGuard;

                default:
                    return 1000;
            }
        }
    }
}