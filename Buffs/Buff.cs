using BaumansGateLibrary.Warriors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buffs
{
    [Serializable]
    public class Buff
    {
        public enum Types
        {
            GoodMood
        }
        protected int TimeToLive;
        protected bool TimeIsAllGame;
        protected Types BuffID;
        protected bool IsOn;
        public Buff() { }
        public int GetTimeToLive() { return TimeToLive; }
        public Types GetBuffID() {  return BuffID; }
        public bool GetTimeIsAllGame() {  return TimeIsAllGame; }
        public bool GetIsOn() {  return IsOn; }
        public void BuffDecrement() { TimeToLive--; }
        public virtual void ProcessBuff(Unit U) { }
    }
}
