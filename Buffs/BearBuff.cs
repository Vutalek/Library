using BaumansGateLibrary.Warriors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Buffs
{
    public class BearBuff : Buff
    {
        public BearBuff()
        {
            BuffName = "Медвежья сила";
            BuffID = Types.BearBuff;
            TimeToLive = 1;
            TimeIsAllGame = true;
            IsOn = false;
        }
        public override void ProcessBuff(Unit U)
        {
            if (!IsOn && TimeToLive != 0)
            {
                IsOn = true;
                Weapon temp = U.GetEquipedWeapon();
                temp.ChangeAverage(2);
                U.WeaponChange(temp);
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
            Weapon temp = U.GetEquipedWeapon();
            temp.ChangeAverage(-2);
            U.WeaponChange(temp);
        }
    }
}
