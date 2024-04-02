using BaumansGateLibrary.Warriors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buffs
{
    [Serializable]
    public class GoodMood : Buff
    {
        public GoodMood() 
        {
            BuffName = "Хорошее настроение";
            BuffID = Types.GoodMood;
            TimeToLive = 1;
            TimeIsAllGame = true;
            IsOn = false;
        }
        public override void ProcessBuff(Unit U)
        {
            if(!IsOn && TimeToLive != 0)
            {
                IsOn = true;
                U.ChangeMaximumDistanceOfMove(2, Unit.HowChange.Increase);
            }
            if (IsOn && !TimeIsAllGame)
                    TimeToLive--;
            if (TimeToLive == 0)
            {
                IsOn = false;
                ProcessUnBuff(U);
            }
        }
        public override void ProcessUnBuff(Unit U)
        {
            U.ChangeMaximumDistanceOfMove(2, Unit.HowChange.Decrease);
        }
    }
}