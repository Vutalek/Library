using BaumansGateLibrary.Warriors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buffs
{
    [Serializable]
    public class HawkBuff : Buff
    {
        float OldTreePenalty;
        float OldSwampPenalty;
        float OldHillPenalty;
        public HawkBuff()
        {
            BuffName = "Орлиный полёт";
            BuffID = Types.HawkBuff;
            TimeToLive = 1;
            TimeIsAllGame = true;
            IsOn = false;
        }
        public override void ProcessBuff(Unit U)
        {
            if (!IsOn && TimeToLive != 0)
            {
                IsOn = true;
                OldHillPenalty = U.GetHillPenalty();
                OldTreePenalty = U.GetTreePenalty();
                OldSwampPenalty = U.GetSwampPenalty();
                U.SetHillPenalty(1);
                U.SetTreePenalty(1);
                U.SetSwampPenalty(1);
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
            U.SetHillPenalty(OldHillPenalty);
            U.SetTreePenalty(OldTreePenalty);
            U.SetSwampPenalty(OldSwampPenalty);
        }
    }
}
