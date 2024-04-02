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
        protected string BuffName;
        public enum Types
        {
            GoodMood,
            JaguarBuff,
            HawkBuff,
            BearBuff
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
        public string GetBuffName() { return BuffName; }
        public void BuffDecrement() { TimeToLive--; }
        public virtual void ProcessBuff(Unit U) { }
        public virtual void ProcessUnBuff(Unit U) { }
    }
}
