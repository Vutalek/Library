using BaumansGateLibrary.Battle;
using BaumansGateLibrary.Warriors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buffs
{
    public static class BuffProcessor
    {
        public static void Process(Dictionary<char, Unit> units)
        {
            foreach(char U in units.Keys)
            {
                if (units[U].GetBuffList().Count == 0)
                    continue;
                foreach(Buff B in units[U].GetBuffList())
                    B.ProcessBuff(units[U]);
            }
        }
        public static void UnBuff(Unit unit, Buff buff)
        {
            int index = unit.GetBuffList().IndexOf(buff);
            buff.ProcessUnBuff(unit);
            unit.GetBuffList().RemoveAt(index);
            GameLog.AddLine("Бафф " + buff.GetBuffName() + " прекратил своё действие.");
        }
        public static void ClearBuffs(Dictionary<char, Unit> units)
        {
            foreach (char U in units.Keys)
                units[U].GetBuffList().Clear();
        }
    }
}
